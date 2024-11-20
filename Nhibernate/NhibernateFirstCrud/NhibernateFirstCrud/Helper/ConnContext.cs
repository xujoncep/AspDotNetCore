using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NhibernateFirstCrud.Mappings;

namespace NhibernateMvcApp.Data
{
    public static class ConnContext
    {
        private static ISessionFactory? _sessionFactory;
        

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012
                            .ConnectionString("Server=DESKTOP-010\\SQLEXPRESS01;Database=NhibernateDb;Trusted_Connection=True;"))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static NHibernate.ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
