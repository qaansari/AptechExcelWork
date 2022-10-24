using System.ComponentModel.DataAnnotations;

namespace ExcelFileUpload.Models.User_Model
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string Title { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
