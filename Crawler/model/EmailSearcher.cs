using System.Text.RegularExpressions;

namespace Crawler.model
{
    public class EmailSearcher
    {
        private const string UsernameRegex = "[A-Z0-9._%+-]+";
        private const string DomainRegex = "[A-Z0-9.-]+\\.[A-Z]{2,}";
        private const string EmailRegex = "\\b" + UsernameRegex + "@" + DomainRegex + "\\b";
        private static readonly Regex EmailPattern = new(EmailRegex, RegexOptions.IgnoreCase);

        private readonly HttpClient _httpClient;

        public EmailSearcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static HashSet<string> MatchEmails(string response)
        {
            var emailMatches = EmailPattern.Matches(response);
            return emailMatches
                .Select(m => m.Value).ToHashSet();
        }

        public async Task<HashSet<string>> Search(string url)
        {
            var responseBody = await GetResponseBodyAsync(url);
            return MatchEmails(responseBody);
        }

        private async Task<string> GetResponseBodyAsync(string url)
        {
            var httpResponse = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            _httpClient.Dispose();
            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}