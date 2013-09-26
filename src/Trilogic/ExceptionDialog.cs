// <copyright file="ExceptionDialog.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;
    using Gtk;

    /// <summary>
    /// Exception dialog.
    /// </summary>
    public partial class ExceptionDialog : Gtk.Dialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.ExceptionDialog"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="stackTrace">Stack trace.</param>
        public ExceptionDialog(string message, string stackTrace)
        {
            this.Build();
            this.labelMessage.Markup = "<b>" + message + "</b>";
            this.labelStackTrace.Text = stackTrace;
            this.KeepAbove = true;
        }

        /// <summary>
        /// Raises the button quit clicked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event argument.</param>
        protected void OnButtonQuitClicked(object sender, EventArgs e)
        {
            Application.Quit();
        }

        /// <summary>
        /// Raises the button continue clicked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event argument.</param>
        protected void OnButtonContinueClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }
    }
}