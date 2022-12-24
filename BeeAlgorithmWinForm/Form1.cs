using BeeAlgorithm;
using System.Text;

namespace BeeAlgorithmWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void countScoutBeesNumeric_ValueChanged(object sender, EventArgs e)
        {
            countLocalSearchNumeric.Maximum = countScoutBeesNumeric.Value;
        }

        private void countLocalSearchNumeric_ValueChanged(object sender, EventArgs e)
        {
            countElitePointsNumeric.Maximum = countLocalSearchNumeric.Value;
        }

        private void lowerBoundNumeric_ValueChanged(object sender, EventArgs e)
        {
            upperBoundNumeric.Minimum = lowerBoundNumeric.Value;
        }

        private void upperBoundNumeric_ValueChanged(object sender, EventArgs e)
        {
            lowerBoundNumeric.Maximum = upperBoundNumeric.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            upperBoundNumeric.Minimum = lowerBoundNumeric.Value;

            lowerBoundNumeric.Maximum = upperBoundNumeric.Value;

            customFunctionTextBox.Text = functionsListBox.Items[0].ToString();
        }

        private async void calculateButton_Click(object sender, EventArgs e)
        {
            calculateButton.Enabled = false;

            string expression = customFunctionTextBox.Text;

            BeeAlgorithm.BeeAlgorithm algorithm = new();

            var optimalArguments = await Task.Run(()
                => algorithm.GetResultArguments(
                    expression,
                    (int)countScoutBeesNumeric.Value,
                    (int)countEliteBeesNumeric.Value,
                    (int)countDefaultBeesNumeric.Value,
                    (int)countLocalSearchNumeric.Value,
                    (int)countElitePointsNumeric.Value,
                    (double)deltaNumeric.Value,
                    (int)countIterationsNumeric.Value,
                    (int)lowerBoundNumeric.Value,
                    (int)upperBoundNumeric.Value));

            var optimumFunc = algorithm.GetResult(optimalArguments);

            var argumentsNames = algorithm.GetNamesArguments();

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < argumentsNames.Length; i++)
            {
                stringBuilder.Append($"{argumentsNames[i]} = {optimalArguments[i]}\n");
            }

            stringBuilder.Append($"Значение функции: {optimumFunc}");

            MessageBox.Show(stringBuilder.ToString());

            calculateButton.Enabled = true;
        }

        private void functionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            customFunctionTextBox.Text = functionsListBox.SelectedItem.ToString();
        }
    }
}