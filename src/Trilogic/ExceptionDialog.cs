namespace Trilogic
{
    using Gtk;
    using System;
    public partial class ExceptionDialog : Gtk.Dialog
    {
        public ExceptionDialog(string message, string stackTrace)
        {
            this.Build();
            this.labelMessage.Markup = "<b>" + message + "</b>";
            this.labelStackTrace.Text = stackTrace;
        }

        protected void ButtonOKClicked(object sender, EventArgs e)
        {
            Application.Quit();
        }
    }
}

