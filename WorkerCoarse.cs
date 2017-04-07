
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

		partial void ExecuteCoarse()
		{
			//The application varies between LeftRightAlignment and UpDownAlignment and this if statment resets everything when 
			//it changes type of aligment
			if (leftRightCoarseAlignment != leftRightUpDownLastCycle)
			{
				leftBkgAquired = false;
				rightBkgAquired = false;
				upBkgAquired = false;
				downBkgAquired = false;
				blackBkgAquired = false;
				blackBkgAquiredUpDown = false;
			}

			if (leftRightCoarseAlignment)
			{
				if (!offline)
				{
					System.Diagnostics.Debug.WriteLine("ONLINE MODE LEFT&RIGHT");

					cameraController.SetAOI(AOIData[2], AOIData[3], AOIData[0], AOIData[1] - AOIData[3]);

					//Left image
					if (coarseCounter == 0)
					{

						Algorithm.CoarsePatternPoints(int.Parse(DAL.GetRawSegmentNumber(segment)), 1, out doublePoints);

						monitor.UpdatePatternVertices(doublePoints, false);

						//The sleep to make sure the image is on the monitor before the application continues
						Thread.Sleep(waitOnMonitor);

						if (cameraCoarseAlignLeft != null)
						{
							cameraCoarseAlignLeft.Dispose();
							cameraCoarseAlignLeft = null;
						}

						cameraCoarseAlignLeft = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

						leftBkgAquired = true;
					}
					//Right image
					else if (coarseCounter == 1)
					{
						Algorithm.CoarsePatternPoints(int.Parse(DAL.GetRawSegmentNumber(segment)), 3, out doublePoints);

						monitor.UpdatePatternVertices(doublePoints, false);

						Thread.Sleep(waitOnMonitor);

						if (cameraCoarseAlignRight != null)
						{
							cameraCoarseAlignRight.Dispose();
							cameraCoarseAlignRight = null;
						}

						cameraCoarseAlignRight = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

						rightBkgAquired = true;
					}

					//Background image
					else if (coarseCounter == 2)
					{
						if (blackBGCounterLeftRight > Int32.Parse(mainWindow.GetBlackBGNumber()))
						{
							monitor.BlackScreen();

							//Wait a number of milliseconds to make sure the image is really on the screen before capturing it. This time is 
							//set by the user in the option form
							Thread.Sleep(waitOnMonitor);

							if (cameraCoarseAlignBlackOne != null)
							{
								cameraCoarseAlignBlackOne.Dispose();
								cameraCoarseAlignBlackOne = null;
							}

							cameraCoarseAlignBlackOne = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

							blackBkgAquired = true;
							blackBGCounterLeftRight = 0;
						}
					}
				}
				//Offline mode
				else
				{
					System.Diagnostics.Debug.WriteLine("OFFLINE MODE LEFT&RIGHT");

					//Left image
					if (coarseCounter == 0)
					{
						if (cameraCoarseAlignLeft != null)
						{
							cameraCoarseAlignLeft.Dispose();
							cameraCoarseAlignLeft = null;
						}

						System.Diagnostics.Debug.WriteLine("GETTING LEFT IMAGE");
						cameraCoarseAlignLeft = DAL.GetCoarseLeftBitmapBackgroundOfflineImage();

						leftBkgAquired = true;
					}
					//Rigth image
					else if (coarseCounter == 1)
					{
						if (cameraCoarseAlignRight != null)
						{
							cameraCoarseAlignRight.Dispose();
							cameraCoarseAlignRight = null;
						}

						System.Diagnostics.Debug.WriteLine("GETTING RIGHT IMAGE");
						cameraCoarseAlignRight = DAL.GetCoarseRightBitmapBackgroundOfflineImage();

						rightBkgAquired = true;
					}
					//Background image
					else if (coarseCounter == 2)
					{
						System.Diagnostics.Debug.WriteLine("Getting Black image");

						if (blackBGCounterLeftRight > Int32.Parse(mainWindow.GetBlackBGNumber()))
						{
							if (cameraCoarseAlignBlackOne != null)
							{
								cameraCoarseAlignBlackOne.Dispose();
								cameraCoarseAlignBlackOne = null;
							}

							System.Diagnostics.Debug.WriteLine("GETTING BLACK IMAGE");
							cameraCoarseAlignBlackOne = DAL.GetCoarseBlackBitmapBackgroundOfflineImage();

							blackBkgAquired = true;

							blackBGCounterLeftRight = 0;
						}
					}
				}

				int dir;

				//The left right alignment has gotten it all its images and can now run the algorithm
				if (leftBkgAquired && rightBkgAquired && blackBkgAquired)
				{
					System.Diagnostics.Debug.WriteLine("");
					System.Diagnostics.Debug.WriteLine("Left right running algorithms");
					System.Diagnostics.Debug.WriteLine("");

					if (returnImg1 != null)
					{
						returnImg1.Dispose();
						returnImg1 = null;
					}

					if (returnImg2 != null)
					{
						returnImg2.Dispose();
						returnImg2 = null;
					}

					if (!offline)
					{
						Algorithm.Coarse_algorithm(cameraCoarseAlignLeft, cameraCoarseAlignRight, cameraCoarseAlignBlackOne, int.Parse(DAL.GetRawSegmentNumber(segment)), out dir, out returnImg1, out returnImg2);
					}
					else
					{
						Algorithm.Coarse_algorithm(cameraCoarseAlignLeft, cameraCoarseAlignRight, cameraCoarseAlignBlackOne, 50, out dir, out returnImg1, out returnImg2);
					}

					//System.Diagnostics.Debug.WriteLine("LEFT&RIGHT DIRECTION: " + dir);

					mainWindow.ShowLeftRightOneBackgroundBitmap(returnImg1);
					mainWindow.ShowLeftRightTwoBackgroundBitmap(returnImg2);

					mainWindow.ShowLeftRightDirection("" + dir);
				}

				blackBGCounterLeftRight++;
			}

			//UpDown Alignment
			if (upDownCoarseAlignment)
			{
				//Online mode
				if (!offline)
				{
					System.Diagnostics.Debug.WriteLine("ONLINE MODE UP&DOWN");
					System.Diagnostics.Debug.WriteLine("Segment: " + segment);
					System.Diagnostics.Debug.WriteLine("Getting Up image");

					cameraController.SetAOI(AOIData[2], AOIData[3], AOIData[0], AOIData[1] - AOIData[3]);
					cameraController.SetExposureTime(exposureRate);

					//Up image
					if (coarseCounter == 0)
					{
						Algorithm.CoarsePatternPoints(int.Parse(DAL.GetRawSegmentNumber(segment)), 2, out doublePoints);

						//MessageBox.Show("P1.X: " + doublePoints[0, 0] + " P1.Y: " + doublePoints[0, 1] + " P2.X: " + doublePoints[1, 0] + " P2.Y: " + doublePoints[1, 1] + " P3.X: " + doublePoints[2, 0] + " P3.Y: " + doublePoints[2, 1]);

						monitor.UpdatePatternVertices(doublePoints, false);

						Thread.Sleep(waitOnMonitor);

						if (cameraCoarseAlignUp != null)
						{
							cameraCoarseAlignUp.Dispose();
							cameraCoarseAlignUp = null;
						}

						cameraCoarseAlignUp = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

						//Bitmap img1 = new Bitmap(cameraCoarseAlignUp);

						//mainWindow.ShowUpDownOneBackgroundBitmap(img1);

						//img1.Dispose();

						upBkgAquired = true;
					}
					//Down image
					else if (coarseCounter == 1)
					{
						System.Diagnostics.Debug.WriteLine("Getting Down image");

						Algorithm.CoarsePatternPoints(int.Parse(DAL.GetRawSegmentNumber(segment)), 4, out doublePoints);

						//MessageBox.Show("P1.X: " + doublePoints[0, 0] + " P1.Y: " + doublePoints[0, 1] + " P2.X: " + doublePoints[1, 0] + " P2.Y: " + doublePoints[1, 1] + " P3.X: " + doublePoints[2, 0] + " P3.Y: " + doublePoints[2, 1]);

						monitor.UpdatePatternVertices(doublePoints, false);

						Thread.Sleep(waitOnMonitor);

						if (cameraCoarseAlignDown != null)
						{
							cameraCoarseAlignDown.Dispose();
							cameraCoarseAlignDown = null;
						}

						cameraCoarseAlignDown = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

						//Bitmap img1 = new Bitmap(cameraCoarseAlignDown);

						//mainWindow.ShowUpDownTwoBackgroundBitmap(img1);

						//img1.Dispose();

						downBkgAquired = true;
					}
					//Background image
					else if (coarseCounter == 2)
					{
						System.Diagnostics.Debug.WriteLine("Getting Black image");

						if (blackBGCounterUpDown > Int32.Parse(mainWindow.GetBlackBGNumber()))
						{
							monitor.BlackScreen();

							Thread.Sleep(waitOnMonitor);

							if (cameraCoarseAlignBlackTwo != null)
							{
								cameraCoarseAlignBlackTwo.Dispose();
								cameraCoarseAlignBlackTwo = null;
							}

							cameraCoarseAlignBlackTwo = cameraController.AquisitionVideo(AOIData[2]).Clone(new Rectangle(new Point(0, 0), new Size(AOIData[2], AOIData[3])), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

							blackBkgAquiredUpDown = true;
							blackBGCounterUpDown = 0;
						}
					}
				}
				//Offline mode
				else
				{
					System.Diagnostics.Debug.WriteLine("OFFLINE MODE UP&DOWN");

					//Up image
					if (coarseCounter == 0)
					{
						System.Diagnostics.Debug.WriteLine("Getting Up image");

						if (cameraCoarseAlignUp != null)
						{
							cameraCoarseAlignUp.Dispose();
							cameraCoarseAlignUp = null;
						}

						cameraCoarseAlignUp = DAL.GetCoarseUpBitmapBackgroundOfflineImage();

						//Bitmap img1 = new Bitmap(cameraCoarseAlignUp);

						//mainWindow.ShowUpDownOneBackgroundBitmap(img1);

						//img1.Dispose();

						upBkgAquired = true;
					}
					//Down image
					else if (coarseCounter == 1)
					{
						System.Diagnostics.Debug.WriteLine("Getting Down image");

						if (cameraCoarseAlignDown != null)
						{
							cameraCoarseAlignDown.Dispose();
							cameraCoarseAlignDown = null;
						}

						cameraCoarseAlignDown = DAL.GetCoarseDownBitmapBackgroundOfflineImage();

						//Bitmap img1 = new Bitmap(cameraCoarseAlignDown);

						//mainWindow.ShowUpDownTwoBackgroundBitmap(img1);

						//img1.Dispose();

						downBkgAquired = true;
					}
					//Background image
					else if (coarseCounter == 2)
					{
						System.Diagnostics.Debug.WriteLine("Getting black image");

						if (blackBGCounterUpDown > Int32.Parse(mainWindow.GetBlackBGNumber()))
						{
							if (cameraCoarseAlignBlackTwo != null)
							{
								cameraCoarseAlignBlackTwo.Dispose();
								cameraCoarseAlignBlackTwo = null;
							}

							cameraCoarseAlignBlackTwo = DAL.GetCoarseBlackBitmapBackgroundOfflineImage();

							blackBkgAquiredUpDown = true;
							blackBGCounterUpDown = 0;
						}
					}
				}

				int dir;

				//All image has been captured for the UpDownAlignment and the algorithm can now be run
				if (upBkgAquired && downBkgAquired && blackBkgAquiredUpDown)
				{
					//Algorithm.Coarse_algorithm(cameraCoarseAlignUp, cameraCoarseAlignDown, cameraCoarseAlignBlack, out dir);

					System.Diagnostics.Debug.WriteLine("Up down aquired, running algorithm");

					if (returnImg3 != null)
					{
						returnImg3.Dispose();
						returnImg3 = null;
					}

					if (returnImg4 != null)
					{
						returnImg4.Dispose();
						returnImg4 = null;
					}

					if (!offline)
					{
						Algorithm.Coarse_algorithm(cameraCoarseAlignUp, cameraCoarseAlignDown, cameraCoarseAlignBlackTwo, int.Parse(DAL.GetRawSegmentNumber(segment)), out dir, out returnImg3, out returnImg4);
						//if (dir == 2) { MessageBox.Show("Lost signal"); }
					}
					else
					{
						Algorithm.Coarse_algorithm(cameraCoarseAlignUp, cameraCoarseAlignDown, cameraCoarseAlignBlackTwo, 50, out dir, out returnImg3, out returnImg4);
					}

					//System.Diagnostics.Debug.WriteLine("LEFT&RIGHT DIRECTION: " + dir);

					mainWindow.ShowUpDownOneBackgroundBitmap(returnImg3);
					mainWindow.ShowUpDownTwoBackgroundBitmap(returnImg4);

					mainWindow.ShowUpDownDirection("" + dir);
				}

				blackBGCounterUpDown++;
			}

			if (coarseCounter >= 2)
			{
				coarseCounter = 0;
			}
			else
			{
				coarseCounter++;
			}

			System.Diagnostics.Debug.WriteLine("Coarse Alignment");

			leftRightUpDownLastCycle = leftRightCoarseAlignment;

			if (leftBkgAquired && rightBkgAquired && blackBkgAquired)
			{
				System.Diagnostics.Debug.WriteLine("Switching to Up down!");

				leftRightCoarseAlignment = false;
				upDownCoarseAlignment = true;

				leftBkgAquired = false;
				rightBkgAquired = false;
				blackBkgAquired = false;

				coarseCounter = 0;
			}
			else if (upBkgAquired && downBkgAquired && blackBkgAquiredUpDown)
			{
				System.Diagnostics.Debug.WriteLine("Switching to left right!");

				leftRightCoarseAlignment = true;
				upDownCoarseAlignment = false;

				upBkgAquired = false;
				downBkgAquired = false;
				blackBkgAquired = false;

				coarseCounter = 0;
			}

		}
	}
}

