using CRUD.Models;
using CRUD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _service;

        public ItemController(ItemService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetById(string id)
        {
            var item = await _service.GetByIdAsync(id);
            return item is null ? NotFound("Item não encontrado.") : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Create(Item item)
        {
            var criado = await _service.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Item item)
        {
            var existente = await _service.GetByIdAsync(id);
            if (existente is null) return NotFound("Item não encontrado.");

            item.Id = id;
            item.CriadoEm = existente.CriadoEm;
            await _service.UpdateAsync(id, item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existente = await _service.GetByIdAsync(id);
            if (existente is null) return NotFound("Item não encontrado.");

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
