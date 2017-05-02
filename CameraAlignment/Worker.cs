using System.Drawing;
using System.Threading;
namespace CameraAlignment
{
	public class Worker
	{
		/// <summary>
		/// The main GUI, the worker rules over the GUI
		/// </summary>
		CameraAlignmentWindow mainWindow;

		string[] _cameraSettings;
		double _exposureRate;

		/// <summary>
		/// The class that controls the camera of this application
		/// </summary>
		MirrorAlignmentSystem.CameraController _cameraController;
		private double _framerate;
		private double _threshold;
		private int _waitOnMonitor;
		private int _waitOnCycle;
		private int _xOffset;
		private int _yOffset;
		private bool _cameraInitialized;

		private Bitmap cameraOverview = new Bitmap(1960, 1216, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
		

		/// <summary>
		///  This class constructor
		/// </summary>
		public Worker(CameraAlignmentWindow mainWindowInput, MirrorAlignmentSystem.CameraController inputCamera)
		{
			mainWindow = mainWindowInput;
			_cameraController = inputCamera;

		}

		/// <summary>
		///  Entry point for the thread
		/// </summary>
		public void Run()
		{
			// The camera settings the applications is using, taken from the Database
			_cameraSettings = MirrorAlignmentSystem.DAL.GetSettings("User");

			// The exposure rate, taken from the database
			_exposureRate = double.Parse(_cameraSettings[0]);
			_exposureRate = 1.0;
			//mainWindow.SetExposureSlider(exposureRate);

			_framerate = double.Parse(_cameraSettings[5]);

			//Threshold to be used in the algorithm
			_threshold = double.Parse(_cameraSettings[2]);

			// The wait time in ms between the image is sent to the monitor and when the camera starts to capture the image
			_waitOnMonitor = int.Parse(_cameraSettings[3]);

			// The wait time between the end of one cycle and the start of the next
			_waitOnCycle = int.Parse(_cameraSettings[4]);

			_xOffset = int.Parse(_cameraSettings[6]);
			_yOffset = int.Parse(_cameraSettings[7]);

			//InitializeBitmaps();

			//massCenter = new Point(0, 0);

			//Creates a class that control the TV. This is the class that uses DirectX
			//monitor = new MonitorHandler();

			//Different stopwatches to time different parts of the application
			//InitializeStopwatches();

			//stopWatchFPS.Start();

			//  Camera initialized or not
			_cameraInitialized = _cameraController.Init();

			_cameraController.SetExposureTime(_exposureRate);
			_cameraController.SetFramerate(_framerate);

			//Creates a thread for the TV so that it updates as fast as possible
			//Thread oThread = new Thread(new ThreadStart(monitor.Run));

			//Starts the thread for the TV

			//oThread.SetApartmentState(ApartmentState.STA);
			//oThread.Start();

			//if (manualMeasureMode)
			//{
			//	RunManualMeasureMode();
			//	return;
			//}
			//else
			//{
			//	MessageBox.Show("This will align the system");
			//	Thread.Sleep(_waitOnCycle);

			//	// Reset variables
			//	manualMeasureMode = true;
			//	bitmapBkgAquired = false;
			//	blackBkgAquired = false;
			//	blackBkgAquiredUpDown = false;
			//	leftBkgAquired = false;
			//	rightBkgAquired = false;
			//	upBkgAquired = false;
			//	downBkgAquired = false;

			//	blackBGCounter = 0;
			//	blackBGCounterLeftRight = 0;
			//	blackBGCounterUpDown = 0;
			//	coarseCounter = 0;

			//	segment = mainWindow.GetSegmentNumber().Trim();
			//	//Gets the Area Of Interest for the segment(currently choosen in the GUI) from the database
			//	AOIData = DAL.GetAOIData(segment);
			//	AOIData[1] = sensorHeight - AOIData[1];
			//	AOIData[5] = sensorHeight - AOIData[5];
			//	AOIData[7] = sensorHeight - AOIData[7];
			//	AOIData[9] = sensorHeight - AOIData[9];
			//	AOIData[11] = sensorHeight - AOIData[11];


			//	//Start fine alignment
			//	//radial
			//	//Tangentiell
			//}
		}


	}
}
