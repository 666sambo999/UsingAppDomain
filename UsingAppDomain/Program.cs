using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace UsingAppDomain
{
    internal static class Program
    {
        static AppDomain drawerDomain;
        static AppDomain textWinDomain;

        static Assembly drawerASM;
        static Assembly textWinASM;

        static Form DrawerWindowsForm;
        static Form TextWindowsForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [LoaderOptimization(LoaderOptimization.MultiDomain)]
        static void Main()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            drawerDomain = AppDomain.CreateDomain("Drawer");
            textWinDomain = AppDomain.CreateDomain("TextWindows");

            drawerASM = drawerDomain.Load(AssemblyName.GetAssemblyName("TextDrawer.exe"));
            textWinASM = textWinDomain.Load(AssemblyName.GetAssemblyName("TextWindows.exe"));

            DrawerWindowsForm = Activator.CreateInstance(drawerASM.GetType("TextDrawer.MainForm")) as Form;
            TextWindowsForm = Activator.CreateInstance(textWinASM.GetType("TextWindows.MainForm"), 
                new object[] { drawerASM.GetModule("TextDrawer.exe"),DrawerWindowsForm}) as Form;
            (new Thread(new ThreadStart(RunVisual))).Start();
            (new Thread(new ThreadStart(RunDrawer))).Start();

            drawerDomain.DomainUnload += new EventHandler(drawer_Domain);
        }
        static void drawer_Domain(object sender, EventArgs e) 
        {
            MessageBox.Show($"Domain{(sender as AppDomain).FriendlyName} was unloade.");
        }   
        static void RunDrawer()
        {
            DrawerWindowsForm.ShowDialog();
            AppDomain.Unload(drawerDomain);
        }
        static void RunVisual()
        {
            TextWindowsForm.ShowDialog();
            Application.Exit();
        }
    }
}
