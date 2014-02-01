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
        /// The table list from file.
        /// </summary>
        private SchemaCollection listFileTables = new SchemaCollection();

        /// <summary>
        /// The procedure list from file.
        /// </summary>
        private SchemaCollection listFileProcedures = new SchemaCollection();

        /// <summary>
        /// The list of database schema.
        /// </summary>
        private SchemaCollection listDBTables = new SchemaCollection();

        /// <summary>
        /// The list of database procedures.
        /// </summary>
        private SchemaCollection listDBProcedures = new SchemaCollection();

        /// <summary>
        /// The combined file.
        /// </summary>
        private SchemaCollection combinedFile = new SchemaCollection();

        /// <summary>
        /// The combined DB.
        /// </summary>
        private SchemaCollection combinedDB = new SchemaCollection();

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
            this.entryDatabase.Text = this.appSettings.DBName;
            this.entryFolder.Text = this.appSettings.DirectoryPath;

            // force the position to this specific position
            this.vpaned1.Position = 300;
            this.hpaned1.Position = 300;

            // set buffer for textviewLog
            this.buffer = textviewLog.Buffer;
            GtkLogService.Instance.Buffer = this.buffer;

            TreeViewColumn columnDB = new TreeViewColumn();
            TreeViewColumn columnDBToggle = new TreeViewColumn();
            TreeViewColumn columnDBStatus = new TreeViewColumn();
            TreeViewColumn columnFile = new TreeViewColumn();
            TreeViewColumn columnFileToggle = new TreeViewColumn();
            TreeViewColumn columnFileStatus = new TreeViewColumn();

            columnDB.Title = "Database Side";
            columnFile.Title = "File Side";
            columnDBToggle.Title = string.Empty;
            columnFileToggle.Title = string.Empty;
            columnDBStatus.Title = "S";
            columnFileStatus.Title = "S";

            columnDBToggle.FixedWidth = 20;
            columnFileToggle.FixedWidth = 20;
            columnDBStatus.FixedWidth = 20;
            columnFileStatus.FixedWidth = 20;

            this.treeviewDB.AppendColumn(columnDBToggle);
            this.treeviewDB.AppendColumn(columnDBStatus);
            this.treeviewDB.AppendColumn(columnDB);
            this.treeviewFile.AppendColumn(columnFileToggle);
            this.treeviewFile.AppendColumn(columnFileStatus);
            this.treeviewFile.AppendColumn(columnFile);

            CellRendererText renderer = new CellRendererText();
            CellRendererText renderer2 = new CellRendererText();
            CellRendererToggle toggleDB = new CellRendererToggle() { Activatable = true };
            CellRendererToggle toggleFile = new CellRendererToggle() { Activatable = true };
            CellRendererText statusDB = new CellRendererText();
            CellRendererText statusFile = new CellRendererText();
            CellRendererText typeDB = new CellRendererText();
            CellRendererText typeFile = new CellRendererText();

            columnDB.PackStart(renderer, false);
            columnDBStatus.PackStart(statusDB, false);
            columnDBToggle.PackStart(toggleDB, true);
            columnFile.PackStart(renderer2, false);
            columnFileStatus.PackStart(statusFile, false);
            columnFileToggle.PackStart(toggleFile, true);

            columnDB.AddAttribute(renderer, "text", 0);
            columnFile.AddAttribute(renderer2, "text", 0);

            columnDB.AddAttribute(renderer, "background", 1);
            columnFile.AddAttribute(renderer2, "background", 1);
            
            columnDBToggle.AddAttribute(toggleDB, "active", 2);
            columnFileToggle.AddAttribute(toggleFile, "active", 2);

            columnDBStatus.AddAttribute(statusDB, "text", 3);
            columnFileStatus.AddAttribute(statusFile, "text", 3);

            // Set toggle signal
            toggleDB.Toggled += (object o, ToggledArgs args) =>
            {
                TreeIter iter;
                this.treeviewDB.Model.GetIterFromString(out iter, args.Path);
                bool enable = (bool)this.treeviewDB.Model.GetValue(iter, 2);
                this.treeviewDB.Model.SetValue(iter, 2, !enable);
            };

            toggleFile.Toggled += (object o, ToggledArgs args) =>
            {
                TreeIter iter;
                this.treeviewFile.Model.GetIterFromString(out iter, args.Path);
                bool enable = (bool)this.treeviewFile.Model.GetValue(iter, 2);
                this.treeviewFile.Model.SetValue(iter, 2, !enable);
            };

            GtkLogService.Instance.Write("Insert the username and start the diff");
        }

        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        /// <value>The database data access handler.</value>
        public SqlServerAccess Sql { get; set; }

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
            string tableDir = dir + "/" + AppSettings.TableDir;
            string procedureDir = dir + "/" + AppSettings.ProcedureDir;

            this.SaveConfiguration();

            this.Sql = new SqlServerAccess(entryHost.Text, entryUser.Text, entryPassword.Text, entryDatabase.Text);
            this.listDBTables = this.Sql.GetTables();
            this.listDBProcedures = this.Sql.GetStoredProcedure();

            int id = 0;

            // Prepare the file list
            this.listFileTables = new SchemaCollection();
            foreach (string file in Directory.GetFiles(tableDir))
            {
                id++;
                string tableName = file.Replace("\\", "/").Substring(tableDir.Length + 1);
                tableName = tableName.Replace(".Table.sql", string.Empty);
                this.listFileTables.Add(new SchemaData { Name = tableName, ObjectID = id, Type = SchemaDataType.Table, FilePath = file, Data = File.ReadAllText(file).Replace("\r\n", "\n") });
            }

            this.listFileProcedures = new SchemaCollection();
            foreach (string file in Directory.GetFiles(procedureDir))
            {
                id++;
                string procedureName = file.Replace("\\", "/").Substring(procedureDir.Length + 1);
                procedureName = procedureName.Replace(".StoredProcedure.sql", string.Empty);
                this.listFileProcedures.Add(new SchemaData { Name = procedureName, ObjectID = id, Type = SchemaDataType.StoredProcedure, FilePath = file, Data = File.ReadAllText(file) });
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

            // Add status to the list
            foreach (SchemaData table in this.listFileTables)
            {
                if (!this.listDBTables.ContainsName(table))
                {
                    table.Status = SchemaDataStatus.Added;
                    continue;
                }

                if (this.listDBTables.IsModified(table))
                {
                    table.Status = SchemaDataStatus.Modified;
                }
            }

            foreach (SchemaData table in this.listDBTables)
            {
                if (!this.listFileTables.ContainsName(table))
                {
                    table.Status = SchemaDataStatus.Added;
                    continue;
                }

                if (this.listFileTables.IsModified(table))
                {
                    table.Status = SchemaDataStatus.Modified;
                }
            }

            // SP list
            foreach (SchemaData sp in this.listFileProcedures)
            {
                if (!this.listDBProcedures.ContainsName(sp))
                {
                    sp.Status = SchemaDataStatus.Added;
                    continue;
                }

                if (this.listDBProcedures.IsModified(sp))
                {
                    sp.Status = SchemaDataStatus.Modified;
                }
            }

            foreach (SchemaData sp in this.listDBProcedures)
            {
                if (!this.listFileProcedures.ContainsName(sp))
                {
                    sp.Status = SchemaDataStatus.Added;
                    continue;
                }

                if (this.listFileProcedures.IsModified(sp))
                {
                    sp.Status = SchemaDataStatus.Modified;
                }
            }

            // Reset the combined file and database variables
            this.combinedFile = new SchemaCollection();
            this.combinedDB = new SchemaCollection();

            this.combinedFile.AppendCollection(this.listFileTables);
            this.combinedFile.AppendCollection(this.listFileProcedures);
            this.combinedDB.AppendCollection(this.listDBTables);
            this.combinedDB.AppendCollection(this.listDBProcedures);

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
            TreeStore list = new TreeStore(typeof(string), typeof(string), typeof(bool), typeof(string), typeof(SchemaData));
            TreeIter iterTable1 = list.AppendValues("Tables");
            TreeIter iterProcedure1 = list.AppendValues("Stored Procedures");
            this.treeviewDB.Model = list;

            TreeStore list2 = new TreeStore(typeof(string), typeof(string), typeof(bool), typeof(string), typeof(SchemaData));
            TreeIter iterTable2 = list2.AppendValues("Tables");
            TreeIter iterProcedure2 = list2.AppendValues("Stored Procedures");
            this.treeviewFile.Model = list2;

            // Render to the tree view
            foreach (SchemaData file in this.combinedFile)
            {
                if (comboShow.Active == 1 && file.Status == SchemaDataStatus.None)
                {
                    continue;
                }

                if (comboType.Active == 1 && file.Type != SchemaDataType.Table)
                {
                    continue;
                }

                if (comboType.Active == 2 && file.Type != SchemaDataType.StoredProcedure)
                {
                    continue;
                }

                // Compare current file to the SQL Schema
                string color = "#ffffff";
                bool check = false;
                string status = string.Empty;
                if (file.Status == SchemaDataStatus.Added)
                {
                    color = "#99ff99";
                    check = true;
                    status = "+";
                }
                else if (file.Status == SchemaDataStatus.Modified)
                {
                    color = "#ffff99";
                    check = true;
                    status = "#";
                }

                TreeIter localIter = iterTable2;
                if (file.Type == SchemaDataType.StoredProcedure)
                {
                    localIter = iterProcedure2;
                }

                list2.AppendValues(localIter, file.Name, color, check, status, file);
            }

            foreach (SchemaData str in this.combinedDB)
            {
                if (comboShow.Active == 1 && str.Status == SchemaDataStatus.None)
                {
                    continue;
                }

                if (comboType.Active == 1 && str.Type != SchemaDataType.Table)
                {
                    continue;
                }

                if (comboType.Active == 2 && str.Type != SchemaDataType.StoredProcedure)
                {
                    continue;
                }

                // Compare the current SQL Schema to the file
                string color = "#ffffff";
                bool check = false;
                string status = string.Empty;
                if (str.Status == SchemaDataStatus.Added)
                {
                    color = "#99ff99";
                    check = true;
                    status = "+";
                }
                else if (str.Status == SchemaDataStatus.Modified)
                {
                    color = "#ffff99";
                    check = true;
                    status = "#";
                }

                TreeIter localIter = iterTable1;
                if (str.Type == SchemaDataType.StoredProcedure)
                {
                    localIter = iterProcedure1;
                }

                list.AppendValues(localIter, str.Name, color, check, status, str);
            }
        }

        /// <summary>
        /// Raises the combo type changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnComboTypeChanged(object sender, EventArgs e)
        {
            this.ShowProcessedList();
        }

        /// <summary>
        /// Raises the tree view row activated event.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="args">The arguments.</param>
        protected void OnTreeviewRowActivated(object o, RowActivatedArgs args)
        {
            TreeView tree = (TreeView)o;
            TreeIter iter;
            tree.Model.GetIter(out iter, args.Path);
            string name = tree.Model.GetValue(iter, 0).ToString();

            SchemaData file = null;
            SchemaData db = null;
            if (tree.Name == "treeviewDB")
            {
                db = (SchemaData)tree.Model.GetValue(iter, 4);
                file = this.combinedFile[db.Name];
            }
            else
            {
                file = (SchemaData)tree.Model.GetValue(iter, 4);
                db = this.combinedDB[file.Name];
            }

            new CompareWindow(this, name, file, db).Show();
        }

        /// <summary>
        /// Raises the button right clicked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnButtonRightClicked(object sender, EventArgs e)
        {
            TreeIter iter;
            TreeModel model = this.treeviewDB.Model;
            model.GetIterFirst(out iter);
            GtkLogService.Instance.Write("Start schema dumping process.");

            do
            {
                TreeIter iterChild;
                bool isChild = model.IterChildren(out iterChild, iter);

                // Ignore the tree that doesn't have child
                if(!isChild)
                {
                    continue;
                }

                do
                {
                    bool active = (bool)model.GetValue(iterChild, 2);
                    if (active == true)
                    {
                        SchemaData schema = (SchemaData)model.GetValue(iterChild, 4);
                        if(schema.Type == SchemaDataType.Table)
                        {
                            File.WriteAllText(this.entryFolder.Text + "/" + AppSettings.TableDir + "/" + schema.Name + "." + schema.Type.ToString() + ".sql", this.Sql.GetTableSchema(schema.ObjectID));
                        }
                        else if(schema.Type == SchemaDataType.StoredProcedure)
                        {
                            File.WriteAllText(this.entryFolder.Text + "/" + AppSettings.ProcedureDir + "/" + schema.Name + "." + schema.Type.ToString() + ".sql", this.Sql.GetStoredProcedureDefinition(schema.ObjectID));
                        }

                     }
                }
                while (model.IterNext(ref iterChild));
            }
            while (model.IterNext(ref iter));

            GtkLogService.Instance.Write("Schema dumping complete.");
        }

        /// <summary>
        /// Raises the button left clicked event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void OnButtonLeftClicked(object sender, EventArgs e)
        {
            TreeIter iter;
            TreeModel model = this.treeviewFile.Model;
            model.GetIterFirst(out iter);
            GtkLogService.Instance.Write("Start schema restoration process.");
            List<string> fk = new List<string>();

            do
            {
                TreeIter iterChild;
                bool isChild = model.IterChildren(out iterChild, iter);

                // Ignore the tree that doesn't have child
                if(!isChild)
                {
                    continue;
                }

                do
                {
                    bool active = (bool)model.GetValue(iterChild, 2);
                    if (active == true)
                    {
                        SchemaData schema = (SchemaData)model.GetValue(iterChild, 4);
                        string mainQuery = schema.Data;
                        if(schema.Type == SchemaDataType.Table)
                        {
                            if(this.Sql.IsTableExist(schema.Name))
                            {
                                this.Sql.RunCommand("DROP TABLE " + schema.Name);
                            }

                            // Search for foreign key and strip it from the main query, process it later
                            int idx = schema.Data.IndexOf("-------- FOREIGN KEY --------");
                            if(idx != -1)
                            {
                                mainQuery = schema.Data.Substring(0, idx);
                                fk.Add(schema.Data.Substring(idx));
                            }
                        }
                        else if(schema.Type == SchemaDataType.StoredProcedure)
                        {
                            if(this.Sql.IsProcedureExist(schema.Name))
                            {
                                this.Sql.RunCommand("DROP PROCEDURE " + schema.Name);
                            }
                        }

                        this.Sql.RunCommand(mainQuery);
                    }
                }
                while (model.IterNext(ref iterChild));
            }
            while (model.IterNext(ref iter));

            if (fk.Count != 0)
            {
                this.Sql.RunCommand(string.Join("\n\n", fk.ToArray()));
            }

            GtkLogService.Instance.Write("Schema restoration complete.");
        }
    }
}