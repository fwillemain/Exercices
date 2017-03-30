﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptages
{
    class Program
    {
        static void Main(string[] args)
        {
            Cryptage.ChargerFichier("../../cle.txt");

            string texte = "Coucou, je ne suis pas crypté!";

            Console.WriteLine(texte);

            Cryptage.CrypterTexte(ref texte);
            Console.WriteLine(texte);

            Cryptage.DecrypterTexte(ref texte);
            Console.WriteLine(texte);

            Console.ReadKey();
        }
    }

    static class Cryptage
    {
        #region Propriétés
        public static Dictionary<char, char> ClefCryptage { get; set; }
        #endregion

        #region Constructeurs
        static Cryptage()
        {
            ClefCryptage = new Dictionary<char, char>();
        }
        #endregion

        #region Méthodes privées
        private static void InverserClefCryptage()
        {
            var clefInversée = new Dictionary<char, char>();

            foreach (var a in ClefCryptage)
            {
                clefInversée.Add(a.Value, a.Key);
            }
            ClefCryptage = clefInversée;
        }
        #endregion

        #region Méthodes publiques
        public static void ChargerFichier(string path)
        {
            string[] ligne, clef = File.ReadAllLines(path);

            for (int i = 0; i < clef.Length; i++)
            {
                ligne = clef[i].Split(' ');
                ClefCryptage.Add(Convert.ToChar(ligne[0]), Convert.ToChar(ligne[1]));
            }
        }

        public static void CrypterTexte(ref string texte)
        {
            char[] c = texte.ToCharArray();
            char ch;
            for (int i = 0; i < c.Length; i++)
            {
                if (ClefCryptage.TryGetValue(c[i], out ch))
                    c[i] = Convert.ToChar(ch);
            }

            texte = new string(c);
        }

        public static void DecrypterTexte(ref string texte)
        {
            InverserClefCryptage();
            CrypterTexte(ref texte);
            InverserClefCryptage();
        }
        #endregion

    }


}