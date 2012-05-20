using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public class CufRepoModel
    {
        string _cufName;
        ulong _cufVersion;
        string _cufAcr;
        string _cufUser;
        public CufRepoModel(AbstractCurricularUnitForm cuf)
        {
            _cufAcr = cuf.CourseFile.CUnit.Acronym;
            _cufVersion = cuf.Version;
            _cufName = cuf.CourseFile.CUnit.Name;
            _cufUser = cuf.Username;
        }
        public String Name { get { return _cufName; } }
        public ulong Version { get { return _cufVersion; } }
        public String Acr { get { return _cufAcr; } }
        public String User { get { return _cufUser; } }
    }
}