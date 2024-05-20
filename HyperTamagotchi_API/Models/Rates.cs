using Newtonsoft.Json;

namespace HyperTamagotchi_API.Helpers.ExchangeRate;

// var apiKey = builder.Configuration.GetValue<string>("ApiKey:ExchangeRate") ?? throw new InvalidOperationException("ApiKey 'ExchangeRate' not found.");
// Rates rates = new(apiKey);
// var currencyExchangeRates = await rates.ImportAsync(CurrencyType.SEK);

public class Rates(string apiKey)
{
    private readonly string _apiKey = apiKey;

    public async Task<API_Obj> ImportAsync(CurrencyType currencyType = CurrencyType.SEK)
    {
        try
        {
            var url = $"https://v6.exchangerate-api.com/v6/{_apiKey}/latest/{currencyType}";
            using (var httpClient = new HttpClient())
            {
                var content = await httpClient.GetStringAsync(url);
                API_Obj currencyExchangeRateObject = JsonConvert.DeserializeObject<API_Obj>(content)!;
                return currencyExchangeRateObject;
            }
        }
        catch (Exception)
        {
            return default;
        }
    }
}
public class API_Obj
{
    public string result { get; set; }
    public string documentation { get; set; }
    public string terms_of_use { get; set; }
    public string time_last_update_unix { get; set; }
    public string time_last_update_utc { get; set; }
    public string time_next_update_unix { get; set; }
    public string time_next_update_utc { get; set; }
    public string base_code { get; set; }
    public ConversionRate conversion_rates { get; set; }
}

public class ConversionRate
{
    public double AED { get; set; }
    public double ARS { get; set; }
    public double AUD { get; set; }
    public double BGN { get; set; }
    public double BRL { get; set; }
    public double BSD { get; set; }
    public double CAD { get; set; }
    public double CHF { get; set; }
    public double CLP { get; set; }
    public double CNY { get; set; }
    public double COP { get; set; }
    public double CZK { get; set; }
    public double DKK { get; set; }
    public double DOP { get; set; }
    public double EGP { get; set; }
    public double EUR { get; set; }
    public double FJD { get; set; }
    public double GBP { get; set; }
    public double GTQ { get; set; }
    public double HKD { get; set; }
    public double HRK { get; set; }
    public double HUF { get; set; }
    public double IDR { get; set; }
    public double ILS { get; set; }
    public double INR { get; set; }
    public double ISK { get; set; }
    public double JPY { get; set; }
    public double KRW { get; set; }
    public double KZT { get; set; }
    public double MXN { get; set; }
    public double MYR { get; set; }
    public double NOK { get; set; }
    public double NZD { get; set; }
    public double PAB { get; set; }
    public double PEN { get; set; }
    public double PHP { get; set; }
    public double PKR { get; set; }
    public double PLN { get; set; }
    public double PYG { get; set; }
    public double RON { get; set; }
    public double RUB { get; set; }
    public double SAR { get; set; }
    public double SEK { get; set; }
    public double SGD { get; set; }
    public double THB { get; set; }
    public double TRY { get; set; }
    public double TWD { get; set; }
    public double UAH { get; set; }
    public double USD { get; set; }
    public double UYU { get; set; }
    public double ZAR { get; set; }
}
public enum CurrencyType
{
    AED,
    ARS,
    AUD,
    BGN,
    BRL,
    BSD,
    CAD,
    CHF,
    CLP,
    CNY,
    COP,
    CZK,
    DKK,
    DOP,
    EGP,
    EUR,
    FJD,
    GBP,
    GTQ,
    HKD,
    HRK,
    HUF,
    IDR,
    ILS,
    INR,
    ISK,
    JPY,
    KRW,
    KZT,
    MXN,
    MYR,
    NOK,
    NZD,
    PAB,
    PEN,
    PHP,
    PKR,
    PLN,
    PYG,
    RON,
    RUB,
    SAR,
    SEK,
    SGD,
    THB,
    TRY,
    TWD,
    UAH,
    USD,
    UYU,
    ZAR
}