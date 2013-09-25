namespace Trilogic
{
    using System;
    using Gtk;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    public partial class MainWindow: Gtk.Window
    {   
        private TextBuffer buffer;
        private AppSettings appSettings;
        public MainWindow(): base (Gtk.WindowType.Toplevel)
        {
            Build();
            appSettings = new AppSettings();
            appSettings.ReadFromFile();

            this.entryHost.Text = appSettings.DBHost;
            this.entryUser.Text = appSettings.DBUser;
            this.entryPassword.Text = appSettings.DBPassword;
            this.entryDatabase.Text = appSettings.DBName;
            this.entryFolder.Text = appSettings.DirectoryPath;

            // force the position to this specific position
            this.vpaned1.Position = 300;
            this.hpaned1.Position = 300;

            this.buffer = textviewLog.Buffer;

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

            this.Log("Insert the username and start the diff");
        }

        public void FillFolder(string folder)
        {
            this.entryFolder.Text = folder;
        }

        public void Quit()
        {
            this.SaveConfiguration();
            Application.Quit();
        }

        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            this.Quit();
        }

        protected void ExitButtonActivated(object sender, EventArgs e)
        {
            this.Quit();
        }

        protected void SaveConfiguration()
        {
            appSettings.DBHost = this.entryHost.Text;
            appSettings.DBUser = this.entryUser.Text;
            appSettings.DBPassword = this.entryPassword.Text;
            appSettings.DBName = this.entryDatabase.Text;
            appSettings.DirectoryPath = this.entryFolder.Text;

            appSettings.WriteToFile();
        }

        protected void ButtonStartDiffClicked(object sender, EventArgs e)
        {
            this.SaveConfiguration();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = entryHost.Text;
            builder.UserID = entryUser.Text;
            builder.Password = entryPassword.Text;
            builder.ConnectTimeout = 5;

            // prepare the list store
            ListStore list = new ListStore(typeof(string));
            treeviewDB.Model = list;

            ListStore list2 = new ListStore(typeof(string));
            treeviewFile.Model = list2;
       
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ToString()))
                {
                    this.Log("Opening connection");
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT h.name + '.' + s.name AS name FROM sys.tables s INNER JOIN sys.schemas h ON h.schema_id = s.schema_id WHERE s.type = 'U' ORDER BY s.name";
                    command.CommandType = CommandType.Text;

                    this.Log("Sending command");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.AppendValues(reader["name"].ToString());
                        }
                    }

                    this.Log("Process finished");
                }
            }
            catch (Exception ex)
            {
                buffer.Text = ex.Message;
            }

            string dir = this.entryFolder.Text;
            if (!Directory.Exists(dir))
            {
                this.Log(string.Concat("Failed to get directory ", dir));
                return;
            }

            string tableDir = dir + "/table";
            if (!Directory.Exists(tableDir))
            {
                this.Log(string.Concat("Failed to get table directory ", tableDir));
                return;
            }

            foreach (string file in Directory.GetFiles(tableDir))
            {
                list2.AppendValues(file.Replace("\\", "/").Substring(tableDir.Length + 1, -4));
            }
        }

        protected void Log(string msg)
        {
            buffer.Text += msg + "\n";
        }

        protected void ButtonOpenFolderClicked(object sender, EventArgs e)
        {
            new FolderSelector(this).ActivateFocus();
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
