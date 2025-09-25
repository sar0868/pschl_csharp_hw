namespace HomeLibrary;

public class YearExceptions : Exception
{
    public YearExceptions(string message) : base(message) { }
}

public class ISBNException : Exception
{
    public ISBNException(string message) : base(message) { }
}
