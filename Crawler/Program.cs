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

