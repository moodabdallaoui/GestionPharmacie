using GestionPharmacie.Model;
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
using System.Windows.Shapes;

namespace GestionPharmacie.MouvementView
{
    /// <summary>
    /// Logique d'interaction pour DetailsMouvementWindow.xaml
    /// </summary>
    public partial class DetailsMouvementWindow : Window
    {
        public DetailsMouvementWindow(Mouvement MdfMouvemmnt)
        {
            InitializeComponent();
            this.MdfMovuevemnt = MdfMouvemmnt;
            DbContext = new AppDataContext();
            OldDetailMouvement = (from Dm in DbContext.GetDetailMouvement(MdfMovuevemnt.Id)
                                  join m in DbContext.GetMedicament()
                                  on Dm.Idmedicament equals m.Id
                                  select new Medicament() { Id = Dm.Idmedicament, Designation = m.Designation, DateExpiration = m.DateExpiration, QuantiterStock = Dm.Quatiter }).ToList();
            List<Medicament> listeMedicamentMouevemnt = (from Dm in DbContext.GetDetailMouvement(MdfMouvemmnt.Id)
                                                         join m in DbContext.GetMedicament()
                                                         on Dm.Idmedicament equals m.Id
                                                         select new Medicament() {Id=Dm.Idmedicament,Designation=m.Designation,DateExpiration=m.DateExpiration,QuantiterStock=Dm.Quatiter }).ToList();

            if (listeMedicamentMouevemnt == null)
                listeMedicamentMouevemnt = new List<Medicament>();
            PanierMedicament = new ObservableCollection<Medicament>(listeMedicamentMouevemnt);
            datagridMedicament.ItemsSource = PanierMedicament;
            listeMedicament = DbContext.GetMedicament();
            listBoxMedicament.ItemsSource = listeMedicament;
            CombPersonnels.ItemsSource = DbContext.getPersonelle();
            CombTypeMouevemnt.Text = MdfMovuevemnt.Type;
            DpDateMouvement.SelectedDate = MdfMovuevemnt.Date;
            Personelle PersonelConserne = DbContext.GetPersonelByMouvemnt(MdfMouvemmnt.Id);
            if(PersonelConserne!=null)
            {
                RadioPerosnnel.IsChecked = true;
                DeclarationMouvement PMouevement= DbContext.DeclarationMouvement().Where(Pm=>Pm.IdDeclaration==PersonelConserne.Id).First();
                txtLieu.Text = PMouevement.Lieu;
                txtService.Text = PMouevement.Service;
                CombPersonnels.SelectedItem = CombPersonnels.ItemsSource.Cast<Personelle>().Where(p => p.Id == PersonelConserne.Id).First();

            }
           
        }

        

        ObservableCollection<Medicament> PanierMedicament;
        AppDataContext DbContext;
        List<Medicament> listeMedicament;
        Mouvement MdfMovuevemnt;
        List<Medicament> OldDetailMouvement;
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
        private void BtnSupprimerMedicament_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Medicament MedicamentSupprimer = (Medicament)((MyControlLibrary.Button)sender).Tag;
            PanierMedicament.Remove(MedicamentSupprimer);
        }
        private void BtnAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (listBoxMedicament.SelectedItem != null)
            {
                Medicament MedicamentSelectionner = (Medicament)listBoxMedicament.SelectedItem;
                if (PanierMedicament.Where(m => m.Id == MedicamentSelectionner.Id).FirstOrDefault() != null)
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

        private void BtnValider_MouseDown(object sender, MouseButtonEventArgs e)
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
                Mouvement NvMouvement = new Mouvement(MdfMovuevemnt.Id, TypeMouvement, DpDateMouvement.SelectedDate.Value);
                int IdMouvement = MdfMovuevemnt.Id;
                DbContext.UpdateMouvement(NvMouvement);
                if (RadioPerosnnel.IsChecked.Value)
                {
                    if (CombPersonnels.SelectedItem == null)
                        throw new Exception("Le personnel doit etre selectionner");
                    DeclarationMouvement PersoneleMouevement = new DeclarationMouvement(((Personelle)CombPersonnels.SelectedItem).Id
                        , IdMouvement
                        , txtLieu.Text
                        , txtService.Text
                          );
                    DbContext.deleteDeclarationMouvement(IdMouvement);
                    DbContext.InsertDeclarationMouvement(PersoneleMouevement);
                }
                else
                {
                    DbContext.deleteDeclarationMouvement(IdMouvement);
                }
                 
                DbContext.DeleteDeteilmouvement(OldDetailMouvement, MdfMovuevemnt.Id, MdfMovuevemnt.Type);
                DbContext.insertdeteilmouvement(PanierMedicament.ToList(), IdMouvement, TypeMouvement);
                MessageBox.Show("Mouvement Modifier");
                DialogResult = true;

            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }
        }

        private void BtnSupprimerMouvement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DbContext.deleteDeclarationMouvement(MdfMovuevemnt.Id);
            DbContext.DeleteMouvement(MdfMovuevemnt, OldDetailMouvement);
            DialogResult = true;
        }

        private void BtnAnnuler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }
    }
}
