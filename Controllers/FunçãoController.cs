using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC1.Data;
using MVC1.Models;

namespace MVC1.Controllers
{
    public class FunçãoController : Controller
    {
        private readonly MVC1Context _context;

        public FunçãoController(MVC1Context context)
        {
            _context = context;
        }

        // GET: Função
        public async Task<IActionResult> Index()
        {
            var mVC1Context = _context.Função.Include(f => f.Empresa);
            return View(await mVC1Context.ToListAsync());
        }

        // GET: Função/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var função = await _context.Função
                .Include(f => f.Empresa)
                .FirstOrDefaultAsync(m => m.FunçãoID == id);
            if (função == null)
            {
                return NotFound();
            }

            return View(função);
        }

        // GET: Função/Create
        public IActionResult Create()
        {
            ViewData["EmpresaID"] = new SelectList(_context.Set<Empresa>(), "EmpresaID", "EmpresaID");
            return View();
        }

        // POST: Função/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FunçãoID,Cargo,EmpresaID")] Função função)
        {
            if (ModelState.IsValid)
            {
                _context.Add(função);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaID"] = new SelectList(_context.Set<Empresa>(), "EmpresaID", "EmpresaID", função.EmpresaID);
            return View(função);
        }

        // GET: Função/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var função = await _context.Função.FindAsync(id);
            if (função == null)
            {
                return NotFound();
            }
            ViewData["EmpresaID"] = new SelectList(_context.Set<Empresa>(), "EmpresaID", "EmpresaID", função.EmpresaID);
            return View(função);
        }

        // POST: Função/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FunçãoID,Cargo,EmpresaID")] Função função)
        {
            if (id != função.FunçãoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(função);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunçãoExists(função.FunçãoID))
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
            ViewData["EmpresaID"] = new SelectList(_context.Set<Empresa>(), "EmpresaID", "EmpresaID", função.EmpresaID);
            return View(função);
        }

        // GET: Função/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var função = await _context.Função
                .Include(f => f.Empresa)
                .FirstOrDefaultAsync(m => m.FunçãoID == id);
            if (função == null)
            {
                return NotFound();
            }

            return View(função);
        }

        // POST: Função/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var função = await _context.Função.FindAsync(id);
            _context.Função.Remove(função);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FunçãoExists(int id)
        {
            return _context.Função.Any(e => e.FunçãoID == id);
        }
    }
}
