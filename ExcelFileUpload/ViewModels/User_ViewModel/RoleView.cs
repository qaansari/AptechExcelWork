namespace ExcelFileUpload.ViewModels.User_ViewModel
{
    public class RoleView
    {
        public int RoleID { get; set; }
        public string Title { get; set; }
        public List<UserView> Users { get; set; }
    }
}
