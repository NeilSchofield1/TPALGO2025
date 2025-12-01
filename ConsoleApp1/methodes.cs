using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class methodes
    {
                // Position du joueur
        public static int positionJoueurX = 0;
        public static int positionJoueurY = 0;

        // Carte (listes parallèles)
        public static List<int> positionsVisiteesX = new List<int>();
        public static List<int> positionsVisiteesY = new List<int>();
        public static List<string> typesBiomesVisites = new List<string>();

        // Inventaire
        public static List<string> inventaireNoms = new List<string>();
        public static List<int> inventaireQuantites = new List<int>();

        // État du jeu
        public static bool estPartieEnCours = false;

        private static readonly Random generateurAleatoire = new Random();
        private const string sauvegardeNomFichier = "sauvegarde.txt";

        
        // ------------------------
        // MENU PRINCIPAL
        // ------------------------

        public static int AfficherMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("------------------------------");
            Console.WriteLine("        Menu principal        ");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1. Jouer une nouvelle partie");
            Console.WriteLine("2. Charger la dernière partie");
            Console.WriteLine("3. Sauvegarder la partie");
            Console.WriteLine("4. Crédits");
            Console.WriteLine("5. Quitter");
            Console.WriteLine("------------------------------");

            return Validation.LireEntierDansIntervalle(1, 5, "Choisissez une option : ");
        }

        public static void AfficherCredits()
        {
            Console.Clear();
            Console.WriteLine("Crédits");
            Console.WriteLine("-------");
            Console.WriteLine("Jeu réalisé par : Neil et son/sa coéquipier(ère).");
            Console.WriteLine();
            Console.WriteLine("Appuyez sur une touche pour revenir...");
            Console.ReadKey(true);
        }

        public static void SauvegarderDepuisMenu()
        {
            if (!estPartieEnCours)
            {
                Console.WriteLine("Aucune partie à sauvegarder.");
                Console.ReadKey(true);
                return;
            }

            EnregistrerEtatJeu();
            Console.WriteLine("Partie sauvegardée.");
            Console.ReadKey(true);
        }


        // ------------------------
        // GESTION DE PARTIE
        // ------------------------

        public static void NouvellePartie()
        {
            positionJoueurX = 0;
            positionJoueurY = 0;

            positionsVisiteesX.Clear();
            positionsVisiteesY.Clear();
            typesBiomesVisites.Clear();

            inventaireNoms.Clear();
            inventaireQuantites.Clear();

            AjouterPositionDansCarte(0, 0, "BASE");

            estPartieEnCours = true;
            BoucleDeJeu();
        }

        public static void ChargerEtLancerPartie()
        {
            if (!ChargerEtatJeu())
            {
                Console.WriteLine("Impossible de charger la partie.");
                Console.ReadKey(true);
                return;
            }

            estPartieEnCours = true;
            BoucleDeJeu();
        }

        private static void BoucleDeJeu()
        {
            bool partieTerminee = false;

            while (!partieTerminee)
            {
                Console.Clear();

                string biomeCourant = ObtenirBiomeDeLaPositionCourante();

                Console.WriteLine($"Position actuelle : [{positionJoueurX},{positionJoueurY}]");
                Console.WriteLine(biomeCourant);
                Console.WriteLine();
                Console.WriteLine("Actions possibles :");
                Console.WriteLine("n,s,e,o = déplacement");
                Console.WriteLine("c = collecter");
                Console.WriteLine("i = inventaire");
                Console.WriteLine("f = fabriquer (seulement à la base)");
                Console.WriteLine("Échap = menu principal");

                char action = Validation.LireActionDuJoueur();

                if (action == 'x') return; // Échap

                switch (action)
                {
                    case 'n':
                    case 's':
                    case 'e':
                    case 'o':
                        DeplacerJoueur(action);
                        break;

                    case 'c':
                        CollecterRessourceCourante();
                        break;

                    case 'i':
                        AfficherInventaire();
                        Console.ReadKey(true);
                        break;

                    case 'f':
                        if (positionJoueurX == 0 && positionJoueurY == 0)
                        {
                            AfficherMenuFabrication();

                            if (ObtenirQuantiteRessource("Maison") >= 1)
                            {
                                Console.Clear();
                                Console.WriteLine("VOUS AVEZ CONSTRUIT UNE MAISON !");
                                Console.WriteLine("Vous survivez à l'hiver !");
                                Console.ReadKey(true);
                                partieTerminee = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vous devez être à la BASE.");
                            Console.ReadKey(true);
                        }
                        break;
                }
            }
        }


        // ------------------------
        // CARTE & BIOMES
        // ------------------------

        private static void DeplacerJoueur(char direction)
        {
            if (direction == 'n') positionJoueurY++;
            if (direction == 's') positionJoueurY--;
            if (direction == 'e') positionJoueurX++;
            if (direction == 'o') positionJoueurX--;

            int index = TrouverIndexDePosition(positionJoueurX, positionJoueurY);

            if (index == -1)
            {
                string biome = (positionJoueurX == 0 && positionJoueurY == 0)
                    ? "BASE"
                    : GenererBiomePourNouvellePosition();

                AjouterPositionDansCarte(positionJoueurX, positionJoueurY, biome);
            }
        }

        private static int TrouverIndexDePosition(int x, int y)
        {
            for (int i = 0; i < positionsVisiteesX.Count; i++)
            {
                if (positionsVisiteesX[i] == x && positionsVisiteesY[i] == y)
                {
                    return i;
                }
            }
            return -1;
        }

        private static string ObtenirBiomeDeLaPositionCourante()
        {
            int index = TrouverIndexDePosition(positionJoueurX, positionJoueurY);
            return typesBiomesVisites[index];
        }

        private static void AjouterPositionDansCarte(int x, int y, string biome)
        {
            positionsVisiteesX.Add(x);
            positionsVisiteesY.Add(y);
            typesBiomesVisites.Add(biome);
        }

        private static string GenererBiomePourNouvellePosition()
        {
            string[] biomes =
            {
                "MONTAGNE", "RIVIERE", "FORET",
                "MARAIS", "PRAIRIE", "DESERT"
            };

            return biomes[generateurAleatoire.Next(biomes.Length)];
        }
    }
}
