﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDrawer
{
    public partial class MainForm : Form
    {
        string text = "Hello";
        Font font;
        public Point Location {get; set;}
        public int Width { get; set;}
       
        public MainForm()
        {
            InitializeComponent();
            font = new Font("Arial", 40);
            panel.Paint += panel_Paint;
            this.Paint += MainForm_Paint;
        }
        
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            if (text.Length> 0) 
            { 
                Image image = new Bitmap(panel.ClientRectangle.Width, panel.ClientRectangle.Height);
                Graphics gDC = Graphics.FromImage(image);
                gDC.Clear(BackColor);
                gDC.DrawString(text, font, Brushes.DarkGray, ClientRectangle, new StringFormat(StringFormatFlags.NoFontFallback));
                e.Graphics.DrawImage(image,0,0);
            }
        }
        private void MainForm_Paint(object sender, PaintEventArgs e) 
        {
            panel_Paint(panel, new PaintEventArgs(panel.CreateGraphics(), panel.ClientRectangle));
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.Font = this.font;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.font = dialog.Font;
                panel_Paint(panel, new PaintEventArgs(panel.CreateGraphics(), panel.ClientRectangle));
            }
        }
        public void SetText(string text)
        { 
            this.text = text;
            panel_Paint(panel, new PaintEventArgs(panel.CreateGraphics(), panel.ClientRectangle));
        }
        public void Move(Point location, int width)
        {
            Location = location;
            Width = width;
        }

    }
}
