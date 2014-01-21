namespace Trilogic
{
    using System;

    /// <summary>
    /// Compare window.
    /// </summary>
    public partial class CompareWindow : Gtk.Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.CompareWindow"/> class.
        /// </summary>
        public CompareWindow(Gtk.Window parent, string compareName) : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.Title += " - " + compareName;
        }
    }
}

