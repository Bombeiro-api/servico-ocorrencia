using Microsoft.AspNetCore.Mvc;
using Ocorrencias.DTO;
using Ocorrencias.Servicos;

namespace Ocorrencias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcorrenciaController : ControllerBase
    {
        private readonly ServOcorrencia _servico;

        public OcorrenciaController(ServOcorrencia servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ocorrencia>>> Listar()
        {
            return Ok(await _servico.Listar());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ocorrencia>> BuscarPorId(int id)
        {
            var ocorrencia = await _servico.BuscarPorId(id);

            if (ocorrencia == null)
                return NotFound();

            return Ok(ocorrencia);
        }

        [HttpPost]
        public async Task<ActionResult<Ocorrencia>> Criar(Ocorrencia ocorrencia)
        {
            var nova = await _servico.Criar(ocorrencia);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = nova.Id },
                nova
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Ocorrencia ocorrencia)
        {
            var atualizado = await _servico.Atualizar(id, ocorrencia);

            if (!atualizado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var excluido = await _servico.Excluir(id);

            if (!excluido)
                return NotFound();

            return NoContent();
        }
    }
}