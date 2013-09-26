// <copyright file="FolderSelector.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;

    /// <summary>
    /// Folder selector.
    /// </summary>
    public partial class FolderSelector : Gtk.Window
    {
        /// <summary>
        /// The main window.
        /// </summary>
        private MainWindow mainWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.FolderSelector"/> class.
        /// </summary>
        /// <param name="main">The main window.</param>
        public FolderSelector(MainWindow main) : base(Gtk.WindowType.Toplevel)
        {
            this.Modal = true;
            this.Build();
            this.mainWindow = main;
        }

        /// <summary>
        /// Files the chooser selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void FileChooserSelectionChanged(object sender, EventArgs e)
        {
            if (this.fileChooser.Uri != null)
            {
                string uri = this.fileChooser.Uri;
                if ("file:///" == uri.Substring(0, 8))
                {
                    uri = uri.Substring(8);
                }

                this.entryFolder.Text = uri;
            }
        }

        /// <summary>
        /// Buttons the cancel clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void ButtonCancelClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

        /// <summary>
        /// Buttons the OK clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void ButtonOKClicked(object sender, EventArgs e)
        {
            this.mainWindow.FillFolder(this.entryFolder.Text);
            this.Destroy();
        }

        /// <summary>
        /// Windows the resize.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void WindowResize(object sender, EventArgs e)
        {
            if (this.WidthRequest < 600)
            {
                this.WidthRequest = 600;
            }

            if (this.HeightRequest < 400)
            {
                this.HeightRequest = 400;
            }
        }
    }
}