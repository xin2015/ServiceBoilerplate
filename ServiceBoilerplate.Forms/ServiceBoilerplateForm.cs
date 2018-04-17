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
    public partial class ServiceBoilerplateForm : Form
    {
        public ServiceBoilerplateForm()
        {
            InitializeComponent();
            Type syncInterface = typeof(ISync);
            Type[] types = Assembly.GetAssembly(syncInterface).GetTypes();
            foreach (Type type in types)
            {
                if (type.Name.EndsWith("Sync") && type.GetInterfaces().Contains(syncInterface))
                {
                    CodeComboBox.Items.Add(type.Name.Replace("Sync", string.Empty));
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
