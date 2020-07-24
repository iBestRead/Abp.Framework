using Volo.Abp;

namespace iBestRead.Abp.Ldap.Exceptions
{
    public class OrganizationNotExistException : BusinessException
    {
        public OrganizationNotExistException(string distinguishedName)
            : base("LDAP:000002", $"the organization distinguished named {distinguishedName} does not exist.")
        {

        }
    }
}