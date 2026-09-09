using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesFiles = FindFiles(storesDirectory);

var reportPath = Path.Combine(storesDirectory, "salesSummary.txt");
GenerateSalesSummary(salesFiles, reportPath);

Console.WriteLine($"Report successfully generated at: {reportPath}");


IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        if (Path.GetFileName(file).Equals("sales.json", StringComparison.OrdinalIgnoreCase))
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}


void GenerateSalesSummary(IEnumerable<string> files, string outputFile)
{
    var report = new StringBuilder();
    double totalSales = 0;
    var details = new StringBuilder();

    foreach (var file in files)
    {
        string salesJson = File.ReadAllText(file);
        
        SalesTotal? data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
        
        double fileSales = data?.Total ?? 0;
        totalSales += fileSales;

        string fileName = Path.GetFileName(file);
        string parentDir = Path.GetFileName(Path.GetDirectoryName(file) ?? "");
        string displayName = string.IsNullOrEmpty(parentDir) ? fileName : $"{parentDir}/{fileName}";

        details.AppendLine($" {displayName}: {fileSales:C}");
    }

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {totalSales:C}");
    report.AppendLine();
    report.AppendLine("Details:");
    report.Append(details.ToString());

    File.WriteAllText(outputFile, report.ToString());
}

class SalesTotal
{
    public double Total { get; set; }
}
