using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1 {

    internal class Program
    {


        static void Main(string[] args)
        {
 Console.OutputEncoding = System.Text.Encoding.UTF8;


            bool quitter = false;
            while (!quitter)
            {
                int choix = Methodes.AfficherMenuPrincipal();

                switch (choix)
                {
                    case 1: 
                        Methodes.NouvellePartie();
                        break;

                    case 2: 
                        Methodes.ChargerEtLancerPartie();
                        break;

                    case 3: 
                        Methodes.SauvegarderDepuisMenu();
                        break;

                    case 4: 
                        Methodes.AfficherCredits();
                        break;

                    case 5: 
                        quitter = true;
                        break;
                }
            }

            Console.WriteLine("Merci d'avoir joué !");
        }
    }
}

    

