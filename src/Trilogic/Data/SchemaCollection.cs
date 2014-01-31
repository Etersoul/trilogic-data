// <copyright file="SchemaCollection.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Schema collection.
    /// </summary>
    public class SchemaCollection : IEnumerable
    {
        /// <summary>
        /// The data list.
        /// </summary>
        private Dictionary<string, SchemaData> dataList;

        /// <summary>
        /// The string list.
        /// </summary>
        private List<string> stringList;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.SchemaCollection"/> class.
        /// </summary>
        public SchemaCollection()
        {
            this.dataList = new Dictionary<string, SchemaData>();
            this.stringList = new List<string>();
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The number of data.</value>
        public int Count
        {
            get
            {
                return this.dataList.Count;
            }
        }

        /// <summary>
        /// Gets the <see cref="Trilogic.SchemaCollection"/> with the specified objectID.
        /// </summary>
        /// <param name="objectID">Object ID.</param>
        /// <returns>The schema data.</returns>
        public SchemaData this[int objectID]
        {
            get
            {
                foreach (KeyValuePair<string, SchemaData> schema in this.dataList)
                {
                    if (schema.Value.ObjectID != objectID)
                    {
                        return schema.Value;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the <see cref="Trilogic.SchemaCollection"/> with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The schema data.</returns>
        public SchemaData this[string name]
        {
            get
            {
                if (this.dataList.ContainsKey(name))
                {
                    return this.dataList[name];
                }

                return null;
            }
        }

        /// <summary>
        /// Add the specified schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        public void Add(SchemaData schema)
        {
            this.dataList.Add(schema.Name, schema);
            this.stringList.Add(schema.Name);
        }

        /// <summary>
        /// Appends the collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void AppendCollection(SchemaCollection collection)
        {
            foreach (SchemaData single in collection)
            {
                this.Add(single);
            }
        }

        /// <summary>
        /// Whether the collection contains the name.
        /// </summary>
        /// <returns><c>true</c>, if name was contained, <c>false</c> otherwise.</returns>
        /// <param name="name">The name.</param>
        public bool ContainsName(string name)
        {
            return this.stringList.Contains(name);
        }

        /// <summary>
        /// Whether the collection contains the name.
        /// </summary>
        /// <returns><c>true</c>, if name was contained, <c>false</c> otherwise.</returns>
        /// <param name="schema">The schema.</param>
        public bool ContainsName(SchemaData schema)
        {
            return this.ContainsName(schema.Name);
        }

        /// <summary>
        /// Determines whether this instance is modified.
        /// </summary>
        /// <returns><c>true</c> if this instance is modified; otherwise, <c>false</c>.</returns>
        /// <param name="schemaName">Schema name.</param>
        /// <param name="base64Hash">Base64 hash.</param>
        public bool IsModified(string schemaName, string base64Hash)
        {
            if (this.dataList[schemaName].Hash != base64Hash)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether this instance is modified.
        /// </summary>
        /// <returns><c>true</c> if this instance is modified; otherwise, <c>false</c>.</returns>
        /// <param name="schema">The schema.</param>
        public bool IsModified(SchemaData schema)
        {
            return this.IsModified(schema.Name, schema.Hash);
        }

        #region IEnumerable implementation
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            return this.dataList.Values.GetEnumerator();
        }
        #endregion
    }
}
