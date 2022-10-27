using ExcelFileUpload.Models.Common_Model;
using System.ComponentModel.DataAnnotations;

namespace ExcelFileUpload.Models.User_Model
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
