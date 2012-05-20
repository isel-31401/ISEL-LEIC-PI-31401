using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CurricularUnitFormManager.Models.CurricularUnit;
//TODO CurricularUnitFormManager: implement methods
namespace CurricularUnitFormManager.Models.Repository
{
    public class CurricularUnitFormRepository
    {
        private readonly static CurricularUnitFormMemoryRepository<OfficialCurricularUnitForm> _OfficialRepo = OfficialCurricularUnitFormRepositoryLocator.Get();
        private readonly static CurricularUnitFormMemoryRepository<ProposalCurricularUnitForm> _ProposalRepo = ProposalCurricularUnitFormRepositoryLocator.Get();

        static CurricularUnitFormRepository _instance;

        private CurricularUnitFormRepository() { }
        public static void Start() { _instance = new CurricularUnitFormRepository(); }
        public static CurricularUnitFormRepository Instance {get{return _instance;}}
        
        //public Boolean New() { throw new NotImplementedException(); }
        /// <summary>
        /// New Proposal of an curricular unit.
        ///  
        /// </summary>
        /// <param name="acr"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public Boolean New(AbstractCurricularUnitForm cuf) 
        {
            return (cuf == null)?false:_ProposalRepo.Add(cuf);
        }
        /// <summary>
        /// Accept an curricular unit
        /// </summary>
        /// <param name="acr"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public Boolean Accept(String acr, ulong version) 
        {
            
            //AbstractCurricularUnitForm pCuf = _ProposalRepo.GetByAcrVersion(acr, version);
            AbstractCurricularUnitForm pCuf = _ProposalRepo.Remove(acr, version);
            AbstractCurricularUnitForm oCuf = Detail(acr);
            
            if (pCuf == null) return false;

            if (_OfficialRepo.Add(pCuf)) 
            {
                //if (oCuf != null) { _OfficialRepo.Deactivate(o.acr, oCuf.Version); }
                _ProposalRepo.Remove(acr, version);
                return true;
            };
            return false; 
        }
        public Boolean Reject(String acr, ulong version) 
        {
            return _ProposalRepo.Deactivate(acr, version); 
        }
        public Boolean Edit(AbstractCurricularUnitForm cuf) 
        {
            return _ProposalRepo.Change(cuf);
        }
        public Boolean Delete(String acr, ulong version) 
        {
            return Reject(acr, version); 
        }

        public AbstractCurricularUnitForm Detail(String acr)
        {
            return _OfficialRepo.GetByAcronym(acr);
        }
        public AbstractCurricularUnitForm Detail(String acr, ulong version) 
        { 
            return _ProposalRepo.GetByAcrVersion(acr,version); 
        }
        /* Official Specific */
        public IEnumerable<CufRepoModel> GetAllOfficialCurricularUnitForms() 
        { 
            return _OfficialRepo.GetActiveCurricularUnits(); 
        }
        
        public AbstractCurricularUnitForm GetOfficialCurricularUnitForm(String acr) 
        {
            return Detail(acr);
        }

        /* Proposal Specific */
        public IEnumerable<CufRepoModel> GetAllProposalCurricularUnitForms() 
        { 
            return _ProposalRepo.GetActiveCurricularUnits(); 
        }
        public IEnumerable<CufRepoModel> GetAllProposalCurricularUnitFormsVersions(String acr) 
        { 
            return _ProposalRepo.GetAllActiveVersionsByAcronym(acr); 
        }
        public AbstractCurricularUnitForm GetProposalCurricularUnitForm(String acr, ulong version) 
        { 
            return _ProposalRepo.GetByAcrVersion(acr,version); 
        }

        //TODO
        public IEnumerable<CufRepoModel> Search(String value)
        {
            return _OfficialRepo.Search(value);
        }

    }
}