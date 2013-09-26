// <copyright file="GtkLogService.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic.Utility
{
    using System;
    using Gtk;

    /// <summary>
    /// Gtk log service.
    /// </summary>
    public sealed class GtkLogService : ILogService
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static GtkLogService instance = null;

        /// <summary>
        /// Prevents a default instance of the <see cref="Trilogic.Utility.GtkLogService"/> class from being created.
        /// </summary>
        private GtkLogService()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static GtkLogService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GtkLogService();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        /// <value>The buffer.</value>
        public TextBuffer Buffer { get; set; }

        /// <summary>
        /// Write the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Write(string message)
        {
            this.Buffer.Text = message + "\n" + this.Buffer.Text;
        }
    }
}