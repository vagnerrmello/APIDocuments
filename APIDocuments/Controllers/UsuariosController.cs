using APIDocuments.Context;
using APIDocuments.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDocuments.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            try
            {
                return _context.Usuarios.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao tentar obter usuarios.");
            }
        }

        [HttpGet("{id}", Name = "ObterUsuario")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(c => c.UsuarioId == id);

                if (usuario == null)
                    return NotFound($"Usuário com id = {id} não foi encontrado.");

                return usuario;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao tentar obter Usuario.");
            }
        }

        [HttpGet("documentos")]
        public ActionResult<IEnumerable<Usuario>> GetUsuarioDocumentos()
        {
            return _context.Usuarios.Include(u => u.Documentos).ToList();
        }

        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        { 
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterUsuario", new { id = usuario.UsuarioId }, usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo deu errado com a inserção.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (id != usuario.UsuarioId)
                    return BadRequest($"Não foi possível encontrar o registro com o id = {id}");

                _context.Entry(usuario).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo deu errado com atualização.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Usuario> Delete(int id)
        {
            try
            {
                var usuario = _context.Usuarios.AsNoTracking().FirstOrDefault(c => c.UsuarioId == id);

                if (id != usuario.UsuarioId)
                    return BadRequest($"Não foi possível encontrar o registro com o id = {id}");

                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo deu errado com a deleção.");
            }
        }

    }
}
