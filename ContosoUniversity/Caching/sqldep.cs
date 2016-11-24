using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ContosoUniversity.Controllers;

namespace ContosoUniversity.Caching
{
    public class sqldep
    {   

        public void Initialization(string query)
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["SchoolContext"].ConnectionString;
            // Create a dependency connection.
            SqlDependency.Start(connstring);          
            SqlConnection conn = new SqlConnection(connstring);           
            conn.Open();
            using (SqlCommand command = new SqlCommand(query,
                conn))
            {
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange += new OnChangeEventHandler((sender,  e) => OnDependencyChange(sender, e, query));                  
                SqlDataReader reader = command.ExecuteReader();
            }
        }

        // Handler method
        public void OnDependencyChange(object sender,
           SqlNotificationEventArgs e, string query)
        {
            //"TEST_SAMO_SELECT \r\n    [Extent1].[ID] AS [ID], \r\n    [Extent1].[LastName] AS [LastName], \r\n    [Extent1].[FirstMidName] AS [FirstMidName], \r\n    [Extent1].[EnrollmentDate] AS [EnrollmentDate]\r\n    FROM [dbo].[Student] AS [Extent1]_"
            Caching.Configuration.Cache.InvalidateItem("TEST_SAMO_"+query+"_");
            // Handle the event (for example, invalidate this cache entry).
        }


    }
}