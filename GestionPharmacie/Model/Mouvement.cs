using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie.Model
{
    public class Mouvement
    {
        int id;
        string type;
        DateTime date;
        public Mouvement() { }
        public Mouvement(int id,string type, DateTime date)
        {
            this.id = id;
            this.type = type;
            this.date = date;
        }

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

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }
    }
}
