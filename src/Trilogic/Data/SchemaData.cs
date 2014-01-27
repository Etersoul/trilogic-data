// <copyright file="SchemaData.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;

    /// <summary>
    /// Schema data type enumeration.
    /// </summary>
    public enum SchemaDataType
    {
        /// <summary>
        /// The table.
        /// </summary>
        Table,

        /// <summary>
        /// The stored procedure.
        /// </summary>
        StoredProcedure
    }

    /// <summary>
    /// Schema data status enumeration.
    /// </summary>
    public enum SchemaDataStatus
    {
        /// <summary>
        /// The added.
        /// </summary>
        Added,

        /// <summary>
        /// The deleted.
        /// </summary>
        Deleted,

        /// <summary>
        /// The modified.
        /// </summary>
        Modified,

        /// <summary>
        /// The none.
        /// </summary>
        None
    }

    /// <summary>
    /// Schema data.
    /// </summary>
    public class SchemaData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.SchemaData"/> class.
        /// </summary>
        public SchemaData()
        {
            this.Status = SchemaDataStatus.None;
        }

        /// <summary>
        /// Gets or sets the path of the file.
        /// </summary>
        /// <value>The path of the file.</value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        /// <value>The object ID</value>
        public int ObjectID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public SchemaDataType Type { get; set; }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        /// <value>The definition.</value>
        public string Definition { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public SchemaDataStatus Status { get; set; }
    }
}
