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
        private List<SchemaData> dataList;

        /// <summary>
        /// The string list.
        /// </summary>
        private List<string> stringList;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.SchemaCollection"/> class.
        /// </summary>
        public SchemaCollection()
        {
            this.dataList = new List<SchemaData>();
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
                foreach (SchemaData schema in this.dataList)
                {
                    if (schema.ObjectID != objectID)
                    {
                        return schema;
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
                foreach (SchemaData schema in this.dataList)
                {
                    if (schema.Name == name)
                    {
                        return schema;
                    }
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
            this.dataList.Add(schema);
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
                this.dataList.Add(single);
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
            return this.stringList.Contains(schema.Name);
        }

        #region IEnumerable implementation
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            return new SchemaEnumerator(this.dataList.ToArray());
        }
        #endregion
    }

    /// <summary>
    /// Schema enumerator.
    /// </summary>
    public class SchemaEnumerator : IEnumerator
    {
        /// <summary>
        /// The schema.
        /// </summary>
        private SchemaData[] schema;

        /// <summary>
        /// The position.
        /// </summary>
        private int position = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.SchemaEnumerator"/> class.
        /// </summary>
        /// <param name="list">The List.</param>
        public SchemaEnumerator(SchemaData[] list)
        {
            this.schema = list;
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>The current element in the collection.</returns>
        /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
        /// <filterpriority>2</filterpriority>
        /// <value>The current.</value>
        public SchemaData Current
        {
            get
            {
                try
                {
                    return this.schema[this.position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the
        /// end of the collection.</returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        /// <filterpriority>2</filterpriority>
        public bool MoveNext()
        {
            this.position++;
            return this.position < this.schema.Length;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        /// <filterpriority>2</filterpriority>
        public void Reset()
        {
            this.position = -1;
        }
    }
}
