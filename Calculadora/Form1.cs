using System;
using System.Data;
using System.Windows.Forms;

namespace CalculadoraApp
{
    public partial class Form1 : Form
    {
        private string currentOperation = "";
        private double result = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Generar botones dinámicamente
            string[] buttons = { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", "C", "0", "=", "+", "←" };
            int row = 0, col = 0;

            foreach (string text in buttons)
            {
                Button btn = new Button
                {
                    Text = text,
                    Font = new System.Drawing.Font("Segoe UI", 10F),
                    Dock = DockStyle.Fill
                };
                btn.Click += Button_Click;
                tableLayoutPanelButtons.Controls.Add(btn, col, row);

                col++;
                if (col > 3)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            Button? btn = sender as Button;
            if (btn == null) return;

            if (btn.Text == "C") // Limpiar todo
            {
                txtDisplay.Text = "";
                currentOperation = "";
                result = 0;
            }
            else if (btn.Text == "=") // Calcular resultado
            {
                try
                {
                    double eval = Convert.ToDouble(new DataTable().Compute(currentOperation, ""));
                    result = eval;
                    txtDisplay.Text = eval.ToString();
                    listBoxHistory.Items.Add($"{currentOperation} = {eval}");
                    currentOperation = eval.ToString();
                }
                catch
                {
                    txtDisplay.Text = "Error";
                }
            }
            else if (btn.Text == "←") // Borrar último carácter
            {
                if (currentOperation.Length > 0)
                {
                    currentOperation = currentOperation.Substring(0, currentOperation.Length - 1);
                    txtDisplay.Text = currentOperation;
                }
            }
            else // Agregar texto del botón
            {
                currentOperation += btn.Text;
                txtDisplay.Text = currentOperation;
            }
        }
    }
}
