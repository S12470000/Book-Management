using Microsoft.AspNetCore.Mvc;
using Book_Manag.Models;
using Book_Manag.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Book_Manag.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
            _bookService.BookAdded += OnBookAdded;
        }

        private void OnBookAdded(object sender, Services.BookEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnBookAdded(object sender, BookEventArgs e)
        {
            // Custom logic, e.g., logging or sending notifications
            Console.WriteLine($"Book added: {e.Book.Title}");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            var createdBook = await _bookService.CreateAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book updatedBook)
        {
            var result = await _bookService.UpdateAsync(id, updatedBook);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
