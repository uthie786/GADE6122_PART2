using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Gade6122_Part1_corrected
{
    public partial class frm1 : Form
    {
        GameEngine gameEngine;
        public frm1()
        {
            KeyPreview = true;
            InitializeComponent();
            gameEngine = new GameEngine();
            lblMap.Text = gameEngine.Display;
            UpdateDisplay();
            lblAttackInfo.Text = attackInfo;
            lblEnemyStats.Text = "The stats of the Enemy your are \nfacing or attacking will show here.";

        }
        public string attackInfo = "";

        private void lblMap_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            CheckMove(e.KeyCode);
            CheckAttack(e.KeyCode);

            UpdateDisplay();
        }
        private void CheckMove(Keys keyCode)
        {
            Debug.WriteLine(keyCode);
            //updatemap() updates vision as well
            gameEngine.map.UpdateMap();

            if (keyCode == Keys.W)
            {
                gameEngine.MovePlayer(Movement.Up);
                gameEngine.map.MoveEnemies();
                UpdateEnemyDisplay(Movement.Up);
                gameEngine.EnemyAttacks();
                gameEngine.map.UpdateMap();
            }
            else if (keyCode == Keys.S)
            {
                gameEngine.MovePlayer(Movement.Down);
                gameEngine.map.MoveEnemies();
                UpdateEnemyDisplay(Movement.Down);
                gameEngine.EnemyAttacks();
                gameEngine.map.UpdateMap();
            }
            else if (keyCode == Keys.D)
            {
                gameEngine.MovePlayer(Movement.Right);
                gameEngine.map.MoveEnemies();
                UpdateEnemyDisplay(Movement.Right);
                gameEngine.EnemyAttacks();
                gameEngine.map.UpdateMap();
            }
            else if (keyCode == Keys.A)
            {
                gameEngine.MovePlayer(Movement.Left);
                gameEngine.map.MoveEnemies();
                UpdateEnemyDisplay(Movement.Left);
                gameEngine.EnemyAttacks();
                gameEngine.map.UpdateMap();
            }

        }
        private void CheckAttack(Keys keyCode)
        {

            Debug.WriteLine(keyCode);
            gameEngine.map.UpdateMap();
            if (keyCode == Keys.I)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Up);
                gameEngine.EnemyAttacks();
                UpdateEnemyDisplay(Movement.Up);
                gameEngine.map.UpdateMap();
            }
            else if (keyCode == Keys.K)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Down);
                gameEngine.EnemyAttacks();
                UpdateEnemyDisplay(Movement.Down);
                gameEngine.map.UpdateMap();
            }
            else if (keyCode == Keys.L)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Right);
                gameEngine.EnemyAttacks();
                UpdateEnemyDisplay(Movement.Right);
                gameEngine.map.UpdateMap();
            }
            else if (keyCode == Keys.J)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Left);
                gameEngine.EnemyAttacks();
                UpdateEnemyDisplay(Movement.Left);
                gameEngine.map.UpdateMap();
            }
            if (attackInfo != null)
            {
                lblAttackInfo.Text = attackInfo;
                gameEngine.map.UpdateMap();
            }
        }

        private void lblAttackInfo_Click(object sender, EventArgs e)
        {

        }

        private void lblHeroStats_Click(object sender, EventArgs e)
        {

        }
        private void UpdateDisplay() //updates hero stats and map
        {
            lblHeroStats.Text = gameEngine.HeroStats;
            lblMap.Text = gameEngine.Display;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UpdateEnemyDisplay(Movement direction)
        {
            lblEnemyStats.Text = gameEngine.ShowEnemyStats(direction);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            gameEngine.Load();
            UpdateDisplay();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            gameEngine.Save();

        }

        private void frm1_Load(object sender, EventArgs e)
        {

        }


    }
}
