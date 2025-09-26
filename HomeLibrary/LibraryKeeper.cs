
namespace HomeLibrary;

internal static class LibraryKeeper
{
    internal static void Load(string filePath, Library library)
    {
        string[] libraryInfo = File.ReadAllLines(filePath);
        foreach (string item in libraryInfo)
        {
            string[] book = item.Split(';');
            if (book.Length != 6)
            {
                Console.WriteLine($"{item} не добавлена в библиотеку");
                continue;
            }
            try
            {
                string? title = book[0];
                string? author = book[1];
                int? year = int.Parse(book[2]);
                if (DateTime.Now.Year < year)
                {
                    Console.WriteLine($"{item} не добавлена в библиотеку");
                    continue;
                }
                string? ISBN = book[3];
                if (ISBN.Length != 13
                    || !Dialog.ContainsDigits(ISBN))
                {
                    Console.WriteLine($"{item} не добавлена в библиотеку");
                    continue;
                }
                if (library.IsThereBookTitleAuthorYear(title, author, year)
                    || library.IsISBN(ISBN))
                {
                    Console.WriteLine($"{item} не добавлена в библиотеку");
                    continue;
                }
                string? comment = book[4];
                bool isRead = bool.Parse(book[5]);
                library.AddBook(new Book(title, author, year, ISBN, comment, isRead));
            }
            catch (Exception)
            {
                Console.WriteLine($"{item} не добавлена в библиотеку");
                continue;
            }
            
        }
    }

    internal static string Save(Library library, string filename)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, filename);
        List<string> libraryInfo = Serialize(library);
        File.AppendAllLines(filePath, libraryInfo);
        return filePath;
    }

    private static List<string> Serialize(Library library)
    {
        var books = library.GetBooks();
        return books.Select(book =>
        $"{book.Title};{book.Author};{book.Year};{book.ISBN};{book.Comment};{book.IsRead}")
        .ToList();
    }
}
