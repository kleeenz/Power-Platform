using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

}
