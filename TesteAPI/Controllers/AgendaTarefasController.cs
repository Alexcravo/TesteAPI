using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteAPI.Data;
using TesteAPI.Entities;

namespace TesteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaTarefasController : ControllerBase
    {
        private readonly ILogger<AgendaTarefasController> _logger;
        private readonly ApplicationContext _applicationContext;
        public AgendaTarefasController(ILogger<AgendaTarefasController> logger,
            ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        [HttpGet(Name = "GetCliente")]
        [ProducesResponseType(201, Type = typeof(AgendaTarefas))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            var listProdutos = await _applicationContext.AgendaTarefas.ToListAsync();
            return Ok(listProdutos);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AgendaTarefas))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Insert(AgendaTarefas agendaTarefas)
        {
            if(agendaTarefas.HoraInicio < DateTime.Now)
            {
                return BadRequest("Hora inicio menor que atual");
            }
            if(agendaTarefas.HoraInicio.Hour > agendaTarefas.HoraInicio.AddHours(5).Hour)
            {
                return BadRequest("Hora da tarefa maior que 5 horas");
            }
            _applicationContext.AgendaTarefas.Add(agendaTarefas);
            await _applicationContext.SaveChangesAsync();
            return Ok(agendaTarefas.Id);
        }

        [HttpPut]
        [ProducesResponseType(201, Type = typeof(AgendaTarefas))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, AgendaTarefas agendaTarefa)
        {
            var agenda = await _applicationContext.AgendaTarefas.FirstOrDefaultAsync(x => x.Id == id);
            if (agenda == null)
            {
                return NotFound("Tarefas não encontrado");
            }
            _applicationContext.Entry(agenda).CurrentValues.SetValues(agendaTarefa);
            await _applicationContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(201, Type = typeof(AgendaTarefas))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _applicationContext.AgendaTarefas.FirstOrDefaultAsync(x => x.Id == id);
            if (cliente != null)
            {
                _applicationContext.AgendaTarefas.Remove(cliente);
                await _applicationContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound("Tarefa não encontrado");
        }
    }
}
