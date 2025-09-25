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

    public (List<Book>, string) FindBook(Field findField, string value)
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



    public string GetStringResultFind(List<Book> books, string msg)
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

    private (List<Book>, string) FindBooksByTitle(string value)
    {
        string msg = $"Книги с названием {value}";
        List<Book> result = new List<Book>();
        foreach (Book book in _books)
        {
            if ( book.Title != null
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

    public void ShowLibrary()
    {
        if (IsEmptyLibrary())
        {
            return;
        }
        Console.WriteLine("Библиотека:");
        Console.WriteLine(this);
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

    public bool IsEmptyLibrary()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Библиотека пуста.");
            return true;
        }
        return false;
    }
    
}
