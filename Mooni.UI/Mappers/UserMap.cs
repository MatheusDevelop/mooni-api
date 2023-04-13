using AutoMapper;
using Mooni.Domain.Entities;
using Mooni.Domain.InputModels.User;

namespace Mooni.UI.Mappers
{
    public class UserMap:Profile
    {
        public UserMap()
        {
            CreateMap<CreateUserInputModel, User>();
        }
    }
}
