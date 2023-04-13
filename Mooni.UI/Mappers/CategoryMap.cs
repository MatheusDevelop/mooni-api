using AutoMapper;
using Mooni.Domain.Entities;
using Mooni.Domain.InputModels.Category;
using Mooni.Domain.ViewModels.Category;

namespace Mooni.UI.Mappers
{
    public class CategoryMap:Profile
    {
        public CategoryMap()
        {
            CreateMap<CreateCategoryInputModel, Category>();
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
