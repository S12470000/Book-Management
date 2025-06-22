using Book_Manag.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book_Manag.Services
{
    //public delegate void BookAddedEventHandler(object sender, BookEventArgs e);

    public interface IBookService
    {
        event BookAddedEventHandler BookAdded;
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task<bool> UpdateAsync(int id, Book updatedBook);
        Task<bool> DeleteAsync(int id);
        Task AddBookAsync(Book book);
    }
}

