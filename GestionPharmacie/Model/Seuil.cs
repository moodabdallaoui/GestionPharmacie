using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie.Model
{
    public class Seuil
    {
        int nbjour;
        int nbQuantite;
        public int Nbjour
        {
            get
            {
                return nbjour;
            }

            set
            {
                nbjour = value;
            }
        }
        public int NbQuantite
        {
            get
            {
                return nbQuantite;
            }

            set
            {
                nbQuantite = value;
            }
        }
    }
}
