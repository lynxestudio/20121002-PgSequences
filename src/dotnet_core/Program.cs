using System;
using System.Data;
using Npgsql;
using NpgsqlTypes;

Console.WriteLine("Connecting to the database...");
var connStr = "Server=127.0.0.1;Port=5432;Database=Test;User ID=postgres;Password=Pa$$W0rd";
var commandText = "usp_createpublisher";
string[] myvalues = {"O'Reilly","Addison-Wesley","Prentice Hall"} ;
try
{
	using (NpgsqlConnection conn = new NpgsqlConnection(connStr)) 
       {
            conn.Open();
            foreach (string s in myvalues) 
            {
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText,conn)) 
            {
               //cmd.CommandType = CommandType.StoredProcedure;
               NpgsqlParameter[] parameters = 
               {
                  new NpgsqlParameter ("p_id", NpgsqlDbType.Integer, 4, "publisherid "),
                  new NpgsqlParameter ("p_name", NpgsqlDbType.Varchar, 512, "publisher"),
                  new NpgsqlParameter ("createddt", NpgsqlDbType.Timestamp, 8, "created")
               };
               //setting the output parameter
               parameters [0].Direction = ParameterDirection.Output;
               //setting the values

               parameters [1].Value = s;
               parameters [2].Value = DateTime.Now;
               //add parameters array to command
               cmd.Parameters.AddRange (parameters);
               cmd.ExecuteNonQuery ();
               Console.WriteLine ("Inserting... {0} - Id:[{1}] ",s, cmd.Parameters [0].Value);
            }
            } //end of foreach
	}
}
catch(Exception ex)
{
	Console.WriteLine($"Error {ex.Message}");
}
