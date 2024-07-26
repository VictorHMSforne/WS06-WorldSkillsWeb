using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MedicosController : Controller
    {
        private readonly AppDbContext _context;

        public MedicosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Medicos
        public IActionResult Index()
        {
            var medico = _context.Medicos.ToList();
            var medicos = medico.Where(f => f.Turno == "Manhã").ToList();
            return View(medicos);
        }
        public IActionResult IndexTarde()
        {
            var medico = _context.Medicos.ToList();
            var medicos = medico.Where(f => f.Turno == "Tarde").ToList();
            return View(medicos);
        }
        public IActionResult IndexNoite()
        {
            var medico = _context.Medicos.ToList();
            var medicos = medico.Where(f => f.Turno == "Noite").ToList();
            return View(medicos);
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }
        public IActionResult SelectTurno()
        {
            return View();
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,HoraEntrada,HoraSaida")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                if (medico.HoraEntrada >= new TimeSpan(6,0,0) && medico.HoraEntrada <= new TimeSpan(11,59,0) ||
                    medico.HoraSaida >= new TimeSpan(6, 0, 0) && medico.HoraSaida <= new TimeSpan(11, 59, 0))
                {
                    medico.Turno = "Manhã";
                    _context.Add(medico);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(SelectTurno));
                }
                else if (medico.HoraEntrada >= new TimeSpan(12, 0, 0) && medico.HoraEntrada <= new TimeSpan(17, 59, 0) ||
                         medico.HoraSaida >= new TimeSpan(12, 0, 0) && medico.HoraSaida <= new TimeSpan(17, 59, 0))
                {
                    medico.Turno = "Tarde";
                    _context.Add(medico);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(SelectTurno));
                }
                else
                {
                    medico.Turno = "Noite";
                    _context.Add(medico);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(SelectTurno));
                }
                
            }
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,HoraEntrada,HoraSaida")] Medico medico)
        {
            if (id != medico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id))
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
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medicos == null)
            {
                return Problem("Entity set 'AppDbContext.Medicos'  is null.");
            }
            var medico = await _context.Medicos.FindAsync(id);
            if (medico != null)
            {
                _context.Medicos.Remove(medico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
          return _context.Medicos.Any(e => e.Id == id);
        }
    }
}
