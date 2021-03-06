
// This file has been generated by the GUI designer. Do not modify.
namespace Trilogic
{
	public partial class ExceptionDialog
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.Label labelMessage;
		private global::Gtk.Label labelStackTrace;
		private global::Gtk.HSeparator hseparator1;
		private global::Gtk.Label label3;
		private global::Gtk.Button buttonContinue;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Trilogic.ExceptionDialog
			this.Name = "Trilogic.ExceptionDialog";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Modal = true;
			this.BorderWidth = ((uint)(6));
			this.Resizable = false;
			// Internal child Trilogic.ExceptionDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.labelMessage = new global::Gtk.Label ();
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Xalign = 0F;
			this.labelMessage.LabelProp = global::Mono.Unix.Catalog.GetString ("Message");
			this.labelMessage.UseMarkup = true;
			this.labelMessage.Wrap = true;
			this.vbox2.Add (this.labelMessage);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.labelMessage]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.labelStackTrace = new global::Gtk.Label ();
			this.labelStackTrace.WidthRequest = 600;
			this.labelStackTrace.Name = "labelStackTrace";
			this.labelStackTrace.Xalign = 0F;
			this.labelStackTrace.LabelProp = global::Mono.Unix.Catalog.GetString ("Stack Trace");
			this.labelStackTrace.Wrap = true;
			this.vbox2.Add (this.labelStackTrace);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.labelStackTrace]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			this.vbox2.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hseparator1]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.WidthRequest = 600;
			this.label3.Name = "label3";
			this.label3.Yalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Click the button below to quit the application or continue using the Application." +
			" Continue to use the application after this exception may cause unexpected behav" +
			"ior and may cause data loss.");
			this.label3.Wrap = true;
			this.vbox2.Add (this.label3);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.label3]));
			w5.PackType = ((global::Gtk.PackType)(1));
			w5.Position = 3;
			w5.Expand = false;
			w5.Fill = false;
			w1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox2]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Internal child Trilogic.ExceptionDialog.ActionArea
			global::Gtk.HButtonBox w7 = this.ActionArea;
			w7.Name = "dialog1_ActionArea";
			w7.Spacing = 10;
			w7.BorderWidth = ((uint)(5));
			w7.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonContinue = new global::Gtk.Button ();
			this.buttonContinue.CanDefault = true;
			this.buttonContinue.CanFocus = true;
			this.buttonContinue.Name = "buttonContinue";
			this.buttonContinue.UseUnderline = true;
			this.buttonContinue.Label = global::Mono.Unix.Catalog.GetString ("_Continue");
			global::Gtk.Image w8 = new global::Gtk.Image ();
			w8.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-execute", global::Gtk.IconSize.Menu);
			this.buttonContinue.Image = w8;
			this.AddActionWidget (this.buttonContinue, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w9 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w7 [this.buttonContinue]));
			w9.Expand = false;
			w9.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = global::Mono.Unix.Catalog.GetString ("_Quit");
			global::Gtk.Image w10 = new global::Gtk.Image ();
			w10.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-quit", global::Gtk.IconSize.Menu);
			this.buttonOk.Image = w10;
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w11 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w7 [this.buttonOk]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 616;
			this.DefaultHeight = 300;
			this.Show ();
			this.buttonContinue.Clicked += new global::System.EventHandler (this.OnButtonContinueClicked);
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnButtonQuitClicked);
		}
	}
}
