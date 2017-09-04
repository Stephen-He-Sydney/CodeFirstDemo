using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBTTest.Data
{
    public class UBTContextCustomInitializer : IDatabaseInitializer<UBTContext>
    {
        public void InitializeDatabase(UBTContext context)
        {
            if (!context.Database.Exists())
            {
                context.Database.Create();
            }
        }
    }
}
