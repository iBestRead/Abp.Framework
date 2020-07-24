namespace iBestRead.Abp.Ldap.Modeling
{
    public interface ILdapUser : ILdapEntry
    {
        string Cn { get; set; }
        string Mail { get; set; }
    }
}