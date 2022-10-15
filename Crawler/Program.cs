using Crawler.models;
using System;

HttpClient httpClient = new HttpClient();
EmailSearcher emailSearcher = new EmailSearcher(httpClient);

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

PrintFoundedEmails(emails);

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
    else
    {
        Console.WriteLine("No email addresses found.");
    }
}

void PrintEmails(HashSet<string> emails)
{
    foreach (string email in emails)
    {
        Console.WriteLine(email);
    }
}

