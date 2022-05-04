#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteMVC.Data;
using TesteMVC.Entities;

namespace TesteMVC.Controllers
{
    public class AgendaTarefasController : Controller
    {
        private readonly ApplicationContext _context;

        public AgendaTarefasController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: AgendaTarefas
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgendaTarefas.ToListAsync());
        }

        // GET: AgendaTarefas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendaTarefas = await _context.AgendaTarefas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendaTarefas == null)
            {
                return NotFound();
            }

            return View(agendaTarefas);
        }

        // GET: AgendaTarefas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgendaTarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,Data,HoraInicio,HoraFim,Prioridade,IsFinalizada")] AgendaTarefas agendaTarefas)
        {
            if (agendaTarefas.HoraInicio < DateTime.Now)
            {
                return BadRequest("Hora inicio menor que atual");
            }
            if (agendaTarefas.HoraInicio.Hour > agendaTarefas.HoraInicio.AddHours(5).Hour)
            {
                return BadRequest("Hora da tarefa maior que 5 horas");
            }
            if (ModelState.IsValid)
            {
                _context.Add(agendaTarefas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agendaTarefas);
        }

        // GET: AgendaTarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var agendaTarefas = await _context.AgendaTarefas.FindAsync(id);
            if (agendaTarefas == null)
            {
                return NotFound();
            }
            return View(agendaTarefas);
        }

        // POST: AgendaTarefas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Data,HoraInicio,HoraFim,Prioridade,IsFinalizada")] AgendaTarefas agendaTarefas)
        {
            if (id != agendaTarefas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendaTarefas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendaTarefasExists(agendaTarefas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(agendaTarefas);
        }

        // GET: AgendaTarefas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendaTarefas = await _context.AgendaTarefas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendaTarefas == null)
            {
                return NotFound();
            }

            return View(agendaTarefas);
        }

        // POST: AgendaTarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agendaTarefas = await _context.AgendaTarefas.FindAsync(id);
            _context.AgendaTarefas.Remove(agendaTarefas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendaTarefasExists(int id)
        {
            return _context.AgendaTarefas.Any(e => e.Id == id);
        }
    }
}
