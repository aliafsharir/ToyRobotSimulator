using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToyRobotSimulator;

namespace ToyRobotSimulatorUI
{
    public partial class frmToyRobot : Form
    {
        Robot _robot;
        Button[,] _table = new Button[5, 5];

        public frmToyRobot()
        {
            InitializeComponent();
        }

        private void frmToyRobot_Load(object sender, EventArgs e)
        {
            _robot = new Robot();
            InitTable();
            btnRunFile.Visible = false;
        }

        private void btnPlace_Click(object sender, EventArgs e)
        {
            _robot.Place((int)nudX.Value, (int)nudY.Value, cbFace.Text);
            RenderRobot();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            _robot.Move();
            RenderRobot();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            _robot.Left();
            RenderRobot();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            _robot.Right();
            RenderRobot();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            lblReport.Text = _robot.Report();
        }

        private void RenderRobot()
        {
            var locStr = _robot.Report().Split(',');

            if (locStr.Length != 3)//report should have 3 parts
                return;

            int x = Int32.Parse(locStr[0]);
            int y = Int32.Parse(locStr[1]);
            Direction face;
            Enum.TryParse(locStr[2].ToUpper(), out face);

            foreach (var b in _table)
                b.Text = "";

            var dirSymbol = string.Empty;

            switch (face)
            {
                case Direction.NORTH:
                    dirSymbol = "^";
                    break;

                case Direction.EAST:
                    dirSymbol = ">";
                    break;

                case Direction.SOUTH:
                    dirSymbol = "_";
                    break;

                case Direction.WEST:
                    dirSymbol = "<";
                    break;
            }

            _table[x, y].Text = dirSymbol;
        }

        private void InitTable()
        {
            for (int i = 4; i >= 0; i--)
            {
                for (int j = 4; j >= 0; j--)
                {
                    var b = new Button();
                    b.Size = new Size(40, 40);
                    b.Text = i + ":" + j;
                    b.Top = (-j * 40) + 250;
                    b.Left = (i * 40) + 10;
                    this.Controls.Add(b);
                    _table[i, j] = b;
                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            lblFileName.Text = openFileDialog1.FileName;
            btnRunFile.Visible = openFileDialog1.FileName.Contains(".txt");
        }

        private async void btnRunFile_Click(object sender, EventArgs e)
        {
            var lines = File.ReadLines(openFileDialog1.FileName);

            foreach (var line in lines)
            {
                var res = await RunCommand(line);

                if (!string.IsNullOrEmpty(res))
                    lblReport.Text = res;

                RenderRobot();
            }
        }

        private async Task<string> RunCommand(string command)
        {
            var res = _robot.CommandByText(command);
            await Task.Delay(650);
            return res;
        }
    }
}
