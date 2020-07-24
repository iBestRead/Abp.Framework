using System.Collections.Generic;
using System.Linq;
using System.Text;
using iBestRead.Abp.Ldap.Exceptions;
using iBestRead.Abp.Ldap.Modeling;
using iBestRead.Abp.Ldap.Options;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace iBestRead.Abp.Ldap
{
    public class LdapUserManager : LdapAbstractManager, ILdapUserManager, ISingletonDependency
    {
        public LdapUserManager(IOptions<AbpLdapOptions> ldapSettingsOptions)
            : base(ldapSettingsOptions)
        {
        }

        /// <summary>
        /// query the specified users.
        /// 
        /// filter: (&(cn=xxx)(objectClass=user)) when userCommonName is not null
        /// filter: (&(objectClass=user)) when userCommonName is null
        /// 
        /// </summary>
        /// <param name="cn"></param>
        /// <returns></returns>
        public IList<LdapUser> GetAll(string cn = null)
        {
            var conditions = new Dictionary<string, string>
            {
                {"objectClass", "user"},
                {"cn", cn},
            };
            return Query<LdapUser>(LdapOptions.SearchBase, conditions);
        }

        /// <summary>
        /// query the specified users.
        /// 
        /// filter: (&((mail=mail)) when mail is not null
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public IList<LdapUser> GetAllByMail(string mail)
        {
            Check.NotNullOrEmpty(mail, nameof(mail));
            var conditions = new Dictionary<string, string>
            {
                {"mail", mail},
            };
            return Query<LdapUser>(LdapOptions.SearchBase, conditions);
        }

        /// <summary>
        /// query the specified users.
        /// 
        /// filter: (&((sAMAccountName=sAMAccountName)) when sAMAccountName is not null
        /// 
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public IList<LdapUser> GetAllBySamAccountName(string samAccountName)
        {
            Check.NotNullOrEmpty(samAccountName, nameof(samAccountName));
            var conditions = new Dictionary<string, string>
            {
                {"sAMAccountName", samAccountName},
            };
            return Query<LdapUser>(LdapOptions.SearchBase, conditions);
        }

        /// <summary>
        /// query the specified User.
        /// 
        /// filter: (&(dn=xxx)(objectCategory=person)(objectClass=user)) when dn is not null
        /// 
        /// </summary>
        /// <param name="dn"></param>
        /// <returns></returns>
        public LdapUser Get(string dn)
        {
            dn = Check.NotNullOrWhiteSpace(dn, nameof(dn));
            var conditions = new Dictionary<string, string>
            {
                {"objectClass", "user"},
                {"distinguishedName", dn},
            };
            return QueryOne<LdapUser>(LdapOptions.SearchBase, conditions);
        }

        public void ResetPassword(string dn, string newPassword)
        {
            dn = Check.NotNullOrWhiteSpace(dn, nameof(dn));
            newPassword = Check.NotNullOrWhiteSpace(newPassword, nameof(newPassword));
            var user = Get(dn);
            if (null == user)
            {
                throw new UserNotExistException(dn);
            }

            ResetPassword(user, newPassword);
        }

        public void ResetPassword(LdapUser ldapUser, string newPassword)
        {
            var encodedBytes = Encoding.Unicode.GetBytes($"\"{newPassword}\"").ToArray();

            var attribute = new LdapAttribute("unicodePwd", encodedBytes);
            var modification = new LdapModification(LdapModification.Replace, attribute);

            using var ldapConnection = GetConnection();
            ldapConnection.Modify(ldapUser.Dn, modification);
        }

        public void DisableUser(LdapUser ldapUser)
        {
            var attribute = new LdapAttribute("userAccountControl", "514");
            var modification = new LdapModification(LdapModification.Replace, attribute);

            using var ldapConnection = GetConnection();
            ldapConnection.Modify(ldapUser.Dn, modification);
        }

        public void DisableUser(string dn)
        {
            dn = Check.NotNullOrWhiteSpace(dn, nameof(dn));

            var user = Get(dn);
            if (null == user)
            {
                throw new UserNotExistException(dn);
            }

            DisableUser(user);
        }

    }
}