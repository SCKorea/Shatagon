using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;
using NSW.StarCitizen.Tools.Lib.Global;
using SCTool_Redesigned.Windows;

namespace SCTool_Redesigned.Pages
{
    /// <summary>
    /// selectDir.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class selectDir : Page
    {
        private const string Defaultdir = "C:\\Program Files\\Roberts Space Industries\\StarCitizen";
        public selectDir()
        {
            InitializeComponent();
            verify_Path(Defaultdir);

            if (App.Settings.GameFolder != null)
            {
                PhasePath.Content = App.Settings.GameFolder;
            }
        }

        private bool verify_Path(string directoryPath)
        {
            App.Logger.Info("Check game folder path");
            var gamePath = GameFolders.SearchGameFolder(directoryPath);

            if (gamePath != null)
            {
                var gameModes = GameFolders.GetGameModes(gamePath);

                foreach (var gameMode in gameModes)
                {
                    App.Settings.GameFolder = gamePath;
                    App.SaveAppSettings();

                    PhasePath.Content = gamePath;

                    MainWindow.UI.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        MainWindow.UI.NextBtn.Visibility = Visibility.Visible;
                    }));

                    App.Logger.Info("Valid game folder path");
                    return true;
                }
            }

            App.Logger.Info("Invalid game folder path");
            return false;
        }

        private void dialogBtn_Click(object sender, RoutedEventArgs e)
        {
            var flag = true;

            while (flag)
            {
                CommonOpenFileDialog dialog = OpenDialog();
                App.Logger.Info("Open game folder selection dialog");

                var result = dialog.ShowDialog();

                if (result == CommonFileDialogResult.Ok)
                {
                    App.Logger.Info("Close game folder selection dialog");

                    var directoryPath = dialog.FileName;

                    if (flag = !verify_Path(directoryPath))
                    {
                        MessageBox.Show(Properties.Resources.MSG_Decs_NotGameFolder, Properties.Resources.MSG_Title_NotGameFolder);
                        App.Logger.Warn("Not a game folder.");
                    }
                }
                else
                {
                    App.Logger.Info("Close game folder selection dialog");
                    break;
                }
            }
        }

        private CommonOpenFileDialog OpenDialog()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = Properties.Resources.MSG_Title_SelectStarCitizenDirectory,
                InitialDirectory = @"C:\Program Files\Roberts Space Industries"
            };

            return dialog;
        }
    }
}
