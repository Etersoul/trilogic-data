namespace Trilogic
{
    using System;

    public partial class FolderSelector : Gtk.Window
    {
        private MainWindow mainWindow;
        public FolderSelector(MainWindow main) : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Modal = true;
            this.Build();
            this.mainWindow = main;
        }

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

        protected void ButtonCancelClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

        protected void ButtonOKClicked(object sender, EventArgs e)
        {
            this.mainWindow.FillFolder(this.entryFolder.Text);
            this.Destroy();
        }

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

