using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1 {

    internal class Program
    {


        static void Main(string[] args)
        {


            static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;


                bool quitter = false;
                while (!quitter)
                {
                    int choix = methodes.AfficherMenuPrincipal();

                    switch (choix)
                    {
                        case 1:
                            methodes.NouvellePartie();
                            break;

                        case 2:
                            methodes.ChargerEtLancerPartie();
                            break;

                        case 3:
                            methodes.SauvegarderDepuisMenu();
                            break;

                        case 4:
                            methodes.AfficherCredits();
                            break;

                        case 5:
                            quitter = true;
                            break;
                    }
                }

                Console.WriteLine("Merci d'avoir joué !");

                // test

                Console.WriteLine("test");

                Console.WriteLine("test");
            }
        }
    }
}

    

