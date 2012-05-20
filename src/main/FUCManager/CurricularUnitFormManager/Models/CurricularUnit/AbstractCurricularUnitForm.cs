using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace CurricularUnitFormManager.Models.CurricularUnit
{
    abstract public class AbstractCurricularUnitForm : ICurricularUnitForm 
    {
        private CurricularUnitForm _courseFile;
        private ulong _version;
        private String _username;
        private DateTime _userTimeStamp;
        private bool _isValid;
        //private String mytype;

        public virtual bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        public virtual DateTime UserTimeStamp
        {
            get { return _userTimeStamp; }
            set { _userTimeStamp = value; }
        }

        [Required]
        public virtual string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public virtual ulong Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public virtual CurricularUnitForm CourseFile
        {
            get { return _courseFile; }
            set { _courseFile = value; }
        }
        
        abstract public String CurricularUnitFormType();
        
    }
}
