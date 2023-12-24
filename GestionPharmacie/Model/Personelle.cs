using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie.Model
{
    public class Personelle
    {
        int id;
        string matricule;
        string nom;
        string prenom;
        string tel;
        string situation;
        string adresse;
        string numContrat;
        DateTime dateContrat;

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

        public string Matricule
        {
            get
            {
                return matricule;
            }

            set
            {
                matricule = value;
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

        public string Tel
        {
            get
            {
                return tel;
            }

            set
            {
                tel = value;
            }
        }

        public string Situation
        {
            get
            {
                return situation;
            }

            set
            {
                situation = value;
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

        public string NumContrat
        {
            get
            {
                return numContrat;
            }

            set
            {
                numContrat = value;
            }
        }

        List<Declaration> declarations;
        List<Mouvement> personnelMouvement;

        public DateTime DateContrat
        {
            get
            {
                return dateContrat;
            }

            set
            {
                dateContrat = value;
            }
        }

        public List<Declaration> Declarations
        {
            get
            {
                AppDataContext dbcontext = new AppDataContext();
                declarations = dbcontext.GetDeclarationByPersonnel(this.Id);
                return declarations;
            }
        }

        public List<Mouvement> PersonnelMouvements
        {
            get
            {
                AppDataContext dbcontext = new AppDataContext();
                personnelMouvement = dbcontext.GetMouvementByPersonnel(this.Id);
                return personnelMouvement;
            }
        }

        public Personelle() { }
        public override string ToString()
        {
            return Nom+" "+Prenom;
        }
        public Personelle(int id, string matricule,string nom,string prenom, string tel, string Situation,string Adresse,string numContrat,DateTime dateContrat)
        {
            this.id = id;
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.tel = tel;
            this.situation = Situation;
            this.adresse = Adresse;
            this.numContrat = numContrat;
            this.dateContrat = dateContrat;

        }

    }
}
