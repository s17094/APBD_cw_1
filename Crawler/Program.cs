using Crawler.model;

HttpClient httpClient = new();
EmailSearcher emailSearcher = new(httpClient);

var destinationUrl = GetUrl(args);

HashSet<string> foundedEmails;
try
{
    foundedEmails = await emailSearcher.Search(destinationUrl);
}
catch (Exception)
{
    Console.WriteLine("Error during page download.");
    return;
}

PrintEmails(foundedEmails);

string GetUrl(string[] args)
{
    try
    {
        var url = args[0];
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
    var result = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                 && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    if (!result)
    {
        throw new ArgumentException("Not valid url: " + url);
    }
}

void PrintEmails(HashSet<string> emails)
{
    if (emails.Any())
    {
        foreach (var email in emails)
        {
            Console.WriteLine(email);
        }
    }
    else
    {
        Console.WriteLine("No email addresses found.");
    }
}