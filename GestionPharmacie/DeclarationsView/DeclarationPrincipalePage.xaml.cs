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

namespace GestionPharmacie.DeclarationsView
{
    /// <summary>
    /// Logique d'interaction pour NouvelleDeclaration.xaml
    /// </summary>
    public partial class NouvelleDeclaration : Page
    {
        public NouvelleDeclaration()
        {
            InitializeComponent();
            DbContext = new AppDataContext();
            datagridDeclaration.ItemsSource = DbContext.GetDeclaration();
            CombPersonnels.ItemsSource = DbContext.getPersonelle();
            CombMedecin.ItemsSource = DbContext.GetMedecint();
            CreateChart();
            WindowHost.Child = chart1;
            var res = from d in DbContext.GetDeclaration()
                      group d by d.DateConsultation.Year into gr
                      select new { Annee = gr.Key, Nb = gr.Count() };
            if(res!=null)
                foreach (var item in res)
                {
                    chart1.Series["NombreDeclaration"].Points.AddXY(item.Annee, item.Nb);
                }
            

        }
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        void CreateChart()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "NombreDeclaration";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(461, 261);
            this.chart1.TabIndex = 0;
        }
        AppDataContext DbContext;
        private void BtnDetails_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Declaration MdfDeclaration = (Declaration)((MyControlLibrary.Button)sender).Tag;
            DeclarationsView.DetailDeclaration DetailDeclarationWindow = new DetailDeclaration(MdfDeclaration);
            if (DetailDeclarationWindow.ShowDialog().Value)
            {
                datagridDeclaration.ItemsSource = DbContext.GetDeclaration();
            }
        }
        private void Cleardeclaration()
        {
            CombPersonnels.Text = "";
            CombTypeDeclaration.Text = "";
            CombMedecin.Text = "";
            DpDateConsultation.SelectedDate = null;
            txtTotalFrais.Clear();
            txtNumCertificat.Clear();
            txtNomPrenomMalade.Clear();
            txtNatureMaladie.Clear();
            txtAgeMalade.Clear();
            CombLienParente.Text = "";


        }
        private void BtnAjouter_MouseDown(object sender, MouseButtonEventArgs e)
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
                Declaration NvDeclaration = new Declaration(-1, ((Personelle)CombPersonnels.SelectedItem).Id,
                    ((Medecin)CombMedecin.SelectedItem).Id,
                    CombTypeDeclaration.Text,
                    totalFraisEngage,
                    DpDateConsultation.SelectedDate.Value,
                    txtNomPrenomMalade.Text,
                    ageMalade,
                    CombLienParente.Text,
                    txtNatureMaladie.Text,
                    txtNumCertificat.Text);
                DbContext.insertDeclaration(NvDeclaration);
                datagridDeclaration.ItemsSource = DbContext.GetDeclaration();
                MessageBox.Show("Declaration bien ajouter");
                Cleardeclaration();
                ExpNvDeclaration.IsExpanded = false;
            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message);
            }    
            
            
        }
        private void BtAnnuler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cleardeclaration();
            ExpNvDeclaration.IsExpanded = false;
        }
    }
}
