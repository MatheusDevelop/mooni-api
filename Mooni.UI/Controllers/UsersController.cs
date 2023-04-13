using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mooni.Domain.Entities;
using Mooni.Domain.InputModels.User;
using Mooni.Domain.Repositories;
using Mooni.Domain.ViewModels.User;
using Mooni.Infrastructure.Context;

namespace Mooni.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CrudController<User,User,CreateUserInputModel>
    {
        private readonly MooniContext _context;

        public UsersController(IBaseRepository<User> repository, IMapper mapper, MooniContext context) : base(repository, mapper)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInitialDataViewModel>>GetById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id.ToString() == id);
            if (user is null)
                return BadRequest("User not founded");
            return Ok(new UserInitialDataViewModel(user.Id.ToString(),user.FirstName,user.LastName,user.AvatarUrl));
        }
    }
}
