namespace iBestRead.Abp.Ldap
{
    public interface ILdapAuthenticateManager
    {
        /// <summary>
        /// Authenticate 
        /// </summary>
        /// <param name="userDomainName">E.g administrator@yourdomain.com.cn </param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Authenticate(string userDomainName, string password);
    }
}