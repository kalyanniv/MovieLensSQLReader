using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLReader
{
    class ReaderDemo
    {
        static void Main()
        {
            ReaderDemo rd = new ReaderDemo();
            rd.SimpleRead();
        }

        public void SimpleRead()
        {

            //Variables
            var sqlConnectionString = "Data Source=tcp:asos-an-ods-generic-live-eun.database.windows.net,1433;Initial Catalog=AzureUsageOds;Integrated Security=False;User ID=IanMargetts@asos-an-ods-generic-live-eun;Password=wrA3rASWudrU;Connect Timeout=60;Encrypt=True";
            var sqlCommandString = "SELECT * FROM [dwh].[DimDepartment]";


            // declare the SqlDataReader, which is used in
            // both the try block and the finally block
            SqlDataReader rdr = null;

            // create a connection object
            SqlConnection conn = new SqlConnection(sqlConnectionString);

            // create a command object
            SqlCommand cmd = new SqlCommand(sqlCommandString, conn);

            try
            {
                // open the connection
                conn.Open();

                // 1. get an instance of the SqlDataReader
                rdr = cmd.ExecuteReader();

                // print a set of column headers
                Console.WriteLine("DepartmentKey            DepartmentName");
                Console.WriteLine("------------             --------------");

                // 2. print necessary columns of each record
                while (rdr.Read())
                {
                    // get the results of each column
                    var departmentKey = (int)rdr["DepartmentKey"];
                    var departmentName = (string)rdr["DepartmentName"];


                    // print out the results
                    Console.Write("{0,-25}", departmentKey);
                    Console.Write("{0,-25}", departmentName);
                    Console.WriteLine();
                }
            }
            finally
            {
                // 3. close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}