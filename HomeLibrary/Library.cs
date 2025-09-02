using System.Text;

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

    public string FindBook(Field findField, string value)
    {
        return findField switch
        {
            Field.Title => FindBooksByTitle(value),
            Field.Author => FindBooksByAuthor(value),
            Field.Year => FindBooksByYear(value),
            Field.ISBN => FindBooksByISBN(value),
            _ => ""
        };

    }

    private string FindBooksByTitle(string value)
    {
        StringBuilder result = new();
        foreach (Book book in _books)
        {
            if (book.Title.Contains(value))
            {
                result.AppendLine(book.ToString());
            }
        }
        if (result.ToString().Length > 0)
        {
            return $"Книги с названием {value}:\n{result.ToString()}";

        }
        return $"Книги с названием {value} не найдены.";
    }
    private string FindBooksByAuthor(string value)
    {
        StringBuilder result = new();
        foreach (Book book in _books)
        {
            if (book.Author.Contains(value))
            {
                result.AppendLine(book.ToString());
            }
        }
        if (result.ToString().Length > 0)
        {
            return $"Книги автора {value}:\n{result.ToString()}";

        }
        return $"Книги автора {value} не найдены.";
    }
    private string FindBooksByYear(string value)
    {
        StringBuilder result = new();
        foreach (Book book in _books)
        {
            if (book.Year == value)
            {
                result.AppendLine(book.ToString());
            }
        }
        if (result.ToString().Length > 0)
        {
            return $"Книги {value} года издания:\n{result.ToString()}";

        }
        return $"Книги {value} года издания не найдены.";
    }
    private string FindBooksByISBN(string value)
    {
        StringBuilder result = new();
        foreach (Book book in _books)
        {
            if (book.ISBN.Contains(value))
            {
                result.AppendLine(book.ToString());
            }
        }
        if (result.ToString().Length > 0)
        {
            return $"Книги ISBN {value}:\n{result.ToString()}";

        }
        return $"Книги ISBN {value} не найдены.";
    }




    public bool IsEmpty()
    {
        return _books.Count == 0;
    }

    public override string ToString()
    {
        StringBuilder result = new();
        foreach (Book book in _books)
        {
            result.AppendLine(book.ToString());
        }
        return result.ToString();
    }
}
