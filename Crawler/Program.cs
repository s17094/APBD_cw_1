using Crawler.models;

HttpClient httpClient = new HttpClient();
EmailSearcher emailSearcher = new EmailSearcher(httpClient);

String url = args[0];
HashSet<String> emailsFounded = await emailSearcher.Search(url);

PrintFoundedEmails(emailsFounded);

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

