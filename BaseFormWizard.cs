using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farming
{
    public partial class BaseFormWizard : Form
    {
        public BaseFormWizard()
        {
            InitializeComponent();
            CustomDesigned();
        }

        private void CustomDesigned()
        {
            panelFarmingSubmenu.Visible = false;
            panelQuestSubmenu.Visible = false;
            panelSetupSubmenu.Visible = false;
        }

        private void HideSubmenu()
        {
            if (panelFarmingSubmenu.Visible == true)
            {
                panelFarmingSubmenu.Visible = false;
            }
            if (panelQuestSubmenu.Visible == true)
            {
                panelQuestSubmenu.Visible = false;
            }
            if (panelSetupSubmenu.Visible == true)
            {
                panelSetupSubmenu.Visible = false;
            }
        }

        private void ShowSubmenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubmenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void buttonFarming_Click(object sender, EventArgs e)
        {
            ShowSubmenu(panelFarmingSubmenu);
        }

        private void buttonQuest_Click(object sender, EventArgs e)
        {

            ShowSubmenu(panelQuestSubmenu);
        }

        private void buttonSetup_Click(object sender, EventArgs e)
        {

            ShowSubmenu(panelSetupSubmenu);
        }

        private Form formActive = null;
        private void OpenChildForm(Form formChild)
        {
            if (formActive != null)
            {
                formActive.Close();
            }

            formActive = formChild;

            formChild.TopLevel = false;
            formChild.FormBorderStyle = FormBorderStyle.None;
            formChild.Dock = DockStyle.Fill;

            panelChild.Controls.Add(formChild);
            panelChild.Tag = formChild;

            formChild.BringToFront();

            formChild.Show();
        }

        private string formActiveName = string.Empty;
        private void buttonFish_Click(object sender, EventArgs e)
        {
            if (!"fish".Equals(formActiveName))
            {
                formActiveName = "fish";
                OpenChildForm(new FormFish());
            }
        }

        private void BaseFormWizard_Load(object sender, EventArgs e)
        {

        }
    }
}
