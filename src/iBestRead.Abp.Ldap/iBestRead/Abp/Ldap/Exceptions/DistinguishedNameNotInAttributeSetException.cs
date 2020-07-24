using Volo.Abp;
namespace iBestRead.Abp.Ldap.Exceptions
{

    public class DistinguishedNameNotInAttributeSetException : BusinessException
    {
        public DistinguishedNameNotInAttributeSetException()
            : base("LDAP:000001", "the key dn or distinguishedname is not in attribute set.")
        {

        }
    }
}