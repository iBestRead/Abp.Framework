namespace iBestRead.Abp.Ldap.Modeling
{
    public interface ILdapOrganization : ILdapEntry
    {
        string Ou { get; set; }
    }
}