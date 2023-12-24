using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionPharmacie.Model;

namespace GestionPharmacie.Model
{
   public class DetailsMouvementModelView
    {
        int quantite;
        List<Medicament> listeMedicament;
        static List<Medicament> liste;
        public List<Medicament> ListeMedicament
        {
            get
            {
                return listeMedicament;
            }

            set
            {
                listeMedicament = value;
            }
        }
        public int Quantite
        {
            get
            {
                return quantite;
            }

            set
            {
                quantite = value;
            }
        }
        static DetailsMouvementModelView()
        {
            AppDataContext db = new AppDataContext();
            liste = db.GetMedicament();
        }
        public DetailsMouvementModelView()
        {
            this.ListeMedicament = liste;
        }
        
    }
}
