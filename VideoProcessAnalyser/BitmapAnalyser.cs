using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace VideoProcessAnalyser
{
    public class BitmapAnalyser
    {
        public Dictionary<GrabRect, Bitmap> m_bItemsDict;
        public BitmapAnalyser(Bitmap b, List<GrabRect> rtList)
        {
            m_bItemsDict = new Dictionary<GrabRect, Bitmap>();
            foreach (var el in rtList)
            {
                Bitmap bufB = b.Clone(el.Rect, PixelFormat.Format1bppIndexed);
                bufB = CalcSides(bufB);
                bufB = Invert(bufB);
                bufB = Inflate(bufB);
                m_bItemsDict.Add(el, bufB);
            }
        }
        private Bitmap Invert(Bitmap bmp)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Bitmap bmpNew = bmp.Clone(rect,PixelFormat.Format24bppRgb);

            int i = 0;
            int j = 0;
            for (i = 0; i < bmpNew.Width; i++)
            {
                for (j = 0; j < bmpNew.Height; j++)
                {
                    Color cc = bmpNew.GetPixel(i, j);
                    if (cc.R != 0 || cc.G != 0 || cc.B != 0)
                    {
                        cc = Color.FromArgb(0, 0, 0);
                    }
                    else
                        cc = Color.FromArgb(255, 255, 255);
                    bmpNew.SetPixel(i, j, cc);
                }
            }
            bmp = bmpNew.Clone(rect, PixelFormat.Format1bppIndexed);
            return bmp;
        }
        private Bitmap CalcSides(Bitmap bmp)
        {
            Bitmap bmpNew = null;
            int iTop = 0;
            int iLeft = 0;
            int iRight = bmp.Width-2;
            int iBottom = bmp.Height - 2;
            int i = 0;
            int j = 0;
            for (i = 0; i < bmp.Width; i++)
            {
                bool bFlag = false;
                for (j = 0; j < bmp.Height; j++)
                {
                    Color cc = bmp.GetPixel(i, j);
                    if (cc.R != 0 || cc.G != 0 || cc.B != 0)
                    {
                        bFlag = true;
                        break;
                    }
                }
                if (bFlag)
                    break;
            }
            i -= 2;
            if (i < bmp.Width && i >= 0)
                iLeft = i;
            for (i = bmp.Width - 1; i >= 0; i--)
            {
                bool bFlag = false;
                for (j = 0; j < bmp.Height; j++)
                {
                    Color cc = bmp.GetPixel(i, j);
                    if (cc.R != 0 || cc.G != 0 || cc.B != 0)
                    {
                        bFlag = true;
                        break;
                    }
                }
                if (bFlag)
                    break;
            }
            i += 2;
            if (i < bmp.Width && i >= 0)
                iRight = i;

            for (j = 0; j < bmp.Height; j++)
            {
                bool bFlag = false;
                for (i = 0; i < bmp.Width; i++)
                {
                    Color cc = bmp.GetPixel(i, j);
                    if (cc.R != 0 || cc.G != 0 || cc.B != 0)
                    {
                        bFlag = true;
                        break;
                    }
                }
                if (bFlag)
                    break;
            }
            j -= 2;
            if (j < bmp.Height && j >= 0)
                iTop = j;

            for (j = bmp.Height-1; j >= 0; j--)
            {
                bool bFlag = false;
                for (i = bmp.Width - 1; i >= 0; i--)                
                {
                    Color cc = bmp.GetPixel(i, j);
                    if (cc.R != 0 || cc.G != 0 || cc.B != 0)
                    {
                        bFlag = true;
                        break;
                    }
                }
                if (bFlag)
                    break;
            }
            j += 2;
            if (j < bmp.Height && j >= 0)
                iBottom = j;
            if (iTop > iBottom || iLeft > iRight)
            {
                iTop = iLeft = 0;
                iBottom = iRight = 2;
            }

            Rectangle rect = new Rectangle(iLeft, iTop, iRight - iLeft + 1, iBottom - iTop + 1);
            bmpNew = bmp.Clone(rect, PixelFormat.Format1bppIndexed);

            return bmpNew;
        }
        private List<Bitmap> GetItems(Bitmap bmp)
        {
            List<Bitmap> items = new List<Bitmap>();
            int i = 0;
            int iPrev = 0;
            bool bWasBreak = false;
            Rectangle rect;
            Bitmap bufbmp = null;
            for (i = 0; i < bmp.Width; i++)
            {
                bool bFlag = false;
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color cc = bmp.GetPixel(i, j);
                    if (cc.R != 0 || cc.G != 0 || cc.B != 0)
                        continue;
                    {
                        bFlag = true;
                        bWasBreak = true;
                        break;
                    }
                }
                if (!bFlag)
                {
                    if (bWasBreak)
                    {
                        rect = new Rectangle(iPrev,0,i-iPrev,bmp.Height);
                        bufbmp = CalcSides(bmp.Clone(rect, PixelFormat.Format1bppIndexed));
                        items.Add(bufbmp);
                        bWasBreak = false;
                    }
                    iPrev = i;
                }
            }
            if (bWasBreak)
            {
                rect = new Rectangle(iPrev, 0, i - iPrev, bmp.Height);
                bufbmp = CalcSides(bmp.Clone(rect, PixelFormat.Format1bppIndexed));
                items.Add(bufbmp);
            }

            return items;
        }
        private Bitmap Inflate(Bitmap bufB)
        {
            Bitmap bmp = new Bitmap(bufB.Width + 6, bufB.Height + 6);
            Graphics gr = Graphics.FromImage(bmp);
            gr.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
            gr.DrawImage(bufB, 3, 3);
            gr.Dispose();
            return bmp;
        }
    }
}








