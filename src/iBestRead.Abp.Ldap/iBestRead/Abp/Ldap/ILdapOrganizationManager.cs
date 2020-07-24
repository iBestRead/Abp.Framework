using System.Collections.Generic;
using iBestRead.Abp.Ldap.Modeling;

namespace iBestRead.Abp.Ldap
{
    public interface ILdapOrganizationManager
    {
        /// <summary>
        /// query the specified organizations.
        /// 
        /// filter: (&(ou=xxx)(objectClass=organizationalUnit)) when organizationName is not null
        /// filter: (&(objectClass=organizationalUnit)) when organizationName is null
        /// 
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        IList<LdapOrganization> GetAll(string organizationName = null);

        /// <summary>
        /// query the specified organization.
        /// 
        /// filter: (&(|(dn=xxx)(distinguishedName=xxx))(objectClass=organizationalUnit)) when organizationName is not null
        /// 
        /// </summary>
        /// <param name="distinguishedName"></param>
        /// <returns></returns>
        LdapOrganization Get(string distinguishedName);
    }
}