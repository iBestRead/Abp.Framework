using iBestRead.Abp.Ldap.Options;
using Microsoft.Extensions.Options;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Testing;
using Xunit;

namespace iBestRead.Abp.Ldap
{
    public class LdapUserManager_Tests : AbpIntegratedTest<LdapTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }

        private readonly ILdapUserManager _ldapUserManager;
        private readonly AbpLdapOptions _ldapOptions;
        public LdapUserManager_Tests()
        {
            _ldapUserManager = GetRequiredService<ILdapUserManager>();
            _ldapOptions = GetRequiredService<IOptions<AbpLdapOptions>>().Value;
        }
        
        [Fact]
        public void GetAll_With_Empty_Condition()
        {
            var result = _ldapUserManager.GetAll();
        
            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetAll_With_Name()
        {
            var result = _ldapUserManager.GetAll("administrator");

            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetAll_With_Mail()
        {
            var result = _ldapUserManager.GetAllByMail("xuliang@yhglobal.com");

            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetAll_With_SamAccountName()
        {
            var result = _ldapUserManager.GetAllBySamAccountName("xuliang");

            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetAll_With_Non_Existent_Name()
        {
            var result = _ldapUserManager.GetAll("NonExistentNameA");

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [Fact]
        public void Get_With_Dn()
        {
            var result = _ldapUserManager.Get($"CN=Administrator,CN=Users,{_ldapOptions.DomainDistinguishedName}");

            result.ShouldNotBeNull();
        }

        [Fact]
        public void Get_With_Non_Existent_Dn()
        {
            var result = _ldapUserManager.Get($"OU=NonExistentNameA,{_ldapOptions.DomainDistinguishedName}");

            result.ShouldBeNull();
        }
        
        [Fact]
        public void Can_DisableUser()
        {
            var result =
                _ldapUserManager.DisableUser($"CN=Administrator,CN=Users,{_ldapOptions.DomainDistinguishedName}");
            
            result.ShouldBeTrue();
            
            
        }
        
    }
}