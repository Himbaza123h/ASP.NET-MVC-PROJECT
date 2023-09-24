
using global::ApiTranslateASP.NETWEB.Models;
using System.Collections.Generic;
using ApiTranslateASP.NETWEB.Contracts;

namespace ApiTranslateASP.NETWEB.Contracts
{
    public interface ITranslationRepository
    {
        IEnumerable<Translation> GetAllTranslations();
        void AddTranslation(Translation translation);
    }
}




