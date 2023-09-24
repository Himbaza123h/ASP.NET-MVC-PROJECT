using Microsoft.AspNetCore.Mvc;
using ApiTranslateASP.NETWEB.Models;
using ApiTranslateASP.NETWEB.Contracts;
using System.Collections.Generic;

namespace ApiTranslateASP.NETWEB.Controllers
{
    public class SavedTranslationsController : Controller
    {
        private readonly ITranslationRepository _translationRepository;

        public SavedTranslationsController(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public IActionResult SavedTranslations()
        {
            IEnumerable<Translation> translations = _translationRepository.GetAllTranslations();
            return View(translations);
        }
    }
}
