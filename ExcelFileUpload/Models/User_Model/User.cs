using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelFileUpload.Models.User_Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public string? ImageName { get; set; }
        [ForeignKey("UserID")]
        public int? CreatedByUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; } = DateTime.UtcNow.AddHours(5);
        [ForeignKey("UserID")]
        public int? UpdatedByUserID { get; set; }
        public DateTime? UpdatedDateTime { get; set; } = DateTime.UtcNow.AddHours(5);
        public User CreatedByUser { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
