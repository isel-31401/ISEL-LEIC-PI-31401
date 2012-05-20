using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace CurricularUnitFormManager.Models.Individual
{
    public sealed class Individual : MembershipUser
    {
        List<string> _roles;

        public Individual(string providername,
                                  string username,
                                  object providerUserKey,
                                  string email,
                                  string passwordQuestion,
                                  string comment,
                                  bool isApproved,
                                  bool isLockedOut,
                                  DateTime creationDate,
                                  DateTime lastLoginDate,
                                  DateTime lastActivityDate,
                                  DateTime lastPasswordChangedDate,
                                  DateTime lastLockedOutDate,
                                  bool isSubscriber,
                                  string customerID) :
            base(providername,
                                       username,
                                       providerUserKey,
                                       email,
                                       passwordQuestion,
                                       comment,
                                       isApproved,
                                       isLockedOut,
                                       creationDate,
                                       lastLoginDate,
                                       lastActivityDate,
                                       lastPasswordChangedDate,
                                       lastLockedOutDate)
            {
                    _roles = new List<string>();
                    _roles.Add(IndividualRoles.User);
                    _roles.Add(IndividualRoles.Anounymous);
                }
        public Individual()
        { 
            _roles = new List<string>();
            _roles.Add(IndividualRoles.User);
            _roles.Add(IndividualRoles.Anounymous);
        }

        public int IndividualId { get; set; }
        public String FullName{set;get;}
        public new String UserName{set;get;}
        public String Password{ set; get; }
        //public String Email { get; set; }
        public String PhotoName { get; set; }
        public Boolean RememberMe { get; set; }
        public String SecurityQuestion { get; set; }
        public String SecurityAnswer { get; set; }

        public Boolean addRole(string role) 
        {
            if (role == null || role.Trim().Length == 0) return false;
            if (! _roles.Exists(mbox => (mbox.Trim() ==role.Trim()))){
                _roles.Add(role);
            }
            return true;
        }

        public Boolean removeRole(string role){
            if (role == null || role.Trim().Length == 0) return false;
            return  _roles.Remove(role);
        }
        
        public Boolean isInRole(string role)
        {
            if (role == null || role.Trim().Length == 0) return false;
            return _roles.Exists(delegate(string s) { return s == role; });            
        }

        public String[] getRoles()
        {
            return _roles.ToArray<string>();
        }

        public override String ToString()
        {
            return String.Format("Name: {0}\nUserName: {1}\nPassword: {2}\n", FullName,  UserName, Password);
        }
    }
}
