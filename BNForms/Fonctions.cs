using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNForms
{
    public class Fonctions
    {
        public static char[,] tj1 = new char[3, 3];
        public static char[,] tj2 = new char[3, 3];

        public static void initialiser()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tj1[i, j] = 'v';
                    tj2[i, j] = 'v';
                }
            }
        }

        public static bool testerVictoire(int joueur)
        {
            bool resu = false;

            int nbP = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (joueur == 1)
                    {
                        if (tj2[j, i] == 'P')
                        {
                            nbP++;
                        }
                    }
                    else
                    {
                        if (tj1[j, i] == 'P')
                        {
                            nbP++;
                        }
                    }
                }
            }

            if (nbP == 0)
            {
                resu = true;
            }
            return resu;
        }

        public static bool tirer(int joueur, char col, int ligne)
        {
            bool resu = false;
            if (joueur == 1)
            {
                if (tj2[positionColonne(col), ligne - 1] == 'P')
                {
                    tj2[positionColonne(col), ligne - 1] = 'T';
                    resu = true;
                }
                else
                {
                    tj2[positionColonne(col), ligne - 1] = 'M';
                }
            }
            else
            {
                if (tj1[positionColonne(col), ligne - 1] == 'P')
                {
                    tj1[positionColonne(col), ligne - 1] = 'T';
                    resu = true;
                }
                else
                {
                    tj1[positionColonne(col), ligne - 1] = 'M';
                }
            }
            return resu;
        }

        public static void placerPedalos(int joueur)
        {
            Console.WriteLine("Placez votre pédalo en indiquant sa colonne (ABC) et sa ligne (123) et faîtes entrée en chaque saisie :");
            char col = Convert.ToChar(Console.ReadLine());
            int lig = Convert.ToInt16(Console.ReadLine());

            if (joueur == 1)
            {
                tj1[positionColonne(col), lig - 1] = 'P';
            }
            else
            {
                tj2[positionColonne(col), lig - 1] = 'P';
            }

            Console.WriteLine("Placez votre 2nd partie de pédalo en indiquant sa colonne (ABC) et sa ligne (123) et faîtes entrée en chaque saisie :");
            char col2 = Convert.ToChar(Console.ReadLine());
            int lig2 = Convert.ToInt16(Console.ReadLine());

            if (placementPossible(positionColonne(col), lig, positionColonne(col2), lig2, tj1))
            {

                if (joueur == 1)
                {
                    tj1[positionColonne(col), lig - 1] = 'P';
                }
                else
                {
                    tj2[positionColonne(col), lig - 1] = 'P';
                }
            }
            else
            {
                Console.WriteLine("Vous ne pouvez pas mettre le pédalo sur cet endroit");
            }
        }

        public static bool placementPossible(int cp1, int lp1, int cp2, int lp2, char[,] tj)
        {
            bool possible = false;
            // on test que la case est bien vide
            if (tj[cp2, lp2] == 'v')
            {
                // ici on vérifie qu'au moins la ligne ou la colonne soit la même pour placer la 2eme partie sur la même ligne ou la même colonne (car les diagonales ne marchent pas)
                if ((cp1 == cp2) || (lp1 == lp2))
                {
                    // On peut placer si l'écart avec la case de la première partie du pédalo n'est pas supérieur à 1
                    int difCol = cp1 - cp2;
                    int difLig = lp1 - lp2;
                    difCol = Math.Abs(difCol);
                    difLig = Math.Abs(difLig);
                    if ((difCol <= 1) && (difLig <= 1))
                    {
                        possible = true;
                    }
                }
            }
            return possible;
        }

        public static int positionColonne(char p)
        {
            int equivalent = -1;
            switch (p)
            {
                case 'A':
                    equivalent = 0;
                    break;
                case 'B':
                    equivalent = 1;
                    break;
                case 'C':
                    equivalent = 2;
                    break;

            }
            return equivalent;
        }

        public static void afficher(int joueur)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (joueur == 1)
                    {
                        Console.Write(tj1[j, i]);

                    }
                    else
                    {
                        Console.Write(tj2[j, i]);
                    }
                }
                Console.WriteLine("");
            }

        }


    }
}
