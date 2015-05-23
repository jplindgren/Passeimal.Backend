using Passeimal.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper.Contrib.Extensions;
using Dapper;
using Passeimal.Backend.Domain.Geometry;

namespace Passeimal.Backend.Dal.Repositories {
    public class PlaceQuery {
        private string connectionString;
        public PlaceQuery(string connectionString) {
            this.connectionString = connectionString;
        }

        public IEnumerable<Place> GetByBoundingBox(int offSet, int pageSize, BoundingBox box) {
            var connection = new SqlConnection(this.connectionString);
            IEnumerable<Place> placesFound;
            StringBuilder query = new StringBuilder();

            //seems slightly slower with 88 miliseconds (cold query)
            //query.Append("select * from places where Location_Latitude > @MinLatitude and Location_Latitude < @MaxLatitude and ");
            //query.Append("Location_Longitude > @MinLongitude and Location_Longitude < @MaxLongitude ");
            //query.Append("order by relevance OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            //seems slightly faster with 59 miliseconds (cold query)
            query.Append("select Id, Name, Vicinity, Latitude, Longitude, PukePoints  from ");
            query.Append("( SELECT ROW_NUMBER()  OVER ( ORDER BY pl.Relevance desc) AS RowNum, pl.Id, pl.Name, pl.Vicinity, pl.Latitude, pl.Longitude, COALESCE(sum(pu.Severity),0) as pukePoints ");
            query.Append("FROM Places pl left join Pukes pu on pu.Place_Id = pl.Id ");
            query.Append("WHERE Latitude > @MinLatitude and Latitude < @MaxLatitude ");
            query.Append("and Longitude > @MinLongitude and Longitude < @MaxLongitude ");
            query.Append("group by pl.Id, pl.Name, pl.Vicinity, pl.Latitude, pl.Longitude, pl.Relevance) AS result ");
            query.Append("WHERE   RowNum >= @Offset ");
            query.Append("AND RowNum < @PageSize ");
            query.Append("ORDER BY RowNum");

            try {
                //best way found to create 'object values' like address. To entity type, use splitOn parameter.
                //http://stackoverflow.com/questions/8994933/call-custom-constructor-with-dapper
                placesFound = connection.Query(query.ToString(),
                                        new {
                                            MinLatitude = box.MinPoint.Latitude,
                                            MinLongitude = box.MinPoint.Longitude,
                                            MaxLatitude = box.MaxPoint.Latitude,
                                            MaxLongitude = box.MaxPoint.Longitude,
                                            Offset = offSet,
                                            PageSize = pageSize
                                        }).Select(row =>
                                                        new Place((int)row.PukePoints) {
                                                            Id = (Guid)row.Id,
                                                            Name = (string)row.Name,
                                                            Vicinity = (string)row.Vicinity,
                                                            Location = new Location((double)row.Latitude, (double)row.Longitude)
                                                        }).AsEnumerable<Place>();
            } finally {
                connection.Close();
            }
            
            //var placesInsideBoundBox = connection.Query<Place, Location>(query.ToString(), new { MinLatitude = box.MinPoint.Latitude, MinLongitude = box.MinPoint.Longitude, 
            //                                                            MaxLatitude = box.MaxPoint.Latitude, MaxLongitude = box.MaxPoint.Longitude,
            //                                                            Offset = offSet, PageSize = pageSize });
            return placesFound;
        }

        public int GetAvegarePukesPoints() {
            var connection = new SqlConnection(this.connectionString);
            int averagePukePoints = 0;
            try {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("select avg(qty) from ");
                queryBuilder.Append("( select Place_id, Sum(Severity) as qty from Pukes ");
                queryBuilder.Append("group by Place_id ) as result");

                averagePukePoints = connection.Query<int>(queryBuilder.ToString()).FirstOrDefault();
            } finally {
                connection.Close();
            }
            return averagePukePoints;
        }
    } //class
}
