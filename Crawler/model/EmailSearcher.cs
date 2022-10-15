using System.Text.RegularExpressions;

namespace Crawler.models
{
    public class EmailSearcher
    {
        private static readonly string USERNAME_REGEX = "[A-Z0-9._%+-]+";
        private static readonly string DOMAIN_REGEX = "[A-Z0-9.-]+\\.[A-Z]{2,}";
        private static readonly string EMAIL_REGEX = "\\b" + USERNAME_REGEX + "@" + DOMAIN_REGEX + "\\b";
        private static readonly Regex EMAIL_PATTERN = new(EMAIL_REGEX, RegexOptions.IgnoreCase);

        private readonly HttpClient httpClient;

        public EmailSearcher(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private static HashSet<string> MatchEmails(string response)
        {
            MatchCollection emailMatches = EMAIL_PATTERN.Matches(response);
            return emailMatches.Cast<Match>()
                .Select(m => m.Value).ToHashSet();
        }

        public async Task<HashSet<string>> Search(string url)
        {
            string responseBody = await GetResponseBodyAsync(url);
            return MatchEmails(responseBody);
        }

        private async Task<string> GetResponseBodyAsync(string url)
        {
            HttpResponseMessage httpResponse = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, url));
            httpClient.Dispose();
            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}