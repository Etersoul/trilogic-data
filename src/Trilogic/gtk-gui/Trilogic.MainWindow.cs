
// This file has been generated by the GUI designer. Do not modify.
namespace Trilogic
{
	public partial class MainWindow
	{
		private global::Gtk.UIManager UIManager;
		private global::Gtk.Action FileAction;
		private global::Gtk.Action quitAction;
		private global::Gtk.Action EditAction;
		private global::Gtk.Action OptionsAction;
		private global::Gtk.VBox vbox4;
		private global::Gtk.MenuBar menubar;
		private global::Gtk.VPaned vpaned1;
		private global::Gtk.VBox vbox2;
		private global::Gtk.HBox hbox6;
		private global::Gtk.Fixed fixed1;
		private global::Gtk.ComboBox comboShow;
		private global::Gtk.ComboBox comboType;
		private global::Gtk.HPaned hpaned1;
		private global::Gtk.VBox vbox5;
		private global::Gtk.Table table1;
		private global::Gtk.Entry entryDatabase;
		private global::Gtk.Entry entryHost;
		private global::Gtk.Entry entryPassword;
		private global::Gtk.Entry entryUser;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Entry entryFolder;
		private global::Gtk.Button buttonOpenFolder;
		private global::Gtk.Label label1;
		private global::Gtk.Label label4;
		private global::Gtk.Label label5;
		private global::Gtk.Label label7;
		private global::Gtk.Label label8;
		private global::Gtk.Button buttonStartDiff;
		private global::Gtk.Button buttonWriteToFile;
		private global::Gtk.HBox hbox5;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TreeView treeviewDB;
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		private global::Gtk.TreeView treeviewFile;
		private global::Gtk.ScrolledWindow GtkScrolledWindow2;
		private global::Gtk.TextView textviewLog;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Trilogic.MainWindow
			this.UIManager = new global::Gtk.UIManager ();
			global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
			this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
			this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
			w1.Add (this.FileAction, null);
			this.quitAction = new global::Gtk.Action ("quitAction", global::Mono.Unix.Catalog.GetString ("Quit"), null, "gtk-quit");
			this.quitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Quit");
			w1.Add (this.quitAction, null);
			this.EditAction = new global::Gtk.Action ("EditAction", global::Mono.Unix.Catalog.GetString ("Edit"), null, null);
			this.EditAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Edit");
			w1.Add (this.EditAction, null);
			this.OptionsAction = new global::Gtk.Action ("OptionsAction", global::Mono.Unix.Catalog.GetString ("Options"), null, null);
			this.OptionsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Options");
			w1.Add (this.OptionsAction, null);
			this.UIManager.InsertActionGroup (w1, 0);
			this.AddAccelGroup (this.UIManager.AccelGroup);
			this.Name = "Trilogic.MainWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("Trilogic Backup");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child Trilogic.MainWindow.Gtk.Container+ContainerChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString ("<ui><menubar name=\'menubar\'><menu name=\'FileAction\' action=\'FileAction\'><menuitem" +
			" name=\'quitAction\' action=\'quitAction\'/></menu><menu name=\'EditAction\' action=\'E" +
			"ditAction\'><menuitem name=\'OptionsAction\' action=\'OptionsAction\'/></menu></menub" +
			"ar></ui>");
			this.menubar = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar")));
			this.menubar.Name = "menubar";
			this.vbox4.Add (this.menubar);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.menubar]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.vpaned1 = new global::Gtk.VPaned ();
			this.vpaned1.CanFocus = true;
			this.vpaned1.Name = "vpaned1";
			this.vpaned1.Position = 375;
			this.vpaned1.BorderWidth = ((uint)(5));
			// Container child vpaned1.Gtk.Paned+PanedChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.fixed1 = new global::Gtk.Fixed ();
			this.fixed1.Name = "fixed1";
			this.fixed1.HasWindow = false;
			this.hbox6.Add (this.fixed1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.fixed1]));
			w3.Position = 0;
			// Container child hbox6.Gtk.Box+BoxChild
			this.comboShow = global::Gtk.ComboBox.NewText ();
			this.comboShow.AppendText (global::Mono.Unix.Catalog.GetString ("Show All"));
			this.comboShow.AppendText (global::Mono.Unix.Catalog.GetString ("Show Difference"));
			this.comboShow.Name = "comboShow";
			this.comboShow.Active = 1;
			this.hbox6.Add (this.comboShow);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.comboShow]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.comboType = global::Gtk.ComboBox.NewText ();
			this.comboType.AppendText (global::Mono.Unix.Catalog.GetString ("All"));
			this.comboType.AppendText (global::Mono.Unix.Catalog.GetString ("Table"));
			this.comboType.AppendText (global::Mono.Unix.Catalog.GetString ("Stored Procedure"));
			this.comboType.AppendText (global::Mono.Unix.Catalog.GetString ("Data"));
			this.comboType.Name = "comboType";
			this.comboType.Active = 0;
			this.hbox6.Add (this.comboType);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.comboType]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox2.Add (this.hbox6);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox6]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hpaned1 = new global::Gtk.HPaned ();
			this.hpaned1.CanFocus = true;
			this.hpaned1.Name = "hpaned1";
			this.hpaned1.Position = 300;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.vbox5 = new global::Gtk.VBox ();
			this.vbox5.Name = "vbox5";
			this.vbox5.Spacing = 6;
			// Container child vbox5.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(5)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entryDatabase = new global::Gtk.Entry ();
			this.entryDatabase.CanFocus = true;
			this.entryDatabase.Name = "entryDatabase";
			this.entryDatabase.IsEditable = true;
			this.entryDatabase.InvisibleChar = '●';
			this.table1.Add (this.entryDatabase);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryDatabase]));
			w7.TopAttach = ((uint)(4));
			w7.BottomAttach = ((uint)(5));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryHost = new global::Gtk.Entry ();
			this.entryHost.CanFocus = true;
			this.entryHost.Name = "entryHost";
			this.entryHost.IsEditable = true;
			this.entryHost.InvisibleChar = '●';
			this.table1.Add (this.entryHost);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryHost]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryPassword = new global::Gtk.Entry ();
			this.entryPassword.CanFocus = true;
			this.entryPassword.Name = "entryPassword";
			this.entryPassword.IsEditable = true;
			this.entryPassword.Visibility = false;
			this.entryPassword.InvisibleChar = '●';
			this.table1.Add (this.entryPassword);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryPassword]));
			w9.TopAttach = ((uint)(3));
			w9.BottomAttach = ((uint)(4));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryUser = new global::Gtk.Entry ();
			this.entryUser.CanFocus = true;
			this.entryUser.Name = "entryUser";
			this.entryUser.IsEditable = true;
			this.entryUser.InvisibleChar = '●';
			this.table1.Add (this.entryUser);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryUser]));
			w10.TopAttach = ((uint)(2));
			w10.BottomAttach = ((uint)(3));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
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
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.entryFolder]));
			w11.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonOpenFolder = new global::Gtk.Button ();
			this.buttonOpenFolder.CanFocus = true;
			this.buttonOpenFolder.Name = "buttonOpenFolder";
			this.buttonOpenFolder.UseUnderline = true;
			this.buttonOpenFolder.Label = global::Mono.Unix.Catalog.GetString ("_Open");
			global::Gtk.Image w12 = new global::Gtk.Image ();
			w12.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-open", global::Gtk.IconSize.Menu);
			this.buttonOpenFolder.Image = w12;
			this.hbox1.Add (this.buttonOpenFolder);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonOpenFolder]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			this.table1.Add (this.hbox1);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1 [this.hbox1]));
			w14.LeftAttach = ((uint)(1));
			w14.RightAttach = ((uint)(2));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Folder");
			this.table1.Add (this.label1);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1 [this.label1]));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.Xalign = 0F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Host");
			this.label4.Justify = ((global::Gtk.Justification)(1));
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table1 [this.label4]));
			w16.TopAttach = ((uint)(1));
			w16.BottomAttach = ((uint)(2));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.Xalign = 0F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("User");
			this.table1.Add (this.label5);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1 [this.label5]));
			w17.TopAttach = ((uint)(2));
			w17.BottomAttach = ((uint)(3));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.Xalign = 0F;
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Password");
			this.label7.Justify = ((global::Gtk.Justification)(1));
			this.table1.Add (this.label7);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1 [this.label7]));
			w18.TopAttach = ((uint)(3));
			w18.BottomAttach = ((uint)(4));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label8 = new global::Gtk.Label ();
			this.label8.Name = "label8";
			this.label8.Xalign = 0F;
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString ("Database");
			this.table1.Add (this.label8);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1 [this.label8]));
			w19.TopAttach = ((uint)(4));
			w19.BottomAttach = ((uint)(5));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox5.Add (this.table1);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.table1]));
			w20.Position = 0;
			w20.Expand = false;
			w20.Fill = false;
			// Container child vbox5.Gtk.Box+BoxChild
			this.buttonStartDiff = new global::Gtk.Button ();
			this.buttonStartDiff.CanFocus = true;
			this.buttonStartDiff.Name = "buttonStartDiff";
			this.buttonStartDiff.UseUnderline = true;
			this.buttonStartDiff.Label = global::Mono.Unix.Catalog.GetString ("Start Diff");
			this.vbox5.Add (this.buttonStartDiff);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.buttonStartDiff]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			// Container child vbox5.Gtk.Box+BoxChild
			this.buttonWriteToFile = new global::Gtk.Button ();
			this.buttonWriteToFile.CanFocus = true;
			this.buttonWriteToFile.Name = "buttonWriteToFile";
			this.buttonWriteToFile.UseUnderline = true;
			this.buttonWriteToFile.Label = global::Mono.Unix.Catalog.GetString ("Write To File");
			this.vbox5.Add (this.buttonWriteToFile);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.buttonWriteToFile]));
			w22.Position = 2;
			w22.Expand = false;
			w22.Fill = false;
			this.hpaned1.Add (this.vbox5);
			global::Gtk.Paned.PanedChild w23 = ((global::Gtk.Paned.PanedChild)(this.hpaned1 [this.vbox5]));
			w23.Resize = false;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeviewDB = new global::Gtk.TreeView ();
			this.treeviewDB.CanFocus = true;
			this.treeviewDB.Name = "treeviewDB";
			this.GtkScrolledWindow.Add (this.treeviewDB);
			this.hbox5.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.GtkScrolledWindow]));
			w25.Position = 0;
			// Container child hbox5.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.treeviewFile = new global::Gtk.TreeView ();
			this.treeviewFile.CanFocus = true;
			this.treeviewFile.Name = "treeviewFile";
			this.GtkScrolledWindow1.Add (this.treeviewFile);
			this.hbox5.Add (this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.GtkScrolledWindow1]));
			w27.Position = 1;
			this.hpaned1.Add (this.hbox5);
			this.vbox2.Add (this.hpaned1);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hpaned1]));
			w29.Position = 1;
			this.vpaned1.Add (this.vbox2);
			global::Gtk.Paned.PanedChild w30 = ((global::Gtk.Paned.PanedChild)(this.vpaned1 [this.vbox2]));
			w30.Resize = false;
			// Container child vpaned1.Gtk.Paned+PanedChild
			this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
			this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
			this.textviewLog = new global::Gtk.TextView ();
			this.textviewLog.CanFocus = true;
			this.textviewLog.Name = "textviewLog";
			this.GtkScrolledWindow2.Add (this.textviewLog);
			this.vpaned1.Add (this.GtkScrolledWindow2);
			this.vbox4.Add (this.vpaned1);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.vpaned1]));
			w33.Position = 1;
			this.Add (this.vbox4);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 840;
			this.DefaultHeight = 527;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
			this.ResizeChecked += new global::System.EventHandler (this.OnResizeChecked);
			this.quitAction.Activated += new global::System.EventHandler (this.OnQuitActionActivated);
			this.OptionsAction.Activated += new global::System.EventHandler (this.OnOptionsActionActivated);
			this.comboShow.Changed += new global::System.EventHandler (this.OnComboShowChanged);
			this.comboType.Changed += new global::System.EventHandler (this.OnComboTypeChanged);
			this.buttonOpenFolder.Clicked += new global::System.EventHandler (this.OnButtonOpenFolderClicked);
			this.buttonStartDiff.Clicked += new global::System.EventHandler (this.OnButtonStartDiffClicked);
			this.treeviewDB.RowActivated += new global::Gtk.RowActivatedHandler (this.OnTreeviewRowActivated);
			this.treeviewFile.RowActivated += new global::Gtk.RowActivatedHandler (this.OnTreeviewRowActivated);
		}
	}
}
