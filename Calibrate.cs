using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace MirrorAlignmentSystem
{
    class Calibrate
    {
        public static int blackhole_x = Algorithm.imagesizex/2 + 16;
        public static int blackhole_y = Algorithm.imagesizey/2 + 173;
        public static string[] segments = {"0111", "0211", "0311", "0411", "0511", "0611","0711","0811","0911","1011","1111","0121","0221","0321","0421", "0521", "0621", "0721", "0821", "0921", "1021", "1121", "0122", "0222", "0322", "0422", "0522", "0622", "0722", "0822", "0922", "1022", "1122", "0131", "0132","0133", "0231", "0232","0233", "0331", "0332", "0333", "0431", "0432", "0433", "0531", "0532", "0533", "0631", "0632", "0633", "0731", "0732", "0733", "0831", "0832", "0833", "0931", "0932", "0933", "1031", "1032", "1033", "1131", "1132", "1133"};
        public static string[] refSegments = { "1022", "0822", "0421", "0911"};// "0221" };
        // refpoint outside the mirror segment CHECK
        private static PointF refpoint = new PointF(960, 580);
        
        public static void mirrorCoM(Bitmap InBMP, string segment, out double[] MirrorCoMPoint, out double[] MirrorCoMOffsetPoint)
        {
            // Find seedpoint = ideal centerpoint
            double [] idealPoint = Algorithm.GetSegmentIdealCGPoint(int.Parse(DAL.GetRawSegmentNumber(segment)));
            PointF seedpoint = new PointF((float)Math.Round(idealPoint[0]), (float)Math.Round(idealPoint[1]));

            //InBMP.Save("c:\\visningsbilder\\watershed_inbmp.bmp");
            Image<Gray, byte> InImg = new Image<Gray, byte>(InBMP);
            Image<Gray, byte> crop = InImg.Copy();
            //crop.SetZero();
            //crop.Draw(new Rectangle(new Point((int)seedpoint.X-120, (int)seedpoint.Y-120), new Size(240, 240)), new Gray(255), -1);
            //InImg = InImg.Copy(crop);

            // Threshold image and erode/dilate
            Mat mask = new Mat();
            double threshold = (double)(InBMP.GetPixel((int)seedpoint.X, (int)seedpoint.Y).A);
            threshold = threshold * 0.8;
            CvInvoke.Threshold(InImg, mask, threshold, 255, ThresholdType.Binary);
            InImg = mask.ToImage<Gray, byte>();
            InImg.Erode(20);
            InImg.Dilate(20);

            // Create a seed and a reference
            Image<Gray, Int32> marker = new Image<Gray, Int32>(InImg.Width, InImg.Height);
            marker.SetValue(new Gray(0));
            marker.Convert<Gray, Int32>();

            marker.Draw(new CircleF(refpoint, (float)1), new Gray(10), 0);
            marker.Draw(new CircleF(seedpoint, (float)1), new Gray(250), 0);

            // Do the watershed
            //InImg.ToBitmap().Save("c:\\visningsbilder\\PreWS.bmp");
            CvInvoke.Watershed(InImg.Convert<Bgr, Byte>(), marker);
            Image<Gray, byte> marker2 = marker.Convert<Gray, byte>();
            Mat mask2 = new Mat();
            CvInvoke.Threshold(marker2, mask2, 200, 255, ThresholdType.Binary);
            marker2 = mask2.ToImage<Gray, byte>();

            //Check if watershed is ok
            //marker2.ToBitmap().Save("c:\\visningsbilder\\marker2.bmp");
            int checksum = CvInvoke.CountNonZero(marker2.Convert<Gray, int>());
            MCvPoint2D64f P2 = new MCvPoint2D64f(1, 1);
            System.Diagnostics.Debug.WriteLine(checksum + " is the sum");

            if (checksum < 11000 && checksum > 8000)
            {
                // Find center of mass

                //erodedMarker.ToBitmap().Save("c:\\visningsbilder\\watershed_afterThreshold.bmp");

                Algorithm.Masscenter(marker2, out P2, 0);
            }
            else
            {

            }
            //Calculate offsetpoint
            double[] tempMirrorCoMOffsetPoint = new double[2];
            tempMirrorCoMOffsetPoint[0] = (P2.X - idealPoint[0]) * Algorithm.pix2mradDISC;
            tempMirrorCoMOffsetPoint[1] = (P2.Y - idealPoint[1]) * Algorithm.pix2mradDISC;

            // Convert to array
            double[] tempMirrorCoMPoint = new double[2];
            tempMirrorCoMPoint[0] = P2.X;
            tempMirrorCoMPoint[1] = P2.Y;

            // Return values
            MirrorCoMOffsetPoint = tempMirrorCoMOffsetPoint;
            MirrorCoMPoint = tempMirrorCoMPoint;
        }

        public static void findBlackHole(Bitmap InBMP, out double[] blackCGpoint, out double[] blackCGoffsetpoint)
        {
            // Convert to Image<>
            Image<Gray, byte> InImg = new Image<Gray, byte>(InBMP);

            // Set AOI for image, where the hole is contained
            Rectangle blackROI = new Rectangle(blackhole_x-40, blackhole_y-40, 80, 80);
            InImg.ROI = blackROI;

            // find inverse
            Image<Gray, byte> blackHoleImg = InImg.Copy();
            Image<Gray, byte> invBlackHoleImg = blackHoleImg.Not();
            Mat mask = new Mat();

            // Apply gaussian blur
            invBlackHoleImg.SmoothGaussian(7);

            // Find point of max value
            invBlackHoleImg.ToBitmap().Save("c:\\visningsbilder\\blackholeimg.bmp");
            double[] maxVL = Algorithm.findImageMax(invBlackHoleImg);
            Point maxLocation = new Point((int)Math.Round(maxVL[1]), (int)Math.Round(maxVL[2]));

            // Limit area to 9x9 pixels
            Image<Gray, byte> circlemask = blackHoleImg.Copy();
            circlemask.SetZero();
            System.Diagnostics.Debug.WriteLine(maxVL[0] + " " + maxVL[1] + " " + maxVL[2]);
            CvInvoke.Circle(circlemask, maxLocation, 4, new MCvScalar(255), -1);
            blackHoleImg = invBlackHoleImg.Copy(circlemask);

            // Calculate CG
            MCvPoint2D64f P2 = new MCvPoint2D64f();
            Algorithm.Masscenter(blackHoleImg, out P2, 0);
            System.Diagnostics.Debug.WriteLine(P2.X + " " + P2.Y);
            P2.X = P2.X + (double)blackhole_x-40;
            P2.Y = P2.Y + (double)blackhole_y-40;

            // Return values
            double[] tempblackCGpoint = new double[2];
            tempblackCGpoint[0] = P2.X;
            tempblackCGpoint[1] = P2.Y;
            blackCGpoint = tempblackCGpoint;

            //Calculate offset and return
            double[] tempBlackCGoffsetPoint=new double[2];
            tempBlackCGoffsetPoint[0] = (tempblackCGpoint[0] - Calibrate.blackhole_x) * Algorithm.pix2distBH;
            tempBlackCGoffsetPoint[1] = (tempblackCGpoint[1] - Calibrate.blackhole_y) * Algorithm.pix2distBH;
            blackCGoffsetpoint = tempBlackCGoffsetPoint;

        }

        public static void drawCrosses(out Bitmap outBitmap, Bitmap canvas, string[] NotOKsegments, double[,] segmentCenterPoints, double[] blackholePoint)
        {
            
            Image<Bgr, byte> crossesImage = new Image<Bgr, byte>(canvas);
            int ticker = 0;
            foreach (string s in refSegments)
            {
                double[] idealCenter = Algorithm.GetSegmentIdealCGPoint(int.Parse(DAL.GetRawSegmentNumber(s)));
                crossesImage.Draw(new Cross2DF(new PointF((float)Math.Round(idealCenter[0]), (float)Math.Round(idealCenter[1])), 50f, 50f), new Bgr(Color.Green), 4);
                crossesImage.Draw(new Cross2DF(new PointF((float)segmentCenterPoints[ticker,0], (float)segmentCenterPoints[ticker, 1]), 50f, 50f), new Bgr(Color.Yellow), 4);
                ticker++;
            }
            ticker = 0;
            System.Diagnostics.Debug.WriteLine(NotOKsegments);
            foreach (string s in NotOKsegments)
            {
                if (s != "ok")
                {
                    double[] idealCenter = Algorithm.GetSegmentIdealCGPoint(int.Parse(DAL.GetRawSegmentNumber(s)));
                    crossesImage.Draw(new Cross2DF(new PointF((float)Math.Round(idealCenter[0]), (float)Math.Round(idealCenter[1])), 50f, 50f), new Bgr(Color.Red), 4);
                    ticker++;
                }
                
            }
            
            crossesImage.Draw(new Cross2DF(new PointF((float)blackholePoint[0], (float)blackholePoint[1]), 50f, 50f), new Bgr(Color.Violet), 4);
            crossesImage.Draw(new Cross2DF(new PointF((float)blackhole_x, (float)blackhole_y), 35f, 35f), new Bgr(Color.Green), 2);
            crossesImage.Draw(new Rectangle(blackhole_x - 40, blackhole_y - 40, 80, 80), new Bgr(Color.Yellow), 2);
            crossesImage.Draw(new CircleF(refpoint, 5), new Bgr(Color.Blue));
            outBitmap = crossesImage.ToBitmap();
            
        }

        public static Bitmap drawSegments(Bitmap Canvas)
        {
            Image<Bgr, byte> segmentsImage = new Image<Bgr, byte>(Canvas);
            foreach (string s in segments)
            {
                int[] AOIData = DAL.GetAOIData(s);
                Point[] Seg = new Point[] {new Point(AOIData[4], AOIData[5]), new Point(AOIData[6], AOIData[7]), new Point(AOIData[8], AOIData[9]), new Point(AOIData[10], AOIData[11])};
                segmentsImage.Draw(Seg, new Bgr(Color.Red), 1);
            }
            segmentsImage.ROI = new Rectangle(620, 390, 690, 440);

            Bitmap outBmp = segmentsImage.Copy().ToBitmap();
            //outBmp.Save("c:\\visningsbilder\\crosses3.bmp");
            return outBmp;
        }

        public static void rotationZ(double[,] refPoints, out double rotZ)
        {
            rotZ = 0;
            
            double angle1 = Math.Atan((refPoints[0,1] - refPoints[1,1]) / (refPoints[0,0] - refPoints[1,0]));
            double angle2 = Math.Atan((refPoints[2,1] - refPoints[3,1]) / (refPoints[2,0] - refPoints[3,0]));
            rotZ = (angle1 + angle2) / 2;
        }
            
            

    }
}
