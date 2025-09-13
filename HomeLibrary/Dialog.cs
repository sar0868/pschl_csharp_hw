using System;

namespace HomeLibrary;

public static class Dialog
{
    public static void Menu()
    {
        Console.WriteLine("\n==============");
        Console.WriteLine("Меню:");
        Console.WriteLine("1. Добавить книгу");
        Console.WriteLine("2. Показать книгу");
        Console.WriteLine("3. Список библиотеки");
        Console.WriteLine("4. Изменить запись о книге");
        Console.WriteLine("5. Выйти");
    }

    public static Book AddBook()
    {
        while (true)
        {
            Console.Write("\nНазвание: ");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Название обязательно!");
                continue;
            }

            Console.Write("Автор: ");
            string? author = Console.ReadLine();

            Console.Write("Год издания: ");
            string? year = Console.ReadLine();

            Console.Write("ISBN: ");
            string? ISBN = Console.ReadLine();

            Console.Write("Comment: ");
            string? comment = Console.ReadLine();

            Console.Write("Читал - 1, Нет - любая клавиша: ");
            string? userInput = Console.ReadLine();
            bool isRead = userInput == "1";
            return new Book(title, author, year, ISBN, comment, isRead);
        }
    }

    public static void FindBookByOptions(Library library)
    {
        (Field option, string? look) = MenuChoiceFindOption(library);
        if (option == Field.Exit)
        {
            return;
        }
        GetFindResult(library, option, look);       
    }

    private static (Field, string?) MenuChoiceFindOption(Library library)
    {
        if (library.IsEmptyLibrary())
        {
            return (Field.Exit, "");
        }
        while (true)
        {
            Console.WriteLine("\nУкажите параметр поиска книги:");
            Console.WriteLine("1. По названию");
            Console.WriteLine("2. По автору");
            Console.WriteLine("3. По году издания");
            Console.WriteLine("4. По ISBN");
            Console.WriteLine("5. Вернутся в меню.");

            string? userChoice = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userChoice)
            || !int.TryParse(userChoice, out int userInput)
            || userInput < 1
            || userInput > 5)
            {
                Console.WriteLine("Не верный ввод. Введите число от 1 до 5.");
                continue;
            }
            if (userInput == 5)
            {
                return (Field.Exit, "");
            }
            (Field option, string? look) = SearchOption(userInput);
            return (option, look);
        }
    }

    private static void GetFindResult(Library library, Field findField, string look)
    {
        (List<Book> books, string msg) = library.FindBook(findField, look);
        Console.WriteLine(library.GetStringResultFind(books, msg));
    }

    private static (Field, string?) SearchOption(int userInput)
    {
        Field option;
        switch (userInput)
        {
            case 1:
                option = Field.Title;
                Console.WriteLine("Укажите название:");
                break;
            case 2:
                option = Field.Author;
                Console.WriteLine("Укажите автора:");
                break;
            case 3:
                option = Field.Year;
                Console.WriteLine("Укажите год издания:");
                break;
            default:
                option = Field.ISBN;
                Console.WriteLine("Укажите ISBN:");
                break;
        }
        string? look = Console.ReadLine();
        return (option, look);
    }

    internal static void EditBook(Library library)
    {
        (Field option, string? look) = MenuChoiceFindOption(library);
        if (option == Field.Exit)
        {
            return;
        }
        (var books, _) = library.FindBook(option, look);
        ShowSelectedBooks(books);
        Book book = GetSelectBook(books);
        ChangeBook(library, book);
    }

    private static void ChangeBook(Library library, Book book)
    {
        while (true)
        {
            Console.WriteLine("\nУкажите новые данные:");
            Console.Write($"\nНазвание ({book.Title}): ");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                title = book.Title;
            }

            Console.Write($"Автор ({book.Author}): ");
            string? author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
            {
                author = book.Author;
            }

            Console.Write($"Год издания ({book.Year}): ");
            string? year = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(year))
            {
                year = book.Year;
            }

            Console.Write($"ISBN ({book.ISBN}): ");
            string? ISBN = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ISBN))
            {
                ISBN = book.ISBN;
            }
            Console.Write($"Comment ({book.Comment}): ");
            string? comment = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(comment))
            {
                comment = book.Comment;
            }

            Console.Write("Читал - 1, Нет - любая клавиша: ");
            string? userInput = Console.ReadLine();
            bool isRead = userInput == "1";
            Book newBook = new Book(title, author, year, ISBN, comment, isRead);
            if (newBook == book)
            {
                Console.WriteLine("Вы не внесли изменния в запись.");
                continue;
            }
            library.RemoveBook(book);
            library.AddBook(newBook);
            Console.WriteLine($"Книга \n\t{book}\nИзменена \n\t{newBook}");
            break;
        }
    }

    private static Book GetSelectBook(List<Book> books)
    {
        while (true)
        {
            Console.WriteLine("Выберете номер изменяемой книги.");
            string? userInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(userInput)
             && int.TryParse(userInput, out int userChoice)
             && userChoice >= 1
             && userChoice < books.Count + 1)
            {
                return books[userChoice - 1];
            }
            Console.WriteLine("Неверный ввод. Повторите выбор.");
        }
        
    }

    private static void ShowSelectedBooks(List<Book> books)
    {
        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {books[i]}");
        }
    }
}
