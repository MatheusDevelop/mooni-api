using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mooni.Domain.Entities;
using Mooni.Domain.InputModels.Category;
using Mooni.Domain.Repositories;
using Mooni.Domain.ViewModels.Category;

namespace Mooni.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CrudController<Category, CategoryViewModel, CreateCategoryInputModel>
    {
        public CategoriesController(IBaseRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
