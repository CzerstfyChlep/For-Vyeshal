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
    public partial class SoldierMoveMenu : Form
    {
        public Dictionary<CheckBox, Soldier> Ls = new Dictionary<CheckBox, Soldier>();
        public SoldierMoveMenu(Province p)
        {
            InitializeComponent();
           
            foreach (Soldier S in p.Soldiers)
            {
                Panel P = new Panel();
                Panel.Controls.Add(P);
                P.Size = new Size(340, 30);
                CheckBox Check = new CheckBox();
                Check.Location = new Point(3, 8);
                Check.Size = new Size(15, 14);
                Check.Enabled = false;
                if (S.MovePoints > 0)
                {
                    Check.Checked = true;
                    Check.Enabled = true;
                }
                Ls.Add(Check, S);
                P.Controls.Add(Check);              
                ProgressBar HPBar = new ProgressBar();
                P.Controls.Add(HPBar);
                HPBar.Size = new Size(100, 28);
                HPBar.Location = new Point(25, 1);
                HPBar.Maximum = 100;
                HPBar.Value = (int)(S.Strength / S.MaxStrength * 100);
                ToolTip TP = new ToolTip();              
                TP.SetToolTip(HPBar, ((int)(S.Strength / S.MaxStrength * 100)).ToString() + "%");
                Label Info = new Label();
                Info.Location = new Point(131, 1);
                Info.Size = new Size(210, 28);
                Info.TextAlign = ContentAlignment.TopLeft;
                Info.Text = $"Stregth: {S.Strength}/{S.MaxStrength}     Move points: {S.MovePoints}/{S.MaxMovePoints}\nFire: {S.Fire}    Armor: {S.Armor}";
                P.Controls.Add(Info);
            }

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }
        public List<Soldier> Out = new List<Soldier>();
        private void Move_Click(object sender, EventArgs e)
        {
            foreach(CheckBox Check in Ls.Keys)
            {
                if (Check.Checked == true)
                {
                    Out.Add(Ls[Check]);
                }
            }
            this.Close();
        }
    }
}
