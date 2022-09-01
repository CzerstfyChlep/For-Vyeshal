/*

    

    To do list:
        -Fighting
        -Buildings
        -Better units
                  
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;

namespace ForVyeshal
{
    public partial class Form1 : Form
    {
        
        public static string MapType;
        public static int pcount = 0;
        public static string CurrentMapType;
        public static Random random = new Random();
        public static Graphics g;
        public static bool Start = false;
        string BorderingText = "";
        public bool UpdateArmies = false;
        public bool ShowRailroads = false;
        public bool RailroadsOn = false;
        public static bool ProvinceViewOpen = false;
        int CurrentHostProvince = -1;
        public static int idcount = 0;
        public static int Clicked = 0;
        public static Font font;
        public static ProvinceView pv;
        public static List<Province> Provinces = new List<Province>();
        public static List<Province> ProvincesRev = new List<Province>();
        public static List<Building> Buildings = new List<Building>();
        public Form1()
        {          
            InitializeComponent();

            pv = new ProvinceView();

            //Very important options
            DoubleBuffered = true;
            CheckForIllegalCrossThreadCalls = false;


            //Font for the map
            FontFamily fontFamily = new FontFamily("Arial");
            font = new Font(fontFamily, 9, FontStyle.Regular, GraphicsUnit.Pixel);


            //Clicking event
            MouseDown += FClick;


            //Graphics
            g = CreateGraphics();


            //This part is responsible for creating provinces. The VyeshalStartup file contains all information about every province.
            string[] text = File.ReadAllLines("VyeshalStartup.txt");
            foreach(string t in text)
            {
                
                    string[] splited = t.Split('$');
                    Province p = new Province(splited[0], splited[1], splited[2], splited[3], splited[4], splited[5], splited[6], splited[7]);    

            }
        

            //Tells the game which provinces border other provinces. It uses a textfile named bordering.txt. By editing this textfile you can make provinces border eachother even if they aren't.
            string text2 = File.ReadAllText("bordering.txt");
            string[] Txt = text2.Split('#');
            foreach(string te in Txt)
            {
                string[] t = te.Split('$');
                foreach (string tt in t)
                {
                    if(Array.IndexOf(t.ToArray(), tt) != 0)
                    SearchByID(int.Parse(t[0])).Bordering.Add(int.Parse(tt));
                }
                
            }

            //Invalidating the form basicly updates all the graphics.
            this.Invalidate();
           

            //Converting themap.png into table of pixels with help of the LockBitmap
            Color[,] Table = new Color[640, 400];
            LockBitmap LB = new LockBitmap(Properties.Resources.themap);
            LB.LockBits();
            for (int x = 0; x < 640; x++)
            {
                for (int y = 0; y < 400; y++)
                {
                    Table[x, y] = LB.GetPixel(x, y);
                }
            }
            LB.UnlockBits();


            //Creating the province clicable area. Program uses the table from previous loop to create areas, pixel by pixel.
            Province Last = Provinces[0];
            for (int x = 0; x < 640; x++)
            {
                for (int y = 0; y < 400; y++)
                {
                    Color c = Table[x, y];
                    if(c == Color.White || c == Color.Black)
                    {
                        continue;
                    }
                    if(c == Last.Color)
                    {
                        Last.R.Union(new Rectangle(x, y, 1, 1));
                        continue;
                    }
                    foreach(Province p in Provinces)
                    {
                        if(p.Color == c)
                        {
                            Last = p;
                            p.R.Union(new Rectangle(x, y, 1, 1));
                            break;
                        }
                        
                    }
                }

            }


            //Removing the one pixel from all of the provinces.
            foreach (Province p in Provinces)
            {
                p.R.Exclude(new RectangleF(0, 0, 1, 1));
            }
           


            //Reverse province list to speed up the searching process.
            ProvincesRev = Provinces.ToList();
            ProvincesRev.Reverse();



            //For tests, delete later 
            Provinces[60].Soldiers.Add(new Soldier("KSR", 1));
            Provinces[60].Soldiers.Add(new Soldier("KSR", 2));
            //For tests, delete later 


            //The only usable railroads in the whole of Vyeshal. Most of them are located inside the Civilised teritory.
            Provinces[87].Railroad[Provinces[91].ID] = 2;
            Provinces[91].Railroad[Provinces[93].ID] = 2;
            Provinces[93].Railroad[Provinces[95].ID] = 2;
            Provinces[95].Railroad[Provinces[94].ID] = 1;
            Provinces[94].Railroad[Provinces[79].ID] = 2;
            //Don't add any more of them, encourage players to build railroads themselfes.
            



            /*
            Making Buildings
            */
            Building Mine = new Building();
            Mine.Name = "Mine";
            Mine.ReqTerrain = "Peak,Mountains,Hills";
            Mine.ProvinceBonus = true;
            Mine.Type = "FactoriesOutput:X,GlobalResourceIncome:X";
            //Mines can be build only in high terrain. They give some Factory bonus and flat resource income



            /*
            Resources
            
                Iron - can be turned into steel. Used for buildings and producing weapons.
                Food - main resource of the game. Used to recruit soldiers, also used for maintaining population.
                Coal - ???
                Wood - ???
                Rare Metals - ???
                Steel - used for making weapons, railroads and special buildings.



            Events:


            Units:

                Each unit has these stats:
                    -Strength:
                        HP of the unit. Says how much damage unit can take before retreating/disbanding. 
                    -Fire: (how much damage this unit deals)
                    -Armor
                    -Move points:
                    -Terrain bonus (in development)
                    -Faction restriction (in development)
                    -Cost
                    


            Unit types:
                
                Infantry - normal unit 

                

            */

        }


        //This method searches the ProvinceList and returns a Province of specified ID
        public Province SearchByID(int id)
        {
            IEnumerable<Province> q = from p in Provinces
                                      where p.ID == id
                                      select p;
            return q.First();
        }       


        //This part is responsible for handling player interaction with the map
        public void FClick(object sender, MouseEventArgs e)
        {

            if (e.X < 640 && e.Y < 400)
            {
               
                    UpdateArmies = true;
                    Province p;
                    if (e.X < 280)
                        p = Find(new Rectangle(e.X, e.Y, 1, 1));
                    else
                        p = Find2(new Rectangle(e.X, e.Y, 1, 1));
                if (e.Button == MouseButtons.Left)
                {
                    if (MapType == "BOR")
                    {
                        if (p != null)
                        {
                            CurrentHostProvince = p.ID;
                            ProvinceOverview.Text = p.ID + "";
                        }
                    }
                    else
                    {
                        int id = -1;
                        if (p != null)
                            id = p.ID;

                        if (Clicked != id)
                        {
                            Clicked = id;
                            if (Clicked != -1)
                            {
                                pv.Update();
                                PopulationLabel.Text = "Population: " + Provinces[Clicked].Population;
                                DevelopmentLabel.Text = "Development: " + Provinces[Clicked].Develompent;
                                FarmsLabel.Text = "Farms: " + Provinces[Clicked].Farms;
                                FactoriesLabel.Text = "Factories: " + Provinces[Clicked].Factories;
                                Panel.Controls.Clear();
                                if (Provinces[Clicked].Soldiers.Any())
                                {
                                    foreach (Soldier S in p.Soldiers)
                                    {
                                        Panel P = new Panel();
                                        Panel.Controls.Add(P);
                                        P.Size = new Size(190, 30);                                                                        
                                        ProgressBar HPBar = new ProgressBar();
                                        P.Controls.Add(HPBar);
                                        HPBar.Size = new Size(50, 28);
                                        HPBar.Location = new Point(5, 1);
                                        HPBar.Maximum = 100;
                                        HPBar.Value = (int)(S.Strength / S.MaxStrength * 100);
                                        ToolTip TP = new ToolTip();
                                        TP.SetToolTip(HPBar, ((int)(S.Strength / S.MaxStrength * 100)).ToString() + "%");
                                        Label Info = new Label();
                                        Info.Location = new Point(60, 1);
                                        Info.Size = new Size(130, 28);
                                        Info.TextAlign = ContentAlignment.TopLeft;
                                        Info.Text = $"Stregth: {S.Strength}/{S.MaxStrength}  MP: {S.MovePoints}/{S.MaxMovePoints}\nFire: {S.Fire}    Armor: {S.Armor}";
                                        P.Controls.Add(Info);
                                    }
                                }
                            }
                        }
                        else
                            Clicked = -1;

                        if (Clicked >= 0)
                        {
                            ProvinceOverview.Text = "" + Provinces[Clicked].Name;
                        }

                    }
                    MapType = CurrentMapType;
                    if (RailroadsOn)
                        ShowRailroads = true;
                    Invalidate();

                }
                else if (e.Button == MouseButtons.Right)
                {
                    if(p!= null && Clicked != -1)
                    {
                        if (Provinces[Clicked].Soldiers.Any() && p.Bordering.Contains(Clicked))
                        {
                            List<Soldier> Move = new List<Soldier>();
                            if (Provinces[Clicked].Soldiers.Count == 1)
                            {
                                Provinces[Clicked].Soldiers[0].MovePoints--;
                                Move.AddRange(Provinces[Clicked].Soldiers);
                                Provinces[Clicked].Soldiers.Clear();                               
                            }
                            else
                            {                             
                                SoldierMoveMenu SMM = new SoldierMoveMenu(Provinces[Clicked]);
                                SMM.ShowDialog();
                                Move = SMM.Out.ToList();
                                foreach(Soldier s in Move)
                                {
                                    s.MovePoints--;
                                    Provinces[Clicked].Soldiers.Remove(s);
                                }
                            }
                    
                                                                      
                            p.Soldiers.AddRange(Move);
                            Clicked = -1;
                            UpdateArmies = true;
                            MapType = CurrentMapType;   
                            Invalidate();
                        }
                    }
                   
                    
                }
            }
            else
            {
                if (MapType == "BOR")
                {
                    CurrentHostProvince = -1;
                }
            }
            
           
        }

        //This method searches for province in the list that player clicked on the map using the areas that were created on the start of the game
        Province Find(Rectangle r)
        {
            IEnumerable<Province> q = from p in Provinces
                                      where Contains(p.R, r, g)
                                      select p;
            if (q.Any())
                return q.First();
            else
                return null;
        }
        //The same as above, but the searching process starts at the end of list. It cuts the time needed for clicking provinces that ID's are above 50
        Province Find2(Rectangle r)
        {
            IEnumerable<Province> q = from p in ProvincesRev
                                      where Contains(p.R, r, g)
                                      select p;
            if (q.Any())
                return q.First();
            else
                return null;
        }

        //Basic Contains method for area's
        bool Contains(Region r, RectangleF r1, Graphics g)
        {
            Region u = r.Clone();
            u.Union(r1);
            return r.Equals(u, g);
        }
     
        //Overriden OnPaint Handles everything related with graphics on the map.
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            g.DrawImage(Properties.Resources.template, 0, 0, 640, 400);

            switch (MapType)
                {
                case "FAR":
                    foreach (Province p in Provinces)
                    {
                        
                        if(p.Farms < 16)                        
                            g.FillRegion(new SolidBrush(Color.FromArgb(255, p.Farms * 15, 0)), p.R);                        
                        else if(p.Farms == 16)
                            g.FillRegion(new SolidBrush(Color.FromArgb(255, 255, 0)), p.R);
                        else if (p.Farms > 16 && p.Farms < 30)
                            g.FillRegion(new SolidBrush(Color.FromArgb(255 - (p.Farms / 2) * 15, 255, 0)), p.R);
                        else if (p.Farms > 29)
                            g.FillRegion(new SolidBrush(Color.FromArgb(0, 255, 0)), p.R);
                    }
                    break;
                case "FAC":
                    foreach (Province p in Provinces)
                    {

                        if (p.Factories < 16)
                            g.FillRegion(new SolidBrush(Color.FromArgb(255, p.Factories * 15, 0)), p.R);
                        else if (p.Factories == 16)
                            g.FillRegion(new SolidBrush(Color.FromArgb(255, 255, 0)), p.R);
                        else if (p.Factories > 16 && p.Factories < 30)
                            g.FillRegion(new SolidBrush(Color.FromArgb(255 - (p.Factories / 2) * 15, 255, 0)), p.R);
                        else if(p.Factories > 29)
                            g.FillRegion(new SolidBrush(Color.FromArgb(0, 255, 0)), p.R);
                    }
                    break;
                case "DEV":
                    foreach (Province p in Provinces)
                    {
                        if (p.Develompent < 11)
                            g.FillRegion(new SolidBrush(Color.FromArgb(255, 42, 0)), p.R);
                        else if (p.Develompent < 21)
                            g.FillRegion(new SolidBrush(Color.FromArgb(255, 126, 0)), p.R);
                        else if (p.Develompent < 31)
                            g.FillRegion(new SolidBrush(Color.FromArgb(255, 228, 0)), p.R);
                        else if (p.Develompent < 41)
                            g.FillRegion(new SolidBrush(Color.FromArgb(156, 255, 0)), p.R);
                        else if (p.Develompent < 60)
                            g.FillRegion(new SolidBrush(Color.FromArgb(154, 255, 64)), p.R);
                        else
                            g.FillRegion(new SolidBrush(Color.FromArgb(12, 255, 0)), p.R);
                    }
                    break;
                case "FRT":
                    foreach (Province p in Provinces)
                    {
                        switch (p.Forts)
                        {
                            case 0:
                                g.FillRegion(new SolidBrush(Color.FromArgb(255, 255, 255)), p.R);
                                break;
                            case 1:
                                g.FillRegion(new SolidBrush(Color.FromArgb(163, 255, 234)), p.R);
                                break;
                            case 2:
                                g.FillRegion(new SolidBrush(Color.FromArgb(0, 255, 240)), p.R);
                                break;
                            case 3:
                                g.FillRegion(new SolidBrush(Color.FromArgb(0, 174, 255)), p.R);
                                break;
                            case 4:
                                g.FillRegion(new SolidBrush(Color.FromArgb(0, 78, 255)), p.R);
                                break;
                            case 5:
                                g.FillRegion(new SolidBrush(Color.FromArgb(0, 0, 255)), p.R);
                                break;
                        }
                    }
                    break;
                case "CON":
                    foreach (Province p in Provinces)
                    {
                        switch (p.Control)
                        {
                            case "KSR":
                                g.FillRegion(new SolidBrush(Color.FromArgb(0, 120, 0)), p.R);
                                break;
                            case "LON":
                                g.FillRegion(new SolidBrush(Color.FromArgb(0, 0, 255)), p.R);
                                break;
                            case "CIV":
                                g.FillRegion(new SolidBrush(Color.FromArgb(255, 255, 0)), p.R);
                                break;
                            case "COM":
                                g.FillRegion(new SolidBrush(Color.FromArgb(255, 0, 0)), p.R);
                                break;
                            case "NON":
                                g.FillRegion(new SolidBrush(Color.FromArgb(255, 255, 255)), p.R);
                                break;

                        }
                    }
                    break;
                case "POP":
                    foreach (Province p in Provinces)
                    {
                        switch (p.Popularity)
                        {
                            case "KSR":
                                g.FillRegion(new SolidBrush(Color.FromArgb(0, 120, 0)), p.R);
                                break;
                            case "LON":
                                g.FillRegion(new SolidBrush(Color.FromArgb(0, 0, 255)), p.R);
                                break;
                            case "CIV":
                                g.FillRegion(new SolidBrush(Color.FromArgb(255, 255, 0)), p.R);
                                break;
                            case "COM":
                                g.FillRegion(new SolidBrush(Color.FromArgb(255, 0, 0)), p.R);
                                break;
                            case "NON":
                                g.FillRegion(new SolidBrush(Color.FromArgb(255, 255, 255)), p.R);
                                break;

                        }
                    }
                    break;
                case "TER":
                    foreach(Province p in Provinces)
                    {
                        switch (p.Terrain)
                        {
                            case "Flat":
                                g.FillRegion(new SolidBrush(Color.FromArgb(156, 255, 50)), p.R);
                                break;
                            case "Normal":
                                g.FillRegion(new SolidBrush(Color.FromArgb(127,191, 91)), p.R);
                                break;
                            case "Hills":
                                g.FillRegion(new SolidBrush(Color.FromArgb(52, 201, 112)), p.R);
                                break;
                            case "Shore":
                                g.FillRegion(new SolidBrush(Color.FromArgb(3, 97, 30)), p.R);
                                break;
                            case "Mountains":
                                g.FillRegion(new SolidBrush(Color.FromArgb(123, 44, 23)), p.R);
                                break;
                            case "Peak":
                                g.FillRegion(new SolidBrush(Color.FromArgb(97, 3, 3)), p.R);
                                break;
                            case "Desert":
                                g.FillRegion(new SolidBrush(Color.FromArgb(237, 244, 33)), p.R);
                                break;

                        }
                    }
                    break;
                case "RAI":
                   
                    break;

                case "BOR":
                    foreach (Province p in Provinces)
                    {
                        g.FillRegion(new SolidBrush(Color.FromArgb(255, 255, 255)), p.R);


                    }
                    if (CurrentHostProvince != -1)
                    {
                        g.FillRegion(Brushes.DarkGreen, Provinces[CurrentHostProvince].R);
                        foreach (int i in Provinces[CurrentHostProvince].Bordering)
                        {
                            g.FillRegion(Brushes.LightGreen, Provinces[i].R);
                        }

                    }
                    break;
            }
            if (Clicked != -1)
            {
                g.FillRegion(Brushes.Khaki, Provinces[Clicked].R);
            }
            StringFormat Sf = new StringFormat();
            Sf.Alignment = StringAlignment.Center;
            Sf.LineAlignment = StringAlignment.Center;
            if(UpdateArmies)
            {
                foreach (Province pr in Provinces)
                {
                    RectangleF r = pr.R.GetBounds(g);
                    r.X = r.X + pr.TextPx;
                    if (pr.Soldiers.Count > 0)
                        g.DrawString(pr.Soldiers.Count.ToString(), font, Brushes.Black, r, Sf);
                
                    //g.DrawString(pr.ID.ToString(), font, Brushes.Black, r, Sf);
                }
                UpdateArmies = false;
            }
           if(ShowRailroads)
            {
                Pen pen = new Pen(new SolidBrush(Color.DarkRed));
                Pen pen2 = new Pen(new SolidBrush(Color.Gray));
                pen.Width = 3;
                pen2.Width = 2;
                foreach (Province p in Provinces)
                {
                    RectangleF r = p.R.GetBounds(g);
                    foreach (int id in p.Railroad.Keys)
                    {
                        RectangleF r2 = Provinces[id].R.GetBounds(g);
                        if (p.Railroad[id] > 0)
                        {
                            
                            Point point = new Point((int)(r.X + (r.Width / 2)), (int)(r.Y + (r.Height / 2)));
                            Point point2 = new Point((int)(r2.X + (r2.Width / 2)), (int)(r2.Y + (r2.Height / 2)));
                            
                            if (p.Railroad[id] == 2)
                            {
                                g.DrawLine(pen, point, point2);
                            }
                            else if (p.Railroad[id] == 1)
                            {
                               g.DrawLine(pen2, point, point2);
                            }
                        }
                    }
                }
                ShowRailroads = false;
            }
            MapType = "";
            
         /*foreach (Province prov in Provinces)
            {
                if (prov.ID < 44)
                    g.FillRegion(Brushes.LightGreen, Provinces[prov.ID].R);
                else
                    g.FillRegion(Brushes.White, Provinces[prov.ID].R);
            }*/
            

        }
       


        //Buttons responsible for changing map mode 
        private void DevButton_Click(object sender, EventArgs e)
        {
            UpdateArmies = true;
            MapType = "DEV";
            CurrentMapType = MapType;
            Invalidate();
    
        }
        private void FortsButton_Click(object sender, EventArgs e)
        {
            UpdateArmies = true;
            MapType = "FRT";
            CurrentMapType = MapType;
            Invalidate();

        }
        private void ControlButton_Click(object sender, EventArgs e)
        {
            UpdateArmies = true;
            MapType = "CON";
            CurrentMapType = MapType;
            Invalidate();

        }
        private void PopButton_Click(object sender, EventArgs e)
        {
            UpdateArmies = true;
            MapType = "POP";
            CurrentMapType = MapType;
            Invalidate();

        }
        private void FarmsButton_Click(object sender, EventArgs e)
        {
            UpdateArmies = true;
            MapType = "FAR";
            CurrentMapType = MapType;
            Invalidate();
        }
        private void FactoriesButton_Click(object sender, EventArgs e)
        {
            UpdateArmies = true;
            MapType = "FAC";
            if (RailroadsOn)
                ShowRailroads = true;
            CurrentMapType = MapType;
            Invalidate();
        }


        //Button used for development, delete later.
        private void button1_Click_1(object sender, EventArgs e)
        {
            MapType = "BOR";
            CurrentMapType = MapType;
            Invalidate();

        }


        //Button used for development, delete or change later.
        private void button2_Click(object sender, EventArgs e)
        {
            g.DrawImage(Properties.Resources.template, 0, 0, 640, 400);
        }
       
        //Button used for showing the province view window.
        private void MoreButton_Click(object sender, EventArgs e)
        {
            if (!ProvinceViewOpen)
            {
                pv.Show();
            }
  
        }
        public static void CreateNewPv()
        {
            Form1.pv = new ProvinceView();
        }
        //Terrain map mode, change name later.
        private void button3_Click(object sender, EventArgs e)
        {
            UpdateArmies = true;
            MapType = "TER";
            if (RailroadsOn)
                ShowRailroads = true;
            CurrentMapType = MapType;
            Invalidate();
        }

        //Railroads map mode, change name later
        private void button4_Click(object sender, EventArgs e)
        {
            RailroadsOn = !RailroadsOn;
            if (RailroadsOn)
                ShowRailroads = true;
            UpdateArmies = true;
            MapType = CurrentMapType;
            Invalidate();
        }
    }


    public class Soldier
    {

        //Move points
        public int MaxMovePoints { get; set; } 
        public int MovePoints { get; set; }

        //Owner, specified in three-letter id
        public string Owner { get; set; }

        //Hit points of the unit
        public double Strength { get; set; }
        public double MaxStrength { get; set; }

        //Indicates how much damage this unit dealts.
        public double Fire { get; set; }

        //Negates the damage dealt to this unit
        public double Armor { get; set; }
        
        //Type of the unit
        public string Type { get; set; }



        //Creates soldier
        public Soldier(string Owner, double Fire)
        {
            this.Owner = Owner;
            MaxStrength = 20;
            Strength = 20;
            MaxMovePoints = 2;
            MovePoints = 2;
            this.Fire = Fire;
            Armor = 0.1;
        }
    }
    public class Player
    {
        //In development
        public string Name { get; set; }
        public string Faction { get; set; }
        public Player()
        {

        }
    }
    public class Building
    {

        //In development
        public int ID { get; set; }
        public string Name { get; set; }
        public bool ProvinceBonus { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }
        public string ReqBuilding { get; set; }
        public string ReqTerrain { get; set; }
        public string ReqTech { get; set; }
        public string ReqFaction { get; set; }
        public Building()
        {
            ID = Form1.Buildings.Count();
            Form1.Buildings.Add(this);
        }

    }
    public class Province
    {   
        //Area of the province in the map
        public Region R { get; set; } = new Region(new Rectangle(0, 0, 1, 1));

        //Color ID of the province
        public Color Color { get; set; }

        //Name of the province
        public string Name { get; set; }

        //Basic stats of the province
        public int Develompent { get; set; }
        public int Forts { get; set; }
        public int Farms { get; set; }
        public int Factories { get; set; }
        public int Population { get; set; }
        public string Terrain { get; set; }

        //Provinces that border this province
        public List<int> Bordering { get; set; } = new List<int>();

        //ID of the province, please don't change it :)
        public int ID { get; set; }

        //This variable indicates who is in control of this province (string represent player who has control, int represents how much control this player has)
        public string Control { get; set; }
        public int ControlPercentage { get; set; }

        //Popularity, the same as above
        public string Popularity { get; set; }
        public int[] PopularityChart { get; set; } = {0,0,0,0};
        
        //List with all of the soldiers in this province
        public List<Soldier> Soldiers { get; set; } = new List<Soldier>();
        
        //Adjustments to the location of the text displayed on the map
        public int TextPx { get; set; }

        //Railroad list in form of dictionary, keys represnt other provinces, values represnt the state of railroads (0 - absent, 1 - broken, 2 - functional)
        public Dictionary<int, int> Railroad { get; set; } = new Dictionary<int, int>();

        //Creator of the province
        public Province(string HEX, string name,string dev, string forts, string control, string pop, string Text, string terrain)
        {
            Color = ColorTranslator.FromHtml(HEX);
            try
            {
                TextPx = int.Parse(Text);
            }
            catch
            {
                MessageBox.Show(Form1.idcount.ToString());
            }
            ID = Form1.idcount;
            Form1.idcount++;
            Name = name;
            Forts = int.Parse(forts);
            Control = control;
            Popularity = pop;
            Terrain = terrain;
            switch (pop)
            {
                case "KSR":
                    PopularityChart[0] = 20;
                    break;
                case "LON":
                    PopularityChart[1] = 20;
                    break;
                case "CIV":
                    PopularityChart[2] = 20;
                    break;
                case "COM":
                    PopularityChart[3] = 20;
                    break;
            }
            Form1.Provinces.Add(this);
            switch (dev)
            {
                case "Low":
                    Develompent = Form1.random.Next(1, 7);                  
                    break;
                case "Below":
                    Develompent = Form1.random.Next(7, 20);
                    break;
                case "Average":
                    Develompent = Form1.random.Next(20, 30);
                    break;
                case "Above":
                    Develompent = Form1.random.Next(30, 41);
                    break;
                case "High":
                    Develompent = Form1.random.Next(41, 51);
                    break;
                case "City":
                    Develompent = Form1.random.Next(60, 81);
                    break;
            }
            switch (Terrain)
            {
                //Add bonuses later            
                case "Normal":
                    
                    break;
                case "Flat":
                   
                    break;
                case "Hills":

                    break;
                case "Shore":

                    break;
                case "Mountains":

                    break;
                case "Peak":

                    break;
                case "Desert":

                    break;
            }
            Population = Form1.random.Next(1 + Develompent /8, 5 + Develompent/2) * 1000;
            Factories = Form1.random.Next(0, (int)Math.Ceiling(Develompent / 1.4));
            Farms = Form1.random.Next(0, (int)Math.Ceiling(Develompent / 1.4));
            foreach(int b in Bordering)
            {
                Railroad.Add(b, 0);
            }
        }
    }

    //LockBitmap is used for faster reading of the map file
    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                int PixelCount = Width * Height;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat);

                // create byte array to copy pixel values
                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                // Unlock bitmap data
                source.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }
    }
}
