// <copyright file="OptionsWindow.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;

    /// <summary>
    /// Options window.
    /// </summary>
    public partial class OptionsWindow : Gtk.Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.OptionsWindow"/> class.
        /// </summary>
        public OptionsWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.KeepAbove = true;
        }

        /// <summary>
        /// Raises the button apply clicked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnButtonApplyClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}