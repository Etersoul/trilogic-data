// <copyright file="MainWindow.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    using Gtk;
    using Trilogic.Data;
    using Trilogic.Utility;

    /// <summary>
    /// Main window.
    /// </summary>
    public partial class MainWindow : Gtk.Window
    {
        /// <summary>
        /// The buffer.
        /// </summary>
        private TextBuffer buffer;

        /// <summary>
        /// The app settings.
        /// </summary>
        private AppSettings appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.MainWindow"/> class.
        /// </summary>
        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.appSettings = new AppSettings();
            this.appSettings.ReadFromFile();

            this.entryHost.Text = this.appSettings.DBHost;
            this.entryUser.Text = this.appSettings.DBUser;
            this.entryPassword.Text = this.appSettings.DBPassword;
            this.entryDatabase.Text = this.appSettings.DBName;
            this.entryFolder.Text = this.appSettings.DirectoryPath;

            // force the position to this specific position
            this.vpaned1.Position = 300;
            this.hpaned1.Position = 300;

            // set buffer for textviewLog
            this.buffer = textviewLog.Buffer;
            GtkLogService.Instance.Buffer = this.buffer;

            TreeViewColumn column = new TreeViewColumn();
            TreeViewColumn columnFile = new TreeViewColumn();

            column.Title = "Table Name";
            columnFile.Title = "File Name";
            this.treeviewDB.AppendColumn(column);
            this.treeviewFile.AppendColumn(columnFile);

            CellRendererText renderer = new CellRendererText();
            CellRendererText renderer2 = new CellRendererText();

            column.PackStart(renderer, true);
            columnFile.PackStart(renderer2, true);

            column.AddAttribute(renderer, "text", 0);
            columnFile.AddAttribute(renderer2, "text", 0);

            GtkLogService.Instance.Write("Insert the username and start the diff");
        }

        /// <summary>
        /// Fills the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        public void FillFolder(string folder)
        {
            this.entryFolder.Text = folder;
        }

        /// <summary>
        /// Quit this instance.
        /// </summary>
        public void Quit()
        {
            this.SaveConfiguration();
            Application.Quit();
        }

        /// <summary>
        /// Raises the delete event event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="a">The delete event arguments.</param>
        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            this.Quit();
        }

        /// <summary>
        /// Raises the resize checked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnResizeChecked(object sender, EventArgs e)
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

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        protected void SaveConfiguration()
        {
            this.appSettings.DBHost = this.entryHost.Text;
            this.appSettings.DBUser = this.entryUser.Text;
            this.appSettings.DBPassword = this.entryPassword.Text;
            this.appSettings.DBName = this.entryDatabase.Text;
            this.appSettings.DirectoryPath = this.entryFolder.Text;

            this.appSettings.WriteToFile();
        }

        /// <summary>
        /// Raises the quit action activated event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnQuitActionActivated(object sender, EventArgs e)
        {
            this.Quit();
        }

        /// <summary>
        /// Raises the button open folder clicked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnButtonOpenFolderClicked(object sender, EventArgs e)
        {   
            new FolderSelector(this).ActivateFocus();
        }

        /// <summary>
        /// Raises the options action activated event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnOptionsActionActivated(object sender, EventArgs e)
        {
            new OptionsWindow().Show();
        }

        /// <summary>
        /// Raises the button start diff clicked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnButtonStartDiffClicked(object sender, EventArgs e)
        {
            this.SaveConfiguration();

            // prepare the list store
            ListStore list = new ListStore(typeof(string));
            treeviewDB.Model = list;

            ListStore list2 = new ListStore(typeof(string));
            treeviewFile.Model = list2;

            SqlServerAccess sql = new SqlServerAccess(entryHost.Text, entryUser.Text, entryPassword.Text, entryDatabase.Text);
            List<string> listTable = sql.GetTables();

            foreach (string str in listTable)
            {
                list.AppendValues(str);
            }

            string dir = this.entryFolder.Text;
            if (!Directory.Exists(dir))
            {
                GtkLogService.Instance.Write(string.Concat("Failed to get directory ", dir));
                return;
            }

            string tableDir = dir + "/table";
            if (!Directory.Exists(tableDir))
            {
                GtkLogService.Instance.Write(string.Concat("Failed to get table directory ", tableDir));
                return;
            }

            foreach (string file in Directory.GetFiles(tableDir))
            {
                list2.AppendValues(file.Replace("\\", "/").Substring(tableDir.Length + 1));
            }
        }
    }
}