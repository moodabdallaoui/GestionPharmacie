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

namespace GestionPharmacie.DeclarationsView
{
    /// <summary>
    /// Logique d'interaction pour DetailDeclaration.xaml
    /// </summary>
    public partial class DetailDeclaration : Window
    {
        public DetailDeclaration(Declaration MdfDeclaration)
        {
            InitializeComponent();
            this.MdfDeclaration = MdfDeclaration;
            DbContext = new AppDataContext();
            List<Personelle> listePersonnel = DbContext.getPersonelle();
            CombPersonnels.ItemsSource = listePersonnel;
            CombPersonnels.SelectedItem = listePersonnel.Where(p => p.Id == MdfDeclaration.IdPersonnel).First();
            List<Medecin> listeMedecin = DbContext.GetMedecint();
            CombMedecin.ItemsSource = listeMedecin;
            CombMedecin.SelectedItem = listeMedecin.Where(m => m.Id == MdfDeclaration.IdMedecin).First();
            CombTypeDeclaration.Text = MdfDeclaration.TypeDeclaration;
            txtTotalFrais.Text = MdfDeclaration.TotalFraisEngage.ToString();
            DpDateConsultation.SelectedDate = MdfDeclaration.DateConsultation;
            txtNomPrenomMalade.Text = MdfDeclaration.NomPrenomMalade;
            txtNatureMaladie.Text = MdfDeclaration.NatureMaladie;
            txtNumCertificat.Text = MdfDeclaration.NumCertificat;
            txtAgeMalade.Text = MdfDeclaration.AgeMalade.ToString();
            CombLienParente.Text = MdfDeclaration.LienParente;


        }
        AppDataContext DbContext;
        Declaration MdfDeclaration;
        private void BtnTelecharger_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GenererDeclarationDuMaladie Doc = new GenererDeclarationDuMaladie();
            System.Windows.Forms.SaveFileDialog SvFile = new System.Windows.Forms.SaveFileDialog();
            SvFile.FileName = MdfDeclaration.Personnel + "Declaration.docx";
            if(SvFile.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            Doc.Telecharger(@"C:\Users\Mouad-Pc\Documents\Visual Studio 2015\Projects\GestionPharmacie\GestionPharmacie\bin\Debug\DECLARATION-DE-MALADIE.docx", SvFile.FileName, MdfDeclaration);
        }

        private void BtnValider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int ageMalade;
                double totalFraisEngage;
                if (CombPersonnels.SelectedItem == null)
                {
                    throw new Exception("Selectionner un personnel ");
                }
                if (CombMedecin.SelectedIndex == -1)
                {
                    throw new Exception("Selectionner un médecin");
                }
                if (!DpDateConsultation.SelectedDate.HasValue)
                {
                    throw new Exception("La date de consultation doit etre selectionner");
                }
                if (!double.TryParse(txtTotalFrais.Text, out totalFraisEngage))
                {
                    throw new Exception("Total frais engagé doit etre numerique");
                }
                if (!int.TryParse(txtAgeMalade.Text, out ageMalade))
                {
                    throw new Exception("l'age doit etre numerique");
                }
                Declaration NvDeclaration = new Declaration(MdfDeclaration.Id, 
                    ((Personelle)CombPersonnels.SelectedItem).Id,
                    ((Medecin)CombMedecin.SelectedItem).Id,
                    CombTypeDeclaration.Text,
                    totalFraisEngage,
                    DpDateConsultation.SelectedDate.Value,
                    txtNomPrenomMalade.Text,
                    ageMalade,
                    CombLienParente.Text,
                    txtNatureMaladie.Text,
                    txtNumCertificat.Text);
                DbContext.UpdateDeclaration(NvDeclaration);
                MessageBox.Show("Declaration modifier");
                DialogResult = true;
            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }
        }

        private void BtnSupprimer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DbContext.deleteDeclaration(MdfDeclaration.Id);
            MessageBox.Show("Declaration supprimer");
            DialogResult = true;
        }

        private void BtFermer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }
    }
}
