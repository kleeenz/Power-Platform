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
        bool IsConnected();
    }

    public interface IConnectTable
    {
        EntityCollection connectToTableInstance();
    }

    public interface ICheckNoRowsAreEmpty
    {
        bool IsEmptyRows();
    }
    
    public interface IPrediction
    {
       
    }

}
