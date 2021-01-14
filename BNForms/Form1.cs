using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BNForms
{
    public partial class Form1 : Form
    {
        int joueurCourant = 0;
        public static PictureBox[,] zoneAffichage;
        public Form1()
        {
            InitializeComponent();
            Fonctions.initialiser();
            zoneAffichage = new PictureBox[3, 3];
            zoneAffichage[0, 0] = pictureBox1;
            zoneAffichage[1, 0] = pictureBox2;
            zoneAffichage[2, 0] = pictureBox3;
            zoneAffichage[0, 1] = pictureBox4;
            zoneAffichage[1, 1] = pictureBox5;
            zoneAffichage[2, 1] = pictureBox6;
            zoneAffichage[0, 2] = pictureBox7;
            zoneAffichage[1, 2] = pictureBox8;
            zoneAffichage[2, 2] = pictureBox9;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p_zoneJeu.Visible = true;
            p_placement.Visible = true;
            MessageBox.Show("Joueur 1, placez vos bâteaux en sélectionnant les cases dans la liste déroulante");
            joueurCourant = 1;
            /*
            Image i = Properties.Resources.bateau;
            pictureBox1.Image = i;
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            placerPedalo();
            
        }


        public void placerPedalo() {
            
            char colonne = tb_case1.Text[0];
            int ligne = Convert.ToInt16(tb_case1.Text[1] + "") - 1;
            int colonneReelle = Fonctions.positionColonne(colonne);

            if (joueurCourant == 1)
            {
                Fonctions.tj1[colonneReelle, ligne] = 'P';
            }
            else
            {
                Fonctions.tj2[colonneReelle, ligne] = 'P';
            }

            char colonne2 = tb_case2.Text[0];
            int ligne2 = Convert.ToInt16(tb_case2.Text[1] + "") - 1;
            int colonneReelle2 = Fonctions.positionColonne(colonne2);

            char[,] tabDuJoueurCourant;

            if (joueurCourant == 1)
                tabDuJoueurCourant = Fonctions.tj1;
            else
                tabDuJoueurCourant = Fonctions.tj2;

            if (Fonctions.placementPossible(colonneReelle, ligne, colonneReelle2, ligne2, tabDuJoueurCourant))
            {

                if (joueurCourant == 1)
                {
                    Fonctions.tj1[colonneReelle2, ligne2] = 'P';
                }
                else
                {
                    Fonctions.tj2[colonneReelle2, ligne2] = 'P';
                }
                afficherDemasque(joueurCourant);

                if (joueurCourant == 1)
                {
                    MessageBox.Show("Pédalo bien placé. Au joueur 2 de placer son pédalo.");
                    afficherDemasque(joueurCourant);
                    joueurCourant = 2;
                }
                else
                {
                    MessageBox.Show("Pédalo bien placé. La partie peut commencer ! Bonne chance !");
                    p_placement.Visible = false;
                    panel1.Visible = true;
                    l_qui.Text = "1";
                    afficherMasque(joueurCourant);
                    joueurCourant = 1;
                }
                
                
            }
            else
            {
                MessageBox.Show("Vous ne pouvez pas mettre le pédalo sur cet endroit");
            }
        }

        public void afficherDemasque(int joueur)
        {
            char[,] jeuDeJ;
            if (joueur == 1)
            {
                // on prend les données du tableau 1
                jeuDeJ = Fonctions.tj1;
            }
            else
            {
                jeuDeJ = Fonctions.tj2;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    switch (jeuDeJ[i, j])
                    {
                        case 'v':
                            zoneAffichage[i, j].Image = Properties.Resources.eau;
                            break;
                        case 'P':
                            zoneAffichage[i, j].Image = Properties.Resources.bateau;
                            break;
                    }
                }
            }
        }

        public void afficherMasque(int joueur)
        {
            char[,] jeuDeJ;
            if (joueur == 1)
            {
                // on prend les données du tableau 1
                jeuDeJ = Fonctions.tj2;
            }
            else
            {
                jeuDeJ = Fonctions.tj1;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    switch (jeuDeJ[i, j])
                    {
                        case 'v':
                            zoneAffichage[i, j].Image = Properties.Resources.eau;
                            break;
                        case 'P':
                            zoneAffichage[i, j].Image = Properties.Resources.eau;
                            break;
                        case 'M':
                            zoneAffichage[i, j].Image = Properties.Resources.rate;
                            break;
                        case 'T':
                            zoneAffichage[i, j].Image = Properties.Resources.bombe;
                            break;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            char colonne = tb_bomb.Text[0];
            int ligne = Convert.ToInt16(tb_bomb.Text[1] + "");

            if (Fonctions.tirer(joueurCourant, colonne, ligne))
            {
                MessageBox.Show("Touché ! Bravo !");
                if (Fonctions.testerVictoire(joueurCourant))
                {
                    MessageBox.Show("bravo, vous avez gagné la partie !");
                    p_zoneJeu.Visible = false;
                    panel1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Raté ! Essayez encore !");
            }
            if (joueurCourant == 1)
                joueurCourant = 2;
            else
                joueurCourant = 1;

            afficherMasque(joueurCourant);
            l_qui.Text = ""+joueurCourant;
        }

        // Point p = PointToClient(Cursor.Position);
    }
}
