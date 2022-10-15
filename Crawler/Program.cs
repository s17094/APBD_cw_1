using Crawler.models;

HttpClient httpClient = new();
EmailSearcher emailSearcher = new(httpClient);

string url = GetUrl(args);

HashSet<string> emails;
try
{
    emails = await emailSearcher.Search(url);
}
catch (Exception)
{
    Console.WriteLine("Error during page download.");
    return;
}

PrintEmails(emails);

string GetUrl(string[] args)
{
    try
    {
        string url = args[0];
        ValidateUrl(url);
        return url;
    }
    catch (IndexOutOfRangeException)
    {
        throw new ArgumentNullException(nameof(args));
    }
}

void ValidateUrl(string url)
{
    bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
        && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    if (!result)
    {
        throw new ArgumentException("Not valid url: " + url);
    }
}

void PrintEmails(HashSet<string> emails)
{
    if (emails != null && emails.Any())
    {
        foreach (string email in emails)
        {
            Console.WriteLine(email);
        }
    }
    else
    {
        Console.WriteLine("No email addresses found.");
    }
}