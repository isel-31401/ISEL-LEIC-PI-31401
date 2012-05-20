using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//TODO CurricularUnitFormManager: Deprecated
namespace CurricularUnitFormManager.Models.Individual
{
    using System.Security.Principal;
    public class IndividualAuthentication :IIdentity
    {
        readonly string _authenticationType = "HTTP Basic Authentication";
        bool _isAuthenticated = false;
        string _userName =null;

        public IndividualAuthentication(String username)
        {
            _isAuthenticated = true;
            _userName = username;
        }

        public string AuthenticationType
        {
            get { return _authenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public string Name
        {
            get { return _userName; }
        }
    }
}
