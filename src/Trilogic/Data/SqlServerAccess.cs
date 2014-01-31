// <copyright file="SqlServerAccess.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    using Trilogic.Utility;

    /// <summary>
    /// SQL Server access.
    /// </summary>
    public class SqlServerAccess : DataAccessBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.Data.SqlServerAccess"/> class.
        /// </summary>
        public SqlServerAccess() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.Data.SqlServerAccess"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="database">The database.</param>
        public SqlServerAccess(string host, string username, string password, string database) : base()
        {
            this.Host = host;
            this.Username = username;
            this.Password = password;
            this.Database = database;
        }
        
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        public string Database { get; set; }

        /// <summary>
        /// Gets the tables.
        /// </summary>
        /// <returns>The tables.</returns>
        public SchemaCollection GetTables()
        {
            SchemaCollection list = new SchemaCollection();

            using (SqlConnection connection = this.CreateConnection())
            {
                this.Logger.Write("Opening connection for table list");

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                SELECT h.name + '.' + s.name AS name, s.object_id
                FROM sys.tables s
                INNER JOIN sys.schemas h
                    ON h.schema_id = s.schema_id
                WHERE s.type = 'U'
                ORDER BY s.name";

                command.CommandType = CommandType.Text;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SchemaData()
                            {
                                Name = (string)reader["name"],
                                ObjectID = (int)reader["object_id"],
                                Type = SchemaDataType.Table,
                                Data = this.GetTableSchema((int)reader["object_id"])
                            });
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Gets the stored procedure.
        /// </summary>
        /// <returns>The stored procedure collection.</returns>
        public SchemaCollection GetStoredProcedure()
        {
            SchemaCollection list = new SchemaCollection();

            using (SqlConnection connection = this.CreateConnection())
            {
                this.Logger.Write("Opening connection for stored procedure list");

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                SELECT h.name + '.' + s.name AS name, s.object_id
                FROM sys.procedures s
                INNER JOIN sys.schemas h
                ON h.schema_id = s.schema_id
                WHERE s.type = 'P'
                ORDER BY s.name";

                command.CommandType = CommandType.Text;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SchemaData()
                            {
                                Name = (string)reader["name"],
                                ObjectID = (int)reader["object_id"],
                                Type = SchemaDataType.StoredProcedure,
                                Data = this.GetStoredProcedureDefinition((int)reader["object_id"])
                            });
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Gets the table schema.
        /// </summary>
        /// <returns>The table schema definition.</returns>
        /// <param name="objectID">Table I.</param>
        public string GetTableSchema(int objectID)
        {
            List<string> list = new List<string>();
            List<string> constraintList = new List<string>();
            string tableName = string.Empty;
            string schemaName = string.Empty;
            string definition = "CREATE TABLE [{0}].[{1}] (\n\t{2}\n);\n\n{3}\n\n{4}";
            string columnDef = "[{0}] [{1}]{2} {3}";
            string constraintDef = "ALTER TABLE [{0}].[{1}] WITH CHECK ADD CONSTRAINT [{2}] CHECK ({3});";
            string indexDef = string.Empty;

            using (SqlConnection connection = this.CreateConnection())
            {
                // Table
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    SELECT s.name AS table_name, h.name AS schema_name, s.object_id
                    FROM sys.tables s
                    INNER JOIN sys.schemas h
                        ON h.schema_id = s.schema_id
                    WHERE s.type = 'U'
                        AND s.object_id = @objectid
                    ORDER BY s.name";
                    command.Parameters.Add(new SqlParameter("objectid", objectID));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tableName = reader["table_name"].ToString();
                            schemaName = reader["schema_name"].ToString();
                        }
                    }
                }

                // Columns
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    SELECT h.name AS column_name, t.name AS type_name, h.is_nullable, h.max_length, h.precision, h.scale,
                        t.max_length AS max_length_default, h.is_identity, t.name AS type_name
                    FROM sys.columns h
                    INNER JOIN sys.types t
                    ON t.user_type_id = h.user_type_id
                    WHERE h.object_id = @objectid
                    ORDER BY h.column_id";

                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("objectid", objectID));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string maxLength = reader["max_length"].ToString();
                            if (maxLength != reader["max_length_default"].ToString())
                            {
                                if (maxLength == "-1")
                                {
                                    maxLength = "(max)";
                                }
                                else if (reader["precision"].ToString() != "0" || reader["scale"].ToString() != "0")
                                {
                                    maxLength = "(" + reader["precision"].ToString() + "," + reader["scale"].ToString() + ")";
                                }
                                else
                                {
                                    // Don't know why, but nchar and nvarchar have length 2 times more than it should from sys.columns
                                    if (reader["type_name"].ToString() == "nchar" || reader["type_name"].ToString() == "nvarchar")
                                    {
                                        maxLength = "(" + (Convert.ToInt32(reader["max_length"]) / 2) + ")";
                                    }
                                    else
                                    {
                                        maxLength = "(" + maxLength + ")";
                                    }
                                }
                            }
                            else
                            {
                                maxLength = string.Empty;
                            }

                            list.Add(string.Format(
                                columnDef,
                                reader["column_name"].ToString(),
                                reader["type_name"].ToString(),
                                maxLength,
                                reader["is_nullable"].ToString() == "False" ? "NOT NULL" : "NULL"));
                        }
                    }
                }

                // Unique constraint & index part
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    SELECT c.name AS column_name, h.is_primary_key, h.name, h.index_id, t.is_descending_key, h.type_desc, h.is_unique
                    FROM sys.indexes h
                    INNER JOIN sys.index_columns t
                        ON t.object_id = h.object_id
                        AND t.index_id = h.index_id
                    INNER JOIN sys.columns c
                        ON c.object_id = h.object_id
                        AND c.column_id = t.column_id
                    WHERE h.object_id = @objectid
                    ORDER BY h.index_id, t.column_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("objectid", objectID));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        string lastName = string.Empty;
                        string lastIsPrimary = string.Empty;
                        string lastIsUnique = string.Empty;
                        string lastTypeDesc = string.Empty;
                        List<string> memberList = new List<string>();
                        List<string> nonUniqueList = new List<string>();
                        while (reader.Read())
                        {
                            if (lastName != reader["name"].ToString())
                            {
                                if (lastName != string.Empty)
                                {
                                    Console.WriteLine(reader["is_unique"].ToString());
                                    if (lastIsUnique == "True")
                                    {
                                        list.Add(string.Format(
                                            "CONSTRAINT [{0}] {1}{2}\n\t(\n\t\t{3}\n\t)",
                                            lastName,
                                            lastIsPrimary == "True" ? "PRIMARY KEY " : (lastIsUnique == "True" ? "UNIQUE " : string.Empty),
                                            lastTypeDesc,
                                            string.Join(",\n\t\t", memberList.ToArray())));
                                    }
                                    else
                                    {
                                        nonUniqueList.Add(string.Format(
                                            "CREATE {0} INDEX [{1}] ON [{2}].[{3}]\n(\n\t{4}\n);",
                                            lastTypeDesc,
                                            lastName,
                                            schemaName,
                                            tableName,
                                            string.Join(",\n\t", memberList.ToArray())));
                                    }
                                }

                                lastName = reader["name"].ToString();
                                lastIsPrimary = reader["is_primary_key"].ToString();
                                lastIsUnique = reader["is_unique"].ToString();
                                lastTypeDesc = reader["type_desc"].ToString();

                                memberList = new List<string>();
                            }

                            memberList.Add(string.Format(
                                "[{0}] {1}",
                                reader["column_name"].ToString(),
                                reader["is_descending_key"].ToString() == "False" ? "ASC" : "DESC"));
                        }

                        if (lastName != string.Empty)
                        {
                            if (lastIsUnique == "True")
                            {
                                list.Add(string.Format(
                                    "CONSTRAINT [{0}] {1}{2}\n\t(\n\t\t{3}\n\t)",
                                    lastName,
                                    lastIsPrimary == "True" ? "PRIMARY KEY " : (lastIsUnique == "True" ? "UNIQUE " : string.Empty),
                                    lastTypeDesc,
                                    string.Join(",\n\t\t", memberList.ToArray())));
                            }
                            else
                            {
                                nonUniqueList.Add(string.Format(
                                    "CREATE {0} INDEX [{1}] ON [{2}].[{3}]\n(\n\t{4}\n);",
                                    lastTypeDesc,
                                    lastName,
                                    schemaName,
                                    tableName,
                                    string.Join(",\n\t", memberList.ToArray())));
                            }
                        }

                        indexDef = string.Join("\n\n", nonUniqueList);
                    }
                }

                // Check constraint part
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    SELECT h.name, h.definition
                    FROM sys.check_constraints h
                    WHERE h.parent_object_id = @objectid";

                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("objectid", objectID));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            constraintList.Add(string.Format(
                                constraintDef,
                                schemaName,
                                tableName,
                                reader["name"].ToString(),
                                reader["definition"].ToString()));
                        }
                    }
                }

                // Foreign key constraint
                /*                
                command = connection.CreateCommand();
                command.CommandText = @"
                SELECT h.name AS column_name, t.name AS type_name
                FROM sys.foreign_keys h
                WHERE h.parent_object_id = @objectid
                ORDER BY h.column_id";

                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("objectid", objectID));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(string.Format(
                            columnDef,
                            reader["column_name"].ToString(),
                            reader["type_name"].ToString(),
                            reader["max_length"].ToString() != reader["max_length_default"].ToString() ? "(" + reader["max_length"] + ")" : string.Empty,
                            reader["is_nullable"].ToString() == "False" ? "NOT NULL" : "NULL"));
                    }
                }*/
            }

            return string.Format(definition, schemaName, tableName, string.Join(",\n\t", list), indexDef, string.Join("\n\n", constraintList));
        }

        /// <summary>
        /// Gets the stored procedure definition.
        /// </summary>
        /// <returns>The stored procedure definition.</returns>
        /// <param name="objectID">Object ID.</param>
        public string GetStoredProcedureDefinition(int objectID)
        {
            using (SqlConnection connection = this.CreateConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                SELECT h.object_id, h.definition
                FROM sys.sql_modules h
                WHERE h.object_id = @objectid";

                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("objectid", objectID));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["definition"].ToString().Replace("\r\n", "\n") + "\n";
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="commandString">Command string.</param>
        public void RunCommand(string commandString)
        {
            using (SqlConnection connection = this.CreateConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = commandString;
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Determines whether this instance table is exist with the specified table Name.
        /// </summary>
        /// <returns><c>true</c> if table is exist with the specified table name; otherwise, <c>false</c>.</returns>
        /// <param name="tableName">Table name.</param>
        public bool IsTableExist(string tableName)
        {
            string[] str = tableName.Split('.');
            string table = str[str.Length - 1];
            using (SqlConnection connection = this.CreateConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT count(*) AS count
                    FROM sys.tables t
                    INNER JOIN sys.schemas s
                        ON s.schema_id = t.schema_id
                        AND s.name = @schema
                    WHERE t.name = @table
                ";
                command.Parameters.Add(new SqlParameter("table", table));
                command.Parameters.Add(new SqlParameter("schema", str[0]));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read() || reader["count"].ToString() == "0")
                    {
                        return false;
                    }

                    return true;
                }
            }
        }

        /// <summary>
        /// Determines whether this procedure is exist with the specified procedure name.
        /// </summary>
        /// <returns><c>true</c> if this instance procedure is exist with the specified procedure name; otherwise, <c>false</c>.</returns>
        /// <param name="procedureName">Procedure name.</param>
        public bool IsProcedureExist(string procedureName)
        {
            string[] str = procedureName.Split('.');
            string proc = str[str.Length - 1];
            using (SqlConnection connection = this.CreateConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT count(*) AS count
                    FROM sys.procedures t
                    INNER JOIN sys.schemas s
                        ON s.schema_id = t.schema_id
                        AND s.name = @schema
                    WHERE t.name = @proc
                ";
                command.Parameters.Add(new SqlParameter("proc", proc));
                command.Parameters.Add(new SqlParameter("schema", str[0]));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read() || reader["count"].ToString() == "0")
                    {
                        return false;
                    }

                    return true;
                }
            }
        }

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns>The connection.</returns>
        protected SqlConnection CreateConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = this.Host;
            builder.UserID = this.Username;
            builder.Password = this.Password;
            builder.InitialCatalog = this.Database;
            builder.ConnectTimeout = 5;

            SqlConnection connection = new SqlConnection(builder.ToString());
            connection.Open();

            return connection;
        }
    }
}