
namespace MirrorAlignmentSystem
{
    /// <summary>
    /// This class handles the OPC and the communication with the PLC
    /// </summary>
    public static class OPC
    {
        /*
        /// <summary>
        /// The server managment
        /// </summary>
        /// 
        static Kepware.ClientAce.OpcDaClient.DaServerMgt DAserver = new Kepware.ClientAce.OpcDaClient.DaServerMgt();
        static Kepware.ClientAce.OpcDaClient.ConnectInfo connectInfo = new Kepware.ClientAce.OpcDaClient.ConnectInfo();

        static string motor1PosOPC, onOffValue, motor1Alarm, motor1SPReached, motor2PosOPC, onOff2Value, motor2Alarm, motor2SPReached, motor1NewPos, motor2NewPos;
        static string motor1Acknowledge, motor2Acknowledge, motor1MDIActive, motor2MDIActive, motor1UpdatePos, motor2UpdatePos, motor1NewSpeed, motor2NewSpeed;
        static ItemIdentifier[] itemIdentifiers = new ItemIdentifier[18];
        static ItemValue[] itemValues = new ItemValue[18];
        static int clientSubscriptionHandle;
        static int activeServerSubscriptionHandle;

        /// <summary>
        /// Initiate the OPC server 
        /// </summary>
        /// <returns>
        /// Returns a boolean indicating if the OPC intiation have been successful
        /// </returns>
        public static bool Init() 
        {
            // Define the server connection URL
            string url = "opcda://localhost/Kepware.KEPServerEX.V5/";

            //Initialize the connect info object data
            ConnectInfo connectInfo = new ConnectInfo();
            connectInfo.LocalId = "en";
            connectInfo.KeepAliveTime = 1000;
            connectInfo.RetryAfterConnectionError = true;
            connectInfo.RetryInitialConnection = false;
            connectInfo.ClientName = "OPC Client";

            bool connectFailed = false;

            //define a client handle for the connection
            int clientHandle = 1;

            //Try to connect with the API connect method:
            try
            {
                DAserver.Connect(url, clientHandle, ref connectInfo, out connectFailed);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Connect exception. Reason: " + ex.Message);
                // Make sure following code knows connection failed:
                connectFailed = true;
            }

            //Subscribe to events
            SubscribeToOPCDAServerEvents();

            SubscribeToData();
            DAserver.SubscriptionModify(activeServerSubscriptionHandle, true);

            return true;
        }

        /// <summary>
        /// Method to retrive motor 1's position
        /// </summary>
        /// <returns>
        /// Returns a string with the current position of the motor
        /// </returns>
        public static string getMotor1Pos() 
        {
            return motor1PosOPC;
        }

        /// <summary>
        /// Method to retrive motor 1's On/Off1 Value
        /// </summary>
        /// <returns>
        /// Returns a string with the current value of On/Off1
        /// </returns>
        public static string getOn1Off()
        {
            return onOffValue;
        }

        /// <summary>
        /// Method to retrive if motor 1 has reached it's setpoint
        /// </summary>
        /// <returns>
        /// Returns a string with a 1/0 depending on if the motor has reached the Setpoint
        /// </returns>
        public static string getMotor1SPReached()
        {
            return motor1SPReached;
        }

        /// <summary>
        /// Method to retrive alarms or fault that motor 1 have
        /// </summary>
        /// <returns>
        /// Returns a string with the fault number 
        /// </returns>
        public static string getMotor1Alarm()
        {
            return motor1Alarm;
        }

        /// <summary>
        /// Method to retrive motor 1's position
        /// </summary>
        /// <returns>
        /// Returns a string with the current position of the motor
        /// </returns>
        public static string getMotor2Pos()
        {
            return motor2PosOPC;
        }

        /// <summary>
        /// Method to retrive motor 1's On/Off1 Value
        /// </summary>
        /// <returns>
        /// Returns a string with the current value of On/Off1
        /// </returns>
        public static string getOn2Off()
        {
            return onOff2Value;
        }

        /// <summary>
        /// Method to retrive if motor 2 has reached it's setpoint
        /// </summary>
        /// <returns>
        /// Returns a string with a 1/0 depending on if the motor has reached the Setpoint
        /// </returns>
        public static string getMotor2SPReached()
        {
            return motor2SPReached;
        }

        /// <summary>
        /// Method to retrive alarms or fault that motor 2 have
        /// </summary>
        /// <returns>
        /// Returns a string with the fault number 
        /// </returns>
        public static string getMotor2Alarm()
        {
            return motor2Alarm;
        }

        /// <summary>
        /// Method to retrive motor 1's MDIActive Value
        /// </summary>
        /// <returns>
        /// Returns a string with the current value of MDIActive
        /// </returns>
        public static string getMotor1MDIActive()
        {
            return motor1MDIActive;
        }

        /// <summary>
        /// Method to retrive motor 2's MDIActive Value
        /// </summary>
        /// <returns>
        /// Returns a string with the current value of MDIActive
        /// </returns>
        public static string getMotor2MDIActive()
        {
            return motor2MDIActive;
        }

        /// <summary>
        /// Subscribe to the server events
        /// </summary>
        private static void SubscribeToOPCDAServerEvents()
         {
             //DAserver.WriteCompleted += new DaServerMgt.WriteCompletedEventHandler(DAserver_WriteCompleted);
             //DAserver.ReadCompleted += new DaServerMgt.ReadCompletedEventHandler(DAserver_ReadCompleted);
             DAserver.DataChanged += new DaServerMgt.DataChangedEventHandler(DAserver_DataChanged);
             DAserver.ServerStateChanged += new DaServerMgt.ServerStateChangedEventHandler(DAserver_ServerStateChanged);
         }

         /// <summary>
         /// This is an event run when the write of the values to the OPC server have been completed
         /// </summary>
         /// <param name="transaction">Transaction</param>
         /// <param name="noErrors">No errors reported</param>   
         /// <param name="itemResults">The result</param>   
         public static void DAserver_WriteCompleted(int transaction, bool noErrors, ItemResultCallback[] itemResults)
         { 
         }

         /// <summary>
         /// This is an event run when the read of the values to the OPC server have been completed
         /// </summary>
         /// <param name="transaction">Transaction</param>
         /// <param name="noErrors">No errors reported</param>   
         /// <param name="quality">Quality of the tags</param> 
         /// <param name="itemValues">The result</param>   
         public static void DAserver_ReadCompleted(int transaction, bool quality, bool noErrors, ItemValueCallback[] itemValues)
         {
             motor1PosOPC = "" + itemValues[1].Value;
             motor2PosOPC = "" + itemValues[5].Value;
         }

         /// <summary>
         /// This is an event that is ran by the program when data has changed in the PLC
         /// </summary>
         /// <param name="clientSubscription">The subscription</param>
         /// <param name="allQualitiesGood">All tags have good quality</param>   
         /// <param name="noErrors">No errors reported</param>   
         /// <param name="itemValues">Values of the items</param>  
         public static void DAserver_DataChanged(int clientSubscription, bool allQualitiesGood, bool noErrors, ItemValueCallback[] itemValues)
         {
             object[] DCevHndlrArray = new object[4];
             DCevHndlrArray[0] = clientSubscription;
             DCevHndlrArray[1] = allQualitiesGood;
             DCevHndlrArray[2] = noErrors;
             DCevHndlrArray[3] = itemValues;

             //MessageBox.Show("DataChanged");

             DataChanged(clientSubscription, allQualitiesGood, noErrors, itemValues);

             //BeginInvoke(new DaServerMgt.DataChangedEventHandler(DataChanged), DCevHndlrArray);
         }

        /// <summary>
        /// This is an method that is triggered when the state of the server changes
        /// </summary>
        /// <param name="clientHandle">The handle of the client</param>
        /// <param name="state">State of the server</param>   
        public static void DAserver_ServerStateChanged(int clientHandle, ServerState state)
        {
            object[] SSCevHndlrArray = new object[2];
            SSCevHndlrArray[0] = clientHandle;
            SSCevHndlrArray[1] = state;
        }

        /// <summary>
        /// This is an event that is ran by the program when data has changed in the PLC
        /// </summary>
        /// <param name="clientSubscription">The subscription</param>
        /// <param name="allQualitiesGood">All tags have good quality</param>   
        /// <param name="noErrors">No errors reported</param>   
        /// <param name="itemValues">Values of the items</param> 
        public static void DataChanged(int clientSubscription, bool allQualitiesGood, bool noErrors, ItemValueCallback[] itemValues)
        {
             try
             {
                foreach(ItemValueCallback itemValue in itemValues)
                {
                    int itemIndex = (int)itemValue.ClientHandle;

                    //MessageBox.Show("Data changed in the PLC");

                    switch (itemIndex)
                    {
                    case 0:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor1PosOPC = "" + itemValue.Value;
                        }
                        break;
                    case 1:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            onOffValue = "" + itemValue.Value;
                        }
                        break;
                    case 2:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor1Alarm = "" + itemValue.Value;
                        }
                        break;
                    case 3:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor1SPReached = "" + itemValue.Value;
                        }
                        break;
                    case 4:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor2PosOPC = "" + itemValue.Value;
                        }
                        break;
                    case 5:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            onOff2Value = "" + itemValue.Value;
                        }
                        break;
                    case 6:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor2Alarm = "" + itemValue.Value;
                        }
                        break;
                    case 7:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor2SPReached = "" + itemValue.Value;
                        }
                        break;
                    case 8:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor1NewPos = "" + itemValue.Value;
                        }
                        break;
                    case 9:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor2NewPos = "" + itemValue.Value;
                        }
                        break;
                    case 10:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor1Acknowledge = "" + itemValue.Value;
                        }
                        break;
                    case 11:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor2Acknowledge = "" + itemValue.Value;
                        }
                        break;
                    case 12:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor1MDIActive = "" + itemValue.Value;
                        }
                        break;
                    case 13:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor2MDIActive = "" + itemValue.Value;
                        }
                        break;
                    case 14:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor1UpdatePos = "" + itemValue.Value;
                        }
                        break;
                    case 15:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor2UpdatePos = "" + itemValue.Value;
                        }
                        break;

                    case 16:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor1NewSpeed = "" + itemValue.Value;
                        }
                        break;

                    case 17:
                        if (itemValue.Value == null)
                        {
                        }
                        else
                        {
                            motor2NewSpeed = "" + itemValue.Value;
                        }
                        break;
                    }
                }
             }
             catch (Exception ex)
             {
                MessageBox.Show("Handled Data Changed exception. Reason: " + ex.Message);
             }
        }

        /// <summary>
        /// This is an method that is triggered when the state of the server changes
        /// </summary>
        /// <param name="clientHandle">The handle of the client</param>
        /// <param name="state">State of the server</param>   
        public static void ServerStateChanged(int clientHandle, ServerState state)
        {
             try
             {
                 //~~ Process the callback information here.
                 switch (state)
                 {
                 case ServerState.ERRORSHUTDOWN:
                 //Unsubscribe();
                 //DisconnectOPCServer();
                    MessageBox.Show("The server is shutting down. The client has automatically disconnected.");
                 break;
                 case ServerState.ERRORWATCHDOG:
                    MessageBox.Show("Server connection has been lost. Client will keep attempting to reconnect.");
                 break;
                 case ServerState.CONNECTED:
                     MessageBox.Show("ServerStateChanged, connected");
                 break;
                 case ServerState.DISCONNECTED:
                    MessageBox.Show("ServerStateChanged, disconnected");
                 break;
                 default:
                    MessageBox.Show("ServerStateChanged, undefined state found.");
                 break;
                 }
             }
             catch (Exception ex)
             {
                MessageBox.Show("Handled Server State Changed exception. Reason: " + ex.Message);
             }
        }

        /// <summary>
        /// Subscribes to all the data on the OPC server that the program want to know
        /// </summary>
        public static void SubscribeToData()
        {
            // Define parameters for Subscribe method:
            int itemIndex;
            //initialize the client subscription handle
            clientSubscriptionHandle = 1;
            //Parameter to specify if the subscription will be added as active or not
            bool active = false;
            // The updateRate parameter is used to tell the server how fast we
            // would like to see data updates.
            int updateRate = 10;
            // The deadband parameter specifies the minimum deviation needed
            // to be considered a change of value. 0 is disabled
            Single deadBand = 0;
            // The revisedUpdateRate parameter is the actual update rate that the
            // server will be using.
            int revisedUpdateRate;
            //Initialize the item identifier values
            itemIdentifiers[0] = new ItemIdentifier();
            itemIdentifiers[0].ClientHandle = 0;
            itemIdentifiers[0].DataType = Type.GetType("System.DWord");
            itemIdentifiers[0].ItemName = "PLC.PLC.Motor1Pos";
            itemIdentifiers[1] = new ItemIdentifier();
            itemIdentifiers[1].ClientHandle = 1;
            itemIdentifiers[1].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[1].ItemName = "PLC.PLC.Motor1OnOff";
            itemIdentifiers[2] = new ItemIdentifier();
            itemIdentifiers[2].ClientHandle = 2;
            itemIdentifiers[2].DataType = Type.GetType("System.Word");
            itemIdentifiers[2].ItemName = "PLC.PLC.Motor1Alarm";
            itemIdentifiers[3] = new ItemIdentifier();
            itemIdentifiers[3].ClientHandle = 3;
            itemIdentifiers[3].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[3].ItemName = "PLC.PLC.Motor1SPReached";
            itemIdentifiers[4] = new ItemIdentifier();
            itemIdentifiers[4].ClientHandle = 4;
            itemIdentifiers[4].DataType = Type.GetType("System.DWord");
            itemIdentifiers[4].ItemName = "PLC.PLC.Motor2Pos";
            itemIdentifiers[5] = new ItemIdentifier();
            itemIdentifiers[5].ClientHandle = 5;
            itemIdentifiers[5].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[5].ItemName = "PLC.PLC.Motor2OnOff";
            itemIdentifiers[6] = new ItemIdentifier();
            itemIdentifiers[6].ClientHandle = 6;
            itemIdentifiers[6].DataType = Type.GetType("System.Word");
            itemIdentifiers[6].ItemName = "PLC.PLC.Motor2Alarm";
            itemIdentifiers[7] = new ItemIdentifier();
            itemIdentifiers[7].ClientHandle = 7;
            itemIdentifiers[7].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[7].ItemName = "PLC.PLC.Motor2SPReached";
            itemIdentifiers[8] = new ItemIdentifier();
            itemIdentifiers[8].ClientHandle = 8;
            itemIdentifiers[8].DataType = Type.GetType("System.DWord");
            itemIdentifiers[8].ItemName = "PLC.PLC.Motor1NewPos";
            itemIdentifiers[9] = new ItemIdentifier();
            itemIdentifiers[9].ClientHandle = 9;
            itemIdentifiers[9].DataType = Type.GetType("System.DWord");
            itemIdentifiers[9].ItemName = "PLC.PLC.Motor2NewPos";
            itemIdentifiers[10] = new ItemIdentifier();
            itemIdentifiers[10].ClientHandle = 10;
            itemIdentifiers[10].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[10].ItemName = "PLC.PLC.Motor1Acknowledge";
            itemIdentifiers[11] = new ItemIdentifier();
            itemIdentifiers[11].ClientHandle = 11;
            itemIdentifiers[11].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[11].ItemName = "PLC.PLC.Motor2Acknowledge";
            itemIdentifiers[12] = new ItemIdentifier();
            itemIdentifiers[12].ClientHandle = 12;
            itemIdentifiers[12].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[12].ItemName = "PLC.PLC.Motor1MDIActive";
            itemIdentifiers[13] = new ItemIdentifier();
            itemIdentifiers[13].ClientHandle = 13;
            itemIdentifiers[13].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[13].ItemName = "PLC.PLC.Motor2MDIActive";
            itemIdentifiers[14] = new ItemIdentifier();
            itemIdentifiers[14].ClientHandle = 14;
            itemIdentifiers[14].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[14].ItemName = "PLC.PLC.Motor1NyPos";
            itemIdentifiers[15] = new ItemIdentifier();
            itemIdentifiers[15].ClientHandle = 15;
            itemIdentifiers[15].DataType = Type.GetType("System.Boolean");
            itemIdentifiers[15].ItemName = "PLC.PLC.Motor2NyPos";
            itemIdentifiers[16] = new ItemIdentifier();
            itemIdentifiers[16].ClientHandle = 16;
            itemIdentifiers[16].DataType = Type.GetType("System.DWord");
            itemIdentifiers[16].ItemName = "PLC.PLC.Motor1NewSpeed";
            itemIdentifiers[17] = new ItemIdentifier();
            itemIdentifiers[17].ClientHandle = 17;
            itemIdentifiers[17].DataType = Type.GetType("System.DWord");
            itemIdentifiers[17].ItemName = "PLC.PLC.Motor2NewSpeed";

            try
            {
                DAserver.Subscribe(clientSubscriptionHandle, active, updateRate, out revisedUpdateRate, deadBand, ref itemIdentifiers, out activeServerSubscriptionHandle);
                for (itemIndex = 0; itemIndex <= 17; itemIndex++)
                {
                    itemValues[itemIndex] = new ItemValue();

                    if (itemIdentifiers[itemIndex].ResultID.Succeeded == false)
                    {
                        MessageBox.Show("Failed to add item " + itemIdentifiers[itemIndex].ItemName + " to subscription");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Subscribe exception. Reason: " + ex.Message);
            }
        }

        /// <summary>
        /// Changes On/Off1 for motor 1
        /// </summary>
        /// <param name="value">The new value of the tag</param>  
        public static void SetMotor1OnOff(int value) 
        {
            try
            {
                itemValues[1].Value = value;

                Random r = new Random(System.DateTime.Now.Millisecond);

                int TransID = r.Next(1, 65535);

                ReturnCode returnCode = DAserver.WriteAsync(TransID, ref itemIdentifiers, itemValues);

                System.Diagnostics.Debug.WriteLine("itemValues[1].Value: " + itemValues[1].Value);

                if (returnCode != ReturnCode.SUCCEEDED)
                {
                    MessageBox.Show("Async Write failed with a result of " + System.Convert.ToString(itemIdentifiers[1].ResultID.Code) + "\r\n" + "Description: " + itemIdentifiers[1].ResultID.Description);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Async Write exception. Reason: " + ex.Message);
            }
        }

        /// <summary>
        /// Changes On/Off1 for motor 2
        /// </summary>
        /// <param name="value">The new value of the tag</param>  
        public static void SetMotor2OnOff(int value)
        {
            try
            {
                itemValues[5].Value = value;

                Random r = new Random(System.DateTime.Now.Millisecond);

                int TransID = r.Next(1, 65535);

                ReturnCode returnCode = DAserver.WriteAsync(TransID, ref itemIdentifiers, itemValues);

                System.Diagnostics.Debug.WriteLine("itemValues[5].Value: " + itemValues[5].Value);

                if (returnCode != ReturnCode.SUCCEEDED)
                {
                    MessageBox.Show("Async Write failed with a result of " + System.Convert.ToString(itemIdentifiers[5].ResultID.Code) + "\r\n" + "Description: " + itemIdentifiers[5].ResultID.Description);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Async Write exception. Reason: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the position for motor 1
        /// </summary>
        /// <param name="value">The new value of the tag</param>  
        public static void SetMotor1NewPos(int value)
        {
            try
            {
                itemValues[8].Value = value;
                itemValues[14].Value = 0;
                itemValues[15].Value = 0;

                Random r = new Random(System.DateTime.Now.Millisecond);

                int TransID = r.Next(1, 65535);

                ReturnCode returnCode = DAserver.WriteAsync(TransID, ref itemIdentifiers, itemValues);

                System.Diagnostics.Debug.WriteLine("itemValues[8].Value: " + itemValues[8].Value);

                if (returnCode != ReturnCode.SUCCEEDED)
                {
                    MessageBox.Show("Async Write failed with a result of " + System.Convert.ToString(itemIdentifiers[8].ResultID.Code) + "\r\n" + "Description: " + itemIdentifiers[8].ResultID.Description);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Async Write exception. Reason: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the position for motor 2
        /// </summary>
        /// <param name="value">The new value of the tag</param> 
        public static void SetMotor2NewPos(int value)
        {
            try
            {
                itemValues[9].Value = value;
                itemValues[14].Value = 0;
                itemValues[15].Value = 0;

                Random r = new Random(System.DateTime.Now.Millisecond);

                int TransID = r.Next(1, 65535);

                ReturnCode returnCode = DAserver.WriteAsync(TransID, ref itemIdentifiers, itemValues);

                if (returnCode != ReturnCode.SUCCEEDED)
                {
                    MessageBox.Show("Async Write failed with a result of " + System.Convert.ToString(itemIdentifiers[9].ResultID.Code) + "\r\n" + "Description: " + itemIdentifiers[9].ResultID.Description);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Async Write exception. Reason: " + ex.Message);
            }
        }

        */
    }
}
