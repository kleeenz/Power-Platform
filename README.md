Power Platform Integration with Dataverse Using Microsoft.PowerPlatform.Dataverse.Client

This project demonstrates how to integrate a custom .NET application with Microsoft Dataverse (formerly Common Data Service) using the `Microsoft.PowerPlatform.Dataverse.Client` package. The application predicts lift conditions based on sensor data and stores the information in Dataverse.

---

Table of Contents

1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Setup and Configuration](#setup-and-configuration)
4. [Project Structure](#project-structure)
5. [Usage](#usage)
6. [Code Walkthrough](#code-walkthrough)
7. [Contributing](#contributing)
8. [License](#license)



Features

- Dataverse Integration: Connect to Microsoft Dataverse using the `ServiceClient` class.
- CRUD Operations: Save sensor data directly into a Dataverse table.
- Prediction Logic: Analyze input parameters (e.g., pressure, temperature) to predict the condition of a lift.
- Validation: Ensure all input values are valid and not empty before processing.
- Extensibility: Designed for easy extension to support additional features or conditions.



Technologies Used

- C# (.NET 6.0): Core programming language for the application.
- Microsoft.PowerPlatform.Dataverse.Client: Package for connecting and interacting with Microsoft Dataverse.
- Microsoft.Xrm.Sdk: Library for working with Dataverse entities.

---

Setup and Configuration

1. Install Required Packages
Install the `Microsoft.PowerPlatform.Dataverse.Client` package using NuGet:

```bash
dotnet add package Microsoft.PowerPlatform.Dataverse.Client
```

 2. Configure the Connection String
Update the connection details in the `ConnectionConfig` class:

```csharp
public const string AuthType = "ClientSecret";
public const string Url = "https://yourorg.crm.dynamics.com";
public const string ClientId = "your-client-id";
public const string ClientSecret = "your-client-secret";
public const string TenantId = "your-tenant-id";
public const string RedirectUri = "http://localhost";
```

Replace the placeholders with your Dataverse environment details.

3. Set Up Dataverse Table
Ensure the Dataverse table (`cr382_environmentalsensordata`) exists with the following columns:
- HumidityLevel (`cr382_humiditylevel`)
- PressureReading (`cr382_pressurereading`)
- RPMValue (`cr382_rpmvalue`)
- TemperatureReading (`cr382_temperaturereading`)
- VibrationLevel (`cr382_vibrationlevel`)

---

Project Structure

Power_Platform/
├── ConnectionConfig.cs       # Handles Dataverse connection string setup
├── ConnectDataverse.cs       # Establishes connection to Dataverse
├── ConnectTable.cs           # Connects to a specific Dataverse table
├── checkNoRowsAreEmpty.cs    # Validates that no input values are empty
├── PredictLiftCondition.cs   # Contains prediction logic and saves data to Dataverse
├── ImplementClass.cs         # Controller class for executing predictions
```

---

Usage

1. Run the Application**
Compile and run the application:

```bash
dotnet run
```

2. Input Sensor Data
Enter the following sensor values when prompted:
- Pressure
- Humidity
- RPM
- Vibration
- Temperature

3. View the Prediction
The application will display one of the following conditions based on the inputs:
- Broken Condition
- Recovery Condition
- Normal Condition

The data is saved to the Dataverse table if the input is valid.

---

Code Walkthrough

Connection Configuration
The `ConnectionConfig` class sets up the connection string for Dataverse:

```csharp
public string ConnectionString()
{
    return $"AuthType={AuthType}; Url={Url}; ClientId={ClientId}; ClientSecret={ClientSecret}; TenantId={TenantId}; RedirectUri={RedirectUri};";
}
```

Establishing Connection
The `ConnectDataverse` class uses the `ServiceClient` to establish a connection:

```csharp
ServiceClient serviceClient = new ServiceClient(_connectionString.ConnectionString());
```

Prediction Logic
The `PredictLiftCondition` class analyzes the input values to determine the lift's condition:

```csharp
if (pressureValue > 80 || humidityValue > 80 || (rpmValue > 0 || rpmValue <= 20))
{
    PredictedValue = "Broken condition";
}
else if ((pressureValue >= 70 && pressureValue <= 80) && ...)
{
    PredictedValue = "Recovery condition";
}
else
{
    PredictedValue = "Normal Condition";
}
```

Saving Data to Dataverse
If inputs are valid, the data is stored in the Dataverse table:

```csharp
Guid recordId = _isDataverse.IsConnected().Create(StoreData);
```

---

Contributing

Feel free to submit issues or pull requests for improvements. Contributions are welcome!

---
