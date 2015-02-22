﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrorHandlerEngine.ModelObjecting;
//using ErrorLogAnalyzer;

namespace TestErrorHandlerBySelf
{
    public partial class Form1 : Form
    {
        private List<Action> Exps;

        public Form1()
        {
            InitializeComponent();

            Exps = new List<Action>
            {
                () => { int a = 0, b = 10, c = b/a; },
                () => { throw new ArithmeticException(); },
                () => { throw new Exception("TEst"); },
                () => { throw new InvalidExpressionException(); },
                () => { throw new ApplicationException(); },
                () => { throw new SystemException(); }
            };

            dataGridView1.CreateColumns(typeof(LazyError));
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            // End the program.
            Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //int index = dataGridView1.CurrentRow != null ? dataGridView1.CurrentRow.Index : 0;
            //if (index < errors.Count && index >= 0)
            //    pictureBox1.Image = errors[index].Snapshot.Value;
        }

        private void btnTestHandledFirstExp_Click(object sender, EventArgs e)
        {
            foreach (var item in Exps)
            {
                try
                {
                    item();
                }
                catch (Exception ex)
                {
                    ex.Data.Add("test1", "1");
                    ex.Data.Add("test2", new Random().Next());
                }
            }
        }

        private void btnTestUnHandledUIExp_Click(object sender, EventArgs e)
        {
            Program.TestUiException();
        }

        private void btnTestUnHandledThreadExp_Click(object sender, EventArgs e)
        {
            int a = 0, b = 10, c = b / a;
        }

        private void btnTestUnhandledTaskExp_Click(object sender, EventArgs e)
        {
            Task.Run(() => { int a = 0, b = 10, c = b / a; });
        }

        private void btnRefreshGridView_Click(object sender, EventArgs e)
        {
           //ErrorLogReader.ReadAsync();
        }
    }
}