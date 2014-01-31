// <copyright file="CompareWindow.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Mono.TextEditor;

    /// <summary>
    /// Compare window.
    /// </summary>
    public partial class CompareWindow : Gtk.Window
    {
        /// <summary>
        /// The editor local.
        /// </summary>
        private Mono.TextEditor.TextEditor editorLocal;

        /// <summary>
        /// The editor D.
        /// </summary>
        private Mono.TextEditor.TextEditor editorDB;

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

            this.editorLocal = new Mono.TextEditor.TextEditor();
            this.editorDB = new Mono.TextEditor.TextEditor();

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

            this.editorLocal.Text = string.Empty;
            this.editorLocal.Caret.Column = 1;
            this.editorLocal.Caret.Line = 1;

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

            TextDocument docLocal = this.editorLocal.Document;
            TextDocument docDB = this.editorDB.Document;

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

                DiffPlex.Model.DiffResult result = new DiffPlex.Differ().CreateLineDiffs(databaseText, fileText, true);

                int spaceA = 0;
                ////int spaceB = 0;

                Dictionary<int, TextLineMarker> databaseMarker = new Dictionary<int, TextLineMarker>();
                Dictionary<int, TextLineMarker> fileMarker = new Dictionary<int, TextLineMarker>();

                foreach (DiffPlex.Model.DiffBlock block in result.DiffBlocks)
                {
                    databaseText = this.InsertLine(databaseText, block.InsertStartB, block.InsertCountB);

                    int modifyLine = Math.Abs(block.InsertCountB - block.DeleteCountA);

                    for (int i = 0; i < block.InsertCountB; i++)
                    {
                        if (i != block.InsertCountB - 1)
                        {
                            databaseMarker.Add(block.InsertStartB + i + 1 + spaceA, deleteMarker);
                            fileMarker.Add(block.InsertStartB + i + 1, addMarker);
                        }
                        else
                        {
                            ////databaseMarker.Add(block.InsertStartB + i + 1 + spaceA, modifyMarker);
                        }
                    }

                    spaceA += block.InsertCountB - 1;

                    for (int i = 0; i < block.DeleteCountA; i++)
                    {
                        int a = block.DeleteStartA + i + 1 + spaceA;
                        if (a >= block.InsertStartB && a <= block.InsertStartB + block.InsertCountB)
                        {
                            databaseMarker.Add(block.DeleteStartA + i + 1 + spaceA, modifyMarker);
                            ////fileMarker.Add(block.InsertStartB + 1, modifyMarker);
                        }
                        else
                        {
                            databaseMarker.Add(block.DeleteStartA + i + 1 + spaceA, deleteMarker);
                        }
                    }

                    Console.WriteLine("Insert B: " + block.InsertStartB + " " + block.InsertCountB + " ... Delete A: " + block.DeleteStartA + " " + block.DeleteCountA);
                }

                docDB.Text = databaseText;
                docLocal.Text = fileText;

                foreach (KeyValuePair<int, TextLineMarker> single in databaseMarker)
                {
                    docDB.AddMarker(single.Key, single.Value);
                }

                foreach (KeyValuePair<int, TextLineMarker> single in fileMarker)
                {
                    docLocal.AddMarker(single.Key, single.Value);
                }

                docDB.CommitUpdateAll();
            }
            else
            {
                docDB.Text = databaseText;
                docLocal.Text = fileText;
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
