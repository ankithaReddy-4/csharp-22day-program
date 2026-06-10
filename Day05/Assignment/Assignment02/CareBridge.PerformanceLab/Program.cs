using CareBridge.PerformanceLab.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
while (true)
{
    Console.Clear();
    Console.WriteLine("=================================");
    Console.WriteLine(" Taming the Cartesian Explosion ");
    Console.WriteLine("=================================");
    Console.WriteLine();

    Console.WriteLine("1. Cartesian Explosion Demo");
    Console.WriteLine("2. SplitQueryDemo");
    Console.WriteLine("3. Exit");

    Console.WriteLine();
    Console.Write("Choose Option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            CartesianExplosionDemo();
            break;

        case "2":
            SplitQueryDemo();
            break;
        case "3":
            return;

        default:
            Console.WriteLine("Invalid Option");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}
static void CartesianExplosionDemo()
{

    using var db = new CareBridgeContext();

    var stopwatch = new Stopwatch();
    stopwatch.Start();


    // SINGLE QUERY with multiple Include paths
    var patient = db.Patients
        .Where(p => p.Mrn == "888888")
        .Include(p => p.Encounters)
            .ThenInclude(e => e.Diagnoses)
        .Include(p => p.Encounters)
            .ThenInclude(e => e.Claims)
        .AsNoTracking()
        .FirstOrDefault();

    stopwatch.Stop();

    // Count objects
    var encounterCount = patient?.Encounters.Count ?? 0;
    var diagnosisCount = patient?.Encounters.SelectMany(e => e.Diagnoses).Count() ?? 0;
    var claimCount = patient?.Encounters.SelectMany(e => e.Claims).Count() ?? 0;

    Console.WriteLine("SINGLE QUERY (default Include)");
    Console.WriteLine("------------------------------------------------");
    Console.WriteLine($"Encounters : {encounterCount}   Diagnoses : {diagnosisCount}   Claims : {claimCount}");
    Console.WriteLine("SQL Statements (Profiler)   : 1");
    
    Console.WriteLine($"Elapsed Time                : {stopwatch.ElapsedMilliseconds} ms");
    Console.WriteLine($"Tracked entities            : {db.ChangeTracker.Entries().Count()}");
}

static void SplitQueryDemo()
{
    using var db = new CareBridgeContext();
    var stopwatch = new Stopwatch();
    stopwatch.Start();

    // SPLIT QUERY version
    var patient = db.Patients
        .Where(p => p.Mrn == "888888")
        .Include(p => p.Encounters)
            .ThenInclude(e => e.Diagnoses)
        .Include(p => p.Encounters)
            .ThenInclude(e => e.Claims)
        .AsSplitQuery()   // key difference
        .AsNoTracking()
        .FirstOrDefault();

    stopwatch.Stop();

    // Count objects
    var encounterCount = patient?.Encounters.Count ?? 0;
    var diagnosisCount = patient?.Encounters.SelectMany(e => e.Diagnoses).Count() ?? 0;
    var claimCount = patient?.Encounters.SelectMany(e => e.Claims).Count() ?? 0;

    Console.WriteLine($"Encounters : {encounterCount}   Diagnoses : {diagnosisCount}   Claims : {claimCount}");

    Console.WriteLine("SPLIT QUERY (AsSplitQuery)");
    Console.WriteLine("------------------------------------------------");
    Console.WriteLine($"Encounters : {encounterCount}   Diagnoses : {diagnosisCount}   Claims : {claimCount}");
    Console.WriteLine("SQL Statements (Profiler)   : 3");
    
    Console.WriteLine($"Elapsed Time                : {stopwatch.ElapsedMilliseconds} ms");
    Console.WriteLine($"Tracked entities            : {db.ChangeTracker.Entries().Count()}");
}





