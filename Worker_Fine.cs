
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MirrorAlignmentSystem
{
	/// <summary>
	///  This class is the main worker of the application
	/// </summary>
	public partial class Worker
	{

		partial void ExecuteFine()
		{
			//Makes sure the number of cycles between each black image on the TV is correct.
			if (blackBGCounter > Int32.Parse(mainWindow.GetBlackBGNumber()))
			{
				stopWatchBackgroundImage.Reset();
				stopWatchBackgroundImage.Start();

				monitor.BlackScreen();

				stopWatchBackgroundImage.Stop();
				System.Diagnostics.Debug.WriteLine("Background image with SlimDX time elapsed: " + stopWatchBackgroundImage.ElapsedMilliseconds + "ms");

				//If the application is running in online mode it asks for a image from the camera
				if (!offline)
				{
					//The sleep to make sure the image is on the monitor before the application continues
					Thread.Sleep(waitOnMonitor);

					cameraController.SetAOI(AOIData[2], AOIData[3], AOIData[0], AOIData[1] - AOIData[3]);
					cameraController.SetExposureTime(exposureRate);

					if (cameraInitialized)
					{
						stopWatchCamera.Reset();
						stopWatchCamera.Start();

						if (cameraFineAlignBlack != null)
						{
							cameraFineAlignBlack.Dispose();
							cameraFineAlignBlack = null;
						}

						cameraFineAlignBlack = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
						//cameraFineAlignBlack.Save("c:\\visningsbilder\\camerafinealignblack.bmp");
						stopWatchCamera.Stop();
						System.Diagnostics.Debug.WriteLine("Camera time elapsed: " + stopWatchCamera.ElapsedMilliseconds + "ms");

						blackBkgAquired = true;
					}
				}
				//If the application is running in offline mode the application asks the database for a image instead
				else
				{
					//System.Diagnostics.Debug.WriteLine("The program is running in offline mode , picking up black background image");

					if (cameraFineAlignBlack != null)
					{
						cameraFineAlignBlack.Dispose();
						cameraFineAlignBlack = null;
					}
					cameraFineAlignBlack = DAL.GetBlackBackgroundOfflineCameraImage();
					blackBkgAquired = true;
				}

				blackBGCounter = 0;
			}
			//The application makes sure a bitmap pattern is to be displayed on the TV
			else
			{
				stopWatchBackgroundImage.Reset();
				stopWatchBackgroundImage.Start();

				doublePoints = new double[4, 2];

				Point[] points = new Point[1];

				points[0] = new Point(AOIData[14], AOIData[15]);

				float sx = (float)Algorithm.screensizex;
				float sy = (float)Algorithm.screensizey;

				doublePoints[0, 0] = ((2.0f / sx) * (points[0].X - dotWidth)) - 1;
				doublePoints[0, 1] = -1 * (((2.0f / sy) * (points[0].Y - dotWidth)) - 1);

				doublePoints[1, 0] = ((2.0f / sx) * (points[0].X - dotWidth)) - 1;
				doublePoints[1, 1] = -1 * (((2.0f / sy) * (points[0].Y + dotWidth)) - 1);

				doublePoints[2, 0] = ((2.0f / sx) * (points[0].X + dotWidth)) - 1;
				doublePoints[2, 1] = -1 * (((2.0f / sy) * (points[0].Y - dotWidth)) - 1);

				doublePoints[3, 0] = ((2.0f / sx) * (points[0].X + dotWidth)) - 1;
				doublePoints[3, 1] = -1 * (((2.0f / sy) * (points[0].Y + dotWidth)) - 1);

				//points[0] = new Point(-10, -10);

				//MessageBox.Show("P1.X: " + doublePoints[0, 0] + " P1.Y: " + doublePoints[0, 1] + " P2.X: " + doublePoints[1, 0] + " P2.Y: " + doublePoints[1, 1] + " P3.X: " + doublePoints[2, 0] + " P3.Y: " + doublePoints[2, 1]);

				monitor.UpdatePatternVertices(doublePoints, true);

				stopWatchBackgroundImage.Stop();
				System.Diagnostics.Debug.WriteLine("Background image with SlimDX time elapsed: " + stopWatchBackgroundImage.ElapsedMilliseconds + "ms");

				//If the application is running in online mode
				if (!offline)
				{
					//The sleep to make sure the image is on the monitor before the application continues
					Thread.Sleep(waitOnMonitor);

					cameraController.SetAOI(AOIData[2], AOIData[3], AOIData[0], AOIData[1]-AOIData[3]);
					cameraController.SetExposureTime(exposureRate);

					if (cameraInitialized)
					{
						stopWatchCamera.Reset();
						stopWatchCamera.Start();

						//Safety measure to secure any memory leaking
						if (cameraFineAlignPattern != null)
						{
							cameraFineAlignPattern.Dispose();
							cameraFineAlignPattern = null;
						}

						cameraFineAlignPattern = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

						stopWatchCamera.Stop();
						System.Diagnostics.Debug.WriteLine("Camera time elapsed: " + stopWatchCamera.ElapsedMilliseconds + "ms");

						bitmapBkgAquired = true;
					}
				}
				//If the application is running in offline mode
				else
				{
					//Safety measure to secure any memory leaking
					if (cameraFineAlignPattern != null)
					{
						cameraFineAlignPattern.Dispose();
						cameraFineAlignPattern = null;
					}

					cameraFineAlignPattern = DAL.GetBitmapBackgroundOfflineCameraImage();
					bitmapBkgAquired = true;
				}
			}

			//If an image of the segment has been photographed while a black image was on the TV
			if (blackBkgAquired)
			{
				System.Diagnostics.Debug.WriteLine("Black aquired");

				stopWatchBlackBkgImage.Reset();
				stopWatchBlackBkgImage.Start();

				Bitmap img2 = new Bitmap(cameraFineAlignBlack);

				//Preventing memory leakage
				img2.Dispose();

				stopWatchBlackBkgImage.Stop();
				System.Diagnostics.Debug.WriteLine("Black background time elapsed: " + stopWatchBlackBkgImage.ElapsedMilliseconds + "ms");
			}

			//If an image of the segment has been photographed while a bitmap pattern was on the TV
			if (bitmapBkgAquired)
			{
				System.Diagnostics.Debug.WriteLine("Bitmap aquired");

				stopWatchBitmapBkgImage.Reset();
				stopWatchBitmapBkgImage.Start();

				Bitmap img1 = new Bitmap(cameraFineAlignPattern);

				//Preventing memory leakage
				img1.Dispose();

				stopWatchBitmapBkgImage.Stop();
				System.Diagnostics.Debug.WriteLine("Bitmap background time elapsed: " + stopWatchBitmapBkgImage.ElapsedMilliseconds + "ms");
			}

			//If both images for fine alignment have been aquired the algorithm runs
			if (bitmapBkgAquired && blackBkgAquired)
			{
				System.Diagnostics.Debug.WriteLine("Black and Bitmap aquired! Running Algorithm");

				stopWatchAlgorithm.Reset();
				stopWatchAlgorithm.Start();

				double[] revOffset = new double[2];
				double[] mradOffset = new double[2];

				//Check if the application is offline or not to be able to sent the correct raw segment number
				if (!offline)
				{
					System.Diagnostics.Debug.WriteLine("cameraFineAlignPattern width och height: " + cameraFineAlignPattern.Width + " " + cameraFineAlignPattern.Height + "cameraFineAlignBlack width och height: " + cameraFineAlignBlack.Width + " " + cameraFineAlignBlack.Height);
					Algorithm.Fine_algorithm(cameraFineAlignPattern, cameraFineAlignBlack, threshold, int.Parse(DAL.GetRawSegmentNumber(segment)), out offsetXY, out offsetRT, out massCenter, out combinedBitmap);

					Algorithm.pix2mrad(int.Parse(DAL.GetRawSegmentNumber(segment)), offsetRT, out mradOffset);

					Algorithm.mrad2revs(int.Parse(DAL.GetRawSegmentNumber(segment)), mradOffset, out revOffset);
				}
				else
				{
					Algorithm.Fine_algorithm(cameraFineAlignPattern, cameraFineAlignBlack, threshold, 28, out offsetXY, out offsetRT, out massCenter, out combinedBitmap);
				}

				stopWatchAlgorithm.Stop();
				System.Diagnostics.Debug.WriteLine("Algorithm time elapsed: " + stopWatchAlgorithm.ElapsedMilliseconds + "ms");

				stopWatchCombinedImage.Reset();
				stopWatchCombinedImage.Start();

				//mainWindow.UpdateCoMLabel(" X: " + (int)Math.Round(offsetXY[0]) + " Y: " + (int)Math.Round(offsetXY[1]) + ", R: " + (int)Math.Round(offsetRT[0]) + ", T: " + (int)Math.Round(offsetRT[1]) + "varv: " + revOffset[0].ToString("0.00") + ", " + revOffset[1].ToString("0.00"), massCenter);
                mainWindow.SetFineImg(revOffset[1], revOffset[0]);
                mainWindow.SetTanRad(mradOffset[0], mradOffset[1]);
                Algorithm.changeSegmentStatus(segment, mradOffset, statusOfSegments, out statusOfSegments);
                System.Diagnostics.Debug.WriteLine("0911 status: " + statusOfSegments[8,0]);
                //statusOfSegments = tempStatus;

				System.Diagnostics.Debug.WriteLine("Load bitmap");
				mainWindow.ShowCombinedBitmap(combinedBitmap);

				stopWatchCombinedImage.Stop();

				System.Diagnostics.Debug.WriteLine("Combined image time elapsed: " + stopWatchCombinedImage.ElapsedMilliseconds + "ms");
			}

			blackBGCounter++;

		}
	}
}

