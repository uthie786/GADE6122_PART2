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
            InitializeComponent();
            gameEngine = new GameEngine();
            lblMap.Text = gameEngine.Display;
            UpdateDisplay();
            lblAttackInfo.Text = attackInfo;
            
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
            }
            else if (keyCode == Keys.S)
            {
                gameEngine.MovePlayer(Movement.Down);
                gameEngine.map.MoveEnemies();
                UpdateEnemyDisplay(Movement.Down);
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.D)
            {
                gameEngine.MovePlayer(Movement.Right);
                gameEngine.map.MoveEnemies();
                UpdateEnemyDisplay(Movement.Right);
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.A)
            {
                gameEngine.MovePlayer(Movement.Left);
                gameEngine.map.MoveEnemies();
                UpdateEnemyDisplay(Movement.Left);
                gameEngine.EnemyAttacks();
            }
            
        }
        private void CheckAttack(Keys keyCode)
        {
            
            Debug.WriteLine(keyCode);
            gameEngine.map.UpdateMap();
            if (keyCode == Keys.I)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Up);
                UpdateEnemyDisplay(Movement.Up);
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.K)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Down);
                UpdateEnemyDisplay(Movement.Down);
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.L)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Right);
                UpdateEnemyDisplay(Movement.Right);
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.J)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Left);
                UpdateEnemyDisplay(Movement.Left);
                gameEngine.EnemyAttacks();
            }
            if (attackInfo != null)
            {
                lblAttackInfo.Text = attackInfo;
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
    }
}
