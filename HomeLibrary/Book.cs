using System;

namespace HomeLibrary;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Year { get; set; }
    public string ISBN { get; set; }

    public Book(string title, string author, string year, string ISBN)
    {
        Title = title;
        Author = author;
        Year = year;
        this.ISBN = ISBN;
    }

    public override string ToString()
{
    return $"Название: {Title}, Автор: {Author}, Год издания: {Year}, ISBN: {ISBN}";
}
}
