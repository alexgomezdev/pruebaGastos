using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinoProjectWeb.Data;
using MinoProjectWeb.Models;

namespace MinoProjectWeb.Controllers
{
    public class IngresoGastoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngresoGastoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IngresoGastoes
        public async Task<IActionResult> Index(int? mes, int? year)
        {
            if (mes == null)
            {
                mes = DateTime.Now.Month;
            }

            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            ViewData["mes"] = mes;
            ViewData["year"] = year;

            var applicationDbContext = _context.IngresoGastos.Include(i => i.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IngresoGastoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresoGasto = await _context.IngresoGastos
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.id == id);
            if (ingresoGasto == null)
            {
                return NotFound();
            }

            return View(ingresoGasto);
        }

        // GET: IngresoGastoes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "id", "Nombre");
            return View();
        }

        // POST: IngresoGastoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CategoriaId,Fecha,Valor")] IngresoGasto ingresoGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingresoGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "id", "Nombre", ingresoGasto.CategoriaId);
            return View(ingresoGasto);
        }

        // GET: IngresoGastoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresoGasto = await _context.IngresoGastos.FindAsync(id);
            if (ingresoGasto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "id", "Nombre", ingresoGasto.CategoriaId);
            return View(ingresoGasto);
        }

        // POST: IngresoGastoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,CategoriaId,Fecha,Valor")] IngresoGasto ingresoGasto)
        {
            if (id != ingresoGasto.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingresoGasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresoGastoExists(ingresoGasto.id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "id", "Nombre", ingresoGasto.CategoriaId);
            return View(ingresoGasto);
        }

        // GET: IngresoGastoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresoGasto = await _context.IngresoGastos
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.id == id);
            if (ingresoGasto == null)
            {
                return NotFound();
            }

            return View(ingresoGasto);
        }

        // POST: IngresoGastoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingresoGasto = await _context.IngresoGastos.FindAsync(id);
            _context.IngresoGastos.Remove(ingresoGasto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngresoGastoExists(int id)
        {
            return _context.IngresoGastos.Any(e => e.id == id);
        }
    }
}
