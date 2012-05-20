using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puma;
using Puma.Net;
using System.IO;
using System.Collections;
using System.Drawing;

namespace BitmapAnalyser
{
    class Program
    {
        static List<string> sNamesList = new List<string>();
        static Dictionary<string, Dictionary<string, double>> vals = new Dictionary<string, Dictionary<string, double>>();
        static double[,] dCounts = null;
        static string[] sNames = null;
        static string[] sTimes = null;
        static void Main(string[] args)
        {
            int i;
            Console.WriteLine(string.Join(" ", args));
            string sErrString = "Should use cmd line: folder showerrors(True/False) correcterrors(True/False) percents(True/False) countname(>0) arrnames[countname]";
            if (args.Length <4)
            {
                Console.WriteLine(sErrString);
                return;
            }
            string sDir = args[0];
            if(!Directory.Exists(sDir))
            {
                Console.WriteLine("No such directory.");
                return;
            }
            bool bShowErrors = false;
            bool bCorrectErros = false;
            bool bPercents = false;
            int iCountNames = 0;
            if (!bool.TryParse(args[1], out bShowErrors) ||
                !bool.TryParse(args[2], out bCorrectErros) ||
                !bool.TryParse(args[3], out bPercents) ||
                !int.TryParse(args[4], out iCountNames) || iCountNames<1 || (iCountNames+5)!=args.Length)
            {
                Console.WriteLine(sErrString);
                return;
            }

            for (i = 0; i < iCountNames; i++)
            {
                sNamesList.Add(args[i + 5]);
            }
            
            foreach (string el in Directory.GetFiles(sDir, "*.gif"))
            {
                FileInfo fi = new FileInfo(el);
                string fExt = fi.Extension;
                string fName = fi.Name.Substring(0, fi.Name.Length - fExt.Length);
                int iPos = fName.IndexOf("-");
                string sTypeName = fName.Substring(iPos + 1);
                fName = fName.Substring(0,iPos);
                
                var pumaPage = new PumaPage(el);
                Console.WriteLine(el);
                double dVal = -1;
                try
                {
                    using (pumaPage)
                    {
                        pumaPage.FileFormat = PumaFileFormat.RtfAnsi;
                        pumaPage.EnableSpeller = false;
                        pumaPage.Language = PumaLanguage.Lithuanian;
                        string sVal = pumaPage.RecognizeToString();
                        
                        i = 0;
                        sVal = sVal.Trim();
                        sVal = sVal.Replace(" ", "");
                        iPos = sVal.IndexOf("%");
                        if (iPos!=-1)
                            sVal = sVal.Substring(0, iPos);
                        sVal = sVal.Replace(".", ",");
                        if (!double.TryParse(sVal, out dVal))
                            dVal = -1;
                        else
                        {
                            while (bPercents&&dVal > 100)
                                dVal /= 100.0;
                        }

                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine(dVal.ToString());
                Dictionary<string, double> bufDict = null;

                if (!vals.TryGetValue(fName, out bufDict))
                {
                    bufDict = new Dictionary<string, double>();
                    vals.Add(fName, bufDict);
                }
                bufDict.Add(sTypeName, dVal);

            }
            if (vals.Count == 0)
                return;
            i = 1;
            string[] lines = new string[vals.Count + 1];

            sNames = sNamesList.ToArray();
            Array.Sort(sNames);
            sTimes = vals.Keys.ToArray();
            Array.Sort(sTimes);
            if (bShowErrors&&bCorrectErros)
            {
                CorrectErrors(bPercents);
            }

            lines[0] = "Секунда;" + string.Join(";", sNamesList.ToArray()) + ";";
            foreach (var t in sTimes)
            {
                bool bHasError = false;
                foreach (var n in sNames)
                {
                    if (vals[t][n] < 0)
                    {
                        bHasError = true;
                        break;
                    }
                }
                if (bHasError && !bShowErrors)
                    continue;
                string sBufLine = t + ";";
                foreach (var n in sNames)
                {
                    sBufLine += vals[t][n].ToString() + ";";
                }
                lines[i++] = sBufLine;
            }
            System.IO.File.WriteAllLines(sDir + ".csv", lines, Encoding.GetEncoding(1251));
        }
        static void CorrectErrors(bool bPercents)
        {
            dCounts = new double[sTimes.Length,sNames.Length];
            int i = 0;
            int j = 0;
            for(i=0;i<sTimes.Length;i++)
            {
                for(j=0;j<sNames.Length;j++)
                {
                    dCounts[i, j] = vals[sTimes[i]][sNames[j]];
                }
            }

            if (bPercents)
            {
                for (i = 0; i < sTimes.Length; i++)
                {
                    double dSumm = 0;
                    double[] bufVals = new double[sNames.Length];
                    for (j = 0; j < sNames.Length; j++)
                    {
                        if (dCounts[i, j] < 0)
                        {
                            dSumm = -1;
                            break;
                        }
                        dSumm += dCounts[i, j];
                        bufVals[j] = dCounts[i, j];
                    }
                    if (dSumm<0)
                        continue;
                    if (dSumm > 100)
                    {
                        for (j = 0; j < sNames.Length; j++)
                        {
                            dCounts[i, j] = -1;
                        }
                        continue;
                    }

                    if (dSumm<100)
                    {
                        for (j = 0; j < sNames.Length; j++)
                        {
                            dSumm -= bufVals[j];
                            bufVals[j] *= 10;
                            dSumm += bufVals[j];
                            if (dSumm == 100)
                                break;
                            dSumm -= bufVals[j];
                            bufVals[j] /= 10;
                            dSumm += bufVals[j];
                        }
                        if (dSumm == 100)
                        {
                            for (j = 0; j < sNames.Length; j++)
                            {
                                dCounts[i, j] = bufVals[j];
                            }
                        }
                    }
                }
            }

            for (j = 0; j < sNames.Length; j++)
            {
                i = 0;
                while (i < sTimes.Length && dCounts[i, j] < 0)
                {
                    dCounts[i, j] = 0;
                    i++;
                }
            }

            for (j = 0; j < sNames.Length; j++)
            {
                for (i = 0; i < sTimes.Length; i++)
                {
                    if (dCounts[i, j] >= 0)
                        continue;
                    if (bPercents && sNames.Length == 2 && dCounts[i, 1 - j] > 0)
                    {
                        dCounts[i, j] = 100 - dCounts[i, 1 - j];
                    }
                }
            }
           
            for (j = 0; j < sNames.Length; j++)
            {
                for (i = 0; i < sTimes.Length; i++)
                {
                    if (dCounts[i, j] >= 0)
                        continue;
                    int iCount = 0;
                    while (dCounts[i + iCount, j] < 0 && (iCount + i) < sTimes.Length)
                        iCount++;
                    double dDiff = (dCounts[i + iCount, j] - dCounts[i - 1, j]) / (double)(iCount + 1);
                    for (int k = 0; k < iCount; k++)
                        dCounts[i + k, j] = (k + 1) * dDiff + dCounts[i - 1, j];
                    i += iCount;
                }
            }
            for (i = 0; i < sTimes.Length; i++)
            {
                for (j = 0; j < sNames.Length; j++)
                {
                    vals[sTimes[i]][sNames[j]] = dCounts[i, j];
                }
            }
        }
    }
}
