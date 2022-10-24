using ExcelFileUpload.Models.User_Model;
using ExcelFileUpload.ViewModels.User_ViewModel;

namespace ExcelFileUpload.Services.Interfaces
{
    public interface IUserService
    {
        //Get All
        List<UserView> GetAll();
        //Get By Id
        UserView GetById(int id);
        //Add
        Task<UserView> Add(UserView userView);
        //Update
        Task<UserView> Update(UserView userView);
        //Delete
       Task<bool> Delete(int id);
        //Login
        Task<User> Login(string email,string hashedPassword);
        
    }
}
