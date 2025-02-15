public interface IExportService
{
    Task<FileResult> ExportToExcelAsync<T>(IEnumerable<T> data, string fileName);
    Task<FileResult> ExportToPdfAsync<T>(IEnumerable<T> data, string fileName);
}

public class FileResult
{
    public string FileUrl { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
} 