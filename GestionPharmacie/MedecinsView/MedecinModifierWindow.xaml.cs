using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GestionPharmacie.Model;

namespace GestionPharmacie.MedecinsView
{
    /// <summary>
    /// Logique d'interaction pour ModifierMedecin.xaml
    /// </summary>
    public partial class ModifierMedecin : Window
    {
        public Medecin MedecinMdf { get; set; }
        public ModifierMedecin(Medecin MedecinMdf)
        {
            InitializeComponent();
            this.MedecinMdf = MedecinMdf;
            DbContext = new AppDataContext();
            txtAdresse.Text = MedecinMdf.Adresse;
            txtNom.Text = MedecinMdf.Nom;
            txtPrenom.Text = MedecinMdf.Prenom;
        }
        AppDataContext DbContext;
        private void BtnValider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MedecinMdf.Nom = txtNom.Text;
            MedecinMdf.Prenom = txtPrenom.Text;
            MedecinMdf.Adresse = txtAdresse.Text;
            if( DbContext.UpdateMedecin(MedecinMdf))
            {
                MessageBox.Show("Medecin bien modifier");
                this.DialogResult = true;

            }
            else
            {
                MessageBox.Show("Echac de modification");
                this.DialogResult = false;
            }

        }

        private void BtnSupprimer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(DbContext.GetDeclaration().Where(d=>d.IdMedecin==MedecinMdf.Id).Count()==0)
            {
                DbContext.DeleteMedecin(MedecinMdf.Id);
                MessageBox.Show("Medecin bien supprimer");
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("impossible de supprimer \ndéja utilisé dans autre enregistrement");
            }
        }

        private void BtAnnuler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
            
        }
    }
}
