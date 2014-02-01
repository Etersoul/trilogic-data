// <copyright file="CompareWindow.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using DiffPlex.Model;
    using Mono.TextEditor;

    /// <summary>
    /// Compare window.
    /// </summary>
    public partial class CompareWindow : Gtk.Window
    {
        /// <summary>
        /// The editor for local file.
        /// </summary>
        private TextEditor editorLocal;

        /// <summary>
        /// The editor for database.
        /// </summary>
        private TextEditor editorDB;

        /// <summary>
        /// The document local.
        /// </summary>
        private TextDocument docLocal;

        /// <summary>
        /// The document database.
        /// </summary>
        private TextDocument docDB;

        /// <summary>
        /// The file side.
        /// </summary>
        private SchemaData fileSide;

        /// <summary>
        /// The database side.
        /// </summary>
        private SchemaData databaseSide;

        /// <summary>
        /// The parent.
        /// </summary>
        private MainWindow parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.CompareWindow"/> class.
        /// </summary>
        /// <param name="parent">The parent window.</param>
        /// <param name="compareName">Compare name.</param>
        /// <param name="fileSide">File side.</param>
        /// <param name="databaseSide">Database side.</param>
        public CompareWindow(MainWindow parent, string compareName, SchemaData fileSide, SchemaData databaseSide) : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.parent = parent;

            this.docLocal = new TextDocument();
            this.docDB = new TextDocument();

            this.editorLocal = new TextEditor(this.docLocal);
            this.editorDB = new TextEditor(this.docDB);

            Gtk.ScrolledWindow scrollLocal = new Gtk.ScrolledWindow();
            Gtk.ScrolledWindow scrollDB = new Gtk.ScrolledWindow();
            scrollLocal.Add(this.editorLocal);
            scrollDB.Add(this.editorDB);

            this.hbox1.Add(scrollDB);
            this.hbox1.Add(scrollLocal);

            Gtk.Box.BoxChild childLocal = (Gtk.Box.BoxChild)this.hbox1[scrollLocal];
            childLocal.Position = 2;

            Gtk.Box.BoxChild childDB = (Gtk.Box.BoxChild)this.hbox1[scrollDB];
            childDB.Position = 0;

            this.ShowAll();

            this.Title += " - " + compareName;

            this.fileSide = fileSide;
            this.databaseSide = databaseSide;

            this.ShowSchema();
        }

        /// <summary>
        /// Shows the schema.
        /// </summary>
        private void ShowSchema()
        {
            string fileText = string.Empty;
            string databaseText = string.Empty;

            if (this.fileSide != null)
            {
                fileText = this.GetFile(this.fileSide);
            }

            if (this.databaseSide != null)
            {
                databaseText = this.GetDBSchema(this.databaseSide);
            }

            if (this.checkBoxDiff.Active)
            {
                StyleTextLineMarker addMarker = new StyleTextLineMarker()
                {
                    BackgroundColor = new Cairo.Color(0.8, 0.8, 1)
                };

                StyleTextLineMarker deleteMarker = new StyleTextLineMarker()
                {
                    BackgroundColor = new Cairo.Color(1, 0.8, 0.8)
                };

                StyleTextLineMarker modifyMarker = new StyleTextLineMarker()
                {
                    BackgroundColor = new Cairo.Color(1, 1, 0.6)
                };

                StyleTextLineMarker grayMarker = new StyleTextLineMarker()
                {
                    BackgroundColor = new Cairo.Color(0.6, 0.6, 0.6)
                };

                DiffResult result = new DiffPlex.Differ().CreateLineDiffs(databaseText, fileText, false);

                Dictionary<int, TextLineMarker> databaseMarker = new Dictionary<int, TextLineMarker>();
                Dictionary<int, TextLineMarker> fileMarker = new Dictionary<int, TextLineMarker>();

                string[] fileTextArray = fileText.Split('\n');
                string[] databaseTextArray = databaseText.Split('\n');

                List<string> processedFile = new List<string>();
                List<string> processedDatabase = new List<string>();

                int i1 = 0; // original database line number, start from 0
                int i2 = 0; // original file line number, start from 0
                int d1 = -1;
                int d2 = -1;

                DiffBlock currentBlock = new DiffBlock(-1, 0, -1, 0);
                if (result.DiffBlocks.Count > 0)
                {
                    currentBlock = result.DiffBlocks[0];
                    result.DiffBlocks.RemoveAt(0);
                }

                for (int i = 0; true; i++)
                {
                    bool change = false;

                    if (i1 == currentBlock.DeleteStartA)
                    {
                        d1 = 0;
                    }

                    if (d1 != -1)
                    {
                        change = true;
                        if (d1 < currentBlock.DeleteCountA)
                        {
                            processedDatabase.Add(databaseTextArray[i1]);
                            databaseMarker.Add(i + 1, deleteMarker);

                            processedFile.Add(string.Empty);
                            fileMarker.Add(i + 1, deleteMarker);

                            i1++;
                            d1++;
                        }
                        else
                        {
                            d1 = -1;
                        }
                    }

                    if (i2 == currentBlock.InsertStartB)
                    {
                        d2 = 0;
                    }

                    if (d2 != -1)
                    {
                        change = true;
                        if (d2 < currentBlock.InsertCountB)
                        {
                            processedDatabase.Add(string.Empty);
                            if (databaseMarker.ContainsKey(i + 1))
                            {
                                databaseMarker[i + 1] = modifyMarker;
                            }
                            else
                            {
                                databaseMarker.Add(i + 1, addMarker);
                            }

                            processedFile.Add(fileTextArray[i2]);

                            if (fileMarker.ContainsKey(i + 1))
                            {
                                fileMarker[i + 1] = modifyMarker;
                            }
                            else
                            {
                                fileMarker.Add(i + 1, addMarker);
                            }

                            i2++;
                            d2++;
                        }
                        else
                        {
                            d2 = -1;
                        }
                    }

                    // Stop the iteration if we are on the last line now
                    if (i1 >= databaseTextArray.Length && i2 >= fileTextArray.Length)
                    {
                        break;
                    }

                    if (d1 == -1 && d2 == -1 && change)
                    {
                        if (result.DiffBlocks.Count > 0)
                        {
                            currentBlock = result.DiffBlocks[0];
                            result.DiffBlocks.RemoveAt(0);
                        }
                        else
                        {
                            currentBlock = new DiffBlock(-1, 0, -1, 0);
                        }

                        change = false;
                    }

                    // No changes, insert the original text without marker
                    if (!change)
                    {
                        processedDatabase.Add(databaseTextArray[i1]);
                        processedFile.Add(fileTextArray[i2]);

                        i1++;
                        i2++;
                    }
                }

                this.docDB.Text = string.Join("\n", processedDatabase.ToArray());
                this.docLocal.Text = string.Join("\n", processedFile.ToArray());

                foreach (KeyValuePair<int, TextLineMarker> single in databaseMarker)
                {
                    this.docDB.AddMarker(single.Key, single.Value);
                }

                foreach (KeyValuePair<int, TextLineMarker> single in fileMarker)
                {
                    this.docLocal.AddMarker(single.Key, single.Value);
                }

                this.docDB.CommitUpdateAll();
            }
            else
            {
                this.docDB.Text = databaseText;
                this.docLocal.Text = fileText;
            }
        }

        /// <summary>
        /// Inserts the line.
        /// </summary>
        /// <returns>The line.</returns>
        /// <param name="haystack">The haystack.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        private string InsertLine(string haystack, int start, int count)
        {
            Match regex = Regex.Match(haystack, "(\n)", RegexOptions.Multiline);

            if (regex.Success && regex.Groups[1].Captures.Count > start)
            {
                int idx = 0;
                if (start != 0)
                {
                    idx = regex.Groups[1].Captures[start - 1].Index;
                }

                for (int i = 0; i < count; i++)
                {
                    if (i == 0 && start == 0)
                    {
                        continue;
                    }

                    haystack = haystack.Insert(idx, "\n");
                }
            }

            return haystack;
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns>Text string</returns>
        private string GetFile(SchemaData schema)
        {
            string str = System.IO.File.ReadAllText(schema.FilePath);

            return str;
        }

        /// <summary>
        /// Gets the DB schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns>Text string</returns>
        private string GetDBSchema(SchemaData schema)
        {
            string str = string.Empty;
            if (schema.Type == SchemaDataType.StoredProcedure)
            {
                str = this.parent.Sql.GetStoredProcedureDefinition(schema.ObjectID);
            }
            else if (schema.Type == SchemaDataType.Table)
            {
                str = this.parent.Sql.GetTableSchema(schema.ObjectID);
            }

            return str;
        }

        /// <summary>
        /// Raises the check box diff toggled event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnCheckBoxDiffToggled(object sender, EventArgs e)
        {
            this.ShowSchema();
        }
    }
}
