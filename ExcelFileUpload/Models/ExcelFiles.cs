using ExcelFileUpload.Models.Common_Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelFileUpload.Models
{
    public class ExcelFiles:CommonModel
    {
        [Key]
        public int ExcelFileID { get; set; }
        public string ExcelFileName { get; set; }
        [NotMapped]
        public IFormFile ExcelFile { get; set; }
    }
}
