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
        if (library.IsEmptyLibrary())
        {
            return;
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
                return;
            }
            (Field option, string look) = SearchOption(userInput);    
            GetFindResult(library, option, look);
        }
    }

    private static void GetFindResult(Library library, Field findField, string look)
    {
       Console.WriteLine(library.FindBook(findField, look));
    }

    private static (Field, string) SearchOption(int userInput)
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
        string look = Console.ReadLine();
        return (option, look);
    }

//     internal static void AddComment()
//     {
// // Выбрать книгу и добавить комментарий(создать новую запись и удалить старую)
//         // throw new NotImplementedException();
//     }

//     internal static void AddIsRead()
//     {
//         // Выбрать книгу и добавить статус(создать новую запись и удалить старую)
//         // throw new NotImplementedException();
//     }

    internal static void EditBook()
    {
    // Выбрать книгу и изменить запись(создать новую запись и удалить старую)
    }
}
