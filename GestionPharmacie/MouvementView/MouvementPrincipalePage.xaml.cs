using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GestionPharmacie.MouvementView
{
    /// <summary>
    /// Logique d'interaction pour MouvementPrincipalePage.xaml
    /// </summary>
    public partial class MouvementPrincipalePage : Page
    {
        public MouvementPrincipalePage()
        {
            InitializeComponent();
            PanierMedicament = new ObservableCollection<Medicament>();
            datagridMedicament.ItemsSource = PanierMedicament;
            DbContext = new AppDataContext();
            listeMedicament = DbContext.GetMedicament();
            listBoxMedicament.ItemsSource = listeMedicament;
            listeMouevemnt = DbContext.GetMouvement();
            datagridMouevements.ItemsSource = listeMouevemnt;
            CombPersonnels.ItemsSource = DbContext.getPersonelle();

             
        }
        ObservableCollection<Medicament> PanierMedicament;
        AppDataContext DbContext;
        List<Medicament> listeMedicament;
        List<Mouvement> listeMouevemnt;
        private void RadioPerosnnel_Checked(object sender, RoutedEventArgs e)
        {
            if (RadioPerosnnel.IsChecked.HasValue)
            {
                if (RadioPerosnnel.IsChecked.Value)
                {
                    lablLieu.Visibility = Visibility.Visible;
                    lablService.Visibility = Visibility.Visible;
                    lablPersonnel.Visibility = Visibility.Visible;
                    txtLieu.Visibility = Visibility.Visible;
                    txtService.Visibility = Visibility.Visible;
                    CombPersonnels.Visibility = Visibility.Visible;
                }
            }
        }

        private void RadioAutre_Checked(object sender, RoutedEventArgs e)
        {
            if (RadioAutre.IsChecked.HasValue)
            {
                if (RadioAutre.IsChecked.Value)
                {

                    lablLieu.Visibility = Visibility.Hidden;
                    lablService.Visibility = Visibility.Hidden;
                    lablPersonnel.Visibility = Visibility.Hidden;
                    txtLieu.Visibility = Visibility.Hidden;
                    txtService.Visibility = Visibility.Hidden;
                    CombPersonnels.Visibility = Visibility.Hidden;
                }
            }
        }

        private void BtnAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(listBoxMedicament.SelectedItem!=null)
            {
                Medicament MedicamentSelectionner = (Medicament)listBoxMedicament.SelectedItem;
                if(PanierMedicament.Where(m=>m.Id==MedicamentSelectionner.Id).FirstOrDefault()!=null)
                {
                    MessageBox.Show("Medicament deja exicte");
                }
                else
                {
                    Medicament MedicamentToPanier = new Medicament(MedicamentSelectionner.Id, MedicamentSelectionner.Designation, MedicamentSelectionner.DateExpiration, 0);
                    PanierMedicament.Add(MedicamentToPanier);
                }
                
            }
        }

        private void BtnSupprimer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Medicament MedicamentSupprimer = (Medicament)((MyControlLibrary.Button)sender).Tag;
            PanierMedicament.Remove(MedicamentSupprimer);
        }

        private void BtnAjouter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string TypeMouvement = "";
                TypeMouvement = CombTypeMouevemnt.Text;
                if (!DpDateMouvement.SelectedDate.HasValue)
                {
                    throw new Exception("La date du mouvement doit etre selectionner");
                }
                if (TypeMouvement == "Sortie")
                {
                    foreach (var item in PanierMedicament)
                    {
                        var Md = listeMedicament.Where(m => m.Id == item.Id && m.QuantiterStock < item.QuantiterStock).FirstOrDefault();
                        if (Md != null)
                            throw new Exception(string.Format("Impossible d'effectuer le mouvement :\n la quantité du {0} en stock est insufusante\ndoit etre inferieur ou egal à {1}", item.Designation, Md.QuantiterStock));
                    }
                }
                Mouvement NvMouvement = new Mouvement(-1, TypeMouvement, DpDateMouvement.SelectedDate.Value);
                int IdMouvement = DbContext.insetrMouvement(NvMouvement);
                if (RadioPerosnnel.IsChecked.Value)
                {
                    if (CombPersonnels.SelectedItem == null)
                        throw new Exception("Le personnel doit etre selectionner");
                    DeclarationMouvement PersoneleMouevement = new DeclarationMouvement(((Personelle)CombPersonnels.SelectedItem).Id
                        , IdMouvement
                        , txtLieu.Text
                        , txtService.Text
                          );
                    DbContext.InsertDeclarationMouvement(PersoneleMouevement);
                }
                DbContext.insertdeteilmouvement(PanierMedicament.ToList(), IdMouvement, TypeMouvement);
                MessageBox.Show("mouevement ajouter");
                listeMouevemnt = DbContext.GetMouvement();
                datagridMouevements.ItemsSource = listeMouevemnt;
                ClearMouvement();

            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }
        }

        private void BtnDetails_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouvement MdfMouevement = (Mouvement)((MyControlLibrary.Button)sender).Tag;
            MouvementView.DetailsMouvementWindow DetailMouevementFenetre = new DetailsMouvementWindow(MdfMouevement);
            if(DetailMouevementFenetre.ShowDialog().Value)
            {
                listeMouevemnt = DbContext.GetMouvement();
                datagridMouevements.ItemsSource = listeMouevemnt;
            }
        }
        private void ClearMouvement()
        {
            RadioAutre.IsChecked = true;
            txtLieu.Text = "";
            txtService.Text = "";
            DpDateMouvement.SelectedDate = null;
            CombPersonnels.SelectedItem = null;
            CombTypeMouevemnt.Text = "";
            PanierMedicament.Clear();
            ExpNvMoueve.IsExpanded = false;
        }

        private void BtnAnnuler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearMouvement();
        }
    }
}
