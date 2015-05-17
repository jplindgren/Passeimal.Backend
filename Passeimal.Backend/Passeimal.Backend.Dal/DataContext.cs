
using Passeimal.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Dal{
    public class DataContext : DbContext, IDataContext{
        public DataContext() : base("DefaultConnection") { }

        //sets
        public IDbSet<Step> Steps { get; set; }
        public IDbSet<Place> Places { get; set; }

        public System.Data.Entity.IDbSet<T> Set<T>() where T : class {
            return base.Set<T>();
        }

        public void ExecuteCommand(string command, params object[] parameters) {
            base.Database.ExecuteSqlCommand(command, parameters);
        }

        public int SaveChanges() {
            return base.SaveChanges();
        }
    }// class
}
