using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System.Runtime.Serialization;
using System;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

public class Program
{
    public static void Main(string[] args)
    {
        string connectionString = "AuthType=ClientSecret; " +
                                "Url=https://org0509c4b1.crm.dynamics.com; " +
                                "ClientId=b83faec7-0f3a-48bc-910a-c3cf5060e4a3; " +
                                "ClientSecret=96.8Q~eatJb-7oR2Mn6TNs5afXCvPdfyT1ip4aCg; " +
                                "TenantId=f53c472b-9ce5-4d34-9277-0883b6507fff; " +
                                "RedirectUri=http://localhost; ";

        try
        {
            using (ServiceClient serviceClient = new ServiceClient(connectionString))
            {
                if (serviceClient.IsReady)
                {
                    Console.WriteLine("Connection successful");

                    QueryExpression query = new QueryExpression("cr382_environmentalsensordata")
                    {
                        ColumnSet = new ColumnSet("cr382_humiditylevel", "cr382_pressurereading"),
                        TopCount = 5
                    };

                    EntityCollection results = serviceClient.RetrieveMultiple(query);

                    foreach (Entity entity in results.Entities)
                    {
                        Console.WriteLine($"Humidity Level: {entity["cr382_humiditylevel"]} Pressure Reading: {entity["cr382_pressurereading"]}");
                    }

                }
                else
                {
                    Console.WriteLine("Connection not successful");
                    Console.WriteLine("Error: " + serviceClient.LastError);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during initialization:");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                Console.WriteLine(ex.InnerException.StackTrace);
            }
        }
        Console.ReadKey();
    }
}


