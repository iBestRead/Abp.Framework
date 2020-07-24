using System.Collections.Generic;
using iBestRead.Abp.Ldap.Modeling;

namespace iBestRead.Abp.Ldap
{
    public interface ILdapUserManager
    {
        /// <summary>
        /// query the specified users.
        /// 
        /// filter: (&(cn=xxx)(objectClass=user)) when userCommonName is not null
        /// filter: (&(objectClass=user)) when userCommonName is null
        /// 
        /// </summary>
        /// <param name="cn"></param>
        /// <returns></returns>
        IList<LdapUser> GetAll(string cn = null);

        /// <summary>
        /// query the specified users.
        /// 
        /// filter: (&((mail=mail)) when mail is not null
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        IList<LdapUser> GetAllByMail(string mail);

        /// <summary>
        /// query the specified users.
        /// 
        /// filter: (&((sAMAccountName=sAMAccountName)) when sAMAccountName is not null
        /// 
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        IList<LdapUser> GetAllBySamAccountName(string samAccountName);

        /// <summary>
        /// query the specified User.
        /// 
        /// filter: (&(dn=xxx)(objectCategory=person)(objectClass=user)) when dn is not null
        /// 
        /// </summary>
        /// <param name="dn"></param>
        /// <returns></returns>
        LdapUser Get(string dn);

    }
}