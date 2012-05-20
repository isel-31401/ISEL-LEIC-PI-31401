using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurricularUnitFormManager.Models.CurricularUnit;

namespace CurricularUnitFormManager.Models.Repository
{
    class OfficialCurricularUnitFormRepositoryLocator
    {
        private readonly static CurricularUnitFormMemoryRepository<OfficialCurricularUnitForm> _repo
            = new CurricularUnitFormMemoryRepository<OfficialCurricularUnitForm>();
        public static CurricularUnitFormMemoryRepository<OfficialCurricularUnitForm> Get()
        {
            #region todelete
                OfficialCurricularUnitForm a = new OfficialCurricularUnitForm();
                OfficialCurricularUnitForm b = new OfficialCurricularUnitForm();
                OfficialCurricularUnitForm c = new OfficialCurricularUnitForm();
                CurricularUnit.CurricularUnit d = new CurricularUnit.CurricularUnit("Matematica 1","M1",6);
                CurricularUnit.CurricularUnit e = new CurricularUnit.CurricularUnit("Matematica 2", "M2", 6.5);
                CurricularUnit.CurricularUnit f = new CurricularUnit.CurricularUnit("Probabilidade e Estatistica", "PE", 6);


                a.CourseFile.CUnit = d;
                b.CourseFile.CUnit = e;
                c.CourseFile.CUnit = f;
                _repo.Add(a);
                _repo.Add(b);
                _repo.Add(c);
            #endregion

            return _repo;
        }
    }
}
