using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie.Model
{
    class DetailMouvement
    {
        int idmouvement;
        int idmedicament;
        int quatiter;

        public int Idmouvement
        {
            get
            {
                return idmouvement;
            }

            set
            {
                idmouvement = value;
            }
        }

        public int Idmedicament
        {
            get
            {
                return idmedicament;
            }

            set
            {
                idmedicament = value;
            }
        }

        public int Quatiter
        {
            get
            {
                return quatiter;
            }

            set
            {
                quatiter = value;
            }
        }

        public DetailMouvement() { }
        public DetailMouvement(int idmouvement,int idmedicament, int quatiter)
        {
            this.idmedicament = idmedicament;
            this.idmouvement = idmouvement;
            this.quatiter = quatiter;
        }

    }
}
