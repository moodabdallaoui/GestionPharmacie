using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie.Model
{
    public class Declaration
    {
        int id;
        int idPersonnel;
        int idMedecin;
        string typeDeclaration;
        double totalFraisEngage;
        DateTime dateConsultation;
        string nomPrenomMalade;
        int ageMalade;
        string lienParente;
        string natureMaladie;
        string numCertificat;

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
        public Personelle Personnel
        {
            get {
                AppDataContext db = new AppDataContext();
                return db.GetPersonnelById(this.idPersonnel);
            }
        }
        public Medecin Medecin
        {
            get
            {
                AppDataContext db = new AppDataContext();
                return db.GetMedecinById(this.idMedecin);
            }
        }
        public int IdPersonnel
        {
            get
            {
                return idPersonnel;
            }

            set
            {
                idPersonnel = value;
            }
        }

        public int IdMedecin
        {
            get
            {
                return idMedecin;
            }

            set
            {
                idMedecin = value;
            }
        }

        public string TypeDeclaration
        {
            get
            {
                return typeDeclaration;
            }

            set
            {
                typeDeclaration = value;
            }
        }

        public double TotalFraisEngage
        {
            get
            {
                return totalFraisEngage;
            }

            set
            {
                totalFraisEngage = value;
            }
        }

        public DateTime DateConsultation
        {
            get
            {
                return dateConsultation;
            }

            set
            {
                dateConsultation = value;
            }
        }

        public string NomPrenomMalade
        {
            get
            {
                return nomPrenomMalade;
            }

            set
            {
                nomPrenomMalade = value;
            }
        }

        public int AgeMalade
        {
            get
            {
                return  ageMalade;
               
            }

            set
            {
                 ageMalade = value;
            }
        }

        public string LienParente
        {
            get
            {
                return lienParente;
            }

            set
            {
                lienParente = value;
            }
        }

        public string NatureMaladie
        {
            get
            {
                return natureMaladie;
            }

            set
            {
                natureMaladie = value;
            }
        }

        public string NumCertificat
        {
            get
            {
                return numCertificat;
            }

            set
            {
                numCertificat = value;
            }
        }

        public Declaration() { }

        public Declaration(int id ,int idPersonnel,int idMedecin,
        string typeDeclaration,double totalFraisEngage,DateTime dateConsultation,
        string nomPrenomMalade,int age,string lienParente,string natureMaladie,
        string numCertificat)
        {
            this.Id = id;this.IdPersonnel = idPersonnel;this.IdMedecin = idMedecin;
            this.TypeDeclaration = typeDeclaration;
            this.TotalFraisEngage = totalFraisEngage;
            this.DateConsultation = dateConsultation;
            this.NomPrenomMalade = nomPrenomMalade;
            this.AgeMalade = age;
            this.LienParente = lienParente;
            this.NatureMaladie = natureMaladie;
            this.NumCertificat = numCertificat;



        }
    }
}
