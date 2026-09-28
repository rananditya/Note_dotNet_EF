using Microsoft.AspNetCore.Mvc;
using Noted.Data;
using Noted.Models;
using Microsoft.EntityFrameworkCore;
using Noted.DependencyInjections.Interfaces;

namespace Noted.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotesServices _notesServices;

        public NotesController(INotesServices notesServices)
        {
           _notesServices = notesServices;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var notes = await _notesServices.GetAllAsync();

            return View(notes);
        }

        // GET: Notes/Details/{id}
        public IActionResult Details(Guid id)
        {
            return View();
        }

        // GET:Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notes note)
        {
            if (!ModelState.IsValid)
                return View(note);

            try
            {
                await _notesServices.CreateAsync(note);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(note);
            }

        }

        // GET: Notes/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var note = await _notesServices.GetByIdAsync(id);

            if (note == null)
                return NotFound();

            return View(note);
        }

        // POST: Notes/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Notes note)
        {
            if (!ModelState.IsValid)
                return View(note);

            try
            {
                note.Id = id;

                await _notesServices.UpdateAsync(note);

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(note);
            }
        }

        // GET: Notes/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var note = await _notesServices.GetByIdAsync(id);

            if (note == null)
                return NotFound();

            return View(note);
        }

        // POST: Notes/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                await _notesServices.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
