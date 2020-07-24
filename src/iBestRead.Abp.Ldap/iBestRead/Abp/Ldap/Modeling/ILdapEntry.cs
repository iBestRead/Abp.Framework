namespace iBestRead.Abp.Ldap.Modeling
{
    public interface ILdapEntry
    {
        public string[] ObjectClass { get; set; }

        public string Dn { get; set; }
    }
}