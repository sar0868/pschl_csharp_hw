using HomeLibrary;

Library library = new Library();

while (true)
{
    Dialog.Menu();
    string? answer = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(answer)
    || !int.TryParse(answer, out int choice)
    || choice < 1
    || choice > 5)
    {
        Console.WriteLine("Не верный ввод. Введите число от 1 до 5.");
        continue;
    }
    switch (choice)
    {
        case 1:
            Dialog.AddBook(library);
            break;
        case 2:
            Dialog.FindBookByOptions(library);
            break;
        case 3:
            library.ShowLibrary();
            break;
        case 4:
            Dialog.EditBook(library);
            break;
        default:
            Console.WriteLine("Выход");
            return;
    }   
}
