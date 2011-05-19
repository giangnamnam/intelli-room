using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUIIntelliRoom : Form
    {
        CommandInterpreter commInter;

        public GUIIntelliRoom()
        {
            InitializeComponent();
            //Ejecuto el comando inicial
            commInter = new CommandInterpreter();
            IntelliRoom.Command.Init();
        }

        private void execute_Click(object sender, EventArgs e)
        {
            String command = commandBox.Text;
            commandBox.Text = "";
            //añadimos el comando insertado
            historyList.Items.Add("Execute: "+command);
            //ejecutamos el comando
            String resCommand = commInter.Interpreter(command);
            //pintamos el resultad
            historyList.Items.Add("Response: " + resCommand);
        }

        private void UpdateHelp(object sender, EventArgs e)
        {
            String command = commandBox.Text;
            List<string> result = commInter.SearchHelp(command);

            helpList.Items.Clear();
            helpList.Items.AddRange(result.ToArray());
        }
    }
}
