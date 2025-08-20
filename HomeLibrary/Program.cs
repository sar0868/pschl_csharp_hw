// See https://aka.ms/new-console-template for more information

string[,] books = new string[5, 4];
for (int i = 0; i < 5; i++)
{
    var book = AddBook();
    books[i, 0] = book[0];
    books[i, 1] = book[1];
    books[i, 2] = book[2];
    books[i, 3] = book[3];
}

Console.WriteLine("Библиотека:");
for (int i = 0; i < 5; i++)
{
    string[] book = [books[i, 0], books[i, 1], books[i, 2], books[i, 3]];
    PrintInfo(book);
}


static string[] AddBook()
{
    string title;
    string author;
    string year;
    string ISBN;

    Console.Write("Название: ");
    title = Console.ReadLine();

    Console.Write("Автор: ");
    author = Console.ReadLine();

    Console.Write("Год издания: ");
    year = Console.ReadLine();

    Console.Write("ISBN: ");
    ISBN = Console.ReadLine();

    return new string[] { title, author, year, ISBN };
}

static void PrintInfo(string[] book)
{
    Console.WriteLine($"Название {book[0]}, Автор {book[1]}, Год издания {book[2]}, ISBN {book[3]}");
}

static void Menu()
{
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
}