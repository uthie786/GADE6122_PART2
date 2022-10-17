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
            lblBtnInfo.Text = "WASD - moves Hero \n" + //additional information such as controls and what the letters represent
                "IJKL - Hero attacks \n" +
                "\n" +
                "H - Hero \n" +
                "S - SwampCreature \n" +
                "M - Mage \n" +
                "G - Gold";


        }
        public string attackInfo = "";

        private void lblMap_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            CheckMove(e.KeyCode);
            CheckAttack(e.KeyCode);
            //updates display after every keypress
            UpdateDisplay();
        }
        private void CheckMove(Keys keyCode) //method for moving the character using keys on the keyboard
        {
            Debug.WriteLine(keyCode); //writes the key clicked into the debug console
            //updatemap() updates vision as well
            gameEngine.map.UpdateMap();
            if (keyCode == Keys.W)
            {
                //moves character, enemies and also makes the enemy attack. The map and vision arrays are updated
                gameEngine.MovePlayer(Movement.Up);
                gameEngine.map.MoveEnemies();
                gameEngine.map.UpdateMap();
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.S)
            {
                gameEngine.MovePlayer(Movement.Down);
                gameEngine.map.MoveEnemies();
                gameEngine.map.UpdateMap();
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.D)
            {
                gameEngine.MovePlayer(Movement.Right);
                gameEngine.map.MoveEnemies();
                gameEngine.map.UpdateMap();
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.A)
            {
                gameEngine.MovePlayer(Movement.Left);
                gameEngine.map.MoveEnemies();
                gameEngine.map.UpdateMap();
                gameEngine.EnemyAttacks();
            }
            
        }
        private void CheckAttack(Keys keyCode)
        {
            //method that makes the character attack and correctly damages and kills enemies in range.
            //enemies also attack after the player attacks
            //the attacking information is displayed in a label on the form
            Debug.WriteLine(keyCode);
            gameEngine.map.UpdateMap();
            if (keyCode == Keys.I)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Up);
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.K)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Down);
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.L)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Right);
                gameEngine.EnemyAttacks();
            }
            else if (keyCode == Keys.J)
            {
                attackInfo = gameEngine.PlayerAttack(Movement.Left);
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
    }
}
