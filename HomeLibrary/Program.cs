// See https://aka.ms/new-console-template for more information


Console.WriteLine("Меню:");
Console.WriteLine("1. Добавить книгу");
Console.WriteLine("2. Показать книгу");
Console.WriteLine("3. Выйти");

int answer = int.Parse(Console.ReadLine());

switch (answer)
{
    case 1:
        AddBook();
        break;
    case 2:
        Console.WriteLine("Показанные книги.");
        break;
    case 3:
        Console.WriteLine("Выход");
        break;
    default:
        Console.WriteLine("Повторите выбор.");
        break;
}


static void AddBook()
{
    string title;
    string author;
    int year;
    string ISBN;

    Console.Write("Название: ");
    title = Console.ReadLine();

    Console.Write("Автор: ");
    author = Console.ReadLine();

    Console.Write("Год издания: ");
    var year_str = Console.ReadLine();
    year = int.Parse(year_str);

    Console.Write("ISBN: ");
    ISBN = Console.ReadLine();

    Console.WriteLine($"Название {title}");
    Console.WriteLine($"Автор {author}");
    Console.WriteLine($"Год издания {year}");
    Console.WriteLine($"ISBN {ISBN}");
}