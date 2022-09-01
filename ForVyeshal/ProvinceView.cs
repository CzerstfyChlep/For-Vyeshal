using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForVyeshal
{
    public partial class ProvinceView : Form
    {
        public ProvinceView()
        {
            InitializeComponent();
            this.GotFocus += Refresh;
            this.FormClosing += CloseEvent;
        }
        public void CloseEvent (object sender, EventArgs e)
        {
            Form1.ProvinceViewOpen = false;
            Form1.CreateNewPv();
        }
        public void Update()
        {
            Refresh(this, new EventArgs());
        }
        public void Refresh(object sender, EventArgs e)
        {
            if(Form1.Clicked != -1)
            {
                DevelopmentLabel.Text = "Development: " + Form1.Provinces[Form1.Clicked].Develompent;
                PopulationLabel.Text = "Population: " + Form1.Provinces[Form1.Clicked].Population;
                FarmsLabel.Text = "Farms: " + Form1.Provinces[Form1.Clicked].Farms;
                FactoriesLabel.Text = "Factories: " + Form1.Provinces[Form1.Clicked].Factories;
                TerrainLabel.Text = "Terrain: " + Form1.Provinces[Form1.Clicked].Terrain;
                int maxPopularity = Form1.Provinces[Form1.Clicked].PopularityChart[0] + Form1.Provinces[Form1.Clicked].PopularityChart[1] + Form1.Provinces[Form1.Clicked].PopularityChart[2] + Form1.Provinces[Form1.Clicked].PopularityChart[3];
                double[] PopularityArray = new double[4];
                if (maxPopularity != 0)
                    PopularityArray = new double[] { Form1.Provinces[Form1.Clicked].PopularityChart[0] / maxPopularity, Form1.Provinces[Form1.Clicked].PopularityChart[1] / maxPopularity, Form1.Provinces[Form1.Clicked].PopularityChart[2] / maxPopularity, Form1.Provinces[Form1.Clicked].PopularityChart[3] / maxPopularity };
                PopularityBox.Text = "";
                PopularityBox.SelectionColor = Color.DarkGreen;
                for (int a = 0; a < PopularityArray[0] * 30; a++)
                {
                    PopularityBox.AppendText("█");
                }
                PopularityBox.SelectionColor = Color.DarkBlue;
                for (int a = 0; a < PopularityArray[1] * 30; a++)
                {
                    PopularityBox.AppendText("█");
                }
                PopularityBox.SelectionColor = Color.Yellow;
                for (int a = 0; a < PopularityArray[2] * 30; a++)
                {
                    PopularityBox.AppendText("█");
                }
                PopularityBox.SelectionColor = Color.Red;
                for (int a = 0; a < PopularityArray[3] * 30; a++)
                {
                    PopularityBox.AppendText("█");
                }

            }
        }
    }
}
