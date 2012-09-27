using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;

using DirectShowLib;

namespace VideoProcessAnalyser
{
    public partial class MainForm : Form
    {
        int iMul = 5;
        IGraphBuilder graphbuilder;
        ISampleGrabber samplegrabber;
        AMMediaType mt;
        public MainForm()
        {
            InitializeComponent();
            graphbuilder = (IGraphBuilder)new FilterGraph();
            samplegrabber = (ISampleGrabber)new SampleGrabber();
            graphbuilder.AddFilter((IBaseFilter)samplegrabber, "samplegrabber");

            mt = new AMMediaType();
            mt.majorType = MediaType.Video;
            mt.subType = MediaSubType.RGB24;
            mt.formatType = FormatType.VideoInfo;
            samplegrabber.SetMediaType(mt);
            PrintSeconds();

        }

        private void SelectFilebutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select file";
            //dlg.Filter = "wmv files|*.wmv";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            FiletextBox.Text = dlg.FileName;
            PreviewFile();
            VideotrackBar.Enabled = true;
            ShowSecondscheckBox.Enabled = true;
            CurrentSecondstextBox.Enabled = true;
        }
        void PreviewFile()
        {
            try
            {
                if (!System.IO.File.Exists(FiletextBox.Text))
                    return;
                int hr = graphbuilder.RenderFile(FiletextBox.Text, null);

                IMediaSeeking mediaSeek = (IMediaSeeking)graphbuilder;

                samplegrabber.SetOneShot(true);
                samplegrabber.SetBufferSamples(true);

                long d = 0;
                mediaSeek.GetDuration(out d);
                long numSecs = d / 10000000;
                VideotrackBar.Maximum = (int)numSecs;
                SelectFrame();
            }
            catch (System.Exception e)
            {
                MessageBox.Show("File type is not supported. " + e.Message);
            }
        }
        private void Analysebutton_Click(object sender, EventArgs e)
        {
            if(!System.IO.File.Exists(FiletextBox.Text))
            {
                MessageBox.Show("File is not selected");
                return;
            }
            int iSecStep = 0;
            if (!int.TryParse(SecondstextBox.Text, out iSecStep) || iSecStep <= 0)
            {
                MessageBox.Show("Step is not filled!");
                return;
            }
            FileInfo fi = new FileInfo(FiletextBox.Text);
            string fExt = fi.Extension;
            string fName = fi.FullName.Substring(0, fi.FullName.Length - fExt.Length);
            Analysebutton.Enabled = false;
            SelectFilebutton.Enabled = false;
            try
            {
                int hr = graphbuilder.RenderFile(FiletextBox.Text, null);

                IMediaEventEx mediaEvt = (IMediaEventEx)graphbuilder;
                IMediaSeeking mediaSeek = (IMediaSeeking)graphbuilder;
                IMediaControl mediaCtrl = (IMediaControl)graphbuilder;
                IBasicAudio basicAudio = (IBasicAudio)graphbuilder;
                IBasicVideo basicVideo = (IBasicVideo)graphbuilder;
                IVideoWindow videoWin = (IVideoWindow)graphbuilder;

                basicAudio.put_Volume(-10000);
                videoWin.put_AutoShow(OABool.False);

                samplegrabber.SetOneShot(true);
                samplegrabber.SetBufferSamples(true);

                long d = 0;
                mediaSeek.GetDuration(out d);
                long numSecs = d / 10000000;
                int iCount = (int)numSecs/iSecStep;
                Directory.CreateDirectory(fName);
                for (int i = 0; i < iCount; i++)
                {
                    Analysebutton.Text = GetSecondsText(i * iSecStep) + " of " + GetSecondsText((int)numSecs);
                    Console.WriteLine(Analysebutton.Text);
                    long secondstocapture = (long)i * iSecStep;
                    DsLong rtStart, rtStop;
                    rtStart = new DsLong(secondstocapture * 10000000);
                    rtStop = rtStart;
                    mediaSeek.SetPositions(rtStart, AMSeekingSeekingFlags.AbsolutePositioning, rtStop, AMSeekingSeekingFlags.AbsolutePositioning);

                    mediaCtrl.Run();
                    EventCode evcode;
                    mediaEvt.WaitForCompletion(-1, out evcode);

                    int width = 0;
                    int height = 0;
                    basicVideo.get_VideoWidth(out width);
                    basicVideo.get_VideoHeight(out height);
                    Bitmap b = new Bitmap(width, height, PixelFormat.Format24bppRgb);

                    uint bytesPerPixel = (uint)(24 >> 3);
                    uint extraBytes = ((uint)width * bytesPerPixel) % 4;
                    uint adjustedLineSize = bytesPerPixel * ((uint)width + extraBytes);
                    uint sizeOfImageData = (uint)(height) * adjustedLineSize;

                    BitmapData bd1 = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    int bufsize = (int)sizeOfImageData;
                    int n = samplegrabber.GetCurrentBuffer(ref bufsize, bd1.Scan0);
                    b.UnlockBits(bd1);
                    b.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    BitmapAnalyser bma = new BitmapAnalyser(b,m_rtList);
                    ImagepictureBox.Image = b;
                    Graphics g = Graphics.FromImage(ImagepictureBox.Image);
                    foreach (var el in bma.m_bItemsDict)
                    {
                        Bitmap bmp = b.Clone(el.Key.Rect, PixelFormat.Format1bppIndexed);
                        g.DrawImage(bmp, el.Key.Rect);
                        string sName = el.Key.Name;
                        sName = sName.Replace("\\", "_").
                            Replace("/", "_").
                            Replace("*", "_").
                            Replace("?", "_").
                            Replace("\"", "_").
                            Replace("<", "_").
                            Replace(">", "_").
                            Replace("|", "_").
                            Replace(":", "_").
                            Trim();
                        el.Value.Save(fName + "\\" + GetSecondsText(i * iSecStep).Replace(":","_") + "-" + el.Key.Name + ".gif", ImageFormat.Gif);
                    }
                    g.Dispose();
                    Application.DoEvents();
                    
                }
            }
            catch (System.Exception e1)
            {
                Console.WriteLine(e1.Message);
            }
            Analysebutton.Text = "Analysing values";
            ExecuteAnalyse(fName);
            if (DeleteFilescheckBox.Checked)
                Directory.Delete(fName, true);
            Analysebutton.Text = "Analyse";
            Analysebutton.Enabled = true;
            SelectFilebutton.Enabled = true;
            MessageBox.Show("Done");
        }
        private void ExecuteAnalyse(string sPath)
        {
            try
            {
                string args = "\"" + sPath + "\" " +
                    ShowErrorscheckBox.Checked.ToString() + " " +
                     CorrectErrorscheckBox.Checked.ToString() + " " +
                     PercentcheckBox.Checked.ToString() + " " +
                    m_rtList.Count.ToString();
                foreach (var el in m_rtList)
                    args += " \"" + el.Name + "\"";
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "BitmapAnalyser.exe";
                p.StartInfo.Arguments = args;
                p.Start();
                p.WaitForExit();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        Bitmap m_b = null;

        private void TimeScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            SelectFrame();
        }
        private void SelectFrame()
        {
            long iSecond = VideotrackBar.Value;

            IMediaEventEx mediaEvt = (IMediaEventEx)graphbuilder;
            IMediaSeeking mediaSeek = (IMediaSeeking)graphbuilder;
            IMediaControl mediaCtrl = (IMediaControl)graphbuilder;
            IBasicAudio basicAudio = (IBasicAudio)graphbuilder;
            IBasicVideo basicVideo = (IBasicVideo)graphbuilder;
            IVideoWindow videoWin = (IVideoWindow)graphbuilder;

            basicAudio.put_Volume(-10000);
            videoWin.put_AutoShow(OABool.False);

            samplegrabber.SetOneShot(true);
            samplegrabber.SetBufferSamples(true);

            DsLong rtStart, rtStop;
            rtStart = new DsLong(iSecond * 10000000);
            rtStop = rtStart;
            mediaSeek.SetPositions(rtStart, AMSeekingSeekingFlags.AbsolutePositioning, rtStop, AMSeekingSeekingFlags.AbsolutePositioning);

            mediaCtrl.Run();
            EventCode evcode;
            mediaEvt.WaitForCompletion(-1, out evcode);

            int width = 0;
            int height = 0;
            basicVideo.get_VideoWidth(out width);
            basicVideo.get_VideoHeight(out height);
            m_b = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            uint bytesPerPixel = (uint)(24 >> 3);
            uint extraBytes = ((uint)width * bytesPerPixel) % 4;
            uint adjustedLineSize = bytesPerPixel * ((uint)width + extraBytes);
            uint sizeOfImageData = (uint)(height) * adjustedLineSize;

            BitmapData bd1 = m_b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int bufsize = (int)sizeOfImageData;
            int n = samplegrabber.GetCurrentBuffer(ref bufsize, bd1.Scan0);
            m_b.UnlockBits(bd1);
            m_b.RotateFlip(RotateFlipType.RotateNoneFlipY);
            RedrawItems();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Marshal.ReleaseComObject(graphbuilder);
            Marshal.ReleaseComObject(samplegrabber);
        }
        List<GrabRect> m_rtList = new List<GrabRect>();
        int iNumbers = 1;
        private void AddButton_Click(object sender, EventArgs e)
        {
            m_rtList.Add(new GrabRect(iNumbers.ToString(), Color.FromArgb(255, 0, 0), new Rectangle(5, 5, 200, 50)));
            iNumbers++;
            RedrawItems();            
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            GrabRect rt = VideopropertyGrid.SelectedObject as GrabRect;
            m_rtList.Remove(rt);
            bDragging = null;
            bSizing = null;
            VideopropertyGrid.SelectedObject = null;
            RedrawItems();
        }

        private void SetButton_Click(object sender, EventArgs e)
        {

        }

        private void VideotrackBar_Scroll(object sender, EventArgs e)
        {
            PrintSeconds();
            SelectFrame();
        }
        private string GetSecondsText(int i)
        {
            string sSeconds;
            if (!ShowSecondscheckBox.Checked)
            {
                int iH = i / 60 / 60;
                i -= iH * 60 * 60;
                int iM = i / 60;
                i -= iM * 60;
                int iS = i;
                sSeconds = iH.ToString() + ":" + iM.ToString("00") + ":" + iS.ToString("00");
            }
            else
                sSeconds = i.ToString();
            return sSeconds;
        }
        private void PrintSeconds()
        {
            CurrentSecondstextBox.Text = GetSecondsText(VideotrackBar.Value);
        }
        private void RedrawItems()
        {
            if (m_b == null)
                return;
            ImagepictureBox.Image = (Image)m_b.Clone();
            Graphics gr = Graphics.FromImage(ImagepictureBox.Image);
            Font f = new Font("Arial", 12);
            foreach (var el in m_rtList.Reverse<GrabRect>())
            {
                Pen p = new Pen(el.Col,1);
                SolidBrush b = new SolidBrush(el.Col);
                gr.DrawRectangle(p, el.Rect);
                gr.DrawString(el.Name, f, b, el.Rect.Left, el.Rect.Top);
                p.Dispose();
                b.Dispose();
            }
            f.Dispose();
            gr.DrawImage(ImagepictureBox.Image,0,0);
            gr.Dispose();
            Application.DoEvents();
        }
        private void ImagepictureBox_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ImagepictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            VideopropertyGrid.SelectedObject = null;
            foreach (var el in m_rtList)
            {
                if(el.Rect.Contains(e.Location))
                {
                    VideopropertyGrid.SelectedObject = el;
                    break;
                }
            }
        }
        Cursor iCursorType = Cursors.Arrow;
        int iDraggingType = -1;
        GrabRect bDragging = null;
        GrabRect bSizing = null;
        Point pOld = new Point(0, 0);
        private void ImagepictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (bSizing == null)
            {
                bDragging = null;
                foreach (var el in m_rtList)
                {
                    Rectangle rt = el.Rect;
                    rt.Inflate(-iMul, -iMul);
                    if (rt.Contains(e.Location))
                    {
                        bDragging = el;
                        Cursor = iCursorType = Cursors.SizeAll;
                        break;
                    }
                }
            }
            if (bDragging == null)
            {
                bSizing = null;
                foreach (var el in m_rtList)
                {
                    if (el.Rect.Contains(e.Location))
                    {
                        iDraggingType = DetectPointOnRect(el.Rect, e.Location);
                        if (iDraggingType >= 1 && iDraggingType <= 8)
                        {
                            bSizing = el;
                            Cursor = iCursorType = GetCursorByType(iDraggingType);
                            break;
                        }
                    }
                }
            }
            pOld = e.Location;
        }

        private void ImagepictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            bDragging = null;
            bSizing = null;
        }

        private void ImagepictureBox_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (bDragging !=null&& pOld != e.Location)
            {
                Cursor = iCursorType;
                bDragging.Rect = new Rectangle(bDragging.Rect.X + e.Location.X - pOld.X, bDragging.Rect.Y + e.Location.Y - pOld.Y, bDragging.Rect.Width, bDragging.Rect.Height);
                pOld = e.Location;
                RedrawItems();
                return;
            }
            if (bSizing != null && pOld != e.Location)
            {
                Cursor = iCursorType;
                int dX = e.Location.X - pOld.X;
                int dY = e.Location.Y - pOld.Y;
                switch (iDraggingType)
                {
                    case 1:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X + dX, bSizing.Rect.Y + dY, bSizing.Rect.Width - dX, bSizing.Rect.Height - dY);
                        break;
                    case 2:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y + dY, bSizing.Rect.Width, bSizing.Rect.Height - dY);
                        break;
                    case 3:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y + dY, bSizing.Rect.Width + dX, bSizing.Rect.Height - dY);
                        break;
                    case 4:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X + dX, bSizing.Rect.Y, bSizing.Rect.Width - dX, bSizing.Rect.Height);
                        break;
                    case 5:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, bSizing.Rect.Width + dX, bSizing.Rect.Height);
                        break;
                    case 6:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X + dX, bSizing.Rect.Y, bSizing.Rect.Width - dX, bSizing.Rect.Height + dY);
                        break;
                    case 7:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, bSizing.Rect.Width, bSizing.Rect.Height + dY);
                        break;
                    case 8:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, bSizing.Rect.Width + dX, bSizing.Rect.Height + dY);
                        break;
                    default:
                        break;
                }
                if (bSizing.Rect.Width < iMul * 2)
                    bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, iMul * 2, bSizing.Rect.Height);
                if (bSizing.Rect.Height < iMul * 2)
                    bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, bSizing.Rect.Width, iMul * 2);
                pOld = e.Location;
                RedrawItems();
                return;
            }
            Cursor iType = null;
            foreach (var el in m_rtList)
            {
                if (el.Rect.Contains(e.Location))
                {
                    iType = GetCursor(el.Rect, e.Location);
                    break;
                }
            }
            if(iType == null)
                iType = Cursors.Arrow;
            Cursor = iType;
        
        }
        private Cursor GetCursorByType(int iCursType)
        {
            switch (iCursType)
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeWE;
                case 6:
                    return Cursors.SizeNESW;
                case 7:
                    return Cursors.SizeNS;
                case 8:
                    return Cursors.SizeNWSE;
                case 9:
                    return Cursors.SizeAll;
                default:
                    return Cursors.Arrow;
            }
        }
        private Cursor GetCursor(Rectangle rt, Point pt)
        {
            return GetCursorByType(DetectPointOnRect(rt, pt));
        }
        private int DetectPointOnRect(Rectangle rt, Point pt)
        {
            Rectangle buf = rt;
            
            buf.Inflate(-iMul, -iMul);
            if (buf.Contains(pt))
            {
                return 9;
            }
            if (!rt.Contains(pt))
            {
                return -1;
            }

            Rectangle rttl = new Rectangle(rt.Left,rt.Top,iMul,iMul)
                ,rtt = new Rectangle(buf.Left,rt.Top,rt.Width-2*iMul,iMul)
                ,rttr = new Rectangle(buf.Right,rt.Top,iMul,iMul)
                ,rtl = new Rectangle(rt.Left,buf.Top,iMul,rt.Height-2*iMul)
                ,rtr = new Rectangle(buf.Right,buf.Top,iMul,rt.Height-2*iMul)
                ,rtbl = new Rectangle(rt.Left,buf.Bottom,iMul,iMul)
                ,rtb = new Rectangle(buf.Left,buf.Bottom,rt.Width-2*iMul,iMul)
                ,rtbr = new Rectangle(buf.Right,buf.Bottom,iMul,iMul);

            if (rttl.Contains(pt))
            {
                return 1;
            }
            if (rtt.Contains(pt))
            {
                return 2;
            }           
            if (rttr.Contains(pt))
            {
                return 3;
            }
            if (rtl.Contains(pt))
            {
                return 4;
            }
            if (rtr.Contains(pt))
            {
                return 5;
            }            
            if (rtbl.Contains(pt))
            {
                return 6;
            }
            if (rtb.Contains(pt))
            {
                return 7;
            }
            if (rtbr.Contains(pt))
            {
                return 8;
            }

            return -1;
        }
        private void VideopropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RedrawItems();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Drawing.Graphics formGraphics = ImagepictureBox.CreateGraphics();
            string drawString = "Sample Text";
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 12);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            float x = 150.0F;
            float y = 50.0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();
        }

        private void ShowErrorscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CorrectErrorscheckBox.Enabled = ShowErrorscheckBox.Checked;
            if (!ShowErrorscheckBox.Checked)
                CorrectErrorscheckBox.Checked = false;
        }

        private void CorrectErrorscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void DeleteTemFilescheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ShowSecondscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PrintSeconds();
        }

        private void CurrentSecondstextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetFrameValue();
            }
        }

        private void CurrentSecondstextBox_Leave(object sender, EventArgs e)
        {
            SetFrameValue();
        }
        private void SetFrameValue()
        {
            int iVal = VideotrackBar.Value;
            if (ShowSecondscheckBox.Checked)
            {
                if (!int.TryParse(CurrentSecondstextBox.Text, out iVal))
                    return;
            }
            else
            {
                int iHours = 0;
                int iMinutes = 0;
                int iSeconds = 0;
                string str = CurrentSecondstextBox.Text;
                string[] sVals = str.Split(':');
                if (sVals.Length != 3)
                    return;
                if (!int.TryParse(sVals[0], out iHours) ||
                    !int.TryParse(sVals[1], out iMinutes) ||
                    !int.TryParse(sVals[2], out iSeconds))
                    return;
                iVal = iHours * 60 * 60 + iMinutes * 60 + iSeconds;
            }
            if (iVal == VideotrackBar.Value)
            {
                return;
            }
            VideotrackBar.Value = iVal;
            SelectFrame();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(FiletextBox.Text);
            string fExt = fi.Extension;
            string fName = fi.FullName.Substring(0, fi.FullName.Length - fExt.Length);

            ExecuteAnalyse(fName);
        }
    }
}
