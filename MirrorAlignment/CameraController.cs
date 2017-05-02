using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

using uEye;
using uEye.Defines;

namespace MirrorAlignmentSystem
{
    /// <summary>
    ///  This class handles everything that have to do with the camera.
    /// </summary>
    public class CameraController
    {
        /// <summary>
        /// The camera variable
        /// </summary>
        Camera camera;

		static Mutex cameraLock = new Mutex();

        /// <summary>
        /// Initialize the camera
        /// </summary>   
        /// <returns>
        /// Returns a boolean depending on if the initialize was successfull or not.
        /// </returns>
        public bool Init()
        {
			cameraLock.WaitOne();

            camera = new Camera();
			if (camera == null)
			{
				MessageBox.Show("Camera allocation failed");
				cameraLock.ReleaseMutex();
				return false;
			}

            //Gamma sätts till 100 = 1.0
            camera.Gamma.Software.Set(100);

            //Turns off Subsampling of the image
            camera.Size.Subsampling.Set(SubsamplingMode.Disable);

            //Sets the Pixel format
            camera.PixelFormat.Set(ColorMode.Mono8);

            //Sets the framerate of the camera
            camera.Timing.Framerate.Set(60);

            //Sets the exposure time of the camera
            camera.Timing.Exposure.Set(5.0);

            //Sets the gain of the camera
            camera.AutoFeatures.Software.Gain.SetMax(0);

            //Set what kind of trigger the camera is using, in this case a software trigger
            camera.Trigger.Set(TriggerMode.Software);

            camera.Display.Mode.Set(uEye.Defines.DisplayMode.DiB);

            //displayHandle = DisplayWindow.Handle;

            //uEye.Defines.Status statusRet = 0;

            //Initialize the camera and return false if not successful
            var statusRet = camera.Init();
            if (statusRet != Status.Success)
            {
				cameraLock.ReleaseMutex();
                MessageBox.Show("Camera initializing failed");
                return false;
            }

            statusRet = camera.Memory.Allocate(DAL.GetMaxWidth(), DAL.GetMaxHeight());
            if (statusRet != uEye.Defines.Status.Success)
            {
				cameraLock.ReleaseMutex();
                MessageBox.Show("Allocate Memory failed");
                return false;
            }

            //Allocate memory for the camera and return false if not successful
            statusRet = camera.Memory.Allocate(1936, 1216);
            if (statusRet != uEye.Defines.Status.Success)
            {
				cameraLock.ReleaseMutex();
                MessageBox.Show("Allocate Memory failed");
                return false;
            }

			cameraLock.ReleaseMutex();
            return true;
        }

        /// <summary>
        /// Aquiring an image from the camera
        /// </summary>   
        /// <returns>
        /// Returns a bitmap aquired from the camera via the memory allocated in the initializion
        /// </returns>
        public Bitmap Aquisition(int width) 
        {
			cameraLock.WaitOne();

            Bitmap returnBitmap;
			
			if (width == 1936)
            {
                camera.Memory.SetActive(2);
            }
            else 
            {
                camera.Memory.SetActive(1);
            }

            camera.Acquisition.Freeze(uEye.Defines.DeviceParameter.Wait);

            Int32 s32MemID;

            camera.Memory.GetActive(out s32MemID);

            //System.Diagnostics.Debug.WriteLine("");
            //System.Diagnostics.Debug.WriteLine("s32MemID: " + s32MemID);
            //System.Diagnostics.Debug.WriteLine("");

            camera.Memory.ToBitmap(s32MemID, out returnBitmap);

			cameraLock.ReleaseMutex();
            return returnBitmap;
        }

        /// <summary>
        /// Aquiring an image from the camera
        /// </summary>   
        /// <returns>
        /// Returns a bitmap aquired from the camera via the memory allocated in the initializion
        /// </returns>
        public Bitmap AquisitionVideo(int width)
        {
			cameraLock.WaitOne();

			Bitmap returnBitmap;

            if (width == 1936)
            {
                camera.Memory.SetActive(2);
            }
            else
            {
                camera.Memory.SetActive(1);
            }

            camera.Acquisition.Capture(uEye.Defines.DeviceParameter.Wait);

            Int32 s32MemID;

            camera.Memory.GetActive(out s32MemID);

            //System.Diagnostics.Debug.WriteLine("");
            //System.Diagnostics.Debug.WriteLine("s32MemID: " + s32MemID);
            //System.Diagnostics.Debug.WriteLine("");

            camera.Memory.ToBitmap(s32MemID, out returnBitmap);

			cameraLock.ReleaseMutex();
            return returnBitmap;
        }

        /// <summary>
        /// Shutdowns the camera and free the memory
        /// </summary>   
        public void Shutdown()
        {
			cameraLock.WaitOne();

			camera.Exit();
            camera = null;

			cameraLock.ReleaseMutex();
        }

        /// <summary>
        /// Sets the camera gamma
        /// </summary>
        /// <param name="newGamma">The new gamma value</param>
        public void SetGamma(int newGamma)
        {
			cameraLock.WaitOne();
			camera.Gamma.Software.Set(newGamma);
			cameraLock.ReleaseMutex();
        }

        /// <summary>
        /// Aquiring the current gamma setting for the camera
        /// </summary>   
        /// <returns>
        /// Returns an integer with the current gamma value of the camera
        /// </returns>
        public int GetGamma()
        {
			cameraLock.WaitOne();

			int returnValue;

            camera.Gamma.Software.Get(out returnValue);

			cameraLock.ReleaseMutex();
            return returnValue;
        }

