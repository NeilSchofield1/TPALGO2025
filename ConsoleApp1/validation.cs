using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class validation
    {
         public static int LireEntierDansIntervalle(int minimum, int maximum, string message)
        {
            int valeurLue;
            bool estValide = false;

            do
            {
                Console.Write(message);
                string saisie = Console.ReadLine();

                if (int.TryParse(saisie, out valeurLue) &&
                    valeurLue >= minimum &&
                    valeurLue <= maximum)
                {
                    estValide = true;
                }
                else
                {
                    Console.WriteLine($"Veuillez entrer un nombre entre {minimum} et {maximum}.");
                }

            } while (!estValide);

            return valeurLue;
        }


        public static char LireActionDuJoueur()
        {
            while (true)
            {
                Console.Write("Choix : ");
                ConsoleKeyInfo toucheLue = Console.ReadKey(true);

                if (toucheLue.Key == ConsoleKey.Escape)
                {
                    // 'x' = retour au menu principal
                    return 'x';
                }

                char caractere = char.ToLower(toucheLue.KeyChar);

                if (caractere == 'n' ||
                    caractere == 's' ||
                    caractere == 'e' ||
                    caractere == 'o' ||
                    caractere == 'c' ||
                    caractere == 'i' ||
                    caractere == 'f')
                {
                    return caractere;
                }

                Console.WriteLine("Action invalide. Utilisez n/s/e/o/c/i/f ou Échap.");
            }
        }


        public static bool VerifierRessourceDisponible(List<string> nomsRessources,
                                                       List<int> quantitesRessources,
                                                       string nomRecherche,
                                                       int quantiteMinimum)
        {
            int indexRessource = -1;

            for (int i = 0; i < nomsRessources.Count; i++)
            {
                if (nomsRessources[i].Equals(nomRecherche, StringComparison.OrdinalIgnoreCase))
                {
                    indexRessource = i;
                    break;
                }
            }

            if (indexRessource == -1)
            {
                return false;
            }

            return quantitesRessources[indexRessource] >= quantiteMinimum;
        }


        public static bool VerifierDeuxRessourcesDisponibles(List<string> nomsRessources,
                                                             List<int> quantitesRessources,
                                                             string premierNom,
                                                             int premiereQuantite,
                                                             string deuxiemeNom,
                                                             int deuxiemeQuantite)
        {
            bool premiereRessourceDisponible =
                VerifierRessourceDisponible(nomsRessources, quantitesRessources, premierNom, premiereQuantite);

            bool deuxiemeRessourceDisponible =
                VerifierRessourceDisponible(nomsRessources, quantitesRessources, deuxiemeNom, deuxiemeQuantite);

            return premiereRessourceDisponible && deuxiemeRessourceDisponible;
    }
}
