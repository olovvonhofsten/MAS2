using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace MirrorAlignmentSystem
{
    class Algorithm
    {
        // Size of the screen
        public static int screensizex = 1280;
        public static int screensizey = 720;
        public static int imagesizex = 1935;
        public static int imagesizey = 1216;
        public static double pix2distDISC = 10.157;
        public static double pix2mradDISC = 0.711;
        public static double pix2distBH = 5.36;
        private static int dotWidth = 1;
        public static int fineTolerance = 2;

        public static void Fine_algorithm(Bitmap IMG, Bitmap BKGR, double threshold, int segmentnr, out double[] offsetXY, out double[] offsetRT, out Point massCenter,
           out Bitmap returnBitmap)
        {
            CvInvoke.UseOpenCL = false;

            System.Diagnostics.Debug.WriteLine("Inside Algorithm");

            // Convert to Image<Gray, byte>
            Image<Gray, byte> ImgIn = new Image<Gray, byte>(IMG);
            Image<Gray, byte> BkrIn = new Image<Gray, byte>(BKGR);
            Image<Gray, byte> Img2 = ImgIn.Copy();
            Img2.SetZero();

            try
            {
                CvInvoke.AbsDiff(ImgIn, BkrIn, Img2);
            }
            catch
            {

            }

            //Mask out the segment. Fine uses an elliptical mask
            int[] currentData = DAL.GetAOIDataRawSegment("" + segmentnr);

            //Mask out ellipse
            // Calculate point for ideal center of gravity
            Point idealC = new Point(currentData[12] - currentData[0], currentData[13] - currentData[1]);

            // Defines segment borders
            Point[] Seg = new Point[] { new Point(currentData[4] - currentData[0], currentData[5] - currentData[1]), new Point(currentData[6] - currentData[0], currentData[7] - currentData[1]), new Point(currentData[8] - currentData[0], currentData[9] - currentData[1]), new Point(currentData[10] - currentData[0], currentData[11] - currentData[1]) };

            // Calculate the angle of the tilt of the ellipse
            double angle = Math.Atan((double)(currentData[8] - currentData[10]) / (currentData[9] - currentData[11]));

            // Calculate the height and width of the ellipse
            double w1 = currentData[12] - (currentData[10] + currentData[8]) / 2;
            double w2 = currentData[13] - (currentData[11] + currentData[9]) / 2;
            double v1 = currentData[12] - (currentData[6] + currentData[8]) / 2;
            double v2 = currentData[13] - (currentData[9] + currentData[7]) / 2;

            double marginw = 0.9;
            double marginh = 0.9;

            // Create mask to define ROI for masscenter calculation
            Image<Gray, byte> mask = Img2.Copy();
            mask.SetZero();

            // The inner segments have a larger eccentricity. This is one way to handle it
            if (segmentnr < 13)
            {
                marginh = 0.8;
                marginw = 0.6;
            }

            // Special segments that are covered also need special treatment
            double widthROI, heightROI;
            RotatedRect roifine = new RotatedRect(idealC, new SizeF(new Point(20, 20)), -(float)(angle * 180 / Math.PI));

            if (segmentnr == 1 | segmentnr == 11) //segment 0111 or 1111
            {
                mask.Draw(Seg, new Gray(255), 1);
            }

            else // Normal case
            {
                heightROI = Math.Sqrt((w1 * w1) + (w2 * w2)) * 2 * marginw;
                widthROI = Math.Sqrt((v1 * v1) + (v2 * v2)) * 2 * marginh;
                // Create the rotated rectangle that defines the ellipse
                roifine = new RotatedRect(idealC, new SizeF(new Point((int)widthROI, (int)heightROI)), -(float)(angle * 180 / Math.PI));
                CvInvoke.Ellipse(mask, roifine, new Bgr(Color.White).MCvScalar, -1);
            }

            // Set everything outside mask to zero
            Image<Gray, Byte> Img2m = Img2.Copy(mask);
            mask.ToBitmap().Save("c:\\visningsbilder\\vombinedFine.bmp");

            // Calculate center of mass
            MCvPoint2D64f massCenterTemp = new MCvPoint2D64f();
            Masscenter(Img2m, out massCenterTemp, threshold);
            massCenter = roundoffPoint(massCenterTemp);

            //Calculate offset from ideal point
            double[] massCenterOffset = new double[2];
            massCenterOffset[0] = massCenterTemp.X - currentData[12] + currentData[0];
            //massCenterOffset[1] = currentData[13] - currentData[1] - massCenterTemp.Y;
            massCenterOffset[1] = massCenterTemp.Y - currentData[13] + currentData[1];

            // calculate offset in polar coordinates
            double[] offset_Rteta = new double[2];
            offset_Rteta[0] = massCenterOffset[0] * Math.Cos(angle) - massCenterOffset[1] * Math.Sin(angle);
            offset_Rteta[1] = massCenterOffset[0] * Math.Cos(angle) + massCenterOffset[1] * Math.Sin(angle);

            // Convert back to bitmap, via image. First draw ellipse borders and segment (but not for 0111 and 1111
            if (segmentnr != 1 && segmentnr != 11)
            {
                CvInvoke.Ellipse(Img2m, roifine, new Gray(255).MCvScalar);
            }

            Image<Bgr, byte> Img2mc = Img2m.Convert<Bgr, byte>();
            CvInvoke.ApplyColorMap(Img2m, Img2mc, ColorMapType.Jet);
            Img2mc.Draw(Seg, new Bgr(Color.Yellow), 1);
            Img2mc.Draw(new Cross2DF(massCenter, 10, 10), new Bgr(Color.Red), 2);
            Img2mc.Draw(new Cross2DF(idealC, 10, 10), new Bgr(Color.Green), 2);
            returnBitmap = Img2mc.ToBitmap();
            returnBitmap.Save("c:\\visningsbilder\\vombinedFine.bmp");

            offsetXY = massCenterOffset;
            offsetRT = offset_Rteta;

            System.Diagnostics.Debug.WriteLine("mass center" + massCenter);
            System.Diagnostics.Debug.WriteLine("mass center offset" + offsetXY[0] + " " + offsetXY[1]);
            System.Diagnostics.Debug.WriteLine("mass center offset" + offsetRT[0] + " " + offsetRT[1]);
            System.Diagnostics.Debug.WriteLine("angle " + (angle));
            IMG.Dispose();
            IMG = null;
            BKGR.Dispose();
            BKGR = null;
        }
        public static void pix2mrad(int segment, double[] pixelOffset, out double[] mradoffset)
        {
            mradoffset = new double[2];
            if (segment < 12)
            {
                mradoffset[0] = pixelOffset[0] / 17;
                mradoffset[1] = pixelOffset[1] / 15;
            }
            else if (segment > 11 && segment < 35)
            {
                mradoffset[0] = pixelOffset[0] / 50;
                mradoffset[1] = pixelOffset[1] / 19;
            }
            else
            {
                mradoffset[0] = -pixelOffset[0] / 30;
                mradoffset[1] = pixelOffset[1] / 31;
            }

        }

        public static void mrad2revs(int segment, double[] mrad, out double[] revs)
        {
            revs = new double[2];
            if (segment < 12)
            {
                revs[0] = (-mrad[0] / 1.4) - (mrad[1] / 4.8);
                revs[1] = (-mrad[0] / 1.4) + (mrad[1] / 4.8);
            }
            else if (segment > 11 && segment < 35)
            {
                revs[0] = (-mrad[0] / 1.4) - (mrad[1] / 5.3);
                revs[1] = (-mrad[0] / 1.4) + (mrad[1] / 5.3);
            }
            else
            {
                revs[0] = (-mrad[0] / 1.4) - (mrad[1] / 5.6);
                revs[1] = (-mrad[0] / 1.4) + (mrad[1] / 5.6);
            }
        }

        public static Point roundoffPoint(MCvPoint2D64f InPoint)
        {
            Point OutPoint = new Point();
            OutPoint.X = (int)Math.Round(InPoint.X, 0);
            OutPoint.Y = (int)Math.Round(InPoint.Y, 0);
            return OutPoint;
        }

        public static double[] findImageMax(Image<Gray, byte> InImg)
        {
            double[] maxval = new double[3];
            double[] minV, maxV;
            Point[] minL, maxL;
            InImg.MinMax(out minV, out  maxV, out minL, out maxL);
            System.Diagnostics.Debug.WriteLine(maxL[0].X + " " + maxL.Length);
            maxval[0] = maxV[0];
            maxval[1] = (double)maxL[0].X;
            maxval[2] = (double)maxL[0].Y;
            return maxval;
        }

        public static double[] GetSegmentIdealCGPoint(int segmentnr)
        {
            int[] currentData = DAL.GetAOIDataRawSegment("" + segmentnr);
            Image<Bgr, byte> temp = new Image<Bgr, byte>(imagesizex, imagesizey);
            Point[] Seg = new Point[] { new Point(currentData[4], currentData[5]), new Point(currentData[6], currentData[7]), new Point(currentData[8], currentData[9]), new Point(currentData[10], currentData[11]) };
            temp.Draw(Seg, new Bgr(Color.White), 1);
            MCvPoint2D64f idealPoint1;
            Masscenter(temp.Convert<Gray, Byte>(), out idealPoint1, 0);
            double[] idealPoint = new double[2];
            idealPoint[0] = idealPoint1.X;
            idealPoint[1] = idealPoint1.Y;

            return idealPoint;
        }

        public static void Coarse_algorithm(Bitmap IMG1, Bitmap IMG2, Bitmap BKGR, int segmentnr, out int direction, out Bitmap bitmap1, out Bitmap bitmap2)
        {
            CvInvoke.UseOpenCL = false;

            // IMG1  = left/up IMG2 = right/down BKGR = black background, segmentnr = id, direction = direction of motors

            //Check the type of segment
            int switchDir = 1;
            // if segment id is more than 34, it is outer segment -> switch direction
            if (segmentnr > 34) { switchDir = -1; }

            // Convert to Image<Gray, byte>
            Image<Gray, byte> Img1In = new Image<Gray, byte>(IMG1);
            Image<Gray, byte> Img2In = new Image<Gray, byte>(IMG2);
            Image<Gray, byte> BkrIn = new Image<Gray, byte>(BKGR);
            Image<Gray, byte> Img1out = Img1In.Copy();
            Image<Gray, byte> Img2out = Img1In.Copy();
            Img1out.SetZero();
            Img2out.SetZero();

            try
            {
                CvInvoke.AbsDiff(Img1In, BkrIn, Img1out);
                CvInvoke.AbsDiff(Img2In, BkrIn, Img2out);
            }
            catch
            {

            }

            // Apply ROI Mask - segment area - this should be picked up from database
            Image<Gray, byte> mask = Img1In.Copy();
            mask.SetZero();

            int[] AOIData = DAL.GetAOIDataRawSegment("" + segmentnr);
            Point[] Seg = new Point[] { new Point(AOIData[4] - AOIData[0], AOIData[5] - AOIData[1]), new Point(AOIData[6] - AOIData[0], AOIData[7] - AOIData[1]), new Point(AOIData[8] - AOIData[0], AOIData[9] - AOIData[1]), new Point(AOIData[10] - AOIData[0], AOIData[11] - AOIData[1]) };
            mask.Draw(Seg, new Gray(255), -1);
            Img1out = Img1out.Copy(mask.Convert<Gray, byte>());
            Img2out = Img2out.Copy(mask.Convert<Gray, byte>());

            mask.ToBitmap().Save("c:\\visningsbilder\\maskCoarse.bmp");
            Img1out.ToBitmap().Save("c:\\visningsbilder\\outbmp1.bmp");
            Img2out.ToBitmap().Save("c:\\visningsbilder\\outbmp2.bmp");

            // Calculate Sum
            MCvScalar s1 = CvInvoke.Sum(Img1out);
            MCvScalar s2 = CvInvoke.Sum(Img2out);

            //Draw ROI
            Img1out.Draw(Seg, new Gray(255), 1);
            Img2out.Draw(Seg, new Gray(255), 1);

            // Convert to bitmap
            bitmap1 = Img1out.ToBitmap();
            bitmap2 = Img2out.ToBitmap();

            // Check if sum is zero
            if (s1.V0 == 0 | s2.V0 == 0) { direction = 2; }
            // When within 10%
            else if (s1.V0 > s2.V0 * 1.1)
            {
                direction = 1 * switchDir;
            }
            else if (s1.V0 < s2.V0 * 0.8)
            {
                direction = -1 * switchDir;
            }
            else
            {
                direction = 0;
            }

            IMG1.Dispose();
            IMG1 = null;
            IMG2.Dispose();
            IMG2 = null;
            BKGR.Dispose();
            BKGR = null;
        }


        // Patterntype: 0 = up/down, 1 = left/right, 2 = down/up, 3 = right/left;
        public static void CoarsePatternPoints(int segmentnr, int patternType, out double[,] PPoints)
        {
            int[] currentData = DAL.GetAOIDataRawSegment("" + segmentnr);

            // Center of the pattern
            int cx = currentData[14];
            int cy = currentData[15];

            //Screen size
            int sy = screensizey;
            int sx = screensizex;

            // Calculate tilt angle of pattern. (y4-y3)/(x3-x4)
            double angle = Math.Atan((double)(currentData[10] - currentData[8]) / (currentData[11] - currentData[9]));

            angle = angle + Math.PI / 2 * (patternType - 1);

            // Calculate tiltline points for centered pattern
            double x1 = sx * Math.Cos(angle) + cx;
            double y1 = sx * Math.Sin(angle) + cy;

            double x2 = sx * Math.Cos(angle + Math.PI) + cx;
            double y2 = sx * Math.Sin(angle + Math.PI) + cy;

            double x3 = sx * Math.Cos(angle + Math.PI / 2) + cx;
            double y3 = sx * Math.Sin(angle + Math.PI / 2) + cy;


            // Make array from list
            double[,] Pts = new double[3, 2] { { x1, y1 }, { x2, y2 }, { x3, y3 } };

            // Round to value -1 to 1
            for (int i = 0; i < 3; i++)
            {
                Pts[i, 0] = Pts[i, 0] / (sx / 2) - 1;
                Pts[i, 1] = -(Pts[i, 1] / (sy / 2) - 1);
            };

            PPoints = Pts;
        }


        public static Point roundpoint(Point P)
        {
            int x1 = P.X;
            if (x1 < 0)
            {
                P.X = 0;
            }

            if (x1 > screensizex)
            {
                P.X = screensizex;
            }

            int x2 = P.Y;
            if (x2 < 0)
            {
                P.Y = 0;
            }

            if (x2 > screensizey)
            {
                P.Y = screensizey;
            }

            return P;
        }

        public static Mat RemoveBackground(Mat IMG, Mat BKGR)
        {
            Mat Result;
            CvInvoke.UseOpenCL = false;

            IInputArray IMGmata = IMG;
            IInputArray BKRmata = BKGR;
            IOutputArray Resultmata = new Mat();
            CvInvoke.AbsDiff(IMGmata, BKRmata, Resultmata);
            Result = (Mat)Resultmata;

            return Result;
        }

        public static void Masscenter(Image<Gray, Byte> IMG, out MCvPoint2D64f gravC, double threshold)
        {
            Mat mask = new Mat();
            double maxval = 255;

            // Set everything under threshold to zero, leave the rest
            CvInvoke.Threshold(IMG, mask, threshold, maxval, ThresholdType.ToZero);

            //Beräkna moment
            MCvMoments m = CvInvoke.Moments(mask, false);

            // Beräkna tyngdpunkt
            MCvPoint2D64f gravityCenter = new MCvPoint2D64f((m.M10 / m.M00), (m.M01 / m.M00));

            gravC = gravityCenter;
        }

        public static void bitmap2mat(Bitmap bmp, out Mat mat)
        {
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            // data = scan0 is a pointer to our memory block.
            IntPtr data = bmpData.Scan0;

            // step = stride = amount of bytes for a single line of the image
            int step = bmpData.Stride;

            // Viktigt med rätt depthtype och antal kanaler
            mat = new Mat(bmp.Height, bmp.Width, DepthType.Cv8U, 1, data, step);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
        }

        // Draws red segment if status = 0 (not ok) and green segments if status = 1 (ok)
        public static Bitmap drawSegmentTypes(int[] status, Bitmap Canvas)
        {
            Image<Bgr, byte> segmentsImage = new Image<Bgr, byte>(Canvas);
            int ticker = 0;
            foreach (string s in Calibrate.segments)
            {
                int[] AOIData = DAL.GetAOIData(s);
                Point[] Seg = new Point[] { new Point(AOIData[4], AOIData[5]), new Point(AOIData[6], AOIData[7]), new Point(AOIData[8], AOIData[9]), new Point(AOIData[10], AOIData[11]) };
                if (status[ticker] == 0)
                {
                    segmentsImage.Draw(Seg, new Bgr(Color.Red), 1);
                }
                if (status[ticker] == 1)
                {
                    segmentsImage.Draw(Seg, new Bgr(Color.Green), 1);
                }
            }

            Bitmap outBmp = segmentsImage.ToBitmap();
            return outBmp;
        }

        public static double[,] SegmentPatternPoints(Point PatternCenter)
        {
            int sx = screensizex;
            int sy = screensizey;
            double[,] doublePoints = new double[4, 2];

            doublePoints[0, 0] = ((2.0f / sx) * (PatternCenter.X - dotWidth)) - 1;
            doublePoints[0, 1] = -1 * (((2.0f / sy) * (PatternCenter.Y - dotWidth)) - 1);

            doublePoints[1, 0] = ((2.0f / sx) * (PatternCenter.X - dotWidth)) - 1;
            doublePoints[1, 1] = -1 * (((2.0f / sy) * (PatternCenter.Y + dotWidth)) - 1);

            doublePoints[2, 0] = ((2.0f / sx) * (PatternCenter.X + dotWidth)) - 1;
            doublePoints[2, 1] = -1 * (((2.0f / sy) * (PatternCenter.Y - dotWidth)) - 1);

            doublePoints[3, 0] = ((2.0f / sx) * (PatternCenter.X + dotWidth)) - 1;
            doublePoints[3, 1] = -1 * (((2.0f / sy) * (PatternCenter.Y + dotWidth)) - 1);

            return doublePoints;
        }

		private static bool rect_check(Size sz, Rectangle r)
		{
			if (r.X < 0) return false;
			if (r.Y < 0) return false;
			if (r.Right  >= sz.Width)  return false;
			if (r.Bottom >= sz.Height) return false;
			return true;
		}


        public static void checkAllSegmentsFine(
			CameraController cameraController, 
			string[] cameraSettings, 
			MonitorHandler monitor, 
			CheckAllForm caf,
			out double[,] fineData, 
			out Bitmap overviewFine )
        {
            fineData = new double[66, 5];
            double [,] doublePoints;
            double exposureRate = 4.5 * double.Parse(cameraSettings[0]);
            double framerate = double.Parse(cameraSettings[5]);
            double threshold = double.Parse(cameraSettings[2]);
            int waitOnMonitor = int.Parse(cameraSettings[3]);
            int waitOnCycle = int.Parse(cameraSettings[4]);
            int tick = 0;
            Bitmap combinedBitmap = new Bitmap(304, 164, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            double[] offsetXY, offsetRT, mradOffset;
            Point massCenter = new Point(0,0);
            int ok;
            //Image<Gray, byte> TotImg = new Image<Gray, byte>(imagesizex, imagesizey);
            //TotImg.SetZero();
            //Rectangle currentROI;

            Image<Bgr, byte> TotImg = new Image<Bgr, byte>(imagesizex, imagesizey);
            TotImg.SetZero();


            int errcnt = 0;
			int i = 0, n = Calibrate.segments.Length;
			foreach (string s in Calibrate.segments)
            {
				++i;
				caf.SetProgress(100 * i / n);

                // Get segment data
                int[] AOIData = DAL.GetAOIData(s);

                //Set cameracontroller parameters
                cameraController.SetAOI(AOIData[2], AOIData[3], AOIData[0], AOIData[1] - AOIData[3]);
                cameraController.SetExposureTime(exposureRate);
                //Rectangle currentROI = new Rectangle((int)AOIData[0], (int)AOIData[1], (int)AOIData[2], (int)AOIData[3]);
                TotImg.ROI = Rectangle.Empty;
                Size sz = TotImg.Size;
                Rectangle currentROI = new Rectangle((int)AOIData[0], (int)AOIData[1], (int)AOIData[2], (int)AOIData[3]);
                TotImg.ROI = currentROI;

				if (!rect_check(sz, currentROI))
				{
					//MessageBox.Show("ROI Error");
                    ++errcnt;
					continue;
				}

                //Show black pattern on monitor
                monitor.BlackScreen();
                Thread.Sleep(waitOnMonitor);

                //Take background image
                Bitmap cameraFineAlignBlack = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                //Show pattern on monitor
                doublePoints = SegmentPatternPoints(new Point(AOIData[14], AOIData[15]));
                monitor.UpdatePatternVertices(doublePoints, true);
                Thread.Sleep(waitOnMonitor);

                //Take background image
                Bitmap cameraFineAlign = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                //Calculate alignment
                Fine_algorithm(cameraFineAlign, cameraFineAlignBlack, threshold, int.Parse(DAL.GetRawSegmentNumber(s)), out offsetXY, out offsetRT, out massCenter, out combinedBitmap);
                Algorithm.pix2mrad(int.Parse(DAL.GetRawSegmentNumber(s)), offsetRT, out mradOffset);
                Image<Bgr, byte> combinedImg = new Image<Bgr, byte>(combinedBitmap);
         
                // check tolerance
                ok = (mradOffset[0]+mradOffset[1] < fineTolerance) ? 1 : 0;

                fineData[tick, 0] = ok;
                fineData[tick, 1] = offsetXY[0];
                fineData[tick, 2] = offsetXY[1];
                fineData[tick, 3] = mradOffset[0];
                fineData[tick, 4] = mradOffset[1];

				Image<Bgr, Byte> addImg = new Image<Bgr, byte>(TotImg.Size);
                CvInvoke.Add(combinedImg, TotImg, addImg);
                CvInvoke.cvCopy(addImg, TotImg, IntPtr.Zero);
                tick++;

				addImg.Dispose();
				combinedImg.Dispose();
                cameraFineAlignBlack.Dispose();
                cameraFineAlign.Dispose();
            }
			TotImg.ROI = Rectangle.Empty;
			overviewFine = TotImg.ToBitmap();
			caf.SetImage(overviewFine);
			caf.WaitClose();
		}

        public static void checkAllSegmentsCoarse(
            CameraController cameraController,
            string[] cameraSettings,
            MonitorHandler monitor,
            CheckAllForm caf,
            out double[,] coarseData,
            out Bitmap overviewX)
        {
            coarseData = new double[66, 3];
            double[,] doublePoints;
            double exposureRate = 4.5 * double.Parse(cameraSettings[0]);
            double framerate = double.Parse(cameraSettings[5]);
            double threshold = double.Parse(cameraSettings[2]);
            int waitOnMonitor = int.Parse(cameraSettings[3]);
            int waitOnCycle = int.Parse(cameraSettings[4]);
            int tick = 0;
            Bitmap coarseBitmap1 = new Bitmap(304, 164, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            Bitmap coarseBitmap2 = new Bitmap(304, 164, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            int dirUD, dirLR;

            int ok;
            //Image<Gray, byte> TotImg = new Image<Gray, byte>(imagesizex, imagesizey);
            //TotImg.SetZero();
            //Rectangle currentROI;

            Image<Bgr, byte> TotImg1 = new Image<Bgr, byte>(imagesizex, imagesizey);
            TotImg1.SetZero();
            Image<Bgr, byte> TotImg2 = new Image<Bgr, byte>(imagesizex, imagesizey);
            TotImg2.SetZero();


            int errcnt = 0;
            int i = 0, n = Calibrate.segments.Length;
            foreach (string s in Calibrate.segments)
            {
                ++i;
                caf.SetProgress(100 * i / n);

                // Get segment data
                int[] AOIData = DAL.GetAOIData(s);

                //Set cameracontroller parameters
                cameraController.SetAOI(AOIData[2], AOIData[3], AOIData[0], AOIData[1] - AOIData[3]);
                cameraController.SetExposureTime(exposureRate);
                //Rectangle currentROI = new Rectangle((int)AOIData[0], (int)AOIData[1], (int)AOIData[2], (int)AOIData[3]);
                TotImg1.ROI = Rectangle.Empty;
                Size sz1 = TotImg1.Size;
                Rectangle currentROI = new Rectangle((int)AOIData[0], (int)AOIData[1], (int)AOIData[2], (int)AOIData[3]);
                TotImg1.ROI = currentROI;

                TotImg2.ROI = Rectangle.Empty;
                Size sz2 = TotImg2.Size;
                TotImg2.ROI = currentROI;

                if (!rect_check(sz1, currentROI))
                {
                    //MessageBox.Show("ROI Error");
                    ++errcnt;
					continue;
                }

                //Show black pattern on monitor
                monitor.BlackScreen();
                Thread.Sleep(waitOnMonitor);

                //Take background image
                Bitmap cameraCoarseAlignBlack = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                Bitmap cameraCoarseAlignBlackUD = new Bitmap(cameraCoarseAlignBlack);

                //Show left/right patterns on monitor and take images
                Algorithm.CoarsePatternPoints(int.Parse(DAL.GetRawSegmentNumber(s)), 1, out doublePoints);
                monitor.UpdatePatternVertices(doublePoints, false);
                Thread.Sleep(waitOnMonitor);
                //Take image
                Bitmap cameraCoarseLR = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                //Show right/left patterns on monitor and take images
                Algorithm.CoarsePatternPoints(int.Parse(DAL.GetRawSegmentNumber(s)), 3, out doublePoints);
                monitor.UpdatePatternVertices(doublePoints, false);
                Thread.Sleep(waitOnMonitor);
                //Take image
                Bitmap cameraCoarseRL = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                //Calculate alignment Left/Right
                Coarse_algorithm(cameraCoarseLR, cameraCoarseRL, cameraCoarseAlignBlack, int.Parse(DAL.GetRawSegmentNumber(s)), out dirLR, out coarseBitmap1, out coarseBitmap2);
                Image<Bgr, byte> coarseLR = new Image<Bgr, byte>(coarseBitmap1);
                
                //////////////////////////////////////////////////////
                
                //Show up/down patterns on monitor and take images
                Algorithm.CoarsePatternPoints(int.Parse(DAL.GetRawSegmentNumber(s)), 0, out doublePoints);
                monitor.UpdatePatternVertices(doublePoints, false);
                Thread.Sleep(waitOnMonitor);
                //Take image
                Bitmap cameraCoarseUD = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                //Show right/left patterns on monitor and take images
                Algorithm.CoarsePatternPoints(int.Parse(DAL.GetRawSegmentNumber(s)), 2, out doublePoints);
                monitor.UpdatePatternVertices(doublePoints, false);
                Thread.Sleep(waitOnMonitor);
                //Take image
                Bitmap cameraCoarseDU = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                //Calculate alignment Up/Down
                Coarse_algorithm(cameraCoarseUD, cameraCoarseDU, cameraCoarseAlignBlackUD, int.Parse(DAL.GetRawSegmentNumber(s)), out dirUD, out coarseBitmap1, out coarseBitmap2);
                Image<Bgr, byte> coarseUD = new Image<Bgr, byte>(coarseBitmap1);

                // check tolerance
                ok = (dirLR == 0 && dirUD == 0) ? 1 : 0;
                coarseData[tick, 0] = dirLR;
                coarseData[tick, 1] = dirUD;

                Image<Bgr, Byte> addImg1 = new Image<Bgr, byte>(TotImg1.Size);
                CvInvoke.Add(coarseUD, TotImg1, addImg1);
                CvInvoke.cvCopy(addImg1, TotImg1, IntPtr.Zero);

                Image<Bgr, Byte> addImg2 = new Image<Bgr, byte>(TotImg1.Size);
                CvInvoke.Add(coarseLR, TotImg2, addImg2);
                CvInvoke.cvCopy(addImg2, TotImg2, IntPtr.Zero);
                tick++;

                addImg1.Dispose();
                addImg2.Dispose();
                coarseUD.Dispose();
                coarseLR.Dispose();
                cameraCoarseAlignBlack.Dispose();
                cameraCoarseAlignBlackUD.Dispose();
                cameraCoarseDU.Dispose();
                cameraCoarseLR.Dispose();
                cameraCoarseRL.Dispose();
                cameraCoarseUD.Dispose();
            }
            TotImg1.ROI = Rectangle.Empty;
            TotImg2.ROI = Rectangle.Empty;
            overviewX = TotImg1.ToBitmap(); //LR
            //overviewY = TotImg2.ToBitmap(); //UD
            caf.SetImage(overviewX);
            caf.WaitClose();
        }
    }
}
