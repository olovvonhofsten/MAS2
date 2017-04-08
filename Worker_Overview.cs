
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

		partial void ExecuteOverview() 
		{

			monitor.SetCalibrateOrNot(false);

			//Set the flag to make sure that if the user switches to calibrate mode the AOI and exposure time is reseted
			calibrateFirstCycle = true;
			System.Diagnostics.Debug.WriteLine("Segment and SegmentLastCycle: " + segment + " " + segmentLastCycle);
			//Check if a new segment have been choosen. If that is the case a new overview image is needed.
			if (String.Compare(segment, segmentLastCycle) != 0 || (offline != offlineOnlineLastCycle) || manualOverviewUpdate || mainWindow.GetLiveMode() == "Checked")
			{
				stopWatchOverviewImage.Reset();
				stopWatchOverviewImage.Start();

				segment = mainWindow.GetSegmentNumber().Trim();

				//Gets the Area Of Interest for the segment(currently choosen in the GUI) from the database
				AOIData = DAL.GetAOIData(segment);

				if (AOIData[0] == 9999)
				{
					MessageBox.Show("The segment does not exist, please change to a valid one!");
					segNotExist = true;
				}
				else
				{
					segNotExist = false;
				}

				if (!segNotExist)
				{

					//validates if the system is online or is running in offline mode
					if (!offline)
					{
                        monitor.BlackScreen();
                        Thread.Sleep(waitOnMonitor);
						//System.Diagnostics.Debug.WriteLine("The program is running in online mode");
						System.Diagnostics.Debug.WriteLine("Aquiring new overview image");
						cameraController.SetAOI(1936, 1216, 0, 0);
						cameraController.SetExposureTime(exposureRate);

						if (cameraInitialized)
						{
							cameraOverview = new Bitmap(cameraController.Aquisition(1936));
						}
					}

                    // Draw segments. Red = not ok, Yellow = current, Green = ok
                    //The application is running in offline mode
					else
					{
						System.Diagnostics.Debug.WriteLine("The program is running in offline mode , picking up overview image");

						//Getting the offline image for the overview picturebox from the database
						cameraOverview = DAL.GetOverviewImageOffline();
					}

					//This part paints the Area Of Interest above the overview image
                    Bitmap cameraOverview2 = Algorithm.drawSegmentTypes(statusOfSegments, cameraOverview);
                    using (Bitmap tempBitmap = new Bitmap(cameraOverview.Width, cameraOverview.Height))
					{
						using (Graphics g = Graphics.FromImage(tempBitmap))
						{
							Pen redPen = new Pen(Color.Red, 10);
							Pen goldPen = new Pen(Color.Yellow, 5);

							g.DrawImage(cameraOverview2, 0, 0);

							//MessageBox.Show("startX: " + (AOIData[0] + xOffset) + " startY: " + (AOIData[1] + yOffset) + " stopX: " + ((AOIData[0] + AOIData[2]) + xOffset) + " stopY: " + (AOIData[1] + yOffset) + " xOffset: " + xOffset + " yOffset: " + yOffset + " ImageWidth: " + cameraOverview.Width + " Cameraheight: " + cameraOverview.Height);
							//Top left corner
							//g.DrawLine(redPen, (AOIData[0] + xOffset), (AOIData[1] + yOffset), ((AOIData[0] + AOIData[2]) + xOffset), (AOIData[1] + yOffset));
							//g.DrawLine(redPen, 0, 0, cameraOverview.Width, cameraOverview.Height);
							//g.DrawLine(redPen, 1136, 1464, cameraOverview.Width, cameraOverview.Height);
							//Top right corner
							//g.DrawLine(redPen, ((AOIData[0] + AOIData[2]) + xOffset), (AOIData[1] + yOffset), ((AOIData[0] + AOIData[2]) + xOffset), ((AOIData[1] + AOIData[3]) + yOffset));

							//Bottom right corner
							//g.DrawLine(redPen, ((AOIData[0] + AOIData[2]) + xOffset), ((AOIData[1] + AOIData[3]) + yOffset), (AOIData[0] + xOffset), ((AOIData[1] + AOIData[3]) + yOffset));

							//Bottom left corner
							//g.DrawLine(redPen, (AOIData[0] + xOffset), ((AOIData[1] + AOIData[3]) + yOffset), (AOIData[0] + xOffset), (AOIData[1] + yOffset));

							//Top left corner
							g.DrawLine(goldPen, (AOIData[4] + xOffset), (AOIData[5] + yOffset), (AOIData[6] + xOffset), (AOIData[7] + yOffset));

							//Top right corner
							g.DrawLine(goldPen, (AOIData[6] + xOffset), (AOIData[7] + yOffset), (AOIData[8] + xOffset), (AOIData[9] + yOffset));

							//Bottom right corner
							g.DrawLine(goldPen, (AOIData[8] + xOffset), (AOIData[9] + yOffset), (AOIData[10] + xOffset), (AOIData[11] + yOffset));

							//Bottom left corner
							g.DrawLine(goldPen, (AOIData[10] + xOffset), (AOIData[11] + yOffset), (AOIData[4] + xOffset), (AOIData[5] + yOffset));

							g.Dispose();
						}

						if (cameraOverview != null)
						{
							cameraOverview.Dispose();
						}

						cameraOverview = new Bitmap(tempBitmap);

						tempBitmap.Dispose();
					}

					//Displays the overview image in the correct picturebox
					mainWindow.ShowOverviewBitmap(cameraOverview);

					stopWatchOverviewImage.Stop();

					System.Diagnostics.Debug.WriteLine("Overview image time elapsed: " + stopWatchOverviewImage.ElapsedMilliseconds + "ms");
				}
			}


		}


	}
}


