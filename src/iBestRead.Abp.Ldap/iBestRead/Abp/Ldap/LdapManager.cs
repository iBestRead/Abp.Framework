// using System;
// using Microsoft.Extensions.Options;
// using Novell.Directory.Ldap;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using Microsoft.Extensions.DependencyInjection;
// using Volo.Abp.DependencyInjection;
// using Volo.Abp.ExceptionHandling;
// using iBestRead.Abp.Ldap.Exceptions;
// using iBestRead.Abp.Ldap.Modeling;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Logging.Abstractions;
// using Volo.Abp;
//
// namespace iBestRead.Abp.Ldap
// {
//     /// <inheritdoc cref="ILdapManager"/>
//     public  class LdapManager : ILdapManager, ISingletonDependency
//     {
//
//         private readonly string[] _attributes =
//         {
//             "objectCategory", "objectClass", "cn", "name", "distinguishedName",
//             "ou",
//             "sAMAccountName", "userPrincipalName", "telephoneNumber", "mail"
//         };
//
//         public ILogger<LdapManager> Logger { get; set; }
//
//         private readonly string _searchBase;
//         private readonly AbpLdapOptions _ldapOptions;
//         private readonly IHybridServiceScopeFactory _hybridServiceScopeFactory;
//
//         public LdapManager(IOptions<AbpLdapOptions> ldapSettingsOptions, IHybridServiceScopeFactory hybridServiceScopeFactory)
//         {
//             Logger = NullLogger<LdapManager>.Instance;
//
//             _ldapOptions = ldapSettingsOptions.Value;
//             _searchBase = _ldapOptions.SearchBase;
//             _hybridServiceScopeFactory = hybridServiceScopeFactory;
//         }
//
//         #region Organization
//         
//
//         #endregion
//
//         #region User
//
//         
//
//         #endregion
//
//         #region Authenticate
//
//
//
//
//         #endregion
//
//
//
//
//         private ILdapConnection GetConnection(string bindUserName = null, string bindUserPassword = null)
//         {
//             // bindUserName/bindUserPassword only be used when authenticate
//             bindUserName ??= _ldapOptions.Credentials.DomainUserName;
//             bindUserPassword ??= _ldapOptions.Credentials.Password;
//
//             var ldapConnection = new LdapConnection() { SecureSocketLayer = _ldapOptions.UseSsl };
//             if (_ldapOptions.UseSsl)
//             {
//                 ldapConnection.UserDefinedServerCertValidationDelegate += (sender, certificate, chain, sslPolicyErrors) => true;
//             }
//             ldapConnection.Connect(_ldapOptions.ServerHost, _ldapOptions.ServerPort);
//
//             if (_ldapOptions.UseSsl)
//             {
//                 ldapConnection.Bind(LdapConnection.LdapV3, bindUserName, bindUserPassword);
//             }
//             else
//             {
//                 ldapConnection.Bind(bindUserName, bindUserPassword);
//             }
//             return ldapConnection;
//         }
//
//         private IList<T> Query<T>(string searchBase, Dictionary<string, string> conditions) where T : class, ILdapEntry
//         {
//             var filter = LdapHelps.BuildFilter(conditions);
//
//             var result = new List<T>();
//
//             using var ldapConnection = GetConnection();
//
//             var search = ldapConnection.Search(
//                 searchBase,
//                 LdapConnection.ScopeSub,
//                 filter,
//                 _attributes,
//                 false);
//
//             while (search.HasMore())
//             {
//                 try
//                 {
//                     var nextEntry = search.Next();
//                     if (typeof(T) == typeof(LdapOrganization))
//                     {
//                         result.Add(new LdapOrganization(nextEntry.GetAttributeSet()) as T);
//                     }
//
//                     if (typeof(T) == typeof(LdapUser))
//                     {
//                         result.Add(new LdapUser(nextEntry.GetAttributeSet()) as T);
//                     }
//                 }
//                 catch (LdapException e)
//                 {
//                     Logger.LogException(e);
//                 }
//             }
//
//             return result;
//         }
//
//         private T QueryOne<T>(string searchBase, Dictionary<string, string> conditions) where T : class, ILdapEntry
//         {
//             var filter = LdapHelps.BuildFilter(conditions);
//
//             using var ldapConnection = GetConnection();
//
//             var search = ldapConnection.Search(
//                 searchBase,
//                 LdapConnection.ScopeSub,
//                 filter,
//                 _attributes,
//                 false);
//
//             while (search.HasMore())
//             {
//                 try
//                 {
//                     var nextEntry = search.Next();
//                     if (typeof(T) == typeof(LdapOrganization))
//                     {
//                         return new LdapOrganization(nextEntry.GetAttributeSet()) as T;
//                     }
//
//                     if (typeof(T) == typeof(LdapUser))
//                     {
//                         return new LdapOrganization(nextEntry.GetAttributeSet()) as T;
//                     }
//                 }
//                 catch (LdapException e)
//                 {
//                     Logger.LogException(e);
//                 }
//             }
//             
//             return null;
//         }
//
//     }
// }