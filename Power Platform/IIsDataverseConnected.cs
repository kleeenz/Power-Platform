using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;


namespace Power_Platform
{

    public interface IconnectionString
    {
        string ConnectionString();
    }
    public interface IIsDataverseConnected 
    {
        ServiceClient IsConnected();
    }

    public interface IConnectTable
    {
        Entity connectToTableInstance();
    }

    public interface ICheckNoRowsAreEmpty
    {
        bool IsEmptyRows(double inputParameter);
    }
    
    public interface IPrediction
    {
       string MakePrediction();
    }

    public interface IRetrieveData
    {
        void Retrieve();
    }

}
