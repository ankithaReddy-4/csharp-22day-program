using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CareBridge.PerformanceLab.Models;
while (true)
{
    Console.Clear();
    Console.WriteLine("=================================");
    Console.WriteLine(" CAREBRIDGE Revenue At Risk Dashboard");
    Console.WriteLine("=================================");
    Console.WriteLine();

    Console.WriteLine("1. Show Revenue At Risk");
    
    Console.WriteLine("2. Exit");

    Console.WriteLine();
    Console.Write("Choose Option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            RevenueAtRisk();
            break;

        case "2":
            return;

        default:
            Console.WriteLine("Invalid Option");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}
static void RevenueAtRisk()
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();

    using var db = new CareBridgeContext();
    var summary = db.Claims
        .Include(c => c.Encounter)   
        .Include(c => c.Insurance)   
        .AsNoTracking()              
        .GroupBy(c => c.Status)
        .Select(g => new ClaimSummaryDto
        {
            Status = g.Key,
            ClaimCount = g.Count(),
            TotalBilled = g.Sum(x => x.BilledAmount),
            TotalReimbursed = g.Sum(x => x.ReimbursedAmt ?? 0),
            Gap = g.Sum(x => x.BilledAmount) - g.Sum(x => x.ReimbursedAmt ?? 0)
        })
        .ToList();

    
    var revenueAtRisk = db.Claims
        .Include(c => c.Encounter)
        .Include(c => c.Insurance)
        .AsNoTracking()
        .Where(c => c.Status != "Paid")
        .Sum(c => c.BilledAmount);

    stopwatch.Stop();

    Console.WriteLine("=== Revenue At Risk Dashboard ===");
    foreach (var row in summary)
    {
        Console.WriteLine($"{row.Status}:");
        Console.WriteLine($"  Claims: {row.ClaimCount}");
        Console.WriteLine($"  Total Billed: {row.TotalBilled:C}");
        Console.WriteLine($"  Total Reimbursed: {row.TotalReimbursed:C}");
        Console.WriteLine($"  Gap: {row.Gap:C}");
        
        Console.WriteLine();
    }

    Console.WriteLine($"Revenue-at-Risk (Unpaid Claims): {revenueAtRisk:C}");

    Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

    
    Console.WriteLine($"Tracked entities: {db.ChangeTracker.Entries().Count()}");
}





