using iBestRead.Abp.Ldap.Exceptions;
using Novell.Directory.Ldap;

namespace iBestRead.Abp.Ldap.Modeling
{
    public abstract class LdapEntryBase : ILdapEntry
    {
        public string[] ObjectClass { get; set; }

        public string Dn { get; set; }


        protected LdapEntryBase() { }

        protected LdapEntryBase(LdapAttributeSet attributeSet)
        {
            ObjectClass = attributeSet.GetAttribute("objectClass")?.StringValueArray;

            if (attributeSet.ContainsKey("distinguishedName"))
            {
                Dn = attributeSet.GetAttribute("distinguishedName")?.StringValue;
            }
            else if (attributeSet.ContainsKey("dn"))
            {
                Dn = attributeSet.GetAttribute("dn")?.StringValue;
            }
            else
            {
                throw new DistinguishedNameNotInAttributeSetException();
            }
        }

    }
}