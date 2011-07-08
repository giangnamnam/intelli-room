using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace GUI
{
    public partial class GUIIntelliRoom : Form
    {
        CommandInterpreter commandInterpreter;

        public GUIIntelliRoom()
        {
            InitializeComponent();
            //Ejecuto el comando inicial
            commandInterpreter = new CommandInterpreter();
            IntelliRoom.Command.Init();
        }

        private void execute_Click(object sender, EventArgs e)
        {
            String command = commandBox.Text;
            commandBox.Text = "";
            //ejecutamos el comando

            var taskCommand = Task.Factory.StartNew(() =>
            {
                return commandInterpreter.CommandsInterpreter(command);
            }).ContinueWith((res) =>
                {
                    string resCommand = res.Result;
                    //pintamos el resultad
                    historyList.Items.Insert(0, "<- " + command);
                    historyList.Items.Insert(0, "-> " + resCommand);
                } , TaskScheduler.FromCurrentSynchronizationContext());
                       
        }

        private void UpdateHelp(object sender, EventArgs e)
        {
            String commands = commandBox.Text;
            string[] command = commands.Split(new char[] { '|' });
            List<string> result = commandInterpreter.SearchCommands(command[command.Length-1]);

            helpList.Items.Clear();
            helpList.Items.AddRange(result.ToArray());

            if (commandInterpreter.ExitsCommand(command[0]))
            {
                execute.Enabled = true;
            }
            else
            {
                execute.Enabled = false;
            }
        }

        private void GetHelpCommand(object sender, EventArgs e)
        {
            if (helpList.SelectedItem != null)
            {
                commandBox.Text = helpList.SelectedItem.ToString();
            }
        }

        private void UpdateInfoList(object sender, EventArgs e)
        {
            if (IntelliRoom.Command.GetMessages().Count != errorList.Items.Count)
            {
                errorList.Items.Clear();
                errorList.Items.AddRange(IntelliRoom.Command.GetMessages().ToArray());
            }
        }

        private void KeyPressCommand(object sender, KeyPressEventArgs e)
        {
            Char key = e.KeyChar;

            if (execute.Enabled==true && key.Equals('\r'))
            {
                execute_Click(null, null);
            }
        }
    }
}
