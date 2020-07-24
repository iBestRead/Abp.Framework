using Novell.Directory.Ldap;

namespace iBestRead.Abp.Ldap.Modeling
{
    public class LdapOrganization : LdapEntryBase, ILdapOrganization
    {
        public string Ou { get; set; }

        public LdapOrganization() { }

        public LdapOrganization(LdapAttributeSet attributeSet)
            : base(attributeSet)
        {
            Ou = attributeSet.GetAttribute("ou")?.StringValue;
        }
    }
}