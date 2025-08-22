// See https://aka.ms/new-console-template for more information


List<string[]> books = new();

while (true)
{
    Menu();
    string answer = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(answer)
        || !int.TryParse(answer, out int choice)
        || choice < 1
        || choice > 4)
        {
            Console.WriteLine("Не верный ввод. Введите число от 1 до 4.");
            continue;
        }
    switch (choice)
    {
        case 1:
            books.Add(AddBook());
            break;
        case 2:
            FindBook();
            break;
        case 3:
            ShowLibrary();
            break;
        default:
            Console.WriteLine("Выход");
            return;
    }
}


string[] AddBook()
{
    string title;
    string author;
    string year;
    string ISBN;

    Console.Write("\nНазвание: ");
    title = Console.ReadLine();

    Console.Write("Автор: ");
    author = Console.ReadLine();

    Console.Write("Год издания: ");
    year = Console.ReadLine();

    Console.Write("ISBN: ");
    ISBN = Console.ReadLine();

    return new string[] { title, author, year, ISBN };
}

void PrintInfo(string[] book)
{
    Console.WriteLine($"Название: {book[0]}, Автор: {book[1]}, Год издания: {book[2]}, ISBN: {book[3]}");
}

void Menu()
{
    Console.WriteLine("\n==============");
    Console.WriteLine("Меню:");
    Console.WriteLine("1. Добавить книгу");
    Console.WriteLine("2. Показать книгу");
    Console.WriteLine("3. Список библиотеки");
    Console.WriteLine("4. Выйти");
}

void ShowLibrary()
{
    if (IsEmptyLibrary())
    {
        return;
    }
    Console.WriteLine("Библиотека:");
    foreach (var item in books)
    {
        PrintInfo(item);
    }
}

void FindBook()
{
    if (IsEmptyLibrary())
    {
        return;
    }
    while (true)
    {
        Console.WriteLine("\nУкажите параметр поиска книги:");
        Console.WriteLine("1. По названию");
        Console.WriteLine("2. По автору");
        Console.WriteLine("3. По ISBN");
        Console.WriteLine("4. Вернутся в меню.");

        string userChoice = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(userChoice)
        || !int.TryParse(userChoice, out int userInput)
        || userInput < 1
        || userInput > 4)
        {
            Console.WriteLine("Не верный ввод. Введите число от 1 до 4.");
            continue;
        }
        if (userInput == 4)
        {
            return;
        }
        SearchOption(userInput);
    }

}

void SearchOption(int userInput)
{
    int findOption;
    string option;
    switch (userInput)
    {
        case 1:
            findOption = 0;
            option = "названием ";
            Console.WriteLine("Укажите название:");
            break;
        case 2:
            findOption = 1;
            option = "автора ";
            Console.WriteLine("Укажите автора:");
            break;
        default:
            findOption = 3;
            option = "ISBN ";
            Console.WriteLine("Укажите ISBN:");
            break;
    }
    string look = Console.ReadLine();
    foreach (string[] item in books)
    {
        if (item[findOption] == look)
        {
            PrintInfo(item);
            return;
        }
    }
    Console.WriteLine($"Книги с {option} {look} нет в библиотеке");
}


bool IsEmptyLibrary()
{
    if (books.Count == 0)
    {
        Console.WriteLine("Библиотека пуста.");
        return true;
    }
    return false;
}