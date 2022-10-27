using AutoMapper;
using ExcelFileUpload.AppDbContext;
using ExcelFileUpload.Models.User_Model;
using ExcelFileUpload.Services.Interfaces;
using ExcelFileUpload.ViewModels.User_ViewModel;

namespace ExcelFileUpload.Services
{
    public class RoleService: IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public RoleService(ApplicationDbContext context,IMapper mapper)
        {
            _context= context;
            _mapper= mapper;
        }
        //Add-Role
        public async Task<RoleView> Add(RoleView roleView)
        {
            try
            {
                var role = _mapper.Map<Role>(roleView);
                role.IsActive = true;
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                return _mapper.Map<RoleView>(role);
            }
            catch (Exception exc)
            {

                throw new Exception("Error Adding Role",exc);
            }
        }
        //Delete-Role
        public async Task<bool> Delete(int id)
        {
            bool isDeleted = false;
            try
            {
                var role = _context.Roles.FirstOrDefault(x => x.RoleID == id);
                if (role.IsActive)
                {
                    role.IsActive = false;
                    _context.ChangeTracker.Clear();
                    _context.Roles.Update(role);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception exc)
            {

                throw new Exception("Error Deleting Role", exc);
            }
            return isDeleted;
        }
        //Get-All-Roles
        public List<RoleView> GetAll()
        {
            try
            {
                var roles = _context.Roles.Where(x => x.IsActive == true).ToList();
                return _mapper.Map<List<Role>, List<RoleView>>(roles);
            }
            catch (Exception exc)
            {

                throw new Exception("Error Getting Roles", exc);
            }
        }
        //Get-Role-By-Id
        public RoleView GetById(int id)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(x => x.RoleID == id);
                return _mapper.Map<RoleView>(role);
            }
            catch (Exception exc)
            {

                throw new Exception("Error Getting Role By Id", exc);
            }
        }
        //Update-Role
        public async Task<RoleView> Update(RoleView roleView)
        {
            try
            {
                var role = _mapper.Map<Role>(roleView);
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
                return _mapper.Map<RoleView>(GetById(role.RoleID));
            }
            catch (Exception exc)
            {

                throw new Exception("Error Updating Role", exc);
            }
        }
    }
}
