namespace HomeLibrary;

internal record Book(string? Title, string? Author, int? Year, string? ISBN, string? Comment, bool IsRead);