namespace HomeLibrary;

internal class YearExceptions : Exception
{
    public YearExceptions(string message) : base(message) { }
}

internal class ISBNException : Exception
{
    public ISBNException(string message) : base(message) { }
}

internal class AddBookException : Exception
{
    public AddBookException(string message) : base() { } 
}