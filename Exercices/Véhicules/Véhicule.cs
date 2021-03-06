﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Véhicules
{
    public delegate void DelegueEntretien(Véhicule v);

    public abstract class Véhicule : IComparable
    {
        #region Propriétés
        public string Nom { get; }
        public int NbRoues { get; protected set; }
        public Energies Energie { get; }
        public virtual string Description
        {
            get { return string.Format("Véhicule {0} roule sur {1} roues et à l'énergie {2}.", Nom, NbRoues, Energie); }
        }
        public abstract int PRK { get; }
        public float Prix { get; set; }
        public Dictionary<DateTime, String> CarnetEntretien { get; }
        #endregion

        #region Constructeurs
        public Véhicule()
        {
            NbRoues = 4;
            CarnetEntretien = new Dictionary<DateTime, string>();
        }
        public Véhicule(string nom, float prix) : this()
        {
            Nom = nom;
            Prix = prix;
        }
        public Véhicule(string nom, int nbRoues, Energies energie) : this()
        {
            Nom = nom;
            NbRoues = nbRoues;
            Energie = energie;
        }
        #endregion

        #region Méthodes Publiques
        public abstract void CalculerConso();

        public int CompareTo(object v)
        {
            if(v is Véhicule)          
                return Prix.CompareTo(((Véhicule)v).Prix);
            else
                throw new ArgumentException();
        }

        public void Entretenir(DateTime d, DelegueEntretien entretien)
        {
            if (!CarnetEntretien.ContainsKey(d))
                CarnetEntretien.Add(d, string.Empty);

            entretien(this);
        }

        public override string ToString()
        {
            string res = string.Empty;
            foreach (var a in CarnetEntretien)
                res += string.Format("Entretien du véhicule {0} du {1} :\n{2}", Nom, a.Key.ToShortDateString(), a.Value);

            return res;
        }
        #endregion
    }

    public class Voiture : Véhicule
    {
        #region Propriétés
        public override string Description
        {
            get
            {
                return string.Format("Je suis une voiture. \r\n") + base.Description;
            }
        }

        public override int PRK { get { return 20; } }
        #endregion

        #region Constructeurs
        public Voiture() : base()
        {

        }
        public Voiture(string nom, float prix) : base(nom, prix)
        {

        }
        public Voiture(string nom, Energies energie) : base(nom, 4, energie)
        {

        }
        #endregion

        #region Méthodes Publiques
        public override void CalculerConso()
        {
            throw new NotImplementedException();
        }

        public string RefaireParallélisme()
        {
            return "Parallélisme refait.";
        }
       
        #endregion
    }

    public class Moto : Véhicule
    {
        #region Propriétés
        public override string Description
        {
            get { return string.Format("Je suis une moto. \r\n") + base.Description; }
        }
        public override int PRK { get { return 10; } }
        #endregion

        #region Constructeurs
        public Moto() : base()
        {
            NbRoues = 2;
        }
        public Moto(string nom, float prix) : base(nom, prix)
        {

        }
        public Moto(string nom, Energies energie) : base(nom, 2, energie)
        {
        }
        #endregion

        #region Méthodes Publiques
        public override void CalculerConso()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
