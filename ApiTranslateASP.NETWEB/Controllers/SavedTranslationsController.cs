using Microsoft.AspNetCore.Mvc;
using ApiTranslateASP.NETWEB.Models;
using System.Collections.Generic;
using ApiTranslateASP.NETWEB.Contracts;


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
            IEnumerable<Translation> savedTranslations = _translationRepository.GetAllTranslations();
            return View("~/Views/Translate/SavedTranslations.cshtml");
        }
    }
}
