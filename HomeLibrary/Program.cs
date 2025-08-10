// See https://aka.ms/new-console-template for more information

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
