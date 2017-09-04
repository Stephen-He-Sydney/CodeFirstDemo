using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using UBTTest.Repository;
using UBTTest.Data.Domain;
using UBTTest.Data;


namespace UBTTest.Business
{
    public class ServiceLayerBootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, UBTContext>();

            container.RegisterType<IRepository<Make>, EFRepository<Make>>();
            container.RegisterType<IRepository<Model>, EFRepository<Model>>();
          
            container.RegisterType<IDataService, DataService>();
        }
    }
}
