using System.Text;

namespace HomeLibrary;

internal class Library
{
    private List<Book> _books;
    internal Library()
    {
        _books = new List<Book>();
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

    internal void AddBook(Book book)
    {
        _books.Add(book);
    }

    internal bool RemoveBook(Book book)
    {
        return _books.Remove(book);
    }

    internal (List<Book>, string) FindBook(Field findField, string value)
    {
        return findField switch
        {
            Field.Title => FindBooksByTitle(value),
            Field.Author => FindBooksByAuthor(value),
            Field.Year => FindBooksByYear(value),
            Field.ISBN => FindBooksByISBN(value),
            _ => (new List<Book>(), "")
        };
    }

    internal void ShowLibrary()
    {
        if (IsEmptyLibrary())
        {
            return;
        }
        Console.WriteLine("Библиотека:");
        Console.WriteLine(this);
    }

    internal bool IsEmpty()
    {
        return _books.Count == 0;
    }

    internal bool IsEmptyLibrary()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Библиотека пуста.");
            return true;
        }
        return false;
    }
   
    internal string GetStringResultFind(List<Book> books, string msg)
    {
        if (books.Count == 0)
        {
            return $"{msg} не найдены.";
        }
        StringBuilder result = new();
        foreach (Book book in books)
        {
            result.AppendLine(book.ToString());
        }
        return $"{msg}:\n{result}";
    }

    internal bool IsThereBookTitleAuthorYear(string title, string author, int? year)
    {
        foreach (Book item in _books)
        {
            if (item.Title == title
                && item.Author == author
                && item.Year == year)
            {
                return true;
            }
        }
        return false;
    }

    internal bool IsISBN(string ISBN)
    {
        foreach (Book item in _books)
        {
            if (item.ISBN == ISBN)
            {
                return true;
            }
        }
        return false;
    }

    internal List<Book> GetBooks()
    {
        return _books;
    }
    private (List<Book>, string) FindBooksByTitle(string value)
    {
        string msg = $"Книги с названием {value}";
        List<Book> result = new List<Book>();
        foreach (Book book in _books)
        {
            if (book.Title != null
                && book.Title.Contains(value))
            {
                result.Add(book);
            }
        }
        return (result, msg);
    }

    private (List<Book>, string) FindBooksByAuthor(string value)
    {
        string msg = $"Книги автора {value}";
        List<Book> result = new List<Book>();
        foreach (Book book in _books)
        {
            if (book.Author != null
                && book.Author.Contains(value))
            {
                result.Add(book);
            }
        }
        return (result, msg);
    }

    private (List<Book>, string) FindBooksByYear(string value)
    {
        int? year;
        try
        {
            year = int.Parse(value);
        }
        catch (FormatException)
        {
            throw new YearExceptions("Год издания должен содержать только цифры!");
        }
        if (DateTime.Now.Year < year)
        {
            throw new YearExceptions("Год издания не может быть позднее текущего года!");

        }
        string msg = $"Книги {value} года издания";
        List<Book> result = new List<Book>();
        foreach (Book book in _books)
        {
            if (book.Year != null
                && book.Year == year)
            {
                result.Add(book);
            }
        }
        return (result, msg);
        }

    private (List<Book>, string) FindBooksByISBN(string value)
    {
        if (value.Length != 13
            || !Dialog.ContainsDigits(value))
        {
            throw new ISBNException("ISBN неверен!"); 
        }
        string msg = $"Книги ISBN {value}";
        List<Book> result = new List<Book>();
        foreach (Book book in _books)
        {
            if (book.ISBN != null
                && book.ISBN.Contains(value))
            {
                result.Add(book);
            }
        }
        return (result, msg);
    }
 
}
