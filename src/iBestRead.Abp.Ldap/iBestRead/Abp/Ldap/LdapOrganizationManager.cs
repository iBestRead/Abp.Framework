using System.Collections.Generic;
using iBestRead.Abp.Ldap.Modeling;
using iBestRead.Abp.Ldap.Options;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace iBestRead.Abp.Ldap
{
    public class LdapOrganizationManager : LdapAbstractManager, ILdapOrganizationManager, ISingletonDependency
    {
        public LdapOrganizationManager(IOptions<AbpLdapOptions> ldapSettingsOptions)
            : base(ldapSettingsOptions)
        {
        }

        /// <summary>
        /// query the specified organizations.
        /// 
        /// filter: (&(ou=xxx)(objectClass=organizationalUnit)) when organizationName is not null
        /// filter: (&(objectClass=organizationalUnit)) when organizationName is null
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<LdapOrganization> GetAll(string name = null)
        {
            var conditions = new Dictionary<string, string>
            {
                {"ou", name},
                {"objectClass", "organizationalUnit"},
            };
            return Query<LdapOrganization>(LdapOptions.SearchBase, conditions);
        }

        /// <summary>
        /// query the specified organization.
        /// 
        /// filter: (&(|(dn=xxx)(distinguishedName=xxx))(objectClass=organizationalUnit)) when organizationName is not null
        /// 
        /// </summary>
        /// <param name="dn"></param>
        /// <returns></returns>
        public LdapOrganization Get(string dn)
        {
            dn = Check.NotNullOrWhiteSpace(dn, nameof(dn));
            var conditions = new Dictionary<string, string>
            {
                {"distinguishedName", dn},
                {"objectClass", "organizationalUnit"},
            };
            return QueryOne<LdapOrganization>(LdapOptions.SearchBase, conditions);
        }

    }
}