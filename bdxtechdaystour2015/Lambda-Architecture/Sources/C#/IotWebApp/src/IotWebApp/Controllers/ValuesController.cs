using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using IotWebApp.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IotWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static readonly string connectionString = "Server=tcp:<your sql server>.database.windows.net,1433;Database=techdaystour;User ID=<user>;Password=<sql password>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        [HttpGet]
        public IEnumerable<Aggregation> Get([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            List<Aggregation> result = new List<Aggregation>();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandText = "SELECT [Date], [Device], [Value] FROM [dbo].[Aggregation] WHERE [Date] BETWEEN @fromDate AND @endDate";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;
            SqlParameter fromParamter = new SqlParameter("@fromDate", SqlDbType.SmallDateTime);
            fromParamter.Value = from;
            SqlParameter endParamter = new SqlParameter("@endDate", SqlDbType.SmallDateTime);
            endParamter.Value = to;
            cmd.Parameters.Add(fromParamter);
            cmd.Parameters.Add(endParamter);
            sqlConnection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Aggregation aggregation = new Aggregation
                {
                    Device = reader.GetString(1),
                    Date = reader.GetDateTime(0),
                    Value = reader.GetDecimal(2)
                };

                result.Add(aggregation);
            }
            sqlConnection.Close();

            return result;
        }
    }
}
