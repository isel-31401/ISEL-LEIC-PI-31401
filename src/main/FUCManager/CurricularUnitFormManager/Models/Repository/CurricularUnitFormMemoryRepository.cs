using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurricularUnitFormManager.Models.CurricularUnit;

namespace CurricularUnitFormManager.Models.Repository
{
    class CurricularUnitFormMemoryRepository<T> : ICurricularUnitFormRepository<AbstractCurricularUnitForm>
    {
        private readonly Dictionary<string, Dictionary<ulong, AbstractCurricularUnitForm>> _repo = new Dictionary<string, Dictionary<ulong, AbstractCurricularUnitForm>>();
        private ulong _cVersion = 1;
   
        /**
         * Devolve um Enumeravel de Todas as unidades Curriculares Activas.
         * */
        public IEnumerable<CufRepoModel> GetActiveCurricularUnits()
        {
            CufRepoModel cuf=null ;
            foreach (var acr in _repo.Keys.Distinct())
            {
                foreach (var v in _repo[acr].Values.Where((item) => (item.IsValid == true)))
                {
                    cuf=new CufRepoModel(v);
                    break;
                }
                if (cuf == null) yield break;
                
                yield return cuf;
            }
        }
        /**
         * Devolve um Enumeravel de Todas as Versões Activas de uma Unidade Curricular.
         * */
        public IEnumerable<CufRepoModel> GetAllActiveVersionsByAcronym(string acr)
        {
            if (String.IsNullOrEmpty(acr) || !_repo.Keys.Contains(acr))
                yield break;
            //filtra por activas para devolver apenas devolver as activas
            foreach (var t in _repo[acr].Values.Where((item) => (item.IsValid == true)))
            {
                yield return new CufRepoModel(t);
            }
        }


        public AbstractCurricularUnitForm GetByAcronym(string acr)
        {
            if (String.IsNullOrEmpty(acr) ||  !_repo.ContainsKey(acr))
                return default(AbstractCurricularUnitForm);
            
            foreach (var f in _repo[acr].Values)
            {
                if (f.IsValid) return f;
            }
            return default(AbstractCurricularUnitForm);
        }

        public AbstractCurricularUnitForm GetByAcrVersion(string acr, ulong version)
        {
            if (String.IsNullOrEmpty(acr) || !_repo[acr].ContainsKey(version))
                return default(AbstractCurricularUnitForm);
            return _repo[acr][version];
        }


        public bool Change(AbstractCurricularUnitForm f)
        {
            if (f == null || f.CourseFile.CUnit.Acronym == null) return false;

            if (!_repo.ContainsKey(f.CourseFile.CUnit.Acronym))
            {
                Add(f);
                return true;
            }
            if (!_repo[f.CourseFile.CUnit.Acronym].ContainsKey(f.Version))
            {
                _repo[f.CourseFile.CUnit.Acronym].Add(f.Version, f);
                return true;
            }

            _repo[f.CourseFile.CUnit.Acronym][f.Version] = f;
            return true;

        }
        
        //TODO : Fix Logic, not to add multiple cuf to repo
        public bool Add(AbstractCurricularUnitForm f)
        {
            if (f == null || f.CourseFile.CUnit.Acronym == null) return false;

            if (f.Version == 0) { f.Version = _cVersion++; };
            f.IsValid = true;

            if (!_repo.ContainsKey(f.CourseFile.CUnit.Acronym))
            {
                Dictionary<ulong, AbstractCurricularUnitForm> r = new Dictionary<ulong, AbstractCurricularUnitForm>();
                r.Add(f.Version, f);
                _repo.Add(f.CourseFile.CUnit.Acronym, r);
                return true;
            }

            _repo[f.CourseFile.CUnit.Acronym].Add(f.Version, f);
            return true;
        }
        

        public AbstractCurricularUnitForm Remove(string acr, ulong version)
        {
            if (String.IsNullOrEmpty(acr) || !_repo.ContainsKey(acr) || !_repo[acr].ContainsKey(version))
            {
                return default(AbstractCurricularUnitForm);
            }

            AbstractCurricularUnitForm t = _repo[acr][version];
            t.IsValid = false;
            _repo[acr].Remove(version);

            if (!_repo[acr].ContainsKey(version))
            {
                _repo.Remove(acr);
            }
            return t;
        }

        public IEnumerable<AbstractCurricularUnitForm> GetAll()
        {
            foreach (var d in _repo.Keys.Distinct())
            {
                foreach (var c in _repo[d].Values.Where((item) => (item.IsValid == true)))
                {
                    yield return c;
                }
            }

        }

        private bool SetActiveFlag(string acr, ulong version, bool flag){
            if (String.IsNullOrEmpty(acr) || !_repo.ContainsKey(acr) || !_repo[acr].ContainsKey(version))
            {
                return false;
            }
            _repo[acr][version].IsValid = flag;
            return true;
        }

        public bool Activate(string acr, ulong version)
        {
            return SetActiveFlag(acr, version, true);
        }

        public bool Deactivate(string acr, ulong version)
        {
            return SetActiveFlag(acr, version, false);
        }

        //TODO: Test
        public IEnumerable<CufRepoModel> Search(String SearchValue)
        {

            foreach (var c in GetActiveCurricularUnits().Where(cuf => cuf.Name.ToUpper().IndexOf(SearchValue.ToUpper()) > -1 || cuf.Acr.ToUpper().IndexOf(SearchValue.ToUpper()) > -1))
            {
                yield return c;
            }

        }
    }
}
