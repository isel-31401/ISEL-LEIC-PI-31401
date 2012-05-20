using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurricularUnitFormManager.Models.Individual
{
    public class IndividualRoles {
        private static readonly String[] _roles = {"Administrator","Coordenator","Colaborator","User","Anonymous"};
        public static String Administrator { get { return _roles[0]; } }
        public static String Coordenator { get { return _roles[1]; } }
        public static String Colaborator { get { return _roles[2]; } }
        public static String User { get { return _roles[3]; } }
        public static String Anounymous { get { return _roles[4]; } }
        public static String[] getRoles() { return _roles; }
    }
}