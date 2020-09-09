using System;
using System.Windows.Forms;
using Autodesk.Revit.DB;

namespace LevelsAndAxesGridWithDimensions
{
    public partial class SetParametersForm : System.Windows.Forms.Form
    {
        public string verticalAxesNumber;
        public string verticalAxesDistance;

        public string horizontalAxesNumber;
        public string horizontalAxesDistance;
        
        public string levelsNumber;
        public string levelsDistance;

        public char decimalSeparator;

        public SetParametersForm(Units units)
        {
            InitializeComponent();

            unitsTB.Text = LabelUtils.GetLabelFor(
                units.GetFormatOptions(UnitType.UT_Length).DisplayUnits);

            if (units.DecimalSymbol == DecimalSymbol.Dot)
            {
                decimalSeparator = '.';
            }
            else
            {
                decimalSeparator = ',';
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            verticalAxesNumber = verticalAxesNumberTB.Text;
            verticalAxesDistance = verticalAxesDistanceTB.Text;

            horizontalAxesNumber = horizontalAxesNumberTB.Text;
            horizontalAxesDistance = horizontalAxesDistanceTB.Text;

            levelsNumber = levelsNumberTB.Text;
            levelsDistance = levelsDistanceTB.Text;

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void fields_Validated(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            if (tb.Text == String.Empty)
            {
                tb.Text = "0";
            }
        }
    }
}
