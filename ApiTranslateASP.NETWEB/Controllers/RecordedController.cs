using ApiTranslateASP.NETWEB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTranslateASP.NETWEB.Controllers
{
    public class RecordedController : Controller
    {
        public async Task<IActionResult> Recorded()
        {
            return View("~/Views/Translate/Recorded.cshtml");
        }
}



}

