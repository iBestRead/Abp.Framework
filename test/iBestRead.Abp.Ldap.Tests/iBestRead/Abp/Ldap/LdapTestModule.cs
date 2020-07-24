using iBestRead.Abp.Ldap.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace iBestRead.Abp.Ldap
{
    [DependsOn(typeof(AbpLdapModule))]
    public class LdapTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<LdapTestModule>();

            var configurationRoot = builder.Build();

            // if you want run unit test
            // first edit your secrets.json

            // not use ssl
            // "LDAP": {
            //     "ServerHost": "127.0.0.1",
            //     "ServerPort": 389,
            //     "UseSSL": false,
            //     "Credentials": {
            //         "DomainUserName": "yourUser@yourdomain.com.cn",
            //         "Password": "yourPassword"
            //     },
            //     "SearchBase": "CN=Users,DC=yourDomain,DC=com,DC=cn",
            //     "DomainName": "yourDomain.com.cn",
            //     "DomainDistinguishedName": "DC=yourDomain,DC=com,DC=cn"
            // }

            // use ssl
            // "LDAP": {
            //     "ServerHost": "127.0.0.1",
            //     "ServerPort": 636,
            //     "UseSSL": true,
            //     "Credentials": {
            //         "DomainUserName": "yourUser@yourdomain.com.cn",
            //         "Password": "yourPassword"
            //     },
            //     "SearchBase": "CN=Users,DC=yourDomain,DC=com,DC=cn",
            //     "DomainName": "yourDomain.com.cn",
            //     "DomainDistinguishedName": "DC=yourDomain,DC=com,DC=cn"
            // }

            Configure<AbpLdapOptions>(options =>
            {
                options.ServerHost = configurationRoot.GetValue<string>("LDAP:ServerHost");
                options.ServerPort = configurationRoot.GetValue<int>("LDAP:ServerPort");
                options.UseSsl = configurationRoot.GetValue<bool>("LDAP:UseSsl");
                options.SearchBase = configurationRoot.GetValue<string>("LDAP:SearchBase");
                options.DomainName = configurationRoot.GetValue<string>("LDAP:DomainName");
                options.DomainDistinguishedName = configurationRoot.GetValue<string>("LDAP:DomainDistinguishedName");
                options.Credentials.DomainUserName = configurationRoot.GetValue<string>("LDAP:Credentials:DomainUserName");
                options.Credentials.Password = configurationRoot.GetValue<string>("LDAP:Credentials:Password");
            });
        }

    }
}