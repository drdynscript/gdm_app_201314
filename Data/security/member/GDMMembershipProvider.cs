using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Data.orm;
using WebMatrix.WebData;
using BCrypt;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Web;
using System.Configuration.Provider;
using System.Globalization;
using System.Collections;

namespace Data.security.member
{
    public class GDMMembershipProvider : ExtendedMembershipProvider 
    {   
        #region Constructor(s) with UnitOfWOrk
        private UnitOfWork _unitOfWork;
        public UnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
            set { this._unitOfWork = value; }
        }
        public GDMMembershipProvider(): this(new UnitOfWork())
        {
        }

        public GDMMembershipProvider(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (string.IsNullOrEmpty(name))
            {
                name = "ExtendedAdapterMembershipProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Adapter Extended Membership Provider");
            }
            base.Initialize(name, config);

            ApplicationName = GetValueOrDefault(config, "applicationName", o => o.ToString(), "GDM");

            EnablePasswordRetrievalInternal = GetValueOrDefault(config, "enablePasswordRetrieval", Convert.ToBoolean, false);
            EnablePasswordResetInternal = GetValueOrDefault(config, "enablePasswordReset", Convert.ToBoolean, true);
            RequiresQuestionAndAnswerInternal = GetValueOrDefault(config, "requiresQuestionAndAnswer", Convert.ToBoolean, false);
            RequiresUniqueEmailInternal = GetValueOrDefault(config, "requiresUniqueEmail", Convert.ToBoolean, true);
            MaxInvalidPasswordAttemptsInternal = GetValueOrDefault(config, "maxInvalidPasswordAttempts", Convert.ToInt32, 3);
            PasswordAttemptWindowInternal = GetValueOrDefault(config, "passwordAttemptWindow", Convert.ToInt32, 10);
            PasswordFormatInternal = GetValueOrDefault(config, "passwordFormat", o =>
            {
                MembershipPasswordFormat format;
                return Enum.TryParse(o.ToString(), true, out format) ? format : MembershipPasswordFormat.Hashed;
            }, MembershipPasswordFormat.Hashed);
            MinRequiredPasswordLengthInternal = GetValueOrDefault(config, "minRequiredPasswordLength", Convert.ToInt32, 6);
            MinRequiredNonAlphanumericCharactersInternal = GetValueOrDefault(config, "minRequiredNonalphanumericCharacters", Convert.ToInt32, 1);
            PasswordStrengthRegularExpressionInternal = GetValueOrDefault(config, "passwordStrengthRegularExpression", o => o.ToString(), string.Empty);
            HashAlgorithmType = GetValueOrDefault(config, "hashAlgorithmType", o => o.ToString(), "SHA1");

