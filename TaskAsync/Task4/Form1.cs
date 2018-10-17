using System;
using System.Windows.Forms;

namespace Task4
{
    public partial class Task4WinForm : Form
    {
        public Task4WinForm()
        {
            InitializeComponent();
        }

        private void IncrementCountButton_Click(object sender, EventArgs e)
        {
            int currentCountValue;
            Int32.TryParse(CountValueLabel.Text, out currentCountValue);
            CountValueLabel.Text = (++currentCountValue).ToString();
        }

        private async void userBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.userBindingSource.EndEdit();
            await this.tableAdapterManager.UpdateAll(this.database1DataSet);
        }

        private async void Task4WinForm_LoadAsync(object sender, EventArgs e)
        {
            await this.userTableAdapter.Fill(this.database1DataSet.User);
        }
    }
}
