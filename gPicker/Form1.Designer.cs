namespace WindowsFormsApplication1
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbM = new System.Windows.Forms.Label();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.tiLayout = new System.Windows.Forms.Timer(this.components);
            this.tiFade = new System.Windows.Forms.Timer(this.components);
            this.tiWait = new System.Windows.Forms.Timer(this.components);
            this.btPick = new WindowsFormsApplication1.MyButton();
            this.btAdd = new WindowsFormsApplication1.MyButton();
            this.SuspendLayout();
            // 
            // lbM
            // 
            this.lbM.AutoSize = true;
            this.lbM.Font = new System.Drawing.Font("Microsoft YaHei", 20.75F);
            this.lbM.Location = new System.Drawing.Point(55, 44);
            this.lbM.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbM.Name = "lbM";
            this.lbM.Size = new System.Drawing.Size(169, 64);
            this.lbM.TabIndex = 1;
            this.lbM.Text = "label1";
            this.lbM.Click += new System.EventHandler(this.lbM_Click);
            // 
            // tbInput
            // 
            this.tbInput.BackColor = System.Drawing.SystemColors.Window;
            this.tbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInput.Font = new System.Drawing.Font("Microsoft YaHei", 20.75F);
            this.tbInput.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.tbInput.Location = new System.Drawing.Point(42, 128);
            this.tbInput.Margin = new System.Windows.Forms.Padding(0);
            this.tbInput.MaximumSize = new System.Drawing.Size(1833, 34);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(792, 34);
            this.tbInput.TabIndex = 2;
            this.tbInput.Visible = false;
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            this.tbInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInput_KeyPress);
            // 
            // tiLayout
            // 
            this.tiLayout.Enabled = true;
            this.tiLayout.Interval = 30;
            this.tiLayout.Tick += new System.EventHandler(this.tiLayout_Tick);
            // 
            // tiFade
            // 
            this.tiFade.Interval = 30;
            this.tiFade.Tick += new System.EventHandler(this.tiFade_Tick);
            // 
            // tiWait
            // 
            this.tiWait.Interval = 400;
            this.tiWait.Tick += new System.EventHandler(this.tiWait_Tick);
            // 
            // btPick
            // 
            this.btPick.BackgroundImage = global::Properties.Resources.dice_48;
            this.btPick.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btPick.FlatAppearance.BorderSize = 0;
            this.btPick.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btPick.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btPick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPick.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btPick.Location = new System.Drawing.Point(121, 246);
            this.btPick.Margin = new System.Windows.Forms.Padding(6);
            this.btPick.Name = "btPick";
            this.btPick.Size = new System.Drawing.Size(32, 32);
            this.btPick.TabIndex = 0;
            this.btPick.TabStop = false;
            this.btPick.UseVisualStyleBackColor = true;
            this.btPick.Click += new System.EventHandler(this.btPick_Click);
            this.btPick.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btPick_MouseDown);
            // 
            // btAdd
            // 
            this.btAdd.BackgroundImage = global::Properties.Resources.plus_48;
            this.btAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btAdd.FlatAppearance.BorderSize = 0;
            this.btAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAdd.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btAdd.Location = new System.Drawing.Point(59, 246);
            this.btAdd.Margin = new System.Windows.Forms.Padding(6);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 3;
            this.btAdd.TabStop = false;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(953, 372);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.btPick);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.lbM);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gPicker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private MyButton btPick;
		private System.Windows.Forms.Label lbM;
		private System.Windows.Forms.TextBox tbInput;
		private MyButton btAdd;
		private System.Windows.Forms.Timer tiLayout;
		private System.Windows.Forms.Timer tiFade;
		private System.Windows.Forms.Timer tiWait;
	}
}

