
// This file has been generated by the GUI designer. Do not modify.
namespace Trilogic
{
	public partial class FolderSelector
	{
		private global::Gtk.VBox vbox1;
		private global::Gtk.FileChooserWidget fileChooser;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Entry entryFolder;
		private global::Gtk.Button buttonOK;
		private global::Gtk.Button buttonCancel;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Trilogic.FolderSelector
			this.Name = "Trilogic.FolderSelector";
			this.Title = global::Mono.Unix.Catalog.GetString ("FolderSelector");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.BorderWidth = ((uint)(6));
			// Container child Trilogic.FolderSelector.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.fileChooser = new global::Gtk.FileChooserWidget (((global::Gtk.FileChooserAction)(2)));
			this.fileChooser.Name = "fileChooser";
			this.vbox1.Add (this.fileChooser);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.fileChooser]));
			w1.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.entryFolder = new global::Gtk.Entry ();
			this.entryFolder.CanFocus = true;
			this.entryFolder.Name = "entryFolder";
			this.entryFolder.IsEditable = false;
			this.entryFolder.InvisibleChar = '●';
			this.hbox1.Add (this.entryFolder);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.entryFolder]));
			w2.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonOK = new global::Gtk.Button ();
			this.buttonOK.WidthRequest = 80;
			this.buttonOK.CanFocus = true;
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.UseUnderline = true;
			// Container child buttonOK.Gtk.Container+ContainerChild
			global::Gtk.Alignment w3 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w4 = new global::Gtk.HBox ();
			w4.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w5 = new global::Gtk.Image ();
			w5.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w4.Add (w5);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w7 = new global::Gtk.Label ();
			w7.LabelProp = global::Mono.Unix.Catalog.GetString ("_OK");
			w7.UseUnderline = true;
			w4.Add (w7);
			w3.Add (w4);
			this.buttonOK.Add (w3);
			this.hbox1.Add (this.buttonOK);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonOK]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.WidthRequest = 80;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			// Container child buttonCancel.Gtk.Container+ContainerChild
			global::Gtk.Alignment w12 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w13 = new global::Gtk.HBox ();
			w13.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w14 = new global::Gtk.Image ();
			w14.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-cancel", global::Gtk.IconSize.Menu);
			w13.Add (w14);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w16 = new global::Gtk.Label ();
			w16.LabelProp = global::Mono.Unix.Catalog.GetString ("Cancel");
			w16.UseUnderline = true;
			w13.Add (w16);
			w12.Add (w13);
			this.buttonCancel.Add (w12);
			this.hbox1.Add (this.buttonCancel);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonCancel]));
			w20.Position = 2;
			w20.Expand = false;
			w20.Fill = false;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 637;
			this.DefaultHeight = 442;
			this.Show ();
			this.ResizeChecked += new global::System.EventHandler (this.WindowResize);
			this.fileChooser.SelectionChanged += new global::System.EventHandler (this.FileChooserSelectionChanged);
			this.buttonOK.Clicked += new global::System.EventHandler (this.ButtonOKClicked);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.ButtonCancelClicked);
		}
	}
}
