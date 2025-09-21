namespace QuotesNestServer.Models;

public class QuotesNestDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public Dictionary<string, string> Collections { get; set; } = new Dictionary<string, string>();

}