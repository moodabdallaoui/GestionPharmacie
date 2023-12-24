using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie.Model
{
    class DeclarationMouvement
    {
        int idDeclaration;
        int idMouvement;
        string lieu;
        string service;

        public DeclarationMouvement() { }
        public DeclarationMouvement(int idDeclaration, int idMouvement, string lieu, string service)
        {

            this.idDeclaration = idDeclaration;
            this.idMouvement = idMouvement;
            this.lieu = lieu;
            this.service = service;
        }

        public int IdDeclaration
        {
            get
            {
                return idDeclaration;
            }

            set
            {
                idDeclaration = value;
            }
        }

        public int IdMouvement
        {
            get
            {
                return idMouvement;
            }

            set
            {
                idMouvement = value;
            }
        }

        public string Lieu
        {
            get
            {
                return lieu;
            }

            set
            {
                lieu = value;
            }
        }

        public string Service
        {
            get
            {
                return service;
            }

            set
            {
                service = value;
            }
        }
    }
}
