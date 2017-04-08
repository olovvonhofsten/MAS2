using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MirrorAlignmentSystem
{
    /// <summary>
    ///  This class work as the Data Access Level and handle all communication with the database.
    /// </summary>
    public static class DAL
    {
        /// <summary>
        /// The connection string to the database
        /// </summary>

        static string SQLString = LocalSettings.SQLString();

        /// <summary>
        /// Gets a image from the database of the whole disc. This method is only used when the application is running in offline mode
        /// and in fine alignment
        /// </summary>
        /// <returns>
        /// Returns a bitmap with the overview image from the database.
        /// </returns>
        public static Bitmap GetOverviewImageOffline()
        {
            Bitmap bitmap = new Bitmap(10, 10);

            SqlConnection myConnection = new SqlConnection(SQLString);

            try 
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM OfflineData WHERE ImageType='3'", myConnection);
                byte[] img = (byte[])command.ExecuteScalar();
                MemoryStream str = new MemoryStream();
                str.Write(img, 0, img.Length);
                bitmap = new Bitmap(str);
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return bitmap;
        }

        /// <summary>
        /// Gets a image from the database of the segment when the monitor is showing a black image. This method is only used when the application is running in offline mode
        /// and fine alignment is choosen
        /// </summary>
        /// <returns>
        /// Returns a bitmap with the segment image from the database
        /// </returns>
        public static Bitmap GetBlackBackgroundOfflineCameraImage() 
        {
            Bitmap bitmap = new Bitmap(10, 10);

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM OfflineData WHERE ImageType='1'", myConnection);
                byte[] img = (byte[])command.ExecuteScalar();
                MemoryStream str = new MemoryStream();
                str.Write(img, 0, img.Length);
                bitmap = new Bitmap(str);
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return bitmap;
        }

        /// <summary>
        /// Gets a image from the database of the segment when the monitor is showing a bitmap image. This method is only used when the application is running in offline mode 
        /// and fine alignment is choosen
        /// </summary>
        /// <returns>
        /// Returns a bitmap with the segment image from the database
        /// </returns>
        public static Bitmap GetBitmapBackgroundOfflineCameraImage()
        {
            Bitmap bitmap = new Bitmap(10, 10);

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM OfflineData WHERE ImageType='2'", myConnection);
                byte[] img = (byte[])command.ExecuteScalar();
                MemoryStream str = new MemoryStream();
                str.Write(img, 0, img.Length);
                bitmap = new Bitmap(str);
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return bitmap;
        }

        /// <summary>
        /// Gets a image from the database of the segment when the monitor is showing the left bitmap image. This method is only used when the application is running in offline mode 
        /// and coarse alignment is choosen
        /// </summary>
        /// <returns>
        /// Returns a bitmap with the segment image from the database
        /// </returns>
        public static Bitmap GetCoarseLeftBitmapBackgroundOfflineImage()
        {
            Bitmap bitmap = new Bitmap(10, 10);

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM OfflineData WHERE ImageType='4'", myConnection);
                byte[] img = (byte[])command.ExecuteScalar();
                MemoryStream str = new MemoryStream();
                str.Write(img, 0, img.Length);
                bitmap = new Bitmap(str);
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return bitmap;
        }

        /// <summary>
        /// Gets a image from the database of the segment when the monitor is showing the right bitmap image. This method is only used when the application is running in offline mode 
        /// and coarse alignment is choosen
        /// </summary>
        /// <returns>
        /// Returns a bitmap with the segment image from the database
        /// </returns>
        public static Bitmap GetCoarseRightBitmapBackgroundOfflineImage()
        {
            Bitmap bitmap = new Bitmap(10, 10);

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM OfflineData WHERE ImageType='5'", myConnection);
                byte[] img = (byte[])command.ExecuteScalar();
                MemoryStream str = new MemoryStream();
                str.Write(img, 0, img.Length);
                bitmap = new Bitmap(str);
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return bitmap;
        }

        /// <summary>
        /// Updates the FPS and the exposure time
        /// </summary>   
        /// <param name="expTime">The new exposure time</param> 
        /// <param name="FPS">The new FPS</param> 
        public static void UpdateFPSExp(double expTime, double FPS)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                //MessageBox.Show(expTime + " " + FPS);
                System.Diagnostics.Debug.WriteLine(expTime + " " + FPS);
                SqlCommand command = new SqlCommand("UPDATE Settings SET ExposureTime=" + expTime + ", FPS=" + FPS + " WHERE Type='User'", myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a image from the database of the segment when the monitor is showing the up bitmap image. This method is only used when the application is running in offline mode 
        /// and coarse alignment is choosen
        /// </summary>
        /// <returns>
        /// Returns a bitmap with the segment image from the database
        /// </returns>
        public static Bitmap GetCoarseUpBitmapBackgroundOfflineImage()
        {
            Bitmap bitmap = new Bitmap(10, 10);

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM OfflineData WHERE ImageType='6'", myConnection);
                byte[] img = (byte[])command.ExecuteScalar();
                MemoryStream str = new MemoryStream();
                str.Write(img, 0, img.Length);
                bitmap = new Bitmap(str);
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return bitmap;
        }

        /// <summary>
        /// Gets a image from the database of the segment when the monitor is showing the down bitmap image. This method is only used when the application is running in offline mode 
        /// and coarse alignment is choosen
        /// </summary>
        /// <returns>
        /// Returns a bitmap with the segment image from the database
        /// </returns>
        public static Bitmap GetCoarseDownBitmapBackgroundOfflineImage()
        {
            Bitmap bitmap = new Bitmap(10, 10);

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM OfflineData WHERE ImageType='7'", myConnection);
                byte[] img = (byte[])command.ExecuteScalar();
                MemoryStream str = new MemoryStream();
                str.Write(img, 0, img.Length);
                bitmap = new Bitmap(str);
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return bitmap;
        }

        /// <summary>
        /// Gets a image from the database of the segment when the monitor is showing a black image. This method is only used when the application is running in offline mode 
        /// and coarse alignment is choosen
        /// </summary>
        /// <returns>
        /// Returns a bitmap with the segment image from the database
        /// </returns>
        public static Bitmap GetCoarseBlackBitmapBackgroundOfflineImage()
        {
            Bitmap bitmap = new Bitmap(10, 10);

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM OfflineData WHERE ImageType='8'", myConnection);
                byte[] img = (byte[])command.ExecuteScalar();
                MemoryStream str = new MemoryStream();
                str.Write(img, 0, img.Length);
                bitmap = new Bitmap(str);
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return bitmap;
        }

        /// <summary>
        /// Inserts an event into the database. This method is used everytime the user changes something or do anykind of action
        /// </summary>
        /// <param name="newValue">The new value of the control</param>
        /// <param name="oldValue">The old value of the control</param>
        /// <param name="username">The user that triggered the event</param>
        /// <param name="eventName">A short description of the event</param>
        /// <param name="control">The control which triggered the event</param>
        public static void InsertEvent(string newValue, string oldValue, string username, string eventName, string control) 
        {
            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Eventlog (NewValue, OldValue, Username, Control, Event) VALUES ('" + newValue + "','" + oldValue + "','" + username + "','" + control + "','" + eventName + "')", myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the Area Of Interest of a certain segment number from the database
        /// </summary>
        /// <param name="segment">The segment number</param>      
        /// <returns>
        /// Returns an array with 16 values representing data of the Area Of Interest
        /// </returns>
        public static int[] GetAOIData(string segment) 
        {
            SqlConnection myConnection = new SqlConnection(SQLString);
            SqlDataReader rdr = null;

            int[] returnValues = new int[16];

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT X,Y,Width,Height,X1,Y1,X2,Y2,X3,Y3,X4,Y4,CenterPointX,CenterPointY,PatternCenterPointX,PatternCenterPointY FROM AOIData WHERE Segment='" + segment + "'", myConnection);
                rdr = command.ExecuteReader();

                rdr.Read();

                if (rdr.HasRows)
                {
                    returnValues[0] = Convert.ToInt32(rdr["X"]);
                    returnValues[1] = Convert.ToInt32(rdr["Y"]);
                    returnValues[2] = Convert.ToInt32(rdr["Width"]);
                    returnValues[3] = Convert.ToInt32(rdr["Height"]);
                    returnValues[4] = Convert.ToInt32(rdr["X1"]);
                    returnValues[5] = Convert.ToInt32(rdr["Y1"]);
                    returnValues[6] = Convert.ToInt32(rdr["X2"]);
                    returnValues[7] = Convert.ToInt32(rdr["Y2"]);
                    returnValues[8] = Convert.ToInt32(rdr["X3"]);
                    returnValues[9] = Convert.ToInt32(rdr["Y3"]);
                    returnValues[10] = Convert.ToInt32(rdr["X4"]);
                    returnValues[11] = Convert.ToInt32(rdr["Y4"]);
                    returnValues[12] = Convert.ToInt32(rdr["CenterPointX"]);
                    returnValues[13] = Convert.ToInt32(rdr["CenterPointY"]);
                    returnValues[14] = Convert.ToInt32(rdr["PatternCenterPointX"]);
                    returnValues[15] = Convert.ToInt32(rdr["PatternCenterPointY"]);
                }
                else 
                {
                    returnValues[0] = 9999;
                    System.Diagnostics.Debug.WriteLine("TOM READER: " + returnValues[0]);
                    return returnValues;
                }

                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return returnValues;
        }

        /// <summary>
        /// Gets the Area Of Interest of a certain segment number from the database, using the raw segment number instead of the string name
        /// </summary>
        /// <param name="segment">The segment number</param>      
        /// <returns>
        /// Returns an array with 16 values representing data of the Area Of Interest
        /// </returns>
        public static int[] GetAOIDataRawSegment(string segment)
        {
            SqlConnection myConnection = new SqlConnection(SQLString);
            SqlDataReader rdr = null;

            int[] returnValues = new int[16];

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT X,Y,Width,Height,X1,Y1,X2,Y2,X3,Y3,X4,Y4,CenterPointX,CenterPointY,PatternCenterPointX,PatternCenterPointY FROM AOIData WHERE RawSegment='" + segment + "'", myConnection);
                rdr = command.ExecuteReader();

                rdr.Read();

                if (rdr.HasRows)
                {
                    returnValues[0] = Convert.ToInt32(rdr["X"]);
                    returnValues[1] = Convert.ToInt32(rdr["Y"]);
                    returnValues[2] = Convert.ToInt32(rdr["Width"]);
                    returnValues[3] = Convert.ToInt32(rdr["Height"]);
                    returnValues[4] = Convert.ToInt32(rdr["X1"]);
                    returnValues[5] = Convert.ToInt32(rdr["Y1"]);
                    returnValues[6] = Convert.ToInt32(rdr["X2"]);
                    returnValues[7] = Convert.ToInt32(rdr["Y2"]);
                    returnValues[8] = Convert.ToInt32(rdr["X3"]);
                    returnValues[9] = Convert.ToInt32(rdr["Y3"]);
                    returnValues[10] = Convert.ToInt32(rdr["X4"]);
                    returnValues[11] = Convert.ToInt32(rdr["Y4"]);
                    returnValues[12] = Convert.ToInt32(rdr["CenterPointX"]);
                    returnValues[13] = Convert.ToInt32(rdr["CenterPointY"]);
                    returnValues[14] = Convert.ToInt32(rdr["PatternCenterPointX"]);
                    returnValues[15] = Convert.ToInt32(rdr["PatternCenterPointY"]);
                }
                else
                {
                    returnValues[0] = 9999;
                    System.Diagnostics.Debug.WriteLine("TOM READER: " + returnValues[0]);
                    return returnValues;
                }

                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return returnValues;
        }

        /// <summary>
        /// Gets the raw segment number of a segment string number
        /// </summary>
        /// <param name="segment">The segment number</param>      
        /// <returns>
        /// Returns a string with the number of the raw segment
        /// </returns>
        public static string GetRawSegmentNumber(string segment)
        {
            string SQLQuery = "SELECT RawSegment FROM AOIData WHERE Segment='" + segment + "'";

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand(SQLQuery, myConnection);
                string rawSegment = (string)command.ExecuteScalar();
                myConnection.Close();

                return rawSegment;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);

                return "";
            }
        }

        /// <summary>
        /// Gets the EventLog from the database
        /// </summary>
        /// <param name="SQLQuery">The SQL string the method uses to get the correct data. This contains the filter applied in the form</param>      
        /// <returns>
        /// Returns an array with all the data within the limits stated by the user using the filter function
        /// </returns>
        public static DataTable GetEventLog(string SQLQuery) 
        {
            //System.Diagnostics.Debug.WriteLine("Startening to open eventlog: " + SQLQuery);

            DataTable returnTable = new DataTable();
            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLQuery, myConnection);
                dataAdapter.Fill(returnTable);

                myConnection.Close();               
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            System.Diagnostics.Debug.WriteLine("Done opening eventlog");

            return returnTable;
        }

        /// <summary>
        /// Get user data from the database
        /// </summary>
        /// <param name="SQLQuery">The SQL string the method uses to get the correct user data</param>      
        /// <returns>
        /// Returns an array with all the user data that exist in the database
        /// </returns>
        public static DataTable GetUserData(string SQLQuery)
        {
            DataTable returnTable = new DataTable();
            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLQuery, myConnection);
                dataAdapter.Fill(returnTable);

                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return returnTable;
        }


        /// <summary>
        /// Add a user to the database
        /// </summary>
        /// <param name="username">The username of the user to be added to the database</param>     
        /// <param name="password">The password of the user to be added to the database</param>  
        /// <returns>
        /// Returns a boolean to indicate if everything went okey or if something went wrong in the process of adding the user to the database
        /// </returns>
        public static bool AddUser(string username, string password)
        {
            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO UserData (Username, Password) VALUES ('" + username + "','" + password + "')", myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Gets the password for a certain user from the database
        /// </summary>
        /// <param name="username">The username of the user which password the application wants to be returned</param>      
        /// <returns>
        /// Returns a string with the password. If the username doesn't exist the method returns an empty string.
        /// </returns>
        public static string GetPasswordForUser(string username) 
        {
            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT Password FROM UserData WHERE Username='" + username + "'", myConnection);
                string password = (string)command.ExecuteScalar();
                System.Diagnostics.Debug.WriteLine("Lösenord: " + password);
                myConnection.Close();

                if (password == null)
                {
                    return "";
                }
                else 
                {
                    return password;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);

                return "User not found";
            }
        }

        /// <summary>
        /// Gets the user type for a certain user from the database
        /// </summary>
        /// <param name="username">The username of the user which user type should be returned</param>      
        /// <returns>
        /// Returns a string with a user type, for example Admin, Operator or User
        /// </returns>
        public static string GetUserTypeForUser(string username)
        {
            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT UserType FROM UserData WHERE Username='" + username + "'", myConnection);
                string password = (string)command.ExecuteScalar();
                myConnection.Close();

                return password;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);

                return "User not found";
            }
        }

        /// <summary>
        /// Changes the password for a certain user in the database
        /// </summary>
        /// <param name="username">The username of the user which password is to be changed</param> 
        /// <param name="currentPassword">The current password</param>  
        /// <param name="newPassword">The new password</param>  
        /// <returns>
        /// Returns a boolean if the change of password went okey or if something went wrong
        /// </returns>
        public static bool ChangePassword(string username, string currentPassword, string newPassword) 
        {
            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE UserData SET Password='" + newPassword + "' WHERE Password='" + currentPassword + "' AND Username='" + username + "'", myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Updates the user type for a certain user in the database
        /// </summary>
        /// <param name="username">The username of the user which user type is to be changed</param> 
        /// <param name="userType">The user type to be added to the user in the database</param>  
        /// <returns>
        /// Returns a boolean if the change of user type went okey or if something went wrong
        /// </returns>
        public static bool UpdateUserType(string username, string userType) 
        {
            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE UserData SET UserType='" + userType + "' WHERE Username='" + username + "'", myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Gets the settings for a certain type. For example factory settings, user settings. This can in the future be made into different profiles
        /// </summary>
        /// <param name="type">The type of settings to return, for example Factory settings or User settings</param>   
        /// <returns>
        /// Returns an array of strings with the settings that are changeable from the option window
        /// </returns>
        public static string[] GetSettings(string type) 
        {
            SqlConnection myConnection = new SqlConnection(SQLString);
            string[] returnData = new string[8];

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Settings WHERE Type='" + type + "'", myConnection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) 
                {
                    returnData[0] = "" + reader["ExposureTime"];
                    returnData[1] = "" + reader["Gain"];
                    returnData[2] = "" + reader["Threshold"];
                    returnData[3] = "" + reader["WaitTimeMonitor"];
                    returnData[4] = "" + reader["WaitTimeCycle"];
                    returnData[5] = "" + reader["FPS"];
                    returnData[6] = "" + reader["XOffset"];
                    returnData[7] = "" + reader["YOffset"];
                }

                reader.Close();
                myConnection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

            return returnData;
        }

        /// <summary>
        /// Updates the settings 
        /// </summary>  
        /// <param name="threshold">The threshold for the camera</param>  
        /// <param name="waitTimeMonitor">Wait time for the image to be transfered to the monitor</param>
        /// <param name="waitTimeCycle">Wait time every cycle</param>
        /// <param name="xOffset">the X offset for the area of interest</param>
        /// <param name="yOffset">the Y offset for the area of interest</param>
        /// <returns>
        /// Returns a boolean with a value depending on if the update of the settings went okey or if something went wrong
        /// </returns>
        public static bool UpdateSettings(string threshold, string waitTimeMonitor, string waitTimeCycle, string xOffset, string yOffset) 
        {
            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE Settings SET Threshold=" + threshold + ", WaitTimeMonitor=" + waitTimeMonitor + ", WaitTimeCycle=" + waitTimeCycle + ", xOffset=" + xOffset + ", yOffset=" + yOffset + " WHERE Type='User'", myConnection);
                //System.Diagnostics.Debug.WriteLine("");
                //System.Diagnostics.Debug.WriteLine("UPDATE Settings SET Threshold=" + threshold + ", WaitTimeMonitor=" + waitTimeMonitor + ", WaitTimeCycle=" + waitTimeCycle + ", xOffset=" + xOffset + ", yOffset=" + yOffset + " WHERE Type='User'");
                //System.Diagnostics.Debug.WriteLine("");
                command.ExecuteNonQuery();
                myConnection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Calls a Store Procedure in the database that stores the image with the data at the time of the capture.
        /// </summary>
        /// <param name="img">The image to be inserted into the database</param>   
        /// <param name="exposureTime">The exposure time of the camera when the image was captured</param> 
        /// <param name="AOI">The Area Of Interest at the time when the image was aquired</param>   
        /// <param name="cameraID">The ID of the camera capturing the image</param>  
        /// <param name="threshold">The threshold at the time for the image aquiring</param>  
        /// <param name="type">The type of image aquired</param>
        /// <param name="massCenter">The center of mass</param>
        public static void AddImage(Bitmap img, double exposureTime, string AOI, int cameraID, double threshold, int type, Point massCenter) 
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Close();

                byteArray = stream.ToArray();
            }

            SqlConnection myConnection = new SqlConnection(SQLString);
            //MessageBox.Show("massCenter x: " + massCenter.X + " y: " + massCenter.Y);
            try
            {
                myConnection.Open();
                SqlCommand cmd = new SqlCommand("SaveImage", myConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@img", SqlDbType.Image).Value = byteArray;
                cmd.Parameters.Add("@exposureTime", SqlDbType.Real).Value = exposureTime;
                cmd.Parameters.Add("@AOI", SqlDbType.NChar).Value = AOI;
                cmd.Parameters.Add("@cameraID", SqlDbType.Int).Value = cameraID;
                cmd.Parameters.Add("@threshold", SqlDbType.Real).Value = threshold;
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = type;
                cmd.Parameters.Add("@massCenterX", SqlDbType.Int).Value = massCenter.X;
                cmd.Parameters.Add("@massCenterY", SqlDbType.Int).Value = massCenter.Y;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the max height of all the area of interest
        /// </summary>   
        /// <returns>
        /// Returns an int with the max height
        /// </returns>
        public static int GetMaxHeight()
        {
            string SQLQuery = "SELECT MAX(Height) FROM AOIData";

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand(SQLQuery, myConnection);
                string maxheight = command.ExecuteScalar().ToString();
                int maxHeight = int.Parse(maxheight);
                //int maxHeight = (int)command.ExecuteScalar();
                myConnection.Close();

                return maxHeight;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Gets the max width of all the area of interest
        /// </summary>   
        /// <returns>
        /// Returns an int with the max width
        /// </returns>
public static int GetMaxWidth()
        {
            string SQLQuery = "SELECT MAX(Width) FROM AOIData";

            SqlConnection myConnection = new SqlConnection(SQLString);

            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand(SQLQuery, myConnection);
                System.Diagnostics.Debug.WriteLine(command.ExecuteScalar().GetType());
                string maxwidth = command.ExecuteScalar().ToString();
                //int maxWidth = (int)(command.ExecuteScalar());
                int maxWidth = int.Parse(maxwidth);
                myConnection.Close();

                return maxWidth;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);

                return 0;
            }
        }
    }
}
