using iBestRead.Abp.Ldap.Options;
using Microsoft.Extensions.Options;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Testing;
using Xunit;

namespace iBestRead.Abp.Ldap
{
    public class LdapOrganizationManager_Tests : AbpIntegratedTest<LdapTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }

        private readonly ILdapOrganizationManager _ldapOrganizationManager;
        private readonly AbpLdapOptions _ldapOptions;
        public LdapOrganizationManager_Tests()
        {
            _ldapOrganizationManager = GetRequiredService<ILdapOrganizationManager>();
            _ldapOptions = GetRequiredService<IOptions<AbpLdapOptions>>().Value;
        }

        [Fact]
        public void GetAll_With_Empty_Condition()
        {
            var result = _ldapOrganizationManager.GetAll();
        
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);
            result.ShouldContain(e => e.Ou == "Domain Controllers");
        }

        [Fact]
        public void GetAll_With_Name()
        {
            var result = _ldapOrganizationManager.GetAll("Domain Controllers");
        
            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetUsers_With_Non_Existent_Name()
        {
            var result = _ldapOrganizationManager.GetAll("NonExistentNameA");
        
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [Fact]
        public void Get_With_Dn()
        {
            var result = _ldapOrganizationManager.Get($"OU=Domain Controllers,{_ldapOptions.DomainDistinguishedName}");

            result.ShouldNotBeNull();
        }

        [Fact]
        public void Get_With_Non_Existent_Dn()
        {
            var result = _ldapOrganizationManager.Get($"OU=NonExistentNameA,{_ldapOptions.DomainDistinguishedName}");

            result.ShouldBeNull();
        }
    }
}