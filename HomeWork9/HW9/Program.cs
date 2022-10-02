using System.Text.Json;
using ConsoleApp1;

namespace Host;

internal static class Program
{
    private static async Task Main(string[] args)
    {

        foreach (var arg in args) Console.WriteLine(arg);

        string pathCatalog = args[0];
        string pathToReport = args[1];
        byte searchDepth = byte.Parse(args[2]);

        double size = 0;
        DateTime min, max, reportDate;

        if (Directory.Exists(pathCatalog))
        {
            ReportItem.sizeOfFolder(pathCatalog, ref size);
            min = Directory.GetCreationTime(pathCatalog);
            max = Directory.GetLastWriteTime(pathCatalog);

            ReportItem report = new ReportItem(size, min, max, reportDate = DateTime.Now);

            Console.WriteLine(report);

            using (FileStream fs = new FileStream(pathToReport, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<ReportItem>(fs, report);
                Console.WriteLine($"Информация сохранена в файл {pathToReport}.");
            }
        }
        else
        {
            min = DateTime.MinValue;
            max = DateTime.MinValue;
            size = 0;
            Console.WriteLine("Каталога не существует.");
        }


    }
}