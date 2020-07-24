using iBestRead.Abp.Ldap.Options;
using Microsoft.Extensions.Options;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;
using Xunit;

namespace iBestRead.Abp.Ldap
{
    
    public class LdapAuthenticateManager_Tests : AbpIntegratedTest<LdapTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }

        private readonly ILdapAuthenticateManager _authenticateManager;
        private readonly AbpLdapOptions _ldapOptions;
        public LdapAuthenticateManager_Tests()
        {
            _authenticateManager = GetRequiredService<ILdapAuthenticateManager>();
            _ldapOptions = GetRequiredService<IOptions<AbpLdapOptions>>().Value;
        }

        [Fact]
        public void Authenticate()
        {
            var result = _authenticateManager.Authenticate(_ldapOptions.Credentials.DomainUserName, _ldapOptions.Credentials.Password);

            result.ShouldBeTrue();
        }

        [Fact]
        public void Authenticate_With_Wrong_Password()
        {
            var result = _authenticateManager.Authenticate("NonExistentNameA", "PasswordA");

            result.ShouldBeFalse();
        }

    }

}