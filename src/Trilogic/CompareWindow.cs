// <copyright file="CompareWindow.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;

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
        /// The parent.
        /// </summary>
        private MainWindow parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.CompareWindow"/> class.
        /// </summary>
        /// <param name="parent">The parent window.</param>
        /// <param name="compareName">Compare name.</param>
        /// <param name="fileSide">File side.</param>
        /// <param name="dbSide">Db side.</param>
        public CompareWindow(MainWindow parent, string compareName, SchemaData fileSide, SchemaData dbSide) : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.parent = parent;

            this.editorLocal = new Mono.TextEditor.TextEditor();
            this.editorDB = new Mono.TextEditor.TextEditor();

            Gtk.ScrolledWindow scrollLocal = new Gtk.ScrolledWindow();
            Gtk.ScrolledWindow scrollDB = new Gtk.ScrolledWindow();
            scrollLocal.Add(editorLocal);
            scrollDB.Add(editorDB);

            this.hbox1.Add(scrollDB);
            this.hbox1.Add(scrollLocal);

            Gtk.Box.BoxChild childLocal = (Gtk.Box.BoxChild)this.hbox1[scrollLocal];
            childLocal.Position = 2;

            Gtk.Box.BoxChild childDB = (Gtk.Box.BoxChild)this.hbox1[scrollDB];
            childDB.Position = 0;

            this.ShowAll();

            editorLocal.Text = string.Empty;
            editorLocal.Caret.Column = 1;
            editorLocal.Caret.Line = 1;

            this.Title += " - " + compareName;

            if (fileSide != null)
            {
                this.GetFile(fileSide);
            }

            if (dbSide != null)
            {
                this.GetDBSchema(dbSide);
            }
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="schema">The schema.</param>
        private void GetFile(SchemaData schema)
        {
            string str = System.IO.File.ReadAllText(schema.FilePath);
            this.editorLocal.Text = str;
        }

        /// <summary>
        /// Gets the DB schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        private void GetDBSchema(SchemaData schema)
        {
            string str = "";
            if (schema.Type == SchemaDataType.StoredProcedure)
            {
                str = this.parent.Sql.GetStoredProcedureDefinition(schema.ObjectID);
            }
            else if(schema.Type == SchemaDataType.Table)
            {
                str = this.parent.Sql.GetTableSchema(schema.ObjectID);
            }

            this.editorDB.Text = str;
        }
    }
}

