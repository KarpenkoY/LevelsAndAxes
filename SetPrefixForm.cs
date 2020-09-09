using System;
using System.Windows.Forms;

namespace LevelsAndAxesGridWithDimensions
{
    public partial class SetPrefixForm : Form
    {
        public string prefix;

        public SetPrefixForm(string orientation)
        {
            InitializeComponent();

            groupBox.Text = $"Prefix for {orientation.ToLower()} axes name";
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            prefix = prefixTB.Text;

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
