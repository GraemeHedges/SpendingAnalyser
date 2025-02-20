using CsvHelper;
using SpendingAnalyser.DTOs;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace SpendingAnalyser.Services;

public class StarlingService : IStarlingService
{
    private readonly HttpClient _httpClient;

    public StarlingService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<string> GetAccountUri(string token)
    {
        if(string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Error: token is null or empty");
            return string.Empty;
        }

        string accountUid = string.Empty;

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "accounts")
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", token),
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
                }
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var account = await response.Content.ReadFromJsonAsync<AccountsDTO>();

                if(account is not null)
                {
                    accountUid = account.Accounts.First().AccountUid;
                }
                else
                {
                    Console.WriteLine("Error: account is null or empty");
                }

                Console.WriteLine($"accountUid: {accountUid}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Http Request Exception: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        return accountUid;
    }

    public async Task<IEnumerable<StatementDTO>> GetStatement(string token, string accountUid, string year, string month)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"accounts/{accountUid}/statement/download?yearMonth={year}-{month}")
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", token),
                    Accept = { new MediaTypeWithQualityHeaderValue("text/csv") }
                }
            };
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<StatementDTO>();
                return records.ToList();
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }

        return Enumerable.Empty<StatementDTO>();
    }
}
