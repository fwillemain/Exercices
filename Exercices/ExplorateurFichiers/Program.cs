﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorateurFichiers
{
    class Program
    {
        static void Main(string[] args)
        {

            string path;
            bool correct = false;

            while (!correct)
            {
                Console.WriteLine("Saisissez le chemin du dossier à explorer : ");
                path = Console.ReadLine();
                if (Directory.Exists(path))
                {
                    Analyseur.AnalyserDossier(path);
                    correct = true;
                }
                else
                    Console.WriteLine("Le chemin spécifié est incorrect.");
            }


            Console.WriteLine(Analyseur.ToString());

            Console.ReadKey();
        }
    }

    public delegate void DelegueAnalyser(Explorateur e);

    public class Explorateur
    {
        #region Propriétés
        public DirectoryInfo dir { get; }
        #endregion

        #region Constructeurs
        public Explorateur(string path)
        {
            dir = new DirectoryInfo(path);
        }
        #endregion

        #region Méthodes publiques
        public void Explorer(DelegueAnalyser analyser)
        {
            analyser(this);
        }
        #endregion
    }

    public static class Analyseur
    {
        #region Propriétés
        public static int CompteFichiers { get; private set; }
        public static int CompteFichiersCs { get; private set; }
        public static string FichierLePlusLong { get; private set; }
        public static FileInfo[] ListeFichiersProj { get; private set; }
        #endregion

        #region Méthodes privées
        private static void NbFichiersDontCs(Explorateur e)
        {
            CompteFichiers = e.dir.GetFiles("*", SearchOption.AllDirectories).Length;
            CompteFichiersCs = e.dir.GetFiles("*.cs", SearchOption.AllDirectories).Length;
        }

        private static void NomFichierPlusLong(Explorateur e)
        {
            var FileList = e.dir.GetFiles("*", SearchOption.AllDirectories);
            long tailleMax = FileList.Max(a => a.Length);
            FichierLePlusLong = FileList.Where(a => a.Length == tailleMax).First().Name; 
        }

        private static void NomFichierCSharp(Explorateur e)
        {
            ListeFichiersProj = e.dir.GetFiles("*.csproj", SearchOption.AllDirectories);
        }

        #endregion

        #region Méthodes publiques
        public static void AnalyserDossier(string path)
        {
            Explorateur explorateur = null;
            explorateur = new Explorateur(path);

            DelegueAnalyser delegue = null;
            delegue += Analyseur.NbFichiersDontCs;
            delegue += Analyseur.NomFichierCSharp;
            delegue += Analyseur.NomFichierPlusLong;

            explorateur.Explorer(delegue);
        }

        public static string ToString()
        {
            string res = string.Format("{0} fichier(s), dont {1} fichier(s) .cs.\n", CompteFichiers, CompteFichiersCs);
            res += string.Format("Nom de fichier le plus long : {0}", FichierLePlusLong);
            res += "Fichiers projets C# :\n";
            foreach (var a in ListeFichiersProj)
                res += Path.GetFileNameWithoutExtension(a.Name) + "\n";

            return res;
        }
        #endregion

    }


}
