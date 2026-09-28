using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.DependencyInjections.Interfaces;
using Noted.Models;

namespace Noted.DependencyInjections.Services
{
    public class NotesServices : INotesServices
    {
        private readonly ApplicationDbContext _context;

        public NotesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notes>> GetAllAsync()
        {
            return await _context.Notes
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<Notes?> GetByIdAsync(Guid id)
        {
            return await _context.Notes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Notes note)
        {
            if (string.IsNullOrWhiteSpace(note.Title))
                throw new ArgumentException("Title is required.");

            //if (string.IsNullOrWhiteSpace(note.Description))
            //    throw new ArgumentException("Description is required.");


            if (string.IsNullOrWhiteSpace(note.Content))
                throw new ArgumentException("Content is required.");

            //if (string.IsNullOrWhiteSpace(note.Category))
            //    throw new ArgumentException("Category is required.");

            note.Id = Guid.NewGuid();
            note.CreatedDate = DateTime.Now;
            note.UpdateDate = DateTime.Now;

            _context.Notes.Add(note);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notes note)
        {
            var existingNote = await _context.Notes
                .FirstOrDefaultAsync(x => x.Id == note.Id);

            if (existingNote == null)
                throw new KeyNotFoundException("Note not found!.");

            if (string.IsNullOrWhiteSpace(note.Title))
                throw new ArgumentException("Title is required.");

            //if (string.IsNullOrWhiteSpace(note.Description))
            //    throw new ArgumentException("Description is required.");


            if (string.IsNullOrWhiteSpace(note.Content))
                throw new ArgumentException("Content is required.");

            //if (string.IsNullOrWhiteSpace(note.Category))
            //    throw new ArgumentException("Category is required.");

            existingNote.Title = note.Title;
            existingNote.Description = note.Description;
            existingNote.Content = note.Content;
            existingNote.Category = note.Category;
            existingNote.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
                throw new KeyNotFoundException("Note not found!.");

            _context.Notes.Remove(note);

            await _context.SaveChangesAsync();
        }
    }
}
