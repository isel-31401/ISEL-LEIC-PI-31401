using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CurricularUnitFormManager.Models.Individual;
using CurricularUnitFormManager.Models.Repository;


namespace CurricularUnitFormManager.Models.Provider
{
    public class CUFMembershipProvider : MembershipProvider
    {
       readonly static IndividualRepository _userRepository = IndividualRepository.Instance;
       readonly static int _MINREQUIREDPASSWORDLENGTH = 6;

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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return _userRepository.ChangePassWord(username, oldPassword, newPassword);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return _userRepository.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return _userRepository.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey,out status);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return _userRepository.DeleteUser(username, deleteAllRelatedData);
        }

        public override string GetPassword(string username, string answer)
        {
            return _userRepository.GetPassword(username, answer);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return _userRepository.GetUser(username, userIsOnline);
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _userRepository.MaxInvalidPasswordAttempts(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _userRepository.MinRequiredNonAlphanumericCharacters(); }
        }

        public override bool ValidateUser(string username, string password)
        {
            return _userRepository.ValidateUser(username, password);
        }
        #region Not Implemented
        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public override int MinRequiredPasswordLength
        {
            get { return _MINREQUIREDPASSWORDLENGTH; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}