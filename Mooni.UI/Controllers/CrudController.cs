using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mooni.Domain.Repositories;
using Mooni.Infrastructure.Context;
using System.Net;

namespace Mooni.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CrudController<TEntity, ViewModel, InputModel> : ControllerBase where TEntity : Domain.Entities.Entity
    {
        public readonly IBaseRepository<TEntity> _repository;
        public readonly IMapper _mapper;
        protected CrudController(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(InputModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            if (entity == null) return StatusCode(500);
            await _repository.Create(entity);
            return StatusCode(201, new { entity.Id });
        }
        [HttpGet]
        public virtual async Task<ActionResult<List<ViewModel>>> Get()
        {
            var entities = await _repository.ReadAll(e=> true);
            List<ViewModel> models = _mapper.Map<List<ViewModel>>(entities);
            return StatusCode(200, models);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.Delete(id);
            return Ok();
        }
    }
}
