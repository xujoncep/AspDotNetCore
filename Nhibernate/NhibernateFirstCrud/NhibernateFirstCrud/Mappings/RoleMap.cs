namespace NhibernateFirstCrud.Mappings;
using NhibernateFirstCrud.Models.Entities;
using FluentNHibernate.Mapping;



public class RoleMap : ClassMap<Role>
{
    public RoleMap()
    {
        Table("Roles");
        Id(x => x.Id);
        Map(x => x.Name);
       
    }
}
