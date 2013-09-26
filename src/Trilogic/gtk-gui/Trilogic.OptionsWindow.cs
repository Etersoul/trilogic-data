
// This file has been generated by the GUI designer. Do not modify.
namespace Trilogic
{
	public partial class OptionsWindow
	{
		private global::Gtk.VBox vbox1;
		private global::Gtk.Frame frame1;
		private global::Gtk.Alignment GtkAlignment;
		private global::Gtk.Table table1;
		private global::Gtk.Entry entry1;
		private global::Gtk.Entry entry2;
		private global::Gtk.Entry entry3;
		private global::Gtk.Entry entry4;
		private global::Gtk.Label label1;
		private global::Gtk.Label label2;
		private global::Gtk.Label label3;
		private global::Gtk.Label label4;
		private global::Gtk.Label GtkLabel;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Button buttonApply;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Trilogic.OptionsWindow
			this.Name = "Trilogic.OptionsWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("Options");
			this.TypeHint = ((global::Gdk.WindowTypeHint)(1));
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Resizable = false;
			// Container child Trilogic.OptionsWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			this.vbox1.BorderWidth = ((uint)(6));
			// Container child vbox1.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			this.GtkAlignment.LeftPadding = ((uint)(12));
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table (((uint)(4)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entry1 = new global::Gtk.Entry ();
			this.entry1.CanFocus = true;
			this.entry1.Name = "entry1";
			this.entry1.IsEditable = true;
			this.entry1.InvisibleChar = '●';
			this.table1.Add (this.entry1);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1 [this.entry1]));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entry2 = new global::Gtk.Entry ();
			this.entry2.CanFocus = true;
			this.entry2.Name = "entry2";
			this.entry2.IsEditable = true;
			this.entry2.InvisibleChar = '●';
			this.table1.Add (this.entry2);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.entry2]));
			w2.TopAttach = ((uint)(1));
			w2.BottomAttach = ((uint)(2));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entry3 = new global::Gtk.Entry ();
			this.entry3.CanFocus = true;
			this.entry3.Name = "entry3";
			this.entry3.IsEditable = true;
			this.entry3.InvisibleChar = '●';
			this.table1.Add (this.entry3);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.entry3]));
			w3.TopAttach = ((uint)(2));
			w3.BottomAttach = ((uint)(3));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entry4 = new global::Gtk.Entry ();
			this.entry4.CanFocus = true;
			this.entry4.Name = "entry4";
			this.entry4.IsEditable = true;
			this.entry4.InvisibleChar = '●';
			this.table1.Add (this.entry4);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.entry4]));
			w4.TopAttach = ((uint)(3));
			w4.BottomAttach = ((uint)(4));
			w4.LeftAttach = ((uint)(1));
			w4.RightAttach = ((uint)(2));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Table");
			this.table1.Add (this.label1);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.label1]));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.Xalign = 0F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Procedure");
			this.table1.Add (this.label2);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.Xalign = 0F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Data");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w7.TopAttach = ((uint)(2));
			w7.BottomAttach = ((uint)(3));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.Xalign = 0F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Table Options");
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.label4]));
			w8.TopAttach = ((uint)(3));
			w8.BottomAttach = ((uint)(4));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			this.GtkAlignment.Add (this.table1);
			this.frame1.Add (this.GtkAlignment);
			this.GtkLabel = new global::Gtk.Label ();
			this.GtkLabel.Name = "GtkLabel";
			this.GtkLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Directory</b>");
			this.GtkLabel.UseMarkup = true;
			this.frame1.LabelWidget = this.GtkLabel;
			this.vbox1.Add (this.frame1);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.frame1]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonApply = new global::Gtk.Button ();
			this.buttonApply.CanFocus = true;
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.UseUnderline = true;
			// Container child buttonApply.Gtk.Container+ContainerChild
			global::Gtk.Alignment w12 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w13 = new global::Gtk.HBox ();
			w13.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w14 = new global::Gtk.Image ();
			w14.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w13.Add (w14);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w16 = new global::Gtk.Label ();
			w16.LabelProp = global::Mono.Unix.Catalog.GetString ("Apply");
			w16.UseUnderline = true;
			w13.Add (w16);
			w12.Add (w13);
			this.buttonApply.Add (w12);
			this.hbox1.Add (this.buttonApply);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonApply]));
			w20.Position = 1;
			w20.Expand = false;
			w20.Fill = false;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w21.Position = 2;
			w21.Expand = false;
			w21.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 500;
			this.DefaultHeight = 300;
			this.Show ();
			this.buttonApply.Clicked += new global::System.EventHandler (this.OnButtonApplyClicked);
		}
	}
}
