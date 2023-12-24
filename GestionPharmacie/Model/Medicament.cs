using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie.Model
{
    public class Medicament
    {
        int id;
        string designation;
        DateTime dateExpiration;
        int quantiterStock;

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

        public string Designation
        {
            get
            {
                return designation;
            }

            set
            {
                designation = value;
            }
        }

        public DateTime DateExpiration
        {
            get
            {
                return dateExpiration;
            }

            set
            {
                dateExpiration = value;
            }
        }

        public int QuantiterStock
        {
            get
            {
                return quantiterStock;
            }

            set
            {
                quantiterStock = value;
            }
        }
        public Medicament() { }
        public Medicament(int id,string Designation, DateTime dateExpiration, int quantiterStock)
             
        {
            this.id = id;
            this.designation = Designation;
            this.dateExpiration = dateExpiration;
            this.quantiterStock = quantiterStock;
        }
        public override string ToString()
        {
            return this.designation;
        }
    }
}