        /// <summary>
        /// Sets the camera subsampling mode
        /// </summary>
        /// <param name="mode">The new subsampling mode</param>
        public void SetSubsamplingMode(uEye.Defines.SubsamplingMode mode)
        {
			cameraLock.WaitOne();

			camera.Size.Subsampling.Set(mode);

			cameraLock.ReleaseMutex();
        }

        /// <summary>
        /// Aquiring the current subsampling mode for the camera
        /// </summary>   
        /// <returns>
        /// Returns an uEye.Defines.SubsamplingMode with the current subsampling mode value of the camera
        /// </returns>
        public uEye.Defines.SubsamplingMode GetSubsamplingMode()
        {
			cameraLock.WaitOne();

			uEye.Defines.SubsamplingMode returnValue;

            camera.Size.Subsampling.Get(out returnValue);

			cameraLock.ReleaseMutex();
            return returnValue;
        }

        /// <summary>
        /// Sets the camera Area Of Interest
        /// </summary>
        /// <param name="width">Width of the new Area Of Interest</param>
        /// <param name="height">Height of the new Area Of Interest</param>
        /// <param name="x">X-point of the new Area Of Interest</param>
        /// <param name="y">Y-point of the new Area Of Interest</param>
        public void SetAOI(int width, int height, int x, int y)
        {
			cameraLock.WaitOne();

			if (camera != null)
                camera.Size.AOI.Set(x, y, width, height);

			cameraLock.ReleaseMutex();
        }

        /// <summary>
        /// Aquiring the current Area Of Interest setting for the camera
        /// </summary>   
        /// <returns>
        /// Returns an rectangle with the current Area Of Interest value of the camera
        /// </returns>
        public System.Drawing.Rectangle GetAOI()
        {
			cameraLock.WaitOne();

			System.Drawing.Rectangle returnValue;

            camera.Size.AOI.Get(out returnValue);

			cameraLock.ReleaseMutex();
            return returnValue;
        }

        /// <summary>
        /// Sets the camera pixel format
        /// </summary>
        /// <param name="mode">The new pixel format</param>
        public void SetPixelFormat(uEye.Defines.ColorMode mode) 
        {
			cameraLock.WaitOne();

			camera.PixelFormat.Set(mode);

			cameraLock.ReleaseMutex();
        }

        /// <summary>
        /// Aquiring the current pixel format for the camera
        /// </summary>   
        /// <returns>
        /// Returns an uEye.Defines.ColorMode with the current pixel format value of the camera
        /// </returns>
        public uEye.Defines.ColorMode GetPixelFormat() 
        {
			cameraLock.WaitOne();

			uEye.Defines.ColorMode returnValue;

            camera.PixelFormat.Get(out returnValue);

			cameraLock.ReleaseMutex();
            return returnValue;
        }

        /// <summary>
        /// Sets the camera exposure time
        /// </summary>
        /// <param name="time">The new exposure time</param>
        public void SetExposureTime(double time)
        {
			cameraLock.WaitOne();

			camera.Timing.Exposure.Set(time);

			cameraLock.ReleaseMutex();
        }

        /// <summary>
        /// Aquiring the current exposure time setting for the camera
        /// </summary>   
        /// <returns>
        /// Returns a double with the current exposure time value of the camera
        /// </returns>
        public double GetExposureTime()
        {
			cameraLock.WaitOne();

			double exposureTime;

			if (camera == null)
			{
				cameraLock.ReleaseMutex();
				return 0.0;
			}

            camera.Timing.Exposure.Get(out exposureTime);

			cameraLock.ReleaseMutex();
            return exposureTime;
        }

        /// <summary>
        /// Sets the framerate
        /// </summary>
        /// <param name="rate">The new framerate</param>
        public void SetFramerate(double rate)
        {
			cameraLock.WaitOne();

			camera.Timing.Framerate.Set(rate);

			cameraLock.ReleaseMutex();
        }

        /// <summary>
        /// Aquiring the current framerate setting for the camera
        /// </summary>   
        /// <returns>
        /// Returns a double with the current framerate value of the camera
        /// </returns>
        public double GetFramerate()
        {
			cameraLock.WaitOne();

			double framerate;

			if (camera == null)
			{
				cameraLock.ReleaseMutex();
				return 0.0;
			}

            camera.Timing.Framerate.Get(out framerate);

			cameraLock.ReleaseMutex();
            return framerate;
        }

        /// <summary>
        /// Aquiring the current range of the framerate
        /// </summary>   
        /// <returns>
        /// return three double values with max, min and increament of the framerate range
        /// </returns>
        public void GetFrameRateRange( out double f64MinFramerate, out double f64MaxFramerate, out double f64IncFramerate)
        {
			cameraLock.WaitOne();

			if (camera == null) { f64MinFramerate = f64MaxFramerate = f64IncFramerate = 0.0; cameraLock.ReleaseMutex(); return; }
            camera.Timing.Framerate.GetFrameRateRange(out f64MinFramerate, out f64MaxFramerate, out f64IncFramerate);
			cameraLock.ReleaseMutex();
        }

        /// <summary>
        /// Aquiring the current range of the exposure time
        /// </summary>   
        /// <returns>
        /// return three double values with max, min and increament of the exposure time range
        /// </returns>
        public void GetExposureTimeRange(out double f64Min, out double f64Max, out double f64Inc)
        {
			cameraLock.WaitOne();

			if (camera == null) { f64Min = f64Max = f64Inc = 0.0; cameraLock.ReleaseMutex(); return; }
            camera.Timing.Exposure.GetRange(out f64Min, out f64Max, out f64Inc);
			cameraLock.ReleaseMutex();
        }
    }
}

