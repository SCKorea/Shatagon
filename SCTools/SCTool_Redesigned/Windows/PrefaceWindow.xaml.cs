using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SCTool_Redesigned.Windows
{
    /// <summary>
    /// preface.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PrefaceWindow : Window
    {
        public static bool IsClosed = false;

        public PrefaceWindow()
        {
            InitializeComponent();

            Closed += PrefaceWindow_Closed;
        }

        private void PrefaceWindow_Closed(object sender, EventArgs e)
        {
            if (!MainWindow.UI.DoNotCloseMainWindow)
            {
                MainWindow.UI.Quit();
            }
        }
    }
}
