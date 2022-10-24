using AutoMapper;
using ExcelFileUpload.Models.User_Model;
using ExcelFileUpload.ViewModels.User_ViewModel;

namespace ExcelFileUpload.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserView, User>().ReverseMap();
        }
    }
}
