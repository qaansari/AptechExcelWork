using ExcelFileUpload.Models.User_Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelFileUpload.Models.Common_Model
{
    public class CommonModel
    {
        public bool IsActive { get; set; }
        [ForeignKey("CreatedByUser")]
        public int CreatedByUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; } = DateTime.UtcNow.AddHours(5);
        [ForeignKey("UpdatedByUser")]
        public int? UpdatedByUserID { get; set; }
        public DateTime? UpdatedDateTime { get; set; } = DateTime.UtcNow.AddHours(5);
        public User CreatedByUser { get; set; }
        public User? UpdatedByUser { get; set; }

    }
}
