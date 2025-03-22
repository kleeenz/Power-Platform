using System;
using System.Runtime.Serialization;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Rest;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System.Configuration;

namespace Power_Platform
{
    public class ConnectionConfig: IconnectionString
    {
        public const string AuthType = "ClientSecret";
        public const string Url = "https://org0509c4b1.crm.dynamics.com";
        public const string ClientId = "b83faec7-0f3a-48bc-910a-c3cf5060e4a3";
        public const string ClientSecret = "96.8Q~eatJb-7oR2Mn6TNs5afXCvPdfyT1ip4aCg";
        public const string TenantId = "f53c472b-9ce5-4d34-9277-0883b6507fff";
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



    public class ConnectDataverse : IIsDataverseConnected
    {

        private readonly IconnectionString _connectionString;

        public ConnectDataverse()
        {
            _connectionString = new ConnectionConfig();
        }
        public bool IsConnected()
        {
            ServiceClient serviceClient = new ServiceClient(_connectionString.ConnectionString());

            return serviceClient.IsReady;
        }
    }


    public class ConnectTable : IConnectTable
    {
        private readonly IconnectionString _connectionString;
        private readonly IIsDataverseConnected _isDataverseConnected;

        public ConnectTable(IconnectionString connectionString, IIsDataverseConnected isDataverseConnected)
        {
            _connectionString = connectionString;
            _isDataverseConnected = isDataverseConnected;
        }
        public EntityCollection connectToTableInstance()
        {

            ServiceClient serviceClient = new ServiceClient(_connectionString.ConnectionString());

            QueryExpression query = null;

            if (_isDataverseConnected.IsConnected())
            {
                Console.WriteLine("Connection successful");

                Entity entity = new Entity("cr382_environmentalsensordata");

            }
            return serviceClient.RetrieveMultiple(query);
        }
    }

    public class checkNoRowsAreEmpty: ICheckNoRowsAreEmpty
    {
        private readonly IConnectTable _connectTable;

        public checkNoRowsAreEmpty(IConnectTable connectTable)
        {
            _connectTable = connectTable;
        }

        public bool IsEmptyRows()
        {
            var fields = _connectTable.connectToTableInstance();
            var boolValue = false;
            foreach (Entity entity in fields.Entities)
            {
                if (entity["cr382_humiditylevel"] != null || entity["cr382_pressurereading"] != null ||
                    entity["cr382_rpmvalue"] != null || entity["cr382_temperaturereading"] != null || entity["cr382_vibrationlevel"] != null)
                {
                    boolValue = true;
                }
            }
            return boolValue;
        }


        public class PredictLiftCondition: IPrediction
    {
        private readonly IConnectTable _connectTable;
        private readonly ICheckNoRowsAreEmpty _checkNoRowsAreEmpty;

            public PredictLiftCondition(IConnectTable connectTable, ICheckNoRowsAreEmpty checkNoRowsAreEmpty)
        {
            _connectTable = connectTable;
            _checkNoRowsAreEmpty = checkNoRowsAreEmpty;
         }
        public string BrokenCondition()
        {
            var results = _connectTable.connectToTableInstance();

                if (_checkNoRowsAreEmpty.IsEmptyRows())
                {
                    foreach (Entity entity in results.Entities)
                    {
                        if ((int)entity["cr382_temperaturereading"] > 0)
                        {
                            if (Convert.ToInt32(entity["cr382_temperaturereading"]) > 50)
                            {
                                return "Lift is broken";
                            }
                        }
                    }
            }
                }
            
        }
        public string RecoveringCondition()
        {
            return "Lift is recovering";
        }
        public string NormalCondition()
        {
            return "Lift is normal";
        }
    }
    {
        
    }
}
