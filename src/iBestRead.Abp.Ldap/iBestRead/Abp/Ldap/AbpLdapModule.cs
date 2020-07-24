using iBestRead.Abp.Ldap.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace iBestRead.Abp.Ldap
{
    [DependsOn(
        typeof(AbpAutofacModule)
    )]
    public class AbpLdapModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<AbpLdapOptions>(configuration.GetSection("LDAP"));
        }
    }
}