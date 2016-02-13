using Autonomous_Downloader.Autonomous_x;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Autonomous_Downloader
{
    public class CommandTemplate
    {
        public String CommandName { get; set; }
        public int NumberOfParameters { get; set; }

        public CommandTemplate(String name, int numParameters)
        {
            CommandName = name;
            NumberOfParameters = numParameters;
        }
    }

    /// <summary>
    /// Interaction logic for ProgramPanel.xaml
    /// </summary>
    public partial class ProgramPanel : UserControl
    {
        private CommandTemplate[] CommandSet = new CommandTemplate[]
        {
            new CommandTemplate("CollectorIn", 0),
            new CommandTemplate("CollectorOut", 0),

            new CommandTemplate("DriveForward", 2),
            new CommandTemplate("DriveBackwards", 2),
            new CommandTemplate("TurnLeft", 3),
            new CommandTemplate("TurnRight", 3),

            new CommandTemplate("Finished", 0)
        };

        public String ProgramNameLabel
        {
            get { return (String)ProgramNameLbl.Content; }
            set { ProgramNameLbl.Content = value; }
        }

        public ObservableCollection<Autonomous_x.Command> Commands
        {
            get
            {
                return (ObservableCollection<Autonomous_x.Command>)ProgramCommandsLB.ItemsSource;
            }
            set
            {
                ProgramCommandsLB.ItemsSource = value;
                if (!ProgramCommandsLB.Items.IsEmpty)
                {
                    ProgramCommandsLB.SelectedIndex = 0;
                }
            }
        }

        public ObservableCollection<String> Parameters
        {
            get
            {
                return (ObservableCollection<String>)ProgramParametersLB.ItemsSource;
            }

            set
            {
                ProgramParametersLB.ItemsSource = value;
            }
        }

        public Command SelectedCommand
        {
            get
            {
                if (ProgramCommandsLB.SelectedItem != null)
                {
                    return (Command)ProgramCommandsLB.SelectedItem;
                }
                else
                {
                    return null;
                }
            }
        }

        public ProgramPanel()
        {
            InitializeComponent();

            InitializeCommandsList();
        }

        private void InitializeCommandsList()
        {
            CommandTemplateLB.ItemsSource = CommandSet;
            CommandTemplateLB.SelectedIndex = 0;
        }

        private void ProgramCommandsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Command selectedCommand = SelectedCommand;
            if (selectedCommand != null)
            {
                ProgramParametersLB.ItemsSource = selectedCommand.Parameters;
            }
            else
            {
                ProgramParametersLB.ItemsSource = null;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CommandTemplateLB.SelectedItem != null)
            {
                CommandTemplate item = (CommandTemplate)CommandTemplateLB.SelectedItem;
                if (item != null)
                {
                    if (ProgramCommandsLB.SelectedItem != null)
                    {
                        int index = ProgramCommandsLB.SelectedIndex + 1;
                        Commands.Insert(index, new Command(item.CommandName, item.NumberOfParameters));
                        ProgramCommandsLB.SelectedIndex = index;
                    }
                    else
                    {
                        Commands.Add(new Command(item.CommandName, item.NumberOfParameters));
                        ProgramCommandsLB.SelectedIndex = ProgramCommandsLB.Items.Count - 1;
                    }
                }
            }
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProgramCommandsLB.SelectedIndex >= 0)
            {
                int index = ProgramCommandsLB.SelectedIndex;
                Commands.RemoveAt(index);
                if (index < ProgramCommandsLB.Items.Count)
                {
                    ProgramCommandsLB.SelectedIndex = index;
                }
                else if (ProgramCommandsLB.Items.Count > 0)
                {
                    ProgramCommandsLB.SelectedIndex = ProgramCommandsLB.Items.Count - 1;
                }
            }
        }

        private void DownBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((ProgramCommandsLB.SelectedIndex >= 0) && (ProgramCommandsLB.SelectedIndex < ProgramCommandsLB.Items.Count - 1))
            {
                int index = ProgramCommandsLB.SelectedIndex;
                Command item = Commands[index];
                Commands.RemoveAt(index);
                Commands.Insert(index + 1, item);
                ProgramCommandsLB.SelectedIndex = index + 1;
            }
        }

        private void UpBtn_Click(object sender, RoutedEventArgs e)
        {
            // if the index is set and greater than the first element
            // move it up
            if (ProgramCommandsLB.SelectedIndex > 0)
            {
                int index = ProgramCommandsLB.SelectedIndex;
                Command item = Commands[index];
                Commands.RemoveAt(index);
                Commands.Insert(index - 1, item);
                ProgramCommandsLB.SelectedIndex = index - 1;
            }
        }

        private void ParameterEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                TextBox box = (TextBox)sender;
                e.Handled = true;

                TextBlock block = (TextBlock)box.Tag;
                box.Visibility = System.Windows.Visibility.Collapsed;
                block.Visibility = System.Windows.Visibility.Visible;

                block.Text = box.Text;

                int selectedIndex = ProgramParametersLB.SelectedIndex;

                try
                {
                    //C Parameters[selectedIndex] = Convert.ToInt32(box.Text);
                    Parameters[selectedIndex] = box.Text;
                }
                catch (Exception ex)
                {
                    /*TODO do something with the error */
                }
            }
        }

        private void ParameterTB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TextBlock block = (TextBlock)sender;
                Panel owner = (Panel)block.Parent;
                TextBox box = (TextBox)block.Tag;

                block.Visibility = System.Windows.Visibility.Collapsed;
                box.Visibility = System.Windows.Visibility.Visible;
                box.Text = block.Text;
                box.CaretIndex = box.Text.Length;
                Keyboard.Focus(box);
            }
        }

        private void CommandTemplateLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CommandTemplateLB.SelectedItem != null)
            {
                CommandTemplate item = (CommandTemplate)CommandTemplateLB.SelectedItem;
                if (item != null)
                {
                    if (ProgramCommandsLB.SelectedItem != null)
                    {
                        int index = ProgramCommandsLB.SelectedIndex + 1;
                        Commands.Insert(index, new Command(item.CommandName, item.NumberOfParameters));
                        ProgramCommandsLB.SelectedIndex = index;
                    }
                    else
                    {
                        Commands.Add(new Command(item.CommandName, item.NumberOfParameters));
                        ProgramCommandsLB.SelectedIndex = ProgramCommandsLB.Items.Count - 1;
                    }   
                }
            }
        }

        private void ParameterEntry_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            int selectedIndex = ProgramParametersLB.SelectedIndex;
            if (selectedIndex >= 0)
            {
                TextBox box = (TextBox)sender;

                try
                {
                    //C Parameters[selectedIndex] = Convert.ToInt32(box.Text);
                    Parameters[selectedIndex] = box.Text;
                }
                catch (Exception ex)
                {
                    /*TODO do something with the error */
                }
            }
        }
    }
}
