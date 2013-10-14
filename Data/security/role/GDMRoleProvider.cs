using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Data.orm;

namespace Data.security.role
{
    public class GDMRoleProvider : RoleProvider
    {
        #region Constructor(s) with UnitOfWOrk
        private UnitOfWork _unitOfWork;
        public UnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
            set { this._unitOfWork = value; }
        }
        public GDMRoleProvider(): this(new UnitOfWork())
        {
        }

        public GDMRoleProvider(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (string.IsNullOrEmpty(name))
                name = "ExtendedAdapterRoleProvider";

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Adapter Extended Role Provider");
            }
            base.Initialize(name, config);

            ApplicationName = GetValueOrDefault(config, "applicationName", o => o.ToString(), "GDM");

            CheckIfConfigAttributesAreApplicable(config);
        }
        #endregion

        #region Implemented Membership Properties
        public string ConnectionStringName { get; set; }
        public override string ApplicationName { get; set; }
        #endregion

        #region Implemented Membership Methods
        public override void AddUsersToRoles(string[] userNames, string[] roleNames)
        {
            if (userNames == null) throw new ArgumentNullException("userNames");
            if (roleNames == null) throw new ArgumentNullException("roleNames");

            if (userNames.Length == 0 || roleNames.Length == 0) return;

            if (userNames.Any(x => x == null)) throw new ArgumentException("User name cannot be null.", "userNames");
            if (userNames.Any(x => x == string.Empty)) throw new ArgumentException("User name cannot be empty.", "userNames");

            if (roleNames.Any(x => x == null)) throw new ArgumentException("Role name cannot be null.", "roleNames");
            if (roleNames.Any(x => x == string.Empty)) throw new ArgumentException("Role name cannot be empty.", "roleNames");

            if (roleNames.Length > 0 && roleNames.Length > 0)
            {
                ICollection<Models.Role> roles = new List<Models.Role>();
                foreach (var roleName in roleNames)
                {
                    var roleObj = this.UnitOfWork.RoleRepository.Get(role => role.Name.Equals(roleName)).FirstOrDefault();
                    if (roleObj != null)
                        roles.Add(roleObj);
                }
                var rolesThatDontExist = roleNames.Except(roles.Select(role => role.Name));
                if (rolesThatDontExist.Any()) throw new ProviderException("Role " + rolesThatDontExist.First() + " doesn't exist.");

                ICollection<Models.User> users = new List<Models.User>();
                foreach (var userName in userNames)
                {
                    var userObj = this.UnitOfWork.UserRepository.Get(user => user.UserName.Equals(userName)).FirstOrDefault();
                    if (userObj != null)
                        users.Add(userObj);
                }
                var usersThatDontExist = userNames.Except(users.Select(user => user.UserName));
                if (usersThatDontExist.Any()) throw new ProviderException("User " + usersThatDontExist.First() + " doesn't exist.");

                foreach (var user in users)
                {
                    foreach (var role in roles)
                    {
                        user.Roles.Add(role);
                    }
                    this.UnitOfWork.UserRepository.Update(user);
                }
                this.UnitOfWork.Save();
            }
            else
            {
                throw new ArgumentException("You didn't provided any usernames and/or rolenames.");
            }
        }

        public override bool RoleExists(string roleName)
        {
            if (roleName == null) throw new ArgumentNullException("roleName");
            if (roleName == string.Empty) throw new ArgumentException("Value cannot be empty.", "roleName");

            var roleObj = this.UnitOfWork.RoleRepository.Get(role => role.Name.Equals(roleName)).FirstOrDefault();
            return roleObj != null;
        }

        public override void CreateRole(string roleName)
        {
            if (roleName == null) throw new ArgumentNullException("roleName");
            if (roleName == string.Empty) throw new ArgumentException("Role name cannot be empty.", "roleName");

            var roles = this.UnitOfWork.RoleRepository.Get(role => role.Name.Equals(roleName));
            if (roles.Count() > 0)
                throw new ProviderException(string.Format("Role {0} already exists. Cannot create a new role with the same name.", roleName));

            var roleObj = new Models.Role();
            roleObj.Name = roleName;
            roleObj.Description = roleName;

            this.UnitOfWork.RoleRepository.Insert(roleObj);
            this.UnitOfWork.Save();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            var roleObj = this.UnitOfWork.RoleRepository.Get(role => role.Name.Equals(roleName)).FirstOrDefault();

            if (roleObj == null) throw new ProviderException(string.Format("Role {0} does not exist.", roleName));

            if (throwOnPopulatedRole && roleObj.Users != null && roleObj.Users.Count > 0)
            {
                throw new ProviderException(string.Format("Role {0} contains users. As throwOnPopulatedRole is true, refusing to delete.", roleName));
            }

            this.UnitOfWork.RoleRepository.Delete(roleObj);
            this.UnitOfWork.Save();

            return true;
        }

        public override bool IsUserInRole(string userName, string roleName)
        {
            if (userName == null) throw new ArgumentNullException("userName");
            if (userName == string.Empty) throw new ArgumentException("User name cannot be empty.", "userName");

            if (roleName == null) throw new ArgumentNullException("roleName");
            if (roleName == string.Empty) throw new ArgumentException("Role name cannot be empty.", "roleName");

            var userObj = this.UnitOfWork.UserRepository.Get(user => user.UserName.Equals(userName)).FirstOrDefault();
            if (userObj == null)
                throw new ProviderException(string.Format("User {0} does not exist.", userName));

            var roleObj = this.UnitOfWork.RoleRepository.Get(role => role.Name.Equals(roleName)).FirstOrDefault();
            if (roleObj == null)
                throw new ProviderException(string.Format("Role {0} does not exist.", roleName));

            return userObj.Roles.Contains(roleObj);
        }

        public override string[] GetAllRoles()
        {
            var roles = this.UnitOfWork.RoleRepository.Get().Select(role => role.Name);
            return roles.ToArray();
        }

        public override string[] GetRolesForUser(string userName)
        {
            if (userName == null) throw new ArgumentNullException("userName");
            if (userName == string.Empty) throw new ArgumentException("User name cannot be empty.", "userName");

            var userObj = this.UnitOfWork.UserRepository.Get(user => user.UserName.Equals(userName)).FirstOrDefault();
            if (userObj == null) 
                    throw new ProviderException(string.Format("User {0} does not exist.", userName));

            var roles = userObj.Roles.Select(role => role.Name);
            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            if (roleName == null) throw new ArgumentNullException("roleName");
            if (roleName == string.Empty) throw new ArgumentException("Role name cannot be empty.", "roleName");

            var roleObj = this.UnitOfWork.RoleRepository.Get(role => role.Name.Equals(roleName)).FirstOrDefault();
            if (roleObj == null) 
                throw new ProviderException(string.Format("Role {0} does not exist.", roleName));

            var users = roleObj.Users.Select(user => user.UserName);
            return users.ToArray();
        }
        #endregion

        #region Non-Implemented Membership Methods
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
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
            config.Remove("connectionString");
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
        #endregion
    }
}
