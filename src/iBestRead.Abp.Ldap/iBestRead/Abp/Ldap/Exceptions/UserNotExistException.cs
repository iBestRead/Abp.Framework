using Volo.Abp;

namespace iBestRead.Abp.Ldap.Exceptions
{
    public class UserNotExistException : BusinessException
    {
        public UserNotExistException(string distinguishedName)
            : base("LDAP:000003", $"the user distinguished named {distinguishedName} does not exist.")
        {

        }
    }
}