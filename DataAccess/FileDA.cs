using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PersianNov.DataAccess
{
    public sealed class FileDA : DALBase<File>
    {
        public FileDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
        public override int Insert(File obj)
        {
            var commandBiulder = new FileCommandBuilder();
            var command = commandBiulder.Insert(obj);
            return DBManager.ExecuteNonQuery(base.ConnectionHandler, command);
        }

        public override int Update(File obj)
        {
            var commandBiulder = new FileCommandBuilder();
            var command = commandBiulder.Update(obj);
            return DBManager.ExecuteNonQuery(base.ConnectionHandler, command);
        }


    }
    internal class FileCommandBuilder
    {
        public IDbCommand Insert(File obj)
        {
            using (var command = new SqlCommand())
            {
                var query = "INSERT INTO [FileManager].[File](";
                var values = string.Empty;
                query += "Id, ";
                values += "@Id, ";
                query += "FileName, ";
                values += "@FileName, ";
                query += "ContentType, ";
                values += "@ContentType, ";
                query += "Extension, ";
                values += "@Extension, ";
                query += "Content, ";
                values += "@Content, ";

                query = query.Substring(0, query.Length - 2);
                values = values.Substring(0, values.Length - 2);
                query = string.Format("{0}) VALUES ({1})", query, values);

                command.CommandText = query;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", obj.Id));
                command.Parameters.Add(string.IsNullOrEmpty(obj.FileName)
                        ? new System.Data.SqlClient.SqlParameter("@FileName", DBNull.Value)
                        : new System.Data.SqlClient.SqlParameter("@FileName", obj.FileName));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContentType", obj.ContentType));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Extension", obj.Extension));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Content", obj.Content));
                
                return command;
            }
        }

        public IDbCommand Update(File obj)
        {
            using (var command = new System.Data.SqlClient.SqlCommand())
            {
                var query = "UPDATE [FileManager].[File] SET ";
                query += "FileName = @FileName, ";
                query += "ContentType = @ContentType, ";
                query += "Extension =@Extension, ";
                query += "Content = @Content, ";
        
                query += " WHERE Id = @Id";

                command.CommandText = query;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", obj.Id));
                command.Parameters.Add(string.IsNullOrEmpty(obj.FileName)
                        ? new System.Data.SqlClient.SqlParameter("@FileName", DBNull.Value)
                        : new System.Data.SqlClient.SqlParameter("@FileName", obj.FileName));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContentType", obj.ContentType));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Extension", obj.Extension));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Content", obj.Content));
               
                return command;
            }
        }


    }
    
}
