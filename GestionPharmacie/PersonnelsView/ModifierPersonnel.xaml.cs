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

namespace GestionPharmacie.PersonnelsView
{
    /// <summary>
    /// Logique d'interaction pour ModifierPersonnel.xaml
    /// </summary>
    public partial class ModifierPersonnel : Window
    {
        public ModifierPersonnel(Personelle MdfPersonnel)
        {
            InitializeComponent();
            this.MdfPersonnel = MdfPersonnel;
            DbContext = new AppDataContext();
            listePersonnel = DbContext.getPersonelle();
            txtMatricule.Text = MdfPersonnel.Matricule;
            txtNom.Text = MdfPersonnel.Nom;
            txtPrenom.Text = MdfPersonnel.Prenom;
            txtTelephone.Text = MdfPersonnel.Tel;
            CombSituation.Text = MdfPersonnel.Situation;
            txtAdresse.Text = MdfPersonnel.Adresse;
            txtNumContrat.Text = MdfPersonnel.NumContrat;
            DpDateContrat.SelectedDate = MdfPersonnel.DateContrat;

        }
        List<Personelle> listePersonnel;
        AppDataContext DbContext;
        Personelle MdfPersonnel;
        private void BtnValider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (listePersonnel.Where(p => p.Matricule == txtMatricule.Text).Count() > 1)
                {
                    throw new Exception("Le matricule doit etre unique");
                }
                if (listePersonnel.Where(p => p.NumContrat == txtNumContrat.Text).Count()>1)
                {
                    throw new Exception("Le numero du contrat doit etre unique");
                }
                if (!DpDateContrat.SelectedDate.HasValue)
                {
                    throw new Exception("Le date du contrat doit etre selectionner");
                }
                Personelle NvPersonnel = new Personelle(MdfPersonnel.Id, txtMatricule.Text,
                                                         txtNom.Text,
                                                         txtPrenom.Text,
                                                         txtTelephone.Text,
                                                         CombSituation.Text,
                                                         txtAdresse.Text,
                                                         txtNumContrat.Text,
                                                         DpDateContrat.SelectedDate.Value);
                DbContext.Updatepersonelle(NvPersonnel);
                MessageBox.Show("personnel bien modifier");
                DialogResult = true;

            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }
        }

        private void BtSupprimer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(MdfPersonnel.Declarations.Count!=0 || MdfPersonnel.PersonnelMouvements.Count!=0)
            {
                MessageBox.Show("impossible de supprimer il est deja utilisé dans autre enregistrement");

            }
            else
            {
                DbContext.DeletePersonelle(MdfPersonnel.Id);
                MessageBox.Show("personnel supprime");
                DialogResult = true;
            }
        }

        private void BtAnnuler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }
    }
}
