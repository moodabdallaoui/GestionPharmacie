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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestionPharmacie.Model;

namespace GestionPharmacie.PersonnelsView
{
    /// <summary>
    /// Logique d'interaction pour PersonnelPrincipalePage.xaml
    /// </summary>
    public partial class PersonnelPrincipalePage : Page
    {
        public PersonnelPrincipalePage()
        {
            InitializeComponent();
            DbContext = new AppDataContext();
            listePersonnel = DbContext.getPersonelle();
            datagridPersonnels.ItemsSource = listePersonnel;

        }
        AppDataContext DbContext = new AppDataContext();

        private void BtnEdition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Personelle MdfPersonnel= (Personelle)((MyControlLibrary.Button)sender).Tag;
            PersonnelsView.ModifierPersonnel MdfPersonnelWindow= new PersonnelsView.ModifierPersonnel(MdfPersonnel);
            if(MdfPersonnelWindow.ShowDialog().Value)
            {
                listePersonnel = DbContext.getPersonelle();
                datagridPersonnels.ItemsSource = listePersonnel;
            }
        }

        private void BtnDetails_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Declaration MdfDeclaration = (Declaration)((MyControlLibrary.Button)sender).Tag;
            DeclarationsView.DetailDeclaration detailsDeclaration = new DeclarationsView.DetailDeclaration(MdfDeclaration);
            if (detailsDeclaration.ShowDialog().Value)
            {
                listePersonnel = DbContext.getPersonelle();
                datagridPersonnels.ItemsSource = listePersonnel;
            }
        }
        List<Personelle> listePersonnel;
        private void BtnDetailsMouvement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouvement MdfMouevement = (Mouvement)((MyControlLibrary.Button)sender).Tag;
            MouvementView.DetailsMouvementWindow DetailMouevementFenetre = new MouvementView.DetailsMouvementWindow(MdfMouevement);
            if (DetailMouevementFenetre.ShowDialog().Value)
            {
                listePersonnel = DbContext.getPersonelle();
                datagridPersonnels.ItemsSource = listePersonnel;
            }
        }

        private void BtnAjouter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (listePersonnel.Where(p => p.Matricule == txtMatricule.Text).FirstOrDefault() != null)
                {
                    throw new Exception("Le matricule doit etre unique");
                }
                if (listePersonnel.Where(p => p.NumContrat == txtNumContrat.Text).FirstOrDefault() != null)
                {
                    throw new Exception("Le numero du contrat doit etre unique");
                }
                if (!DpDateContrat.SelectedDate.HasValue)
                {
                    throw new Exception("Le date du contrat doit etre selectionner");
                }
                Personelle NvPersonnel = new Personelle(-1, txtMatricule.Text,
                                                         txtNom.Text,
                                                         txtPrenom.Text,
                                                         txtTelephone.Text,
                                                         CombSituation.Text,
                                                         txtAdresse.Text,
                                                         txtNumContrat.Text,
                                                         DpDateContrat.SelectedDate.Value);
                DbContext.insertPersonelle(NvPersonnel);
                listePersonnel = DbContext.getPersonelle();
                datagridPersonnels.ItemsSource = listePersonnel;
                MessageBox.Show("Personnel bien ajouter");
                ClearPersonnel();
                ExpNvPerso.IsExpanded = false;

            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }
        }

        private void BtAnnuler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearPersonnel();
            ExpNvPerso.IsExpanded = false;
        }

        private void BtnRechercher_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (combChoixRecherche.Text)
            {
                case "": datagridPersonnels.ItemsSource = listePersonnel;
                    break;
                case "Nom": datagridPersonnels.ItemsSource = listePersonnel.Where(p => p.Nom.Contains(txtRecherche.Text));
                    break;
                case "Prenom":
                    datagridPersonnels.ItemsSource = listePersonnel.Where(p => p.Prenom.Contains(txtRecherche.Text));
                    break;
                case "Matricule":
                    datagridPersonnels.ItemsSource = listePersonnel.Where(p => p.Matricule==txtRecherche.Text);
                    break;
                case "N° contrat":
                    datagridPersonnels.ItemsSource = listePersonnel.Where(p => p.NumContrat == txtRecherche.Text);
                    break;
                default:
                    break;
            }
        }

        private void BtnTous_MouseDown(object sender, MouseButtonEventArgs e)
        {
            datagridPersonnels.ItemsSource = listePersonnel;
        }

        private void ClearPersonnel()
        {
            txtMatricule.Clear();
            txtAdresse.Clear();
            txtNom.Clear();
            txtNumContrat.Clear();
            txtPrenom.Clear();
            txtTelephone.Clear();
            CombSituation.Text = "";
            DpDateContrat.SelectedDate = DateTime.Now;


        }
    }
}
