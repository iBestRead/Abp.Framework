using System.Collections.Generic;
using iBestRead.Abp.Ldap.Modeling;
using iBestRead.Abp.Ldap.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using Volo.Abp.DependencyInjection;

namespace iBestRead.Abp.Ldap
{
    public abstract class LdapAbstractManager 
    {
        protected readonly string[] Attributes =
        {
            "objectClass", "distinguishedName", "dn",
            "ou",
            "cn", "mail"
        };

        public ILogger<LdapAbstractManager> Logger { get; set; }
        public IHybridServiceScopeFactory HybridServiceScopeFactory { get; set; }

        public AbpLdapOptions LdapOptions => _options.Value;

        private readonly IOptions<AbpLdapOptions> _options;

        protected LdapAbstractManager(IOptions<AbpLdapOptions> ldapSettingsOptions)
        {
            _options = ldapSettingsOptions;
        }

        protected virtual ILdapConnection GetConnection(string bindUserName = null, string bindUserPassword = null)
        {
            // bindUserName/bindUserPassword only be used when authenticate
            bindUserName ??= LdapOptions.Credentials.DomainUserName;
            bindUserPassword ??= LdapOptions.Credentials.Password;

            var ldapConnection = new LdapConnection() { SecureSocketLayer = LdapOptions.UseSsl };
            if (LdapOptions.UseSsl)
            {
                ldapConnection.UserDefinedServerCertValidationDelegate += (sender, certificate, chain, sslPolicyErrors) => true;
                ldapConnection.SecureSocketLayer = true;
            }
            ldapConnection.Connect(LdapOptions.ServerHost, LdapOptions.ServerPort);
            ldapConnection.Bind(bindUserName, bindUserPassword);

            return ldapConnection;
        }

        

        protected virtual IList<T> Query<T>(string searchBase, Dictionary<string, string> conditions) where T : class, ILdapEntry
        {
            var filter = LdapHelps.BuildFilter(conditions);

            var result = new List<T>();

            using var ldapConnection = GetConnection();

            var search = ldapConnection.Search(
                searchBase,
                LdapConnection.ScopeSub,
                filter,
                Attributes,
                false);

            while (search.HasMore())
            {
                try
                {
                    var nextEntry = search.Next();
                    if (typeof(T) == typeof(LdapOrganization))
                    {
                        result.Add(new LdapOrganization(nextEntry.GetAttributeSet()) as T);
                    }

                    if (typeof(T) == typeof(LdapUser))
                    {
                        result.Add(new LdapUser(nextEntry.GetAttributeSet()) as T);
                    }
                }
                catch (LdapException e)
                {
                    Logger.LogException(e);
                }
            }

            return result;
        }

        protected virtual T QueryOne<T>(string searchBase, Dictionary<string, string> conditions) where T : class, ILdapEntry
        {
            var filter = LdapHelps.BuildFilter(conditions);

            using var ldapConnection = GetConnection();

            var search = ldapConnection.Search(
                searchBase,
                LdapConnection.ScopeSub,
                filter,
                Attributes,
                false);

            while (search.HasMore())
            {
                try
                {
                    var nextEntry = search.Next();
                    if (typeof(T) == typeof(LdapOrganization))
                    {
                        return new LdapOrganization(nextEntry.GetAttributeSet()) as T;
                    }

                    if (typeof(T) == typeof(LdapUser))
                    {
                        return new LdapUser(nextEntry.GetAttributeSet()) as T;
                    }
                }
                catch (LdapException e)
                {
                    Logger.LogException(e);
                }
            }

            return null;
        }

    }
}