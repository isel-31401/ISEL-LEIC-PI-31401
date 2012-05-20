using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CurricularUnitFormManager.Models.Individual;

namespace CurricularUnitFormManager.Models.Provider
{
    public class CUFRoleProvider : RoleProvider
    {
        Repository.IndividualRepository _userRepository = Repository.IndividualRepository.Instance;

        #region Not Implemented
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        #endregion

        //Overrided Metthod
        public override string[] GetRolesForUser(string username)
        {
            if (username == null) return null;

            if (_userRepository.HasUser(username))
            {
                return ((Individual.Individual)_userRepository.GetUser(username,false)).getRoles();
            }
            return null;
        }
        public override string[] GetAllRoles()
        {
            return Individual.IndividualRoles.getRoles();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            if (!_userRepository.HasUser(username)) return false;

            return GetRolesForUser(username).Contains(roleName);
        }
        public override bool RoleExists(string roleName)
        {
            return GetAllRoles().Contains(roleName);
        }

    }
}