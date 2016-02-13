using Autonomous_Downloader.Autonomous_x;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Autonomous_Downloader
{
    /// <summary>
    /// Interaction logic for AutonomousWindow.xaml
    /// </summary>
    public partial class AutonomousWindow : Window
    {
        private const String ProgramVersion = "v1.3";
        private ModeGroup mProgramModes = null;
        String mSaveFilename;

        private String SaveFilename
        {
            get
            {
                return mSaveFilename;
            }
            set
            {
                mSaveFilename = value;
                SetWindowTitle(mSaveFilename);
            }
        }

        private AutonomousMode SelectedProgram
        {
            get
            {
                if (ProgramModeLB.SelectedItem != null)
                {
                    return (AutonomousMode)ProgramModeLB.SelectedItem;
                }
                else
                {
                    return null;
                }
            }
        }

        private Command SelectedCommand
        {
            get
            {
                return ProgramPnl.SelectedCommand;
            }
        }

        public AutonomousWindow()
        {
            InitializeComponent();

            InitializeProgram();
            //LoadFile("amode238.txt");
        }

        private void InitializeProgram()
        {
            ModeGroup programModes = new ModeGroup();
            programModes.AutonomousModes.Add(new AutonomousMode("new"));
            SetWindowTitle("");
            AddNewMode(programModes);
        }

        private void SetWindowTitle(String filename)
        {
            String title = String.Format("{0}  Autonomous Downloader {1}", filename, ProgramVersion);
            Title = title;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Autonomous JSON File(*.txt)|*.txt|Autonomous JSON File (*.json)|*.json|All(*.*)|*.*";
            dlg.DefaultExt = ".txt";
            dlg.Title = "Open an Autonomous File";
            dlg.ShowDialog();

            if (!String.IsNullOrEmpty(dlg.FileName))
            {
                LoadFile(dlg.FileName);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ModeGroup programList = mProgramModes;
            if (programList != null)
            {
                if (String.IsNullOrEmpty(SaveFilename))
                {
                    SaveAsButton_Click(sender, e);
                }
                else
                {
                    SaveFile(SaveFilename);
                }
            }
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Autonomous JSON File(*.txt)|*.txt|Autonomous JSON File (*.json)|*.json|All(*.*)|*.*";
            dlg.DefaultExt = ".txt";
            dlg.Title = "Save an Autonomous File";
            dlg.ShowDialog();

            if (!String.IsNullOrEmpty(dlg.FileName))
            {
                SaveFile(dlg.FileName);
            }
        }

        private bool LoadFile(String filename)
        {
            bool retval = false;
            try
            {
                Autonomous_x.ModeGroup programList = null;
                programList = Autonomous_x.ModeGroup.Load(filename);

                if (programList != null)
                {
                    SaveFilename = filename;
                    AddNewMode(programList);
                }

                retval = true;
            }
            catch (Exception ex)
            {
                String msg = String.Format("Unable to load JSON file\n{0}", ex.Message);
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                retval = false;
            }

            return retval;
        }

        private void AddNewMode(Autonomous_x.ModeGroup programList)
        {
            mProgramModes = programList;
            ProgramModeLB.ItemsSource = mProgramModes.AutonomousModes;
            ProgramModeLB.SelectedIndex = 0;
        }

        private void SaveFile(String filename)
        {
            ModeGroup programList = mProgramModes;
            if (programList != null)
            {
                programList.Save(filename);
                SaveFilename = filename;
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ProgramModeLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AutonomousMode selectedMode = SelectedProgram;

            // display the new program mode in the right side panel
            if (selectedMode != null)
            {
                ProgramPnl.ProgramNameLabel = selectedMode.Name;
                ProgramPnl.Commands = selectedMode.Commands;
            }
        }

        private void DownBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((ProgramModeLB.SelectedIndex >= 0) && (ProgramModeLB.SelectedIndex < ProgramModeLB.Items.Count - 1))
            {
                int index = ProgramModeLB.SelectedIndex;
                AutonomousMode item = mProgramModes.AutonomousModes[index];
                mProgramModes.AutonomousModes.RemoveAt(index);
                mProgramModes.AutonomousModes.Insert(index + 1, item);
                ProgramModeLB.SelectedIndex = index + 1;
            }
        }

        private void UpBtn_Click(object sender, RoutedEventArgs e)
        {
            // if the index is set and greater than the first element
            // move it up
            if (ProgramModeLB.SelectedIndex > 0)
            {
                int index = ProgramModeLB.SelectedIndex;
                AutonomousMode item = mProgramModes.AutonomousModes[index];
                mProgramModes.AutonomousModes.RemoveAt(index);
                mProgramModes.AutonomousModes.Insert(index - 1, item);
                ProgramModeLB.SelectedIndex = index - 1;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AutonomousMode mode = new AutonomousMode("new");

            if ((ProgramModeLB.SelectedIndex >= 0) && (ProgramModeLB.SelectedIndex < ProgramModeLB.Items.Count))
            {
                mProgramModes.AutonomousModes.Insert(ProgramModeLB.SelectedIndex + 1, mode);
            }
            else
            {
                mProgramModes.AutonomousModes.Add(mode);
            }
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProgramModeLB.SelectedIndex >= 0)
            {
                mProgramModes.AutonomousModes.RemoveAt(ProgramModeLB.SelectedIndex);
            }
        }

        private void ProgramModeTB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {

                TextBlock displayText = (TextBlock)sender;
                displayText.Visibility = System.Windows.Visibility.Collapsed;

                TextBox editBox = (TextBox)displayText.Tag;
                editBox.Visibility = System.Windows.Visibility.Visible;
                editBox.Text = SelectedProgram.Name;
            }
        }

        private void ProgramModeEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            AutonomousMode mode = SelectedProgram;
            if (mode != null)
            {
                TextBox editBox = sender as TextBox;
                mode.Name = editBox.Text;
                ProgramPnl.ProgramNameLabel = mode.Name;
            }
        }

        private void ProgramModeEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                DropProgramModeEditor(sender);
                e.Handled = true;
            }
        }

        private void ProgramModeEntry_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            DropProgramModeEditor(sender);
        }

        private void DropProgramModeEditor(object sender)
        {
            TextBox editor = (TextBox)sender;
            editor.Visibility = System.Windows.Visibility.Collapsed;

            TextBlock display = (TextBlock)editor.Tag;
            display.Visibility = System.Windows.Visibility.Visible;
            display.Text = SelectedProgram.Name;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AutonomousWindow win = (AutonomousWindow)sender;
            double width = win.Width;
            double height = win.Height;

            double desktopWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double desktopHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            if (width > desktopWidth)
            {
                win.Width = desktopWidth * 0.80;
                win.Left = 20.0;
            }

            if (height > desktopHeight)
            {
                win.Height = desktopHeight * 0.90;
                win.Top = 20.0;
            }
        }
    }
}
