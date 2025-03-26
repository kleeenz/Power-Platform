using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;


namespace Power_Platform
{
    // setup the connection string
    public class ConnectionConfig : IconnectionString
    {
        public const string AuthType = "ClientSecret";
        public const string Url = "https://org********.crm.dynamics.com";
        public const string ClientId = "***faec7-****-***-***-c3cf5060e***";
        public const string ClientSecret = "***.8Q~eatJb-7o*****afXCvPdfyT1ip4a***";
        public const string TenantId = "*******-9ce5-****-****-****b6507***";
        public const string RedirectUri = "http://localhost";

        public string ConnectionString()
        {
            return $"AuthType={AuthType}; " +
                   $"Url={Url}; " +
                   $"ClientId={ClientId}; " +
                   $"ClientSecret={ClientSecret}; " +
                   $"TenantId={TenantId}; " +
                   $"RedirectUri={RedirectUri};";
        }
    }


    //create a connection to dataverse using the ServiceClient class
    public class ConnectDataverse : IIsDataverseConnected
    {

        private readonly IconnectionString _connectionString;

        public ConnectDataverse(IconnectionString connectionString)
        {
            _connectionString = connectionString;
        }
        public ServiceClient IsConnected()
        {
            ServiceClient serviceClient = new ServiceClient(_connectionString.ConnectionString());

            return serviceClient;
        }
    }


    //connect to the table instance if the connection to dataverse is successful
    public class ConnectTable : IConnectTable
    {

        private readonly IIsDataverseConnected _isDataverseConnected;

        public ConnectTable(IIsDataverseConnected isDataverseConnected)
        {

            _isDataverseConnected = isDataverseConnected;
        }
        public Entity connectToTableInstance()
        {

            var checkDataverseConnected = _isDataverseConnected.IsConnected();

            Entity entity1 = new Entity();

            if (checkDataverseConnected.IsReady)
            {
                Console.WriteLine("Connection successful");

                entity1 = new Entity("cr382_environmentalsensordata");


            }
            return entity1;
        }
    }


    // the class to check for empty values
    public class checkNoRowsAreEmpty : ICheckNoRowsAreEmpty
    {
        public bool IsEmptyRows(double inputParameters)
        {
            return inputParameters != null;

        }
    }


    // accept input, check that input is not null and make prediction based on defined condition. Save input to dataverse
        public class PredictLiftCondition : IPrediction
        {
            private readonly IConnectTable _connectTable;
            private readonly ICheckNoRowsAreEmpty _checkNoRowsAreEmpty;
            private readonly IIsDataverseConnected _isDataverse;

            public PredictLiftCondition(IConnectTable connectTable, ICheckNoRowsAreEmpty checkNoRowsAreEmpty, IIsDataverseConnected isDataverse)
            {
                _connectTable = connectTable;
                _checkNoRowsAreEmpty = checkNoRowsAreEmpty;
                _isDataverse = isDataverse;
            }


            public string MakePrediction()
            {
                var StoreData = _connectTable.connectToTableInstance();

                Console.WriteLine("Enter the value of Pressure");
                string Pressure = Console.ReadLine();
                bool pressureConvertToInt = double.TryParse(Pressure, out double pressureValue);
                bool PValue = _checkNoRowsAreEmpty.IsEmptyRows(pressureValue);

                Console.WriteLine("Enter the value of Humidity");
                string Humidity = Console.ReadLine();
                bool humidityToInt = double.TryParse(Humidity, out double humidityValue);
                bool HValue = _checkNoRowsAreEmpty.IsEmptyRows(humidityValue);

                Console.WriteLine("Enter the value of RPM");
                string RPM = Console.ReadLine();
                bool rpmToInt = double.TryParse(RPM, out double rpmValue);
                bool RValue = _checkNoRowsAreEmpty.IsEmptyRows(rpmValue);

                Console.WriteLine("Enter the value of Vibration");
                string Vibration = Console.ReadLine();
                bool vibrationToInt = double.TryParse(Vibration, out double vibrationValue);
                bool VValue = _checkNoRowsAreEmpty.IsEmptyRows(vibrationValue);

                Console.WriteLine("Enter the value of Temperature");
                string Temperature = Console.ReadLine();
                bool temperatureToInt = double.TryParse(Temperature, out double temperatureValue);
                bool TValue = _checkNoRowsAreEmpty.IsEmptyRows(temperatureValue);

                string PredictedValue = "";
                if (PValue && HValue && RValue && VValue && TValue)
                {
                    StoreData["cr382_humiditylevel"] = humidityValue;
                    StoreData["cr382_pressurereading"] = pressureValue;
                    StoreData["cr382_rpmvalue"] = rpmValue;
                    StoreData["cr382_temperaturereading"] = temperatureValue;
                    StoreData["cr382_vibrationlevel"] = vibrationValue;
                    try
                    {
                        Guid recordId = _isDataverse.IsConnected().Create(StoreData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    if (pressureValue > 80 || humidityValue > 80 || (rpmValue > 0 || rpmValue <= 20) || vibrationValue > 0 || temperatureValue > 0)
                    {
                        PredictedValue = "Broken condition";
                    }
                    else if ((pressureValue >= 70 && pressureValue <= 80) && (humidityValue >= 60 && humidityValue <= 80) && (rpmValue > 0 && rpmValue <= 20) && vibrationValue <= 0 && (temperatureValue >= 30 && temperatureValue <= 35))
                    {
                        PredictedValue = "Recovery condition";
                    }

                    else
                    {
                        PredictedValue = "Normal Condition";
                    }
                }
                else
                {
                    Console.WriteLine("One of the input is null or an invalid number, please enter an integer value");
                    Console.ReadKey();
                }

                return PredictedValue;
            }
        }



    //controller class
        public class ImplementClass
        {
            private readonly IPrediction prediction;

            public ImplementClass(IPrediction _prediction)
            {
                this.prediction = _prediction;
            }

            public void callPredictionMethod()
            {
                Console.WriteLine(prediction.MakePrediction());
            }
        }
    }

