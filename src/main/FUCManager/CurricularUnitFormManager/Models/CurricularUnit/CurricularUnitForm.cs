using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public class CurricularUnitForm
    {
        private ulong _id;
        private CurricularUnit _cUnit;
        private CurricularUnitType _type;
        private CurricularUnitDescription _description;
        private List<CurricularUnitForm> _requiredCourses;

        public CurricularUnitForm()
        { 
            _cUnit = new CurricularUnit("","");
            _type = new CurricularUnitType();
            _description = new CurricularUnitDescription();
            _requiredCourses = new List<CurricularUnitForm>();
        }

        public CurricularUnitForm(String name, String acr)
        {
            _cUnit = new CurricularUnit(name, acr);
            _type = new CurricularUnitType();
            _description = new CurricularUnitDescription();
            _requiredCourses = new List<CurricularUnitForm>();
        }

        public CurricularUnitForm(CurricularUnit course, CurricularUnitType type, CurricularUnitDescription description)
        {
            _cUnit = course;
            _type = type;
            _description = description;
        }

        public ulong ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public CurricularUnitDescription Description
        {
            get { return _description; }
            set { _description = value; }
        }
        [Required]
        public CurricularUnitType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public CurricularUnit CUnit
        {
            get { return _cUnit; }
            set { _cUnit = value; }
        }

        public string GetAcronym()
        {
            return _cUnit.Acronym;
        }

        public string GetName()
        {
            return _cUnit.Name;
        }

        public List<CurricularUnitForm> RequiredCourses
        {
            get { return _requiredCourses; }
            set { _requiredCourses = value; }
        }

        public bool AddRequiredCourses(CurricularUnitForm unit)
        {
            if (_requiredCourses.Contains(unit))
                return false;
            _requiredCourses.Add(unit);
            return true;
        }

        public bool RemoveRequiredCourses(CurricularUnitForm unit)
        {
            if (!_requiredCourses.Contains(unit))
                return false;
            _requiredCourses.Remove(unit);
            return true;
        }

    }
}
