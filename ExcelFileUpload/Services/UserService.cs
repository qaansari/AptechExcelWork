using AutoMapper;
using ExcelFileUpload.AppDbContext;
using ExcelFileUpload.Models.User_Model;
using ExcelFileUpload.Services.Interfaces;
using ExcelFileUpload.ViewModels.User_ViewModel;
using Microsoft.EntityFrameworkCore;


namespace ExcelFileUpload.Services
{
    public class UserService : IUserService
    {

        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        
        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
  
        }
        //Add---User
        public async Task<UserView> Add(UserView userView)
        {
            try
            {
                var user = _mapper.Map<User>(userView);
                user.IsActive = true;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return _mapper.Map<UserView>(user);
            }
            catch (Exception e)
            {

                throw new Exception("Error Adding User", e);
            }

        }
        //Delete---User
        public async Task<bool> Delete(int id)
        {
            var isDeleted = false;
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserID == id);
                if (user.IsActive)
                {
                    user.IsActive = false;
                    _context.ChangeTracker.Clear();
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                throw new Exception("Error Deleting User", e);
            }
            return isDeleted;

        }
        //Get---All---Users
        public List<UserView> GetAll()
        {
            try
            {
                var users = _context.Users.Where(x => x.IsActive == true).AsNoTracking().ToList();
                return _mapper.Map<List<User>, List<UserView>>(users);
            }
            catch (Exception e)
            {

                throw new Exception("Error Getting Users", e);
            }
        }
        //Get---User---By---ID
        public UserView GetById(int id)
        {
            try
            {
                var user = _context.Users.AsNoTracking().FirstOrDefault(x => x.UserID == id);
                return _mapper.Map<UserView>(user);
            }
            catch (Exception e)
            {

                throw new Exception("Error Getting User By Id", e);
            }
        }
        //Update---User
        public async Task<UserView> Update(UserView userView)
        {
            try
            {
                var user = _mapper.Map<User>(userView);
                _context.ChangeTracker.Clear();
                _context.Users.Update(user);
                _context.SaveChangesAsync();
                return _mapper.Map<UserView>(GetById(user.UserID));
            }
            catch (Exception e)
            {

                throw new Exception("Error Updating User", e);
            }
        }
        //Login-Signed-Up-User
        public async Task<User> Login(string email, string hashedPassword)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == hashedPassword);
                if (user != null)
                {
                    return user;
                }
            }
            catch (Exception e)
            {

                throw new Exception("Error Logging In User", e);
            }
            return null;
        }
    }
}

