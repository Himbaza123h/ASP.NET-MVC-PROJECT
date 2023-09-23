using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTranslateASP.NETWEB.Controllers
{
    public class TranslateController : Controller
    {
        private const string BaseUrl = "https://api.funtranslations.com/translate/leetspeak.json";
        private readonly HttpClient _httpClient;

        public TranslateController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public IActionResult Translate()
        {
            return View("~/Views/Translate/Index.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Translate(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                ViewBag.TranslatedText = "Input text is empty or contains only whitespace.";
            }
            else
            {
                try
                {
                    // Translate input text to Leetspeak using the API
                    var translationResult = await TranslateToLeetspeakAsync(inputText);

                    // Console the original input text and translated text
                    Console.WriteLine("Input Text: " + inputText);
                    Console.WriteLine("Leetspeak Text: " + translationResult.Contents.Translated);

                    ViewBag.TranslatedText = translationResult.Contents.Translated;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Translation Error: " + ex.Message);
                    ViewBag.TranslatedText = "Translation Error";
                }
            }

            return View("~/Views/Translate/Index.cshtml");
        }

        private async Task<TranslationResponse> TranslateToLeetspeakAsync(string inputText)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", inputText)
            });

            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<TranslationResponse>(responseContent);
            }
            else
            {
                Console.WriteLine($"Translation API error: {response.StatusCode}");
                throw new Exception($"Translation API error: {response.StatusCode}");
            }
        }
    }
}

public class TranslationResponse
{
    public TranslationContents Contents { get; set; }
}

public class TranslationContents
{
    public string Translated { get; set; }
}
