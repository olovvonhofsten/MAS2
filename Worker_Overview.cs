
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
                    System.Diagnostics.Debug.WriteLine("Overview, 0911 status: " + statusOfSegments[8, 0]);
                    cameraOverview = Algorithm.drawSegmentTypes(statusOfSegments, cameraOverview);
                    cameraOverview = Algorithm.drawCurrentSegment(segment, cameraOverview);

					//Displays the overview image in the correct picturebox
					mainWindow.ShowOverviewBitmap(cameraOverview);

					stopWatchOverviewImage.Stop();

					System.Diagnostics.Debug.WriteLine("Overview image time elapsed: " + stopWatchOverviewImage.ElapsedMilliseconds + "ms");
				}
			}


		}


	}
}


