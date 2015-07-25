using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Demot.RandomOrgApi;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		RandomOrgApiClient ROClient;
		List<string> Items;
		List<string> LastItems;
		List<Button> DeleteButtons;
        List<Label> lbMs;
		bool Inputing;
		int Fading;
		bool InputChanged;
		string Result;

		int DestHeight;
		int DestWidth;
		int DestPickTop;

		const int LineSpace = 35;
		const int BaseButtonTop = 18;
		const int BaseHeight = 97;
		const int MinWidth = 180;
		const int BaseWidth = 90;
        const int BaseItemTop = 11;
        Font TextFont = new System.Drawing.Font("Microsoft YaHei", 16.5F, FontStyle.Regular);

		public Form1()
		{
			InitializeComponent();
			Items = new List<string>();
			DeleteButtons = new List<Button>();
            lbMs = new List<Label>();
			Fading = 4;
			InputChanged = false;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			string key;
			key = "627cc29b-fabd-4f00-b0b0-2fead2262323";
			if (File.Exists("key.ini"))
			{
				FileStream fkey = new FileStream("key.ini", FileMode.Open);
				StreamReader srkey = new StreamReader(fkey);
				string sLine = srkey.ReadLine();
				if (sLine != null && sLine.Trim() != "")
				{
					key = sLine.Trim();
				}
				srkey.Close();
				fkey.Close();
			}

			ROClient = new RandomOrgApiClient(key);

			lbM.Text = "";
			lbM.Top = 10;
			btAdd.Top = BaseButtonTop;
			btPick.Top = BaseButtonTop;
            tbInput.Font = TextFont;
            this.MinimumSize = new Size(MinWidth, BaseHeight);
            this.MaximumSize = new Size(MinWidth, BaseHeight);
			this.Height = BaseHeight;
			this.Width = MinWidth;
		}

		private void btAdd_Click(object sender, EventArgs e)
		{
			if (Fading < 4)
				return;

			if (!Inputing)
			{
				tbInput.Text = "";
                tbInput.Top = Items.Count * LineSpace + BaseItemTop;
                tbInput.Width = 1;
				tbInput.Visible = true;
				tbInput.Focus();
				Inputing = true;
			}
			else
			{
				Add(tbInput.Text);
				Inputing = false;
			}
		}

		private void btPick_Click(object sender, EventArgs e)
		{
			if (Inputing)
			{
				btAdd_Click(null, null);
				return;
			}
			if (Fading < 4)
				return;
			if (Items.Count < 2)
				return;

			LastItems = new List<string>(Items);

			foreach (Button button in DeleteButtons)
				button.Visible = false;

			waiticon = 0;
			tiWait.Enabled = true;
			btAdd.Visible = false;
            btPick.BackgroundImage = global::Properties.Resources.wait1_48;

			Fading = 0;
			tiFade.Enabled = true;
		}


		private void btDelete_Click(object sender, EventArgs e)
		{
			Button me = (Button)sender;
			for (int i = 0; i < Items.Count; i++)
			{
				if (me == DeleteButtons[i])
				{
					Delete(i);
					break;
				}
			}
		}

		private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				btAdd_Click(null, null);
				e.Handled = true;
			}
			else if (e.KeyChar == 27)
			{
				Inputing = false;
				e.Handled = true;
			}
		}

		private void Add(string str)
		{
			if (str == "")
				return;

			Items.Add(str);

			MyButton aButton = new MyButton();
			aButton.Width = 14;
			aButton.Height = 14;
            aButton.Top = (Items.Count - 1) * LineSpace + BaseItemTop + 11;
			aButton.FlatStyle = FlatStyle.Flat;
			aButton.Left = (TextRenderer.MeasureText(str, TextFont)).Width + 30;
			aButton.Click += btDelete_Click;
			aButton.TabStop = false;
			aButton.FlatAppearance.BorderSize = 0;
			aButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            aButton.BackgroundImage = global::Properties.Resources.cross;
            aButton.BackgroundImageLayout = ImageLayout.Stretch;
			this.Controls.Add(aButton);
			aButton.BringToFront();
			DeleteButtons.Add(aButton);

            Label aLabel = new Label();
            aLabel.Font = TextFont;
            aLabel.AutoSize = true;
            aLabel.Top = (Items.Count - 1) * LineSpace + BaseItemTop;
            aLabel.Left = 20;
            aLabel.Text = str;
            aLabel.Click += Form1_Click;
            this.Controls.Add(aLabel);
            lbMs.Add(aLabel);
		}

		private void Delete(int i)
		{
			Button meb = DeleteButtons[i];
			DeleteButtons.RemoveAt(i);
			this.Controls.Remove(meb);
            meb.Dispose();

            Label mel = lbMs[i];
            lbMs.RemoveAt(i);
            this.Controls.Remove(mel);
            mel.Dispose();

            Items.RemoveAt(i);
		}

		private void tiLayout_Tick(object sender, EventArgs e)
		{
			int N = Items.Count;
			//lbM.Top = 20;

			string lbm = "";
			int maxwidth = 0;
			for (int i = 0; i < N; i++)
			{
				lbm += Items[i] + "\r\n";
                lbMs[i].Top = i * LineSpace + BaseItemTop;
                if (lbMs[i].Text != Items[i])
                    lbMs[i].Text = Items[i];
                int width = (TextRenderer.MeasureText(Items[i], TextFont)).Width;
				if (width > maxwidth)
					maxwidth = width;
                DeleteButtons[i].Top = i * LineSpace + BaseItemTop + 11;
                if (DeleteButtons[i].Left != width + 30)
                DeleteButtons[i].Left = width + 30;
			}

			if (Fading <= 2)
			{
				DestPickTop = BaseButtonTop;
				DestHeight = BaseHeight;
				DestWidth = BaseWidth;
			}
			else if (Inputing)
			{
                btAdd.BackgroundImage = global::Properties.Resources.ok_48;
                tbInput.Top = N * LineSpace + BaseItemTop;
				DestPickTop = (N + 1) * LineSpace + BaseButtonTop;
				DestHeight = (N + 1) * LineSpace + BaseHeight;
                int inputwidth = (TextRenderer.MeasureText(tbInput.Text, TextFont)).Width;
				tbInput.Width = Math.Max(inputwidth + 10, 50);
				if (inputwidth > maxwidth)
					maxwidth = inputwidth;
				DestWidth = maxwidth + BaseWidth;
				if (InputChanged)
				{
					int ss = tbInput.SelectionStart;
					tbInput.SelectionStart = 0;
					tbInput.SelectionStart = ss;
					InputChanged = false;
				}
			}
			else
			{
                btAdd.BackgroundImage = global::Properties.Resources.plus_48;
				tbInput.Visible = false;
				DestPickTop = N * LineSpace + BaseButtonTop;
				DestHeight = N * LineSpace + BaseHeight;
				DestWidth = maxwidth + BaseWidth;
			}

			if (DestPickTop != btAdd.Top)
			{
				double grow = DestPickTop - btAdd.Top;
				grow = grow * 0.2;
				if (Math.Abs(grow) < 1)
				{
					grow = Math.Sign(grow);
				}
				btAdd.Top += (int)grow;
				btPick.Top += (int)grow;
			}
			if (DestHeight != this.Height)
			{
				double grow = DestHeight - this.Height;
				grow = grow * 0.15;
				if (Math.Abs(grow) < 1)
				{
					grow = Math.Sign(grow);
				}
                this.MaximumSize = new Size(MaximumSize.Width, Height + (int)grow);
				this.Height += (int)grow;
			}
			if (DestWidth < MinWidth)
				DestWidth = MinWidth;
			if (DestWidth != this.Width)
			{
				double grow = DestWidth - this.Width;
				grow = grow * 0.15;
				if (Math.Abs(grow) < 1)
				{
					grow = Math.Sign(grow);
				}
                this.MaximumSize = new Size(Width + (int)grow, MaximumSize.Height);
				this.Width += (int)grow;
			}
		}

		int tiFadetick;
		private void tiFade_Tick(object sender, EventArgs e)
		{
			if (Fading == 0)
			{
				tiFadetick = 0;
				Fading = 1;
			}
			if (Fading == 1)
			{
				tiFadetick++;
                foreach (Label lb in lbMs)
                {
                    Color fc = lb.ForeColor;
                    Color bc = lb.BackColor;
                    int nr, ng, nb;
                    nr = (int)(fc.R + (bc.R - fc.R) * 0.5);
                    ng = (int)(fc.G + (bc.G - fc.G) * 0.5);
                    nb = (int)(fc.B + (bc.B - fc.B) * 0.5);

                    lb.ForeColor = Color.FromArgb(nr, ng, nb);
                }
				if (tiFadetick == 1)
				{
					Thread threadget = new Thread(ThreadGet);
					threadget.Start();
				}
				if (tiFadetick >= 50 && Result != null)
				{
					while (Items.Count > 0)
					{
						Delete(0);
					}
					Fading = 2;
					tiFadetick = 0;
				}
			}
			else if (Fading == 2)
			{
				tiFadetick++;
				if (tiFadetick == 30)
				{
					Add(Result);
                    foreach (Label lb in lbMs)
                    {
                        lb.ForeColor = lb.BackColor;
                    }
					Fading = 3;
					tiFadetick = 0;
				}
			}
			else if (Fading == 3)
			{
				tiFadetick++;
                foreach (Label lb in lbMs)
                {
                    Color fc = lb.ForeColor;
                    Color dc = SystemColors.ControlText;
                    double dr, dg, db;
                    dr = (fc.R - dc.R) * 0.03;
                    dg = (fc.G - dc.G) * 0.03;
                    db = (fc.B - dc.B) * 0.03;
                    if (dr != 0 && Math.Abs(dr) < 1)
                        dr = Math.Sign(dr);
                    if (dg != 0 && Math.Abs(dg) < 1)
                        dg = Math.Sign(dg);
                    if (db != 0 && Math.Abs(db) < 1)
                        db = Math.Sign(db);

                    lb.ForeColor = Color.FromArgb((int)(fc.R - dr), (int)(fc.G - dg), (int)(fc.B - db));
                }
				if (tiFadetick == 60)
				{
					Fading = 4;
					tiFadetick = 0;
					tiFade.Enabled = false;
				}
			}
		}

		private void tbInput_TextChanged(object sender, EventArgs e)
		{
			InputChanged = true;
		}

		int waiticon;
		private void tiWait_Tick(object sender, EventArgs e)
		{
			if (Fading >= 3)
			{
				waiticon = 0;
                btPick.BackgroundImage = global::Properties.Resources.dice_48;
				btAdd.Visible = true;
				tiWait.Enabled = false;
				return;
			}
			waiticon++;
			if (waiticon == 3)
				waiticon = 0;
			if (waiticon == 0)
                btPick.BackgroundImage = global::Properties.Resources.wait1_48;
			else if (waiticon == 1)
                btPick.BackgroundImage = global::Properties.Resources.wait2_48;
			else if (waiticon == 2)
                btPick.BackgroundImage = global::Properties.Resources.wait3_48;
		}

		private void ThreadGet()
		{
			Result = null;
			Response R = ROClient.GenerateIntegers(1, 0, Items.Count - 1);
			int ri = R.Integers[0];
			if (ri == -1)
			{
				MessageBox.Show("random.org API key invalid. Please enter a valid key in file key.ini");
				return;
			}
			Result = Items[ri];
		}

		private void btPick_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (Fading < 4)
					return;
				if (Items.Count > 0)
					return;
				if (Inputing)
				{
					Inputing = false;
					tbInput.Visible = false;
				}

				if (LastItems != null)
				{
					while (Items.Count > 0)
						Delete(0);
					this.Refresh();
					Thread.Sleep(50);
					foreach (string str in LastItems)
						Add(str);
				}
			}
		}

		private void Form1_Click(object sender, EventArgs e)
		{
			if (Inputing)
			{
				btAdd_Click(null, null);
			}
		}

		private void lbM_Click(object sender, EventArgs e)
		{
			if (Inputing)
			{
				btAdd_Click(null, null);
			}
		}
	}
}
