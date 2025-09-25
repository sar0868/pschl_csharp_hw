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
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Автор обязателен!");
                continue;
            }
            Console.Write("Год издания: ");
            string? inputYear = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputYear))
            {
                Console.WriteLine("Год издания обязателен!");
                continue;
            }
            if (!ContainsDigits(inputYear))
            {
                Console.WriteLine("Год издания должен содержать только цифры!"); 
                continue;
            }
            int? year = int.Parse(inputYear);
            if (DateTime.Now.Year < year)
            {
                Console.WriteLine("Год издания не может быть позднее текущего года!");
                continue;
            }
            // проверка на совпадения названия, автора и года издания
            Console.Write("ISBN: ");
            string? ISBN = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ISBN))
            {
                Console.WriteLine("ISBN обязателен!");
                continue;
            }
            if (ISBN.Length != 13
                || !ContainsDigits(ISBN))
            {
                Console.WriteLine("ISBN неверен!"); // должен содержать 13 цифр, проверку на имеющийся ISBN
                continue;
            }
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
        while (true)
        {
            (Field option, string? look) = MenuChoiceFindOption(library);
            if (option == Field.Exit)
            {
                return;
            }
            try
            {
                GetFindResult(library, option, look);
            }
            catch (YearExceptions ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            catch (ISBNException ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            break;
        }

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
        while (true)
        {
            (Field option, string? look) = MenuChoiceFindOption(library);
            if (option == Field.Exit)
            {
                return;
            }
            List<Book> books = new();
            try
            {
                (books, _) = library.FindBook(option, look);
            }
            catch (YearExceptions ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            catch (ISBNException ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            ShowSelectedBooks(books);
            Book book = GetSelectBook(books);
            ChangeBook(library, book);
            break;
        }
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
            string? inputYear = Console.ReadLine();
            int? year = 0;
            if (string.IsNullOrWhiteSpace(inputYear))
            {
                year = book.Year;
            }
            else
            {
                try
                {
                    year = int.Parse(inputYear);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Неверно указан год: {inputYear}");
                    continue;
                }
                if (DateTime.Now.Year < year)
                {
                    Console.WriteLine("Год издания не может быть позднее текущего года!");
                    continue;
                }
            }
            Console.Write($"ISBN ({book.ISBN}): ");
            string? ISBN = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ISBN))
            {
                ISBN = book.ISBN;
            }
            else
            {
                if (ISBN.Length != 13
                    || !ContainsDigits(ISBN))
                {
                    Console.WriteLine("ISBN неверен!"); 
                    continue;
                }
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
            //проверка на совпадение ISBN, 
            // а также в случае полного совпадения имени автора, названия и года издания
            // кроме совпадения с изменяемой книгой

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
    
    internal static bool ContainsDigits(string input)
    {
        return input.All(char.IsDigit);
    }
}
