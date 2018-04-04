using ServiceBoilerplate.BLL.Syncs.Base;
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

namespace ServiceBoilerplate.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            Type syncInterface = typeof(ISync);
            foreach (Type type in types)
            {
                if (type.Name.EndsWith("Sync"))
                {
                    if (type.GetInterfaces().Contains(syncInterface))
                    {
                        CodeComboBox.Items.Add(type.Name);
                    }
                }
            }
        }

        private void BeginTimeLabel_Click(object sender, EventArgs e)
        {

        }

        private void EndTimeLabel_Click(object sender, EventArgs e)
        {

        }

        private void EndTimeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BeginTimeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CodeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
