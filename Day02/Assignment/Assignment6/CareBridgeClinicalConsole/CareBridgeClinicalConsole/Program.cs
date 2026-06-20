using Microsoft.Data.SqlClient;

string connectionString =
    "Server=localhost;" +
    "Database=CareBridgeDB;" +
    "Trusted_Connection=True;" +
    "TrustServerCertificate=True;";

while (true)
{
    Console.Clear();

    Console.WriteLine("================================");
    Console.WriteLine(" HIPAA COMPLIANT ACCESS PORTAL");
    Console.WriteLine("================================");
    Console.WriteLine("1. Clinical Team");
    Console.WriteLine("2. Billing Team");
    Console.WriteLine("3. Analytics Team");
    Console.WriteLine("4. Exit");

    Console.Write("\nSelect Role: ");

    int choice =
        Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            ShowClinicalData(connectionString);
            break;

        case 2:
            ShowBillingData(connectionString);
            break;

        case 3:
            ShowAnalyticsData(connectionString);
            break;

        case 4:
            return;

        default:
            Console.WriteLine("Invalid Option");
            break;
    }

    Console.WriteLine("\nPress any key...");
    Console.ReadKey();
}

static void ShowClinicalData(string connectionString)
{
    using SqlConnection conn =
        new SqlConnection(connectionString);

    string query = "SELECT * FROM vw_Clinical";

    using SqlCommand cmd =
        new SqlCommand(query, conn);

    conn.Open();

    using SqlDataReader reader =
        cmd.ExecuteReader();

    Console.WriteLine("\nClinical Team View");
    Console.WriteLine("------------------");

    while (reader.Read())
    {
        Console.WriteLine(
            $"MRN: {reader["MRN"]} | " +
            $"Name: {reader["FullName"]} | " +
            $"Encounter: {reader["EncounterId"]} | " +
            $"Type: {reader["EncounterType"]} | " +
            $"ICD Code: {reader["IcdCode"]} | " +
            $"Diagnosis: {reader["Description"]}"
        );
    }
}

static void ShowAnalyticsData(
    string connectionString)
{
    using SqlConnection conn =
        new SqlConnection(connectionString);

    string query =
        "SELECT * FROM vw_Analytics_DeId";

    using SqlCommand cmd =
        new SqlCommand(query, conn);

    conn.Open();

    using SqlDataReader reader =
        cmd.ExecuteReader();

    Console.WriteLine("\nAnalytics Team View");
    Console.WriteLine("-------------------");

    while (reader.Read())
    {
        Console.WriteLine(
            $"Age Band: {reader["AgeBand"]} | " +
            $"Encounter Type: {reader["EncounterType"]}");
    }
}


static void ShowBillingData(
    string connectionString)
{
    using SqlConnection conn =
        new SqlConnection(connectionString);

    string query =
        "SELECT * FROM vw_Billing";

    using SqlCommand cmd =
        new SqlCommand(query, conn);

    conn.Open();

    using SqlDataReader reader =
        cmd.ExecuteReader();

    Console.WriteLine("\nBilling Team View");
    Console.WriteLine("-----------------");

    while (reader.Read())
    {
        Console.WriteLine(
            $"Claim: {reader["ClaimId"]} | " +
            $"Status: {reader["Status"]} | " +
            $"Billed: {reader["BilledAmount"]} | " +
            $"Reimbursed: {reader["ReimbursedAmt"]}");
    }
}