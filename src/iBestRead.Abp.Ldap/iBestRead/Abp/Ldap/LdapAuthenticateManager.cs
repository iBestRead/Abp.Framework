using System;
using iBestRead.Abp.Ldap.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ExceptionHandling;

namespace iBestRead.Abp.Ldap
{
    public class LdapAuthenticateManager : LdapAbstractManager, ILdapAuthenticateManager, ISingletonDependency
    {
        public LdapAuthenticateManager(IOptions<AbpLdapOptions> ldapSettingsOptions)
            : base(ldapSettingsOptions)
        {
        }

        /// <summary>
        /// Authenticate 
        /// </summary>
        /// <param name="userDomainName">E.g administrator@yourdomain.com.cn </param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Authenticate(string userDomainName, string password)
        {
            try
            {
                using (GetConnection(userDomainName, password))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                using var scope = HybridServiceScopeFactory.CreateScope();
                scope.ServiceProvider
                    .GetRequiredService<IExceptionNotifier>()
                    .NotifyAsync(ex);

                return false;
            }
        }
    }
}