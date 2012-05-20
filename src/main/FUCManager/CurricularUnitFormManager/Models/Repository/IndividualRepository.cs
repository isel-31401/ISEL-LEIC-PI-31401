using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CurricularUnitFormManager.Models.Individual;
using System.Web.Security;
using CurricularUnitFormManager.Models.Mail;

namespace CurricularUnitFormManager.Models.Repository
{
    public class IndividualRepository
    {
        readonly int _MAX_ATTEMPTS = 3;
        readonly int _MIN_NON_ALPHA_NUM_CHARS = 2;

        readonly static Dictionary<string, Individual.Individual> _userRepo = UserLoad.Instance;
        readonly static Dictionary<int, Individual.Individual> _register = new Dictionary<int, Individual.Individual>();

        private static volatile IndividualRepository _instance = null;
        private Int32 _id = 10000;

        private IndividualRepository() { UserLoad.Start(); }

        public static void Start() { _instance = new IndividualRepository(); }
        public static IndividualRepository Instance { get { return _instance; } }


        public Boolean ChangePassWord(String user, String oldPassword, String newPassword)
        {
            if (_userRepo[user].Password != oldPassword) return false;
            if (!_userRepo.ContainsKey(user)) return false;
            _userRepo[user].Password = newPassword;
            return true;
        }
        public Boolean ChangePicture(String user, String newPicture)
        {
            if (!_userRepo.ContainsKey(user)) return false;
            _userRepo[user].PhotoName = newPicture;
            return true;
        }
        public Boolean AddRole(String user, String newRole)
        {
            if (!_userRepo.ContainsKey(user)) return false;
            return _userRepo[user].addRole(newRole);
        }
        public Boolean ChangeRole(String user, String oldRole, String newRole)
        {
            if (!_userRepo.ContainsKey(user)) return false;
            if (_userRepo[user].isInRole(oldRole))
            {
                if (!_userRepo[user].isInRole(newRole))
                {
                    _userRepo[user].removeRole(oldRole);
                    return _userRepo[user].addRole(newRole);
                }
            }
            return false;
        }
        public Boolean DeleteRole(String user, String oldRole)
        {
            if (!_userRepo.ContainsKey(user)) return false;
            return _userRepo[user].removeRole(oldRole);
        }
        public Boolean IsInRole(String user, String role)
        {
            if (!_userRepo.ContainsKey(user)) return false;
            return _userRepo[user].isInRole(role);
        }
        public Boolean ChangeSecurityQuestion(String user, String question)
        {
            if (!_userRepo.ContainsKey(user)) return false;
            _userRepo[user].SecurityQuestion = question;
            return true;
        }
        public Boolean ChangeSecurityAnswer(String user, String answer)
        {
            if (!_userRepo.ContainsKey(user)) return false;
            _userRepo[user].SecurityAnswer = answer;
            return true;
        }

        public Boolean Register(CurricularUnitFormManager.Models.Account.AccountModels.RegisterModel model)
        {

            if (_userRepo.ContainsKey(model.UserName))
            {
                return false;
            }
            string requestedDomain = HttpContext.Current.Request.ServerVariables["HTTP_HOST"];

            // Attempt to register the user
            int nbr = (new Random()).Next(100000000, 900000000);
            String email = "Hi " + model.FullName + "\n, You recently registered or updated your email preferences for Curricular Unit Forma Manager. Please enable your account by";
            email = email + " copy and paste the following link into the address bar of your browser:\n http://"+requestedDomain+"/Account/Activate?ActivationCode=" + nbr + " \nand writte the\n Activation Code : " + nbr + "\n. Thanks, Team CUFM";
            MembershipCreateStatus createStatus;
            Individual.Individual user = (Individual.Individual)Membership.CreateUser(model.UserName, model.Password, model.Email, model.SecurityQuestion, model.SecurityAnswer, true, Guid.NewGuid(), out createStatus);
            user.FullName = model.FullName;
            Mailer.SendMail(model.Email, email);

            if (createStatus == MembershipCreateStatus.Success)
            {
                _register.Add(nbr, user);
                return true;
            }
            return false;
        }
        public Boolean ConfirmRegistration(String username, String password, int confirmationCode)
        {
            if (!_register.ContainsKey(confirmationCode)) return false;
            if (_register[confirmationCode].UserName != username || _register[confirmationCode].Password != password) return false;

            Individual.Individual user = _register[confirmationCode];
            try
            {
                _userRepo.Add(username, user);
            }
            catch (Exception)
            {
               
            }
            if (!_userRepo.ContainsKey(username)) return false;
            _register.Remove(confirmationCode);
            return true;
        }


        public Boolean DeleteUser(string username, bool deleteAllRelatedData)
        {
            if (!_userRepo.ContainsKey(username)) return false;

            _userRepo[username].IsApproved = false;
            if (deleteAllRelatedData)
            {
                _userRepo.Remove(username);
            }
            return true;

        }
        public MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            if (_userRepo.ContainsKey(username))
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }
            DateTime date = DateTime.UtcNow;
            Individual.Individual i = new Individual.Individual("CUFMembershipProvider",
                                        username,
                                        providerUserKey,
                                        email,
                                        passwordQuestion,
                                        "",
                                        isApproved,
                                        false,
                                        date,
                                        date,
                                        date,
                                        date,
                                        date,
                                        false,
                                        new Random().Next(987654321) + "");
            i.Password = password;
            i.UserName = username;
            i.PhotoName = "anonymous.png";
            i.addRole(IndividualRoles.User);
            i.IndividualId = ++_id;

            status = MembershipCreateStatus.Success;
            return i;
        }

        public Boolean ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            if (!_userRepo.ContainsKey(username) || _userRepo[username].Password != password) return false;

            _userRepo[username].SecurityQuestion = newPasswordQuestion;
            _userRepo[username].SecurityAnswer = newPasswordAnswer;
            return true;
        }

        public String GetPassword(string username, string answer)
        {
            if (!_userRepo.ContainsKey(username)) return null;

            if (_userRepo[username].SecurityAnswer != answer) return null;
            return _userRepo[username].Password;

        }

        public MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (!_userRepo.ContainsKey(username)) return null;
            if (userIsOnline)
            {
                //   userIsOnline:
                //     true to update the last-activity date/time stamp for the user; false to return
                //     user information without updating the last-activity date/time stamp for the
                //     user.
            }
            return _userRepo[username];
        }

        public int MaxInvalidPasswordAttempts()
        {
            return _MAX_ATTEMPTS;
        }
        public int MinRequiredNonAlphanumericCharacters()
        {
            return _MIN_NON_ALPHA_NUM_CHARS;
        }

        public bool ValidateUser(string username, string password)
        {
            if (username == null || username.Trim().Length == 0) return false;
            if (password == null || password.Trim().Length == 0) return false;

            if (!_userRepo.ContainsKey(username)) return false;
            if (_userRepo[username].Password == password) return true;

            return false;
        }


        public Boolean HasUser(String username) { return _userRepo.ContainsKey(username); }

        public IEnumerable<Individual.Individual> getUserList()
        {
            foreach (var i in _userRepo.Values)
            {
                yield return i;
            }
        }

    }
}