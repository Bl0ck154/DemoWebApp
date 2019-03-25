using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.AppContext;
using WebApplication.Entities;

namespace WebApplication.Controllers
{
    public class StudentMarksController : Controller
    {
        private readonly AppDbContext _context;

        public StudentMarksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StudentMarks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StudentMark.Include(s => s.Mark).Include(s => s.Student);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StudentMarks
        public async Task<IActionResult> Rating()
        {
            var appDbContext = _context.StudentMark
                .Include(s => s.Mark)
                .Include(s => s.Student)
                .GroupBy(s => s.Student)
                .Select(s => new StudentMark
                {
                    Id = s.Key.Id,
                    Student = s.Key,
                    Mark = new Mark() { Id = 0, Value = (int)s.Average(v => v.Mark.Value) }
                });
            return View(await appDbContext.ToListAsync());
        }

        // GET: StudentMarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentMark = await _context.StudentMark
                .Include(s => s.Mark)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentMark == null)
            {
                return NotFound();
            }

            return View(studentMark);
        }

        // GET: StudentMarks/Create
        public IActionResult Create()
        {
            ViewData["MarkId"] = new SelectList(_context.Marks, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: StudentMarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,MarkId")] StudentMark studentMark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarkId"] = new SelectList(_context.Marks, "Id", "Id", studentMark.MarkId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", studentMark.StudentId);
            return View(studentMark);
        }

        // GET: StudentMarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentMark = await _context.StudentMark.FindAsync(id);
            if (studentMark == null)
            {
                return NotFound();
            }
            ViewData["MarkId"] = new SelectList(_context.Marks, "Id", "Id", studentMark.MarkId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", studentMark.StudentId);
            return View(studentMark);
        }

        // POST: StudentMarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,MarkId")] StudentMark studentMark)
        {
            if (id != studentMark.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentMarkExists(studentMark.StudentId))
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
            ViewData["MarkId"] = new SelectList(_context.Marks, "Id", "Id", studentMark.MarkId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", studentMark.StudentId);
            return View(studentMark);
        }

        // GET: StudentMarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentMark = await _context.StudentMark
                .Include(s => s.Mark)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentMark == null)
            {
                return NotFound();
            }

            return View(studentMark);
        }

        // POST: StudentMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentMark = await _context.StudentMark.FindAsync(id);
            _context.StudentMark.Remove(studentMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentMarkExists(int id)
        {
            return _context.StudentMark.Any(e => e.StudentId == id);
        }
    }
}
