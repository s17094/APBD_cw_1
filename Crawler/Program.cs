using Crawler.models;

HttpClient httpClient = new HttpClient();
EmailSearcher emailSearcher = new EmailSearcher(httpClient);

string url = GetUrl(args);
HashSet<String> emailsFounded = await emailSearcher.Search(url);
    
PrintFoundedEmails(emailsFounded);

string GetUrl(string[] args)
{
    try
    {
        return args[0];
    }
    catch (IndexOutOfRangeException)
    {
        throw new ArgumentNullException(nameof(args));
    }
}

void PrintFoundedEmails(HashSet<String> emailsFounded)
{
    if (emailsFounded != null && emailsFounded.Any())
    {
        PrintEmails(emailsFounded);
    }
}

void PrintEmails(HashSet<string> emails)
{
    foreach (string email in emails)
    {
        Console.WriteLine(email);
    }
}

