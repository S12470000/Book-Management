using Book_Manag.Models;

public class BookEventArgs : EventArgs
{
    public Book Book { get; }
    public BookEventArgs(Book book) => Book = book;
}