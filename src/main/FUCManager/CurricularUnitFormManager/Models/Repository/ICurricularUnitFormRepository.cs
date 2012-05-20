using System.Collections.Generic;
using CurricularUnitFormManager.Models.CurricularUnit;

namespace CurricularUnitFormManager.Models.Repository
{
    interface ICurricularUnitFormRepository<T>
    {
         IEnumerable<T> GetAll(); 
         T Remove(string acr, ulong version);
         bool Add(T f);
         bool Change(T f);
         T GetByAcrVersion(string acr, ulong version);
         T GetByAcronym(string acr);
         IEnumerable<CufRepoModel> GetAllActiveVersionsByAcronym(string acr);
         IEnumerable<CufRepoModel> GetActiveCurricularUnits();
    }
}

