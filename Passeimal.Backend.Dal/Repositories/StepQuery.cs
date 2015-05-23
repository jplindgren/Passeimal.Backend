using Passeimal.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Passeimal.Backend.Dal.Repositories {
    public class StepQuery {
        public StepQuery(string connectionString){
            this.connectionString = connectionString;
        }

        public IEnumerable<Step> GetAll() {
            var connection = new SqlConnection(this.connectionString);
            var steps = connection.GetAll<Step>();
            return steps;
        }

        public string connectionString { get; set; }
    } //class
}
