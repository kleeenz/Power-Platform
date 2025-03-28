using Power_Platform;


// main method that executes the controller class method.
public class program
{
    public static void Main()
    {
        IconnectionString connectionString = new ConnectionConfig();
        IIsDataverseConnected dataverseConn = new ConnectDataverse(connectionString);
        IConnectTable connectTable = new ConnectTable(dataverseConn);
        ICheckNoRowsAreEmpty checkRows = new checkNoRowsAreEmpty();
        IPrediction prediction = new PredictLiftCondition(connectTable, checkRows, dataverseConn);
        IRetrieveData retrieveData = new RetrieveData(connectTable, dataverseConn);

        ImplementClass imp = new ImplementClass(prediction, retrieveData);

        //call the Predict Method
        imp.callPredictionMethod();

        Console.ReadKey();
    }
}


