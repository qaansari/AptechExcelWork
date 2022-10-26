using ExcelFileUpload.Models.User_Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelFileUpload.ViewModels.User_ViewModel
{
    public class UserView
    {

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int RoleID { get; set; }
        public RoleView Role { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int? CreatedByUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? UpdatedByUserID { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public User CreatedByUser { get; set; }
        public User UpdatedByUser { get; set; }
    }
}
