using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
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
		bool Inputing;
		int Fading;
		bool InputChanged;
		string Result;

		int DestHeight;
		int DestWidth;
		int DestPickTop;

		const int LineSpace = 28;
		const int BaseButtonTop = 20;
		const int BaseHeight = 95;
		const int MinWidth = 135;
		const int BaseWidth = 70;

		public Form1()
		{
			InitializeComponent();
			Items = new List<string>();
			DeleteButtons = new List<Button>();
			Fading = 4;
			InputChanged = false;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			ROClient = new RandomOrgApiClient("627cc29b-fabd-4f00-b0b0-2fead2262323");

			lbM.Text = "";
			lbM.Top = 10;
			btAdd.Top = BaseButtonTop;
			btPick.Top = BaseButtonTop;
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
			btPick.Image = global::Properties.Resources.wait1;

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
			aButton.Width = 11;
			aButton.Height = 11;
			aButton.Top = (Items.Count - 1) * LineSpace + 19;
			aButton.FlatStyle = FlatStyle.Flat;
			aButton.Left = 15;
			//aButton.Left = (TextRenderer.MeasureText(str, lbM.Font)).Width + 30;
			aButton.Click += btDelete_Click;
			aButton.TabStop = false;
			aButton.FlatAppearance.BorderSize = 0;
			aButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
			aButton.Image = global::Properties.Resources.cross_10;
			this.Controls.Add(aButton);
			aButton.BringToFront();
			DeleteButtons.Add(aButton);
		}

		private void Delete(int i)
		{
			Button me = DeleteButtons[i];
			Items.RemoveAt(i);
			DeleteButtons.RemoveAt(i);
			this.Controls.Remove(me);
			me.Dispose();
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
				DeleteButtons[i].Top = i * LineSpace + 19;
				int width = (TextRenderer.MeasureText(Items[i], lbM.Font)).Width;
				if (width > maxwidth)
					maxwidth = width;
			}
			if (lbM.Text != lbm)
				lbM.Text = lbm;

			if (Fading <= 2)
			{
				DestPickTop = BaseButtonTop;
				DestHeight = BaseHeight;
				DestWidth = BaseWidth;
			}
			else if (Inputing)
			{
				btAdd.Image = global::Properties.Resources.ok;
				tbInput.Top = N * LineSpace + 10;
				DestPickTop = (N + 1) * LineSpace + BaseButtonTop;
				DestHeight = (N + 1) * LineSpace + BaseHeight;
				int inputwidth = (TextRenderer.MeasureText(tbInput.Text, tbInput.Font)).Width;
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
				btAdd.Image = global::Properties.Resources.plus;
				tbInput.Visible = false;
				DestPickTop = N * LineSpace + BaseButtonTop;
				DestHeight = N * LineSpace + BaseHeight;
				DestWidth = maxwidth + BaseWidth;
			}

			if (DestPickTop != btAdd.Top)
			{
				double grow = DestPickTop - btAdd.Top;
				grow = grow * 0.1;
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
				grow = grow * 0.1;
				if (Math.Abs(grow) < 1)
				{
					grow = Math.Sign(grow);
				}
				this.Height += (int)grow;
			}
			if (DestWidth < MinWidth)
				DestWidth = MinWidth;
			if (DestWidth != this.Width)
			{
				double grow = DestWidth - this.Width;
				grow = grow * 0.1;
				if (Math.Abs(grow) < 1)
				{
					grow = Math.Sign(grow);
				}
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
				Color fc = lbM.ForeColor;
				Color bc = lbM.BackColor;
				int nr, ng, nb;
				nr = (int)(fc.R + (bc.R - fc.R) * 0.5);
				ng = (int)(fc.G + (bc.G - fc.G) * 0.5);
				nb = (int)(fc.B + (bc.B - fc.B) * 0.5);
				lbM.ForeColor = Color.FromArgb(nr, ng, nb);
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
					Fading = 3;
					tiFadetick = 0;
				}
			}
			else if (Fading == 3)
			{
				tiFadetick++;
				Color fc = lbM.ForeColor;
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
				lbM.ForeColor = Color.FromArgb((int)(fc.R - dr), (int)(fc.G - dg), (int)(fc.B - db));
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
				btPick.Image = global::Properties.Resources.dice;
				btAdd.Visible = true;
				tiWait.Enabled = false;
				return;
			}
			waiticon++;
			if (waiticon == 3)
				waiticon = 0;
			if (waiticon == 0)
				btPick.Image = global::Properties.Resources.wait1;
			else if (waiticon == 1)
				btPick.Image = global::Properties.Resources.wait2;
			else if (waiticon == 2)
				btPick.Image = global::Properties.Resources.wait3;
		}

		private void ThreadGet()
		{
			Result = null;
			Response R = ROClient.GenerateIntegers(1, 0, Items.Count - 1);
			int ri = R.Integers[0];
			Result = Items[ri];
		}

		private void btPick_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (Fading < 4)
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
