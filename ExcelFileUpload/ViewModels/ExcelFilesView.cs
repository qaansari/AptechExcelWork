namespace ExcelFileUpload.ViewModels
{
    public class ExcelFilesView
    {
        public int ExcelFileID { get; set; }
        public string ExcelFileName { get; set; }
        public IFormFile ExcelFile { get; set; }
    }
}
