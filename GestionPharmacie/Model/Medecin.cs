using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie.Model
{
   public class Medecin
    {
        int id;
        string nom;
        string prenom;
        string adresse;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                prenom = value;
            }
        }

        public string Adresse
        {
            get
            {
                return adresse;
            }

            set
            {
                adresse = value;
            }
        }
        public Medecin() { }
        public override string ToString()
        {
            return Nom+" "+Prenom;
        }
        public Medecin(int id, string nom ,string prenom, string adresse)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
        }
    }
}
