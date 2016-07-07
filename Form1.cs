using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Ini;

namespace Ini
{
	public class Form1 : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
		}
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		private void InitializeComponent()
		{
            this.listCat = new System.Windows.Forms.ListBox();
            this.listKey = new System.Windows.Forms.ListBox();
            this.tValue = new System.Windows.Forms.TextBox();
            this.tCat = new System.Windows.Forms.TextBox();
            this.bCatAdd = new System.Windows.Forms.Button();
            this.lCat = new System.Windows.Forms.Label();
            this.lKeys = new System.Windows.Forms.Label();
            this.bKeyAdd = new System.Windows.Forms.Button();
            this.tKey = new System.Windows.Forms.TextBox();
            this.lValue = new System.Windows.Forms.Label();
            this.tFileName = new System.Windows.Forms.TextBox();
            this.bReadIni = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.lFileName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.listCat.Location = new System.Drawing.Point(8, 28);
            this.listCat.Name = "listCat";
            this.listCat.Size = new System.Drawing.Size(132, 225);
            this.listCat.TabIndex = 0;
            this.listCat.SelectedIndexChanged += new System.EventHandler(this.listCat_SelectedIndexChanged);

            this.listKey.Location = new System.Drawing.Point(146, 28);
            this.listKey.Name = "listKey";
            this.listKey.Size = new System.Drawing.Size(171, 186);
            this.listKey.TabIndex = 1;
            this.listKey.SelectedIndexChanged += new System.EventHandler(this.listKey_SelectedIndexChanged);
            // 
            // tValue
            // 
            this.tValue.Location = new System.Drawing.Point(146, 266);
            this.tValue.Name = "tValue";
            this.tValue.Size = new System.Drawing.Size(171, 20);
            this.tValue.TabIndex = 2;
            // 
            // tCat
            // 
            this.tCat.Location = new System.Drawing.Point(8, 266);
            this.tCat.Name = "tCat";
            this.tCat.Size = new System.Drawing.Size(132, 20);
            this.tCat.TabIndex = 3;
            // 
            // bCatAdd
            // 
            this.bCatAdd.Location = new System.Drawing.Point(12, 292);
            this.bCatAdd.Name = "bCatAdd";
            this.bCatAdd.Size = new System.Drawing.Size(128, 20);
            this.bCatAdd.TabIndex = 4;
            this.bCatAdd.Text = "Add";
            this.bCatAdd.Click += new System.EventHandler(this.bCatAdd_Click);
            // 
            // lCat
            // 
            this.lCat.Location = new System.Drawing.Point(5, 9);
            this.lCat.Name = "lCat";
            this.lCat.Size = new System.Drawing.Size(80, 16);
            this.lCat.TabIndex = 5;
            this.lCat.Text = "Categories";
            // 
            // lKeys
            // 
            this.lKeys.Location = new System.Drawing.Point(143, 9);
            this.lKeys.Name = "lKeys";
            this.lKeys.Size = new System.Drawing.Size(40, 16);
            this.lKeys.TabIndex = 5;
            this.lKeys.Text = "Keys";
            // 
            // bKeyAdd
            // 
            this.bKeyAdd.Location = new System.Drawing.Point(146, 292);
            this.bKeyAdd.Name = "bKeyAdd";
            this.bKeyAdd.Size = new System.Drawing.Size(171, 20);
            this.bKeyAdd.TabIndex = 4;
            this.bKeyAdd.Text = "Add";
            this.bKeyAdd.Click += new System.EventHandler(this.bKeyAdd_Click);
            // 
            // tKey
            // 
            this.tKey.Location = new System.Drawing.Point(146, 232);
            this.tKey.Name = "tKey";
            this.tKey.Size = new System.Drawing.Size(171, 20);
            this.tKey.TabIndex = 3;
            // 
            // lValue
            // 
            this.lValue.Location = new System.Drawing.Point(143, 251);
            this.lValue.Name = "lValue";
            this.lValue.Size = new System.Drawing.Size(171, 12);
            this.lValue.TabIndex = 5;
            this.lValue.Text = "Then write a value for a key";
            // 
            // tFileName
            // 
            this.tFileName.Location = new System.Drawing.Point(323, 28);
            this.tFileName.Name = "tFileName";
            this.tFileName.Size = new System.Drawing.Size(221, 20);
            this.tFileName.TabIndex = 2;
            // 
            // bReadIni
            // 
            this.bReadIni.Location = new System.Drawing.Point(323, 80);
            this.bReadIni.Name = "bReadIni";
            this.bReadIni.Size = new System.Drawing.Size(221, 20);
            this.bReadIni.TabIndex = 4;
            this.bReadIni.Text = "Read Ini";
            this.bReadIni.Click += new System.EventHandler(this.bReadIni_Click);
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(323, 54);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(221, 20);
            this.bSave.TabIndex = 4;
            this.bSave.Text = "Save Ini";
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // lFileName
            // 
            this.lFileName.Location = new System.Drawing.Point(320, 9);
            this.lFileName.Name = "lFileName";
            this.lFileName.Size = new System.Drawing.Size(152, 16);
            this.lFileName.TabIndex = 5;
            this.lFileName.Text = "File Saving and Reading";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(143, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Choose a category and write a key";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(556, 365);
            this.Controls.Add(this.lCat);
            this.Controls.Add(this.bCatAdd);
            this.Controls.Add(this.tCat);
            this.Controls.Add(this.tValue);
            this.Controls.Add(this.listKey);
            this.Controls.Add(this.listCat);
            this.Controls.Add(this.lKeys);
            this.Controls.Add(this.bKeyAdd);
            this.Controls.Add(this.tKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lValue);
            this.Controls.Add(this.tFileName);
            this.Controls.Add(this.bReadIni);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.lFileName);
            this.Name = "Form1";
            this.Text = "IniHandler Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private System.Windows.Forms.ListBox listCat;
		private System.Windows.Forms.ListBox listKey;
		private System.Windows.Forms.TextBox tValue;
		private System.Windows.Forms.TextBox tCat;
		private System.Windows.Forms.Button bCatAdd;
		private System.Windows.Forms.Label lCat;
		private System.Windows.Forms.Label lKeys;
		private System.Windows.Forms.Button bKeyAdd;
		private System.Windows.Forms.TextBox tKey;
        private System.Windows.Forms.Label lValue;
		private System.Windows.Forms.TextBox tFileName;
		private System.Windows.Forms.Button bReadIni;

		IniStructure inis = new IniStructure();
        private System.Windows.Forms.Button bSave;
        private Label label1;
        private System.Windows.Forms.Label lFileName;

		private void Form1_Load(object sender, System.EventArgs e)
		{
		}

		private new void Refresh()
		{
			tValue.Text ="";
			listCat.Items.Clear();
			listKey.Items.Clear();
			foreach (string name in inis.GetCategories())
			{
				listCat.Items.Add(name);
			}
		}

		private void bCatAdd_Click(object sender, System.EventArgs e)
		{
			if (!inis.AddCategory(tCat.Text))
				MessageBox.Show("AddCategory returned false","error");
			Refresh();
		}

		private void bKeyAdd_Click(object sender, System.EventArgs e)
		{
			if (GetCatName() == "")
			{
				MessageBox.Show("You have to select a category","error");
				return;
			}			if (!inis.AddValue(GetCatName(), tKey.Text, tValue.Text))
							MessageBox.Show("AddValue returned false","error");
			Refresh();
			
		}

		private void listCat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			listKey.Items.Clear();
			string[] Keys = inis.GetKeys(GetCatName());
			if (Keys == null)
				return;
			tValue.Text = "";
			foreach (string key in Keys )
			{
				listKey.Items.Add(key);
			}
		}
		private void listKey_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			tValue.Text  = "";
			tValue.Text = inis.GetValue(GetCatName(), GetKeyName());
		}
		private string GetCatName()
		{
			if (listCat.SelectedIndex == -1)
				return "";
			return listCat.Items[listCat.SelectedIndex].ToString();
		}

		private string GetKeyName()
		{
			if (listKey.SelectedIndex == -1)
				return null;
			return listKey.Items[listKey.SelectedIndex].ToString();
		}
		private void bReadIni_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Ini files (*.ini)|*.ini|All files (*.*)|*.*";
			dialog.InitialDirectory = @"C:\";
			dialog.RestoreDirectory = true;
			
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				tFileName.Text = dialog.FileName;

				inis = IniStructure.ReadIni(tFileName.Text);
				if (inis == null)
					MessageBox.Show("Something went wrong","error");	
			}
		}

		private void bSave_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Filter = "Ini files (*.ini)|*.ini|All files (*.*)|*.*";
			dialog.DefaultExt = "ini";
			dialog.RestoreDirectory = true;
			dialog.InitialDirectory = @"C:\";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				tFileName.Text = dialog.FileName;

				if (!IniStructure.WriteIni(inis,tFileName.Text))
					MessageBox.Show("Something went wrong","error");
			}
		}


	}
}
