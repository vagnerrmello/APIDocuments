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
    public class DocumentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Documento>> Get()
        {
            try
            {
                return _context.Documentos.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao tentar obter o(s) Documento(s).");
            }
        }

        [HttpGet("{id}", Name = "ObterDocumento")]
        public async Task<ActionResult<Documento>> Get(int id)
        {
            try
            {
                var documento = await _context.Documentos.AsNoTracking().FirstOrDefaultAsync(c => c.DocumentoId == id);

                if (documento == null)
                    return NotFound($"Documento com id = {id} não foi encontrado.");

                return documento;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao tentar obter Usuario.");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Documento documento)
        {
            try
            {
                _context.Documentos.Add(documento);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterDocumento", new { id = documento.DocumentoId }, documento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo deu errado com a inserção.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Documento documento)
        {
            try
            {
                if (id != documento.DocumentoId)
                    return BadRequest($"Não foi possível encontrar o registro com o id = {id}");

                _context.Entry(documento).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo deu errado com atualização.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Documento> Delete(int id)
        {
            try
            {
                var documento = _context.Documentos.AsNoTracking().FirstOrDefault(c => c.DocumentoId == id);

                if (id != documento.DocumentoId)
                    return BadRequest($"Não foi possível encontrar o registro com o id = {id}");

                _context.Documentos.Remove(documento);
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
