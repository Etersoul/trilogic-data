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
        /// The list file.
        /// </summary>
        private List<string> listFile = new List<string>();

        /// <summary>
        /// The list database schema.
        /// </summary>
        private List<string> listDB = new List<string>();

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

            column.AddAttribute(renderer, "background", 1);
            columnFile.AddAttribute(renderer2, "background", 1);

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
            string dir = this.entryFolder.Text;
            string tableDir = dir + "/table";

            this.SaveConfiguration();

            SqlServerAccess sql = new SqlServerAccess(entryHost.Text, entryUser.Text, entryPassword.Text, entryDatabase.Text);
            this.listDB = sql.GetTables();

            // Prepare the file list
            this.listFile = new List<string>();
            foreach (string file in Directory.GetFiles(tableDir))
            {
                string tableName = file.Replace("\\", "/").Substring(tableDir.Length + 1);
                tableName = tableName.Replace(".Table.sql", "");
                this.listFile.Add(tableName);
            }

            if (!Directory.Exists(dir))
            {
                GtkLogService.Instance.Write(string.Concat("Failed to get directory ", dir));
                return;
            }

            if (!Directory.Exists(tableDir))
            {
                GtkLogService.Instance.Write(string.Concat("Failed to get table directory ", tableDir));
                return;
            }

            this.ShowProcessedList();
        }

        /// <summary>
        /// Raises the combo show changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnComboShowChanged(object sender, EventArgs e)
        {
            this.ShowProcessedList();
        }

        /// <summary>
        /// Shows the processed list.
        /// </summary>
        protected void ShowProcessedList()
        {
            // Prepare the list store
            ListStore list = new ListStore(typeof(string), typeof(string));
            this.treeviewDB.Model = list;

            ListStore list2 = new ListStore(typeof(string), typeof(string));
            this.treeviewFile.Model = list2;

            List<string> listViewFile = new List<string>();
            List<string> listViewDB = new List<string>();
            if (this.comboShow.Active == 0)
            {
                listViewFile = this.listFile;
                listViewDB = this.listDB;
            }
            else if (this.comboShow.Active == 1)
            {
                foreach (string file in this.listFile)
                {
                    if (!this.listDB.Contains(file))
                    {
                        listViewFile.Add(file);
                    }
                }

                foreach (string str in this.listDB)
                {
                    if (!this.listFile.Contains(str))
                    {
                        listViewDB.Add(str);
                    }
                }
            }

            // Render to the tree view
            foreach (string file in listViewFile)
            {
                // Compare current file to the SQL Schema
                string color = "#ffffff";
                if (!listViewDB.Contains(file))
                {
                    color = "#99ff99";
                }

                list2.AppendValues(file, color);
            }

            foreach (string str in listViewDB)
            {
                // Compare the current SQL Schema to the file
                string color = "#ffffff";
                if (!listViewFile.Contains(str))
                {
                    color = "#99ff99";
                }

                list.AppendValues(str, color);
            }

        }
    }
}