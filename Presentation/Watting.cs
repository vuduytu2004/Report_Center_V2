using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Report_Center.Presentation
{
    public partial class F003_Splash : Form
    {
        private F003_Splash()
        {
            InitializeComponent();
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.Transparent;
            //this.TransparencyKey = Color.Transparent; // I had to add this to get it to work.
            this.TransparencyKey = SystemColors.Control;
        }

        // Thredding.
        private static Thread _splashThread;
        private static F003_Splash _splashForm;


        // Show the Splash Screen (Loading...)      
        public static void ShowSplash()
        {
            if (_splashThread == null)
            {
                // show the form in a new thread            
                _splashThread = new Thread(new ThreadStart(DoShowSplash));
                _splashThread.IsBackground = true;
                _splashThread.Start();
            }
        }

        // Called by the thread    
        private static void DoShowSplash()
        {
            if (_splashForm == null)
            {
                _splashForm = new F003_Splash();
                //_splashForm.MdiParent = me;
                _splashForm.StartPosition = FormStartPosition.CenterScreen;
                //_splashForm.StartPosition = FormStartPosition.CenterParent;
                //_splashForm.MdiParent = "Main";
                _splashForm.TopMost = true;
            }
            // create a new message pump on this thread (started from ShowSplash)        
            Application.Run(_splashForm);
        }

        // Close the splash (Loading...) screen    
        public static void CloseSplash()
        {
            // Need to call on the thread that launched this splash        
            if (_splashForm.InvokeRequired)
                _splashForm.Invoke(new MethodInvoker(CloseSplash));
            else
                Application.ExitThread();
        }

        private void F003_Splash_Load(object sender, EventArgs e)
        {

        }
    }
}
