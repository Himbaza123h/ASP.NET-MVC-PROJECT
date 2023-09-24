using ApiTranslateASP.NETWEB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace ApiTranslateASP.NETWEB.Controllers
{
    public class TranslateController : Controller
    {
        private const string BaseUrl = "https://api.funtranslations.com/translate/leetspeak.json";
        private readonly HttpClient _httpClient;
        private readonly IMongoCollection<Translation> _translationsCollection;

        public TranslateController(IMongoDatabase database)
        {
            _httpClient = new HttpClient();
            _translationsCollection = database.GetCollection<Translation>("translations");
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

                    // Create a new Translation object
                    var translation = new Translation
                    {
                        OriginalText = inputText,
                        TranslatedText = translationResult.Contents.Translated
                    };

                    // Save the translation to MongoDB
                    await _translationsCollection.InsertOneAsync(translation);

                    ViewBag.SuccessMessage = "Translated successfully!";
                    Console.WriteLine("Output" + translation);

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

        [HttpGet]
        public async Task<IActionResult> GetTranslations()
        {
            try
            {
                // Retrieve all translations from MongoDB
                var translations = await _translationsCollection.Find(_ => true).ToListAsync();

                // Return the translations as JSON
                return Json(translations);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching translations: " + ex.Message);
                return StatusCode(500, "Error fetching translations");
            }
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

    public class TranslationResponse
    {
        public TranslationContents Contents { get; set; }
    }

    public class TranslationContents
    {
        public string Translated { get; set; }
    }
}
