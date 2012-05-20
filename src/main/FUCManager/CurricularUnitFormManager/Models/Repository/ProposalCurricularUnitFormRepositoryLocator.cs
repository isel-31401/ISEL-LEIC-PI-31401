using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurricularUnitFormManager.Models.CurricularUnit;


namespace CurricularUnitFormManager.Models.Repository
{
    class ProposalCurricularUnitFormRepositoryLocator
    {
        private readonly static CurricularUnitFormMemoryRepository<ProposalCurricularUnitForm> _repo = new CurricularUnitFormMemoryRepository<ProposalCurricularUnitForm>();
        public static CurricularUnitFormMemoryRepository<ProposalCurricularUnitForm> Get()
        {
            #region todelete
                ProposalCurricularUnitForm a = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm b = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm c = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm d = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm e = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm f = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm g = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm h = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm i = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm j = new ProposalCurricularUnitForm();
                ProposalCurricularUnitForm k = new ProposalCurricularUnitForm();
                
                CurricularUnit.CurricularUnit a1 = new CurricularUnit.CurricularUnit("Arquitectura1", "AC1", 6);
                CurricularUnit.CurricularUnit b1 = new CurricularUnit.CurricularUnit("Arquitectura2", "AC2", 6);
                CurricularUnit.CurricularUnit c1 = new CurricularUnit.CurricularUnit("Arquitectura1", "AC1", 6);
                CurricularUnit.CurricularUnit d1 = new CurricularUnit.CurricularUnit("Arquitectura2", "AC2", 6);
                CurricularUnit.CurricularUnit e1 = new CurricularUnit.CurricularUnit("Arquitectura1", "AC1", 6);
                CurricularUnit.CurricularUnit f1 = new CurricularUnit.CurricularUnit("Arquitectura2", "AC2", 6);
                CurricularUnit.CurricularUnit g1 = new CurricularUnit.CurricularUnit("Arquitectura1", "AC1", 6);
                CurricularUnit.CurricularUnit h1 = new CurricularUnit.CurricularUnit("Arquitectura2", "AC2", 6);
                CurricularUnit.CurricularUnit i1 = new CurricularUnit.CurricularUnit("Arquitectura1", "AC1", 6);
                CurricularUnit.CurricularUnit j1 = new CurricularUnit.CurricularUnit("Arquitectura2", "AC2", 6);
                CurricularUnit.CurricularUnit k1 = new CurricularUnit.CurricularUnit("Arquitectura1", "AC1", 6);

                a.CourseFile.CUnit = a1;
                b.CourseFile.CUnit = b1;
                c.CourseFile.CUnit = c1;
                d.CourseFile.CUnit = d1;
                e.CourseFile.CUnit = e1;
                f.CourseFile.CUnit = f1;
                g.CourseFile.CUnit = g1;
                h.CourseFile.CUnit = h1;
                i.CourseFile.CUnit = i1;
                j.CourseFile.CUnit = j1;
                k.CourseFile.CUnit = k1;
                _repo.Add(a);
                _repo.Add(b);
                _repo.Add(c);
                _repo.Add(d);
                _repo.Add(e);
                _repo.Add(f);
                _repo.Add(g);
                _repo.Add(h);
                _repo.Add(i);
                _repo.Add(j);
                _repo.Add(k);
            #endregion
            return _repo;
        
        }
    }
}
