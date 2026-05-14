using CRUD.Models;
using CRUD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service) => _service = service;

        
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetAll() =>
            Ok(await _service.GetAllAsync());

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(string id)
        {
            var usuario = await _service.GetByIdAsync(id);
            return usuario is null ? NotFound("Usuário não encontrado.") : Ok(usuario);
        }

        
        [HttpGet("email/{email}")]
        public async Task<ActionResult<Usuario>> GetByEmail(string email)
        {
            var usuario = await _service.GetByEmailAsync(email);
            return usuario is null ? NotFound("Usuário não encontrado.") : Ok(usuario);
        }

       
        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario usuario)
        {
            var existente = await _service.GetByEmailAsync(usuario.Email);
            if (existente is not null)
                return Conflict("Já existe um usuário com esse email.");

            var criado = await _service.CreateAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Usuario usuario)
        {
            var existente = await _service.GetByIdAsync(id);
            if (existente is null) return NotFound("Usuário não encontrado.");

            usuario.Id = id;
            usuario.CriadoEm = existente.CriadoEm;
            await _service.UpdateAsync(id, usuario);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existente = await _service.GetByIdAsync(id);
            if (existente is null) return NotFound("Usuário não encontrado.");

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
