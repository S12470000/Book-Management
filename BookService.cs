using Book_Manag.Models;
using Book_Manag.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book_Manag.Services
{
    public delegate void BookAddedEventHandler(object sender, BookEventArgs e);

    // Removed duplicate delegate definition

    public class BookEventArgs : EventArgs
    {
        public Book Book { get; }

        public BookEventArgs(Book book)
        {
            Book = book;
        }
    }

    public class BookService : IBookService
    {
        public event BookAddedEventHandler? BookAdded; // Declare the event as nullable to resolve CS8618

        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            OnBookAdded(book);
            return book;
        }

        public async Task<bool> UpdateAsync(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublishedYear = updatedBook.PublishedYear;
            book.Genre = updatedBook.Genre;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddBookAsync(Book book)
        {
            await CreateAsync(book);
        }

        protected virtual void OnBookAdded(Book book)
        {
            BookAdded?.Invoke(this, new BookEventArgs(book));
        }
    }
}

 