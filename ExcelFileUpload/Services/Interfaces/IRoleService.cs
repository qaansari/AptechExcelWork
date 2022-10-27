using ExcelFileUpload.ViewModels.User_ViewModel;

namespace ExcelFileUpload.Services.Interfaces
{
    public interface IRoleService
    {
        //Get All
        List<RoleView> GetAll();
        //Get By Id
        RoleView GetById(int id);
        //Add
        Task<RoleView> Add(RoleView roleView);
        //Update
        Task<RoleView> Update(RoleView roleView);
        //Delete
        Task<bool> Delete(int id);
        
    }
}
