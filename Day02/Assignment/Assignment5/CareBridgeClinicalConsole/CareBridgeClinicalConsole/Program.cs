using Microsoft.Data.SqlClient;
using System.Data;

string connectionString =
    "Server=DESKTOP-UDBEJ14;" +
    "Database=CareBridgeDB;" +
    "Trusted_Connection=True;" +
    "TrustServerCertificate=True;";

while (true)
{
    Console.Clear();

    Console.WriteLine("====================================");
    Console.WriteLine(" CAREBRIDGE CLINICAL OPERATIONS");
    Console.WriteLine("====================================");
    Console.WriteLine("1. 30-Day Readmissions");
    Console.WriteLine("2. High-Risk Patients");
    Console.WriteLine("3. Provider Workload");
    Console.WriteLine("4. Revenue Analysis");
    Console.WriteLine("5. Exit");

    Console.Write("\nSelect Option: ");

    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            ShowReadmissions(connectionString);
            break;

        case 2:
            ShowHighRiskPatients(connectionString);
            break;

        case 3:
            ShowProviderWorkload(connectionString);
            break;

        case 4:
            ShowRevenueAnalysis(connectionString);
            break;

        case 5:
            return;

        default:
            Console.WriteLine("Invalid Choice");
            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

static void ShowReadmissions(string connectionString)
{
    using SqlConnection conn =
        new SqlConnection(connectionString);

    using SqlCommand cmd =
        new SqlCommand(
            "usp_ReadmissionAnalytics",
            conn);

    cmd.CommandType =
        CommandType.StoredProcedure;

    cmd.Parameters.AddWithValue(
        "@WithinDays",
        30);

    conn.Open();

    using SqlDataReader reader =
        cmd.ExecuteReader();

    Console.WriteLine("\n30-Day Readmission Report");
    Console.WriteLine("--------------------------");

    while (reader.Read())
    {
        Console.WriteLine(
            $"Patient: {reader["PatientId"]} | " +
            $"Encounter: {reader["EncounterId"]} | " +
            $"Days Since Previous Visit: " +
            $"{reader["DaysSincePreviousVisit"]}");
    }
}

static void ShowHighRiskPatients(
    string connectionString)
{
    using SqlConnection conn =
        new SqlConnection(connectionString);

    using SqlCommand cmd =
        new SqlCommand(
            "usp_HighRiskPatients",
            conn);

    cmd.CommandType =
        CommandType.StoredProcedure;

    conn.Open();

    using SqlDataReader reader =
        cmd.ExecuteReader();

    Console.WriteLine("\nHigh-Risk Patients");
    Console.WriteLine("------------------");

    while (reader.Read())
    {
        Console.WriteLine(
            $"Patient: {reader["PatientId"]} | " +
            $"Admissions: {reader["AdmissionCount"]}");
    }
}

static void ShowProviderWorkload(
    string connectionString)
{
    using SqlConnection conn =
        new SqlConnection(connectionString);

    using SqlCommand cmd =
        new SqlCommand(
            "usp_ProviderWorkload",
            conn);

    cmd.CommandType =
        CommandType.StoredProcedure;

    conn.Open();

    using SqlDataReader reader =
        cmd.ExecuteReader();

    Console.WriteLine("\nProvider Workload");
    Console.WriteLine("-----------------");

    while (reader.Read())
    {
        Console.WriteLine(
            $"Provider: {reader["ProviderId"]} | " +
            $"Encounters: {reader["TotalEncounters"]}");
    }
}

static void ShowRevenueAnalysis(
    string connectionString)
{
    using SqlConnection conn =
        new SqlConnection(connectionString);

    using SqlCommand cmd =
        new SqlCommand(
            "usp_RevenueAnalysis",
            conn);

    cmd.CommandType =
        CommandType.StoredProcedure;

    conn.Open();

    using SqlDataReader reader =
        cmd.ExecuteReader();

    Console.WriteLine("\nRevenue Analysis");
    Console.WriteLine("----------------");

    while (reader.Read())
    {
        Console.WriteLine(
            $"Status: {reader["Status"]} | " +
            $"Claims: {reader["TotalClaims"]} | " +
            $"Billed: {reader["TotalBilled"]} | " +
            $"Reimbursed: {reader["TotalReimbursed"]} | " +
            $"Outstanding: {reader["OutstandingAmount"]}");
    }
}

