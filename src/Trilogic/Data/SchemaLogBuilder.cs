// <copyright file="SchemaLogBuilder.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Schema log builder.
    /// </summary>
    public class SchemaLogBuilder
    {
        /// <summary>
        /// The name of the base file.
        /// </summary>
        private readonly string baseFileName = "__SYNCLOG";

        /// <summary>
        /// The path.
        /// </summary>
        private string path;

        /// <summary>
        /// The list.
        /// </summary>
        private List<string> list = new List<string>();

        /// <summary>
        /// The name of the database.
        /// </summary>
        private string databaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.SchemaLogBuilder"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="databaseName">Database name.</param>
        public SchemaLogBuilder(string path, string databaseName)
        {
            this.path = path;
            this.databaseName = databaseName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.SchemaLogBuilder"/> class.
        /// </summary>
        /// <param name="databaseName">Database name.</param>
        public SchemaLogBuilder(string databaseName)
        {
            this.databaseName = databaseName;
        }

        /// <summary>
        /// Append the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void Append(SchemaCollection collection)
        {
            foreach (SchemaData data in collection)
            {
                this.list.Add(data.Type.ToString() + ";" + data.Name + ";" + data.ModifyDate.ToString("s"));
            }
        }

        /// <summary>
        /// Reads the log.
        /// </summary>
        /// <returns>The log.</returns>
        public SchemaCollection ReadLog()
        {
            return this.ReadLog(this.path);
        }

        /// <summary>
        /// Reads the log.
        /// </summary>
        /// <returns>The log.</returns>
        /// <param name="path">The path.</param>
        public SchemaCollection ReadLog(string path)
        {
            SchemaCollection collection = new SchemaCollection();
            foreach (string line in System.IO.File.ReadAllLines(path + "/" + this.baseFileName + "_" + this.databaseName.ToString()))
            {
                string[] split = line.Split(';');
                SchemaData schema = new SchemaData();
                schema.Name = split[1];
                schema.Type = (SchemaDataType)Enum.Parse(typeof(SchemaDataType), split[0]);
                schema.ModifyDate = DateTime.Parse(split[2]);
                collection.Add(schema);
            }

            return collection;
        }

        /// <summary>
        /// Commit this instance.
        /// </summary>
        public void Commit()
        {
            this.Commit(this.path);
        }

        /// <summary>
        /// Commit the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Commit(string path)
        {
            System.IO.File.WriteAllText(path + "/" + this.baseFileName + "_" + this.databaseName.ToUpper(), string.Join("\n", this.list.ToArray()));
        }
    }
}
