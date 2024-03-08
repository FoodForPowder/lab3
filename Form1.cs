using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation_Lab_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int A = 20;

        private void btStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView.ClearSelection();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int[] rules = new int[8];
            int rule = (int)edRule.Value;
            int[,] cells = new int[A, A];

            //перевод правила перехода в двоичную систему и запись в массив
            string str = Convert.ToString(rule, 2); 
            int c = Convert.ToInt32(str);
            for (int j = 7; j >= 0; j--)
            {
                rules[j] = c % 10;
                c = c / 10;
            }

            // задаем нулевую строку
            cells[0, 0] = 0; cells[0, 1] = 1; cells[0, 2] = 0; cells[0, 3] = 0; cells[0, 4] = 1; cells[0, 5] = 0; cells[0, 6] = 0; cells[0, 7] = 0; cells[0, 8] = 0; cells[0, 9] = 1; cells[0, 10] = 1;
            cells[0, 11] = 0; cells[0, 12] = 1; cells[0, 13] = 0; cells[0, 14] = 0; cells[0, 15] = 1; cells[0, 16] = 0; cells[0, 17] = 1; cells[0, 18] = 0; cells[0, 19] = 0;

            //алгоритм при "зацикливании" строки
            for (int i = 0; i < A - 1; i++) 
            {
                int xyz = cells[i, A - 1] * 100 + cells[i, 0] * 10 + cells[i, 1];
                string str1 = Convert.ToString(xyz);
                int xyz1 = Convert.ToInt32(str1, 2);

                //перевод по правилу перехода
                for (int l = 0; l < 8; l++) 
                {
                    if (l == xyz1) cells[i + 1, 0] = rules[l];
                }

                for (int j = 1; j < A - 1; j++)
                {
                    xyz = cells[i, j - 1] * 100 + cells[i, j] * 10 + cells[i, j + 1];
                    str1 = Convert.ToString(xyz);
                    xyz1 = Convert.ToInt32(str1, 2);

                    //перевод по правилу перехода
                    for (int l = 0; l < 8; l++) 
                    {
                        if (l == xyz1) cells[i + 1, j] = rules[l];
                    }
                }

                xyz = cells[i, A - 2] * 100 + cells[i, A - 1] * 10 + cells[i, 0];
                str1 = Convert.ToString(xyz);
                xyz1 = Convert.ToInt32(str1, 2);

                //перевод по правилу перехода
                for (int l = 0; l < 8; l++) 
                {
                    if (l == xyz1) cells[i + 1, A - 1] = rules[l];
                }
            }

            // Создаем ячейки 
            dataGridView.ColumnCount = A; 
            dataGridView.RowCount = A;

            for (int i = 0; i < A; i++)
            {
                // раскрашиваем 
                for (int j = 0; j < A; j++)
                {
                    if (cells[i, j] == 1) 
                        dataGridView.Rows[i].Cells[j].Style.BackColor = Color.Gray;
                }
            }
        }

        private void edRule_ValueChanged(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
        }
    }

}
