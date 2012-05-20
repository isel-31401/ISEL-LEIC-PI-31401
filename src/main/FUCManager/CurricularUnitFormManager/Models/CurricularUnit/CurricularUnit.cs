using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public class CurricularUnit : IEquatable<CurricularUnit>
    {

        private string _name;
        private string _acronym;
        private double _ects;
                
        public CurricularUnit()
        { }

        public CurricularUnit(string name, string acronym)
        {
            _name = name;
            _acronym = acronym;
        }
        public CurricularUnit(string name, string acronym, double ects)
        {
            _name = name;
            _acronym = acronym;
            _ects = ects;
        }
        [Required]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [Required]
        public string Acronym
        {
            get { return _acronym; }
            set { _acronym = value; }
        }
        [Required]
        [DataType(DataType.Currency)]
        public double ECTS
        {
            get { return _ects; }
            set { _ects = value; }
        }

        public bool Equals(CurricularUnit other)
        {
            return this._acronym.Equals(other.Acronym);
        }
    }
}