            CheckIfConfigAttributesAreApplicable(config);
        }
        #endregion

        #region Non-Implemented Mebersip Methods
        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            throw new NotImplementedException();
        }        

        public override DateTime GetCreateDate(string userName)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            throw new NotImplementedException();
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            throw new NotImplementedException();
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            throw new NotImplementedException();
        }

        public override bool IsConfirmed(string userName)
        {
            throw new NotImplementedException();
        }

        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
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

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
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

        #region Implemented Membership Properties
        private string HashAlgorithmType { get; set; }

        public override string ApplicationName { get; set; }

        public override bool EnablePasswordReset
        {
            get { return EnablePasswordResetInternal; }
        }

        private bool EnablePasswordResetInternal { get; set; }

        public override bool EnablePasswordRetrieval
        {
            get { return EnablePasswordRetrievalInternal; }
        }

        private bool EnablePasswordRetrievalInternal { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { return MaxInvalidPasswordAttemptsInternal; }
        }

        private int MaxInvalidPasswordAttemptsInternal { get; set; }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return MinRequiredNonAlphanumericCharactersInternal; }
        }

        private int MinRequiredNonAlphanumericCharactersInternal { get; set; }

        public override int MinRequiredPasswordLength
        {
            get { return MinRequiredPasswordLengthInternal; }
        }

        private int MinRequiredPasswordLengthInternal { get; set; }

        public override int PasswordAttemptWindow
        {
            get { return PasswordAttemptWindowInternal; }
        }

        private int PasswordAttemptWindowInternal { get; set; }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return PasswordFormatInternal; }
        }

        private MembershipPasswordFormat PasswordFormatInternal { get; set; }

        public override string PasswordStrengthRegularExpression
        {
            get { return PasswordStrengthRegularExpressionInternal; }
        }

        private string PasswordStrengthRegularExpressionInternal { get; set; }

        public override bool RequiresQuestionAndAnswer
        {
            get { return RequiresQuestionAndAnswerInternal; }
        }

        private bool RequiresQuestionAndAnswerInternal { get; set; }

        public override bool RequiresUniqueEmail
        {
            get { return RequiresUniqueEmailInternal; }
        }

        private bool RequiresUniqueEmailInternal { get; set; }
        #endregion   
        
        #region Implemented Membership Methods
        public override bool ValidateUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("User name cannot be empty.", "userName");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty.", "password");

            var member = this.UnitOfWork.UserRepository.Get(user => user.UserName.Equals(userName)).FirstOrDefault();
            if (member == null)
                return false;

            var internalMember = (Models.Member)member;
            if (internalMember.ConfirmedDate == null)
                return false;

            if (BCrypt.Net.BCrypt.HashPassword(password, internalMember.PasswordSalt) != internalMember.Password)
                return false;

            return true;
        }

        public override string GetUserNameFromId(int userId)
        {
            var member = this.UnitOfWork.UserRepository.GetByID(userId);
            if (member != null)
                return member.UserName;
            return null;
        }

        public override bool HasLocalAccount(int userId)
        {
            var member = this.UnitOfWork.UserRepository.Get(user => user.Id.Equals(userId)).FirstOrDefault();
            return member is Models.Member;
        }

        public override MembershipUser GetUser(string userName, bool userIsOnline)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("User name cannot be empty.", "userName");

            var member = this.UnitOfWork.UserRepository.Get(u => u.UserName.Equals(userName)).FirstOrDefault();
            if (member == null)
                return null;

            const string passwordQuestion = null;
            const string comment = null;
            const bool isApproved = true;
            const bool isLockedOut = false;
            var creationDate = member.CreatedDate;
            var lastLoginDate = DateTime.MinValue;
            var lastActivityDate = DateTime.MinValue;
            var lastPasswordChangedDate = DateTime.MinValue;
            var lastLockoutDate = DateTime.MinValue;

            return new MembershipUser(Membership.Provider.Name, userName, member.Id, member.Email, passwordQuestion, comment,
                isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate);
        }

        public override string GetUserNameByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty.", "email");

            var member = this.UnitOfWork.UserRepository.Get(user => user.Email.Equals(email)).FirstOrDefault();
            if (member != null)
                return member.UserName;
            return null;
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {
            if (string.IsNullOrEmpty(userName))
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidUserName);

            if (string.IsNullOrEmpty(password))
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidPassword);

            var member = this.UnitOfWork.UserRepository.Get(user => user.UserName.Equals(userName)).FirstOrDefault();
            if (member != null)
                throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);

            var memberToBeSaved = new Models.Member();
            memberToBeSaved.UserName = userName;
            memberToBeSaved.Email = (string)values["Email"];
            memberToBeSaved.PasswordSalt = BCrypt.Net.BCrypt.GenerateSalt(12);
            memberToBeSaved.Password = BCrypt.Net.BCrypt.HashPassword(password, memberToBeSaved.PasswordSalt);
            memberToBeSaved.ConfirmPassword = memberToBeSaved.Password;
            memberToBeSaved.ConfirmationToken = GenerateToken();
            if (!requireConfirmation)
            {
                memberToBeSaved.ConfirmedDate = DateTime.Now;
            }
                

            Models.Person person;
            switch((string)values["PersonType"])
            {
                case "PERSON":
                    person = new Models.Person();
                    break;
                case "STUDENT":
                    person = new Models.Student();
                    break;
                case "LECTURER":
                    person = new Models.Lecturer();
                    break;
                default:
                    person = new Models.Person();
                    break;
            }
            person.FirstName = (string)values["FirstName"];
            person.SurName = (string)values["SurName"];
            memberToBeSaved.Person = person;

            if ((int[])values["Roles"] != null)
            {
                var ids = (int[])values["Roles"];
                if (ids != null && ids.Length > 0)
                {
                    var roles = new List<global::Models.Role>();
                    foreach (var id in ids)
                    {
                        var role = UnitOfWork.RoleRepository.GetByID(Convert.ToInt16(id));
                        roles.Add(role);
                    }
                    memberToBeSaved.Roles = roles;
                }
                
            }

            this.UnitOfWork.UserRepository.Insert(memberToBeSaved);
            this.UnitOfWork.Save();

            return memberToBeSaved.ConfirmationToken;
        }
        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            if (string.IsNullOrEmpty(accountConfirmationToken))
                throw new ArgumentException("Activation code cannot be null.", "accountConfirmationToken");

            var member = this.UnitOfWork.MemberRepository.Get(user => user.ConfirmationToken.Equals(accountConfirmationToken)).FirstOrDefault();

            if (member == null)
                throw new ProviderException("Activation code is incorrect.");

            if(member.ConfirmedDate != null)
                throw new ProviderException("Your account is already activated.");

            member.ConfirmedDate = DateTime.Now;
            this.UnitOfWork.UserRepository.Update(member);
            this.UnitOfWork.Save();

            return true;
        }
        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("User name cannot be null.", "userName");

            if (string.IsNullOrEmpty(accountConfirmationToken))
                throw new ArgumentException("Activation code cannot be null.", "accountConfirmationToken");

            var member = this.UnitOfWork.MemberRepository.Get(user => user.UserName.Equals(userName)).FirstOrDefault();

            if (member == null)
                throw new ProviderException("The UserName doesn't exits.");

            if (member.ConfirmationToken != accountConfirmationToken)
                throw new ProviderException("The activation code is incorrect.");

            if (member.ConfirmedDate != null)
                throw new ProviderException("Your account is already activated.");

            member.ConfirmedDate = DateTime.Now;
            this.UnitOfWork.UserRepository.Update(member);
            this.UnitOfWork.Save();

            return true;
        }
        public override bool DeleteAccount(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("User name cannot be null.", "userName");

            var member = this.UnitOfWork.MemberRepository.Get(user => user.UserName.Equals(userName)).FirstOrDefault();
            if (member == null)
                throw new ProviderException("Account doesn't exists.");

            this.UnitOfWork.UserRepository.Delete(member);
            this.UnitOfWork.Save();
            return true;
        }
        #endregion

        #region OAuth Membership
        public override int GetUserIdFromOAuth(string provider, string providerUserId)
        {
            var oAuthMember = this.UnitOfWork.OAuthRepository.Get(user => user.Provider.Equals(provider) && user.ProviderUserId.Equals(providerUserId)).FirstOrDefault();
            if (oAuthMember != null)
                return oAuthMember.Id;
            return -1;
        }
        public override void DeleteOAuthAccount(string provider, string providerUserId)
        {
            var oAuthMember = this.UnitOfWork.OAuthRepository.Get(user => user.Provider.Equals(provider) && user.ProviderUserId.Equals(providerUserId)).FirstOrDefault();
            if (oAuthMember != null)
            {
                this.UnitOfWork.UserRepository.Delete(oAuthMember);
                this.UnitOfWork.Save();
            } 
        }

        public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
        {
            var member = this.UnitOfWork.UserRepository.Get(user => user.UserName.Equals(userName)).FirstOrDefault();
            if (member == null)
                throw new Exception("User was no created for the specified member");
            var oAuthMember = (Models.OAuthMember)member;
            oAuthMember.Provider = provider;
            oAuthMember.ProviderUserId = providerUserId;
            this.UnitOfWork.UserRepository.Update(oAuthMember);
            this.UnitOfWork.Save();
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            var accounts = this.UnitOfWork.OAuthRepository.Get(user => user.UserName.Equals(userName));
            if (accounts != null)
            {
                var collection = new List<OAuthAccountData>();
                foreach (var account in accounts)
                {
                    collection.Add(new OAuthAccountData(account.Provider, account.ProviderUserId));
                }
                return collection;
            }
            return null;
        }
        public override void DeleteOAuthToken(string token)
        {
            throw new NotImplementedException(); ;
        }
        public override string GetOAuthTokenSecret(string token)
        {
            throw new NotImplementedException();
        }
        public override void StoreOAuthRequestToken(string requestToken, string requestTokenSecret)
        {
            throw new NotImplementedException();
        }
        public override void ReplaceOAuthRequestTokenWithAccessToken(string requestToken, string accessToken, string accessTokenSecret)
        {
            throw new NotImplementedException();
        }        
        #endregion

        #region Helpers
        private void CheckIfConfigAttributesAreApplicable(NameValueCollection config)
        {
            config.Remove("name");
            config.Remove("description");
            config.Remove("applicationName");
            config.Remove("connectionStringName");
            config.Remove("enablePasswordRetrieval");
            config.Remove("enablePasswordReset");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("passwordFormat");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");
            config.Remove("hashAlgorithmType");
            config.Remove("connectionStringName");

            if (config.Count <= 0)
                return;
            var key = config.GetKey(0);
            if (string.IsNullOrEmpty(key))
                return;

            throw new ProviderException(
                string.Format(CultureInfo.CurrentCulture, "The role provider does not recognize the configuration attribute {0}.", key));
        }

        private static T GetValueOrDefault<T>(NameValueCollection nvc, string key, Func<object, T> converter, T defaultIfNull)
        {
            var val = nvc[key];

            if (val == null)
                return defaultIfNull;

            try
            {
                return converter(val);
            }
            catch
            {
                return defaultIfNull;
            }
        }

        private static string GenerateToken()
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                return GenerateToken(provider);
            }
        }

        private static string GenerateToken(RandomNumberGenerator generator)
        {
            var data = new byte[0x10];
            generator.GetBytes(data);
            return HttpServerUtility.UrlTokenEncode(data);
        }
        #endregion        
    }
}
