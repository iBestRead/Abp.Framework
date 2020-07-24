using Novell.Directory.Ldap;

namespace iBestRead.Abp.Ldap.Modeling
{
    public class LdapUser : LdapEntryBase, ILdapUser
    {
        public string Cn { get; set; }
        public string Mail { get; set; }


        public LdapUser() { }

        public LdapUser( LdapAttributeSet attributeSet)
            : base(attributeSet)
        {
            Cn = attributeSet.GetAttribute("cn")?.StringValue;
            if(attributeSet.ContainsKey("mail"))
                Mail = attributeSet.GetAttribute("mail")?.StringValue;
        }
    }
}