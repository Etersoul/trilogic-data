// <copyright file="DataAccessBase.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;
    using Trilogic.Utility;

    /// <summary>
    /// Data access base.
    /// </summary>
    public class DataAccessBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.DataAccessBase"/> class.
        /// </summary>
        public DataAccessBase()
        {
            this.Logger = GtkLogService.Instance;
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected GtkLogService Logger { get; set; }
    }
}