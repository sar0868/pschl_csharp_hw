using HomeLibrary;

Library library = new Library();

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
            library.AddBook(AddBook());
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


Book AddBook()
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

    return new Book(title, author, year, ISBN);
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
    Console.WriteLine(library);
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
        Console.WriteLine("3. По году издания");
        Console.WriteLine("4. По ISBN");
        Console.WriteLine("5. Вернутся в меню.");

        string userChoice = Console.ReadLine();
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
        SearchOption(userInput);
    }

}

void SearchOption(int userInput)
{
    int findOption;
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
    Console.WriteLine(library.FindBook(option, look));
}


bool IsEmptyLibrary()
{
    if (library.IsEmpty())
    {
        Console.WriteLine("Библиотека пуста.");
        return true;
    }
    return false;
}