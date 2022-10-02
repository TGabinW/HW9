using System.Text.Json.Serialization;

namespace ConsoleApp1;

public class ReportItem
{
    [JsonPropertyName("Catalog size")]
    public double size { get; set; }
    [JsonPropertyName("Create date")]
    public DateTime min { get; set; }
    [JsonPropertyName("Last change date")]
    public DateTime max { get; set; }
    [JsonPropertyName("Report create date")]
    public DateTime reportDate { get; set; }

    public ReportItem(double size, DateTime min, DateTime max, DateTime reportDate)
    {
        this.size = size;
        this.min = min;
        this.max = max;
        this.reportDate = reportDate;
    }

    public override string ToString()
    {
        return $"Размер: {size}, дата создания: {min}, дата последнего изменения: {max}, дата создания отчета: {reportDate}.";
    }

    public static double sizeOfFolder(string folder, ref double catalogSize)
    {
        try
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            DirectoryInfo[] diA = di.GetDirectories();
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo f in fi)
            {
                catalogSize = catalogSize + f.Length;
            }
            foreach (DirectoryInfo df in diA)
            {
                sizeOfFolder(df.FullName, ref catalogSize);
            }
            return Math.Round((double)(catalogSize / 1024 / 1024 / 1024), 1);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
            return 0;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка. Обратитесь к администратору. Ошибка: " + ex.Message);
            return 0;
        }
    }
}
