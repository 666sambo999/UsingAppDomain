using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextWindows
{
    
    public partial class Form1 : Form
    {
        Module Module { get; set; }
        object drawer;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(Module module, object targetWin) : this()
        {
            this.Module = module;
            this.drawer = targetWin;
        }
        private void textBox_Text(object sender, EventArgs e)
        {
            Module.GetType("TextDrawer.MainForm").GetMethod("SetText").Invoke(drawer, new object[] { textBox.Text });
        }
        private void MainForm_Location(object sender, EventArgs e)
        {
            Module.GetType("TextDrawer.MainForm").GetMethod("Move").Invoke(drawer, new object[] { new Point(this.Location.X, this.Location.Y + this.Height), this.Width });
        }
    }
}
