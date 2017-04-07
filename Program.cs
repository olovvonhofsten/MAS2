using System;
using System.Threading;
using System.Windows.Forms;

namespace MirrorAlignmentSystem
{
    static class Program
    {
        public static CameraController CameraController
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public static MainWindow MainWindow
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public static Worker Worker
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			CameraController cameraController = new CameraController();
            MainWindow mainWindow = new MainWindow(cameraController);

            //Creates the worker
            Worker worker = new Worker(mainWindow, cameraController);

            Thread oThread = new Thread(new ThreadStart(worker.Run));


            oThread.Start();

            Application.Run(mainWindow);
        }
    }
}
