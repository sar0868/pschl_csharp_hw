using System;

namespace HomeLibrary;

public class Library
{
    private List<Book> _books;
    public Library()
    {
        _books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public bool RemoveBook(Book book)
    {
        return _books.Remove(book);
    }

    public void FindBookToTitle(string title)
    {

    }

    // private Book GetBook(Book book)
    // {
    //     return _books.Find(book);
    // }
}
