using System;
using System.Collections.Generic;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Diagnostics;
using System.Windows;

namespace  GestionPharmacie.Model
{
    public class GenererDeclarationDuMaladie
    {

        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText,
                        ref matchCase, ref matchWholeWord,
                        ref matchWildCards, ref matchSoundLike,
                        ref nmatchAllForms, ref forward,
                        ref wrap, ref format, ref replaceWithText,
                        ref replace, ref matchKashida,
                        ref matchDiactitics, ref matchAlefHamza,
                        ref matchControl);
        }
        private void CreateWordDocument(object filename, object savaAs, Declaration declaration)
        {
            List<int> processesbeforegen = getRunningProcesses();
            object missing = Missing.Value;


            Word.Application wordApp = new Word.Application();

            Word.Document aDoc = null;

            if (File.Exists((string)filename))
            {
                DateTime today = DateTime.Now;

                object readOnly = false; //default
                object isVisible = false;

                wordApp.Visible = false;

                aDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing, ref missing);

                aDoc.Activate();

                //Find and replace:
                this.FindAndReplace(wordApp, "<ID>",declaration.Id.ToString() );
                this.FindAndReplace(wordApp, "<Nom>", declaration.Personnel.Nom);
                this.FindAndReplace(wordApp, "<prenom>",declaration.Personnel.Prenom);
                this.FindAndReplace(wordApp, "<totalfrais>", declaration.TotalFraisEngage.ToString());
                this.FindAndReplace(wordApp, "<dateConsultation>", declaration.DateConsultation.ToShortDateString());
                this.FindAndReplace(wordApp, "<nomM  prenomM>", declaration.NomPrenomMalade);
                this.FindAndReplace(wordApp, "<AgeM>",declaration.AgeMalade.ToString());
                this.FindAndReplace(wordApp, "<NatureMaladie>", declaration.NatureMaladie);
                this.FindAndReplace(wordApp, "<NCertificat>",declaration.NumCertificat);
                this.FindAndReplace(wordApp, "<NContrat>",declaration.Personnel.NumContrat);
                if(declaration.LienParente== "lui-même")
                {
                    this.FindAndReplace(wordApp, "< lui-même>", "√");
                    this.FindAndReplace(wordApp, "< conjoint >", " ");
                    this.FindAndReplace(wordApp, "< Enfant >", " ");
                }
                else if (declaration.LienParente == "Conjoint")
                {
                    this.FindAndReplace(wordApp, "< lui-même>", " ");
                    this.FindAndReplace(wordApp, "< conjoint >", "√");
                    this.FindAndReplace(wordApp, "< Enfant >", " ");
                }
                else if(declaration.LienParente == "Enfant")
                {
                    this.FindAndReplace(wordApp, "< lui-même>", " ");
                    this.FindAndReplace(wordApp, "< conjoint >", " ");
                    this.FindAndReplace(wordApp, "< Enfant >", "√");
                }
                else
                {
                    this.FindAndReplace(wordApp, "< lui-même>", " ");
                    this.FindAndReplace(wordApp, "< conjoint >", " ");
                    this.FindAndReplace(wordApp, "< Enfant >", " ");
                }
                if(declaration.TypeDeclaration== "Medicament")
                {
                    this.FindAndReplace(wordApp, "<m>", "√");
                    this.FindAndReplace(wordApp, "<d>", " ");
                    this.FindAndReplace(wordApp, "<o>", " ");
                }
                else if (declaration.TypeDeclaration == "Optique")
                {
                    this.FindAndReplace(wordApp, "<m>", " ");
                    this.FindAndReplace(wordApp, "<d>", " ");
                    this.FindAndReplace(wordApp, "<o>", "√");
                }
                else if (declaration.TypeDeclaration == "Dentaire")
                {
                    this.FindAndReplace(wordApp, "<m>", " ");
                    this.FindAndReplace(wordApp, "<d>", "√");
                    this.FindAndReplace(wordApp, "<o>", " ");
                }
                else
                {
                    this.FindAndReplace(wordApp, "<m>", " ");
                    this.FindAndReplace(wordApp, "<d>", " ");
                    this.FindAndReplace(wordApp, "<o>", " ");
                }
                this.FindAndReplace(wordApp, "<medecin>", declaration.Medecin.ToString());






            }
            else
            {
                MessageBox.Show("Le model de declaration n'exicte pas");
                return;
            }

            //Save as: filename
            aDoc.SaveAs2(ref savaAs, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

            //Close Document:
            //aDoc.Close(ref missing, ref missing, ref missing);

            MessageBox.Show("Telechargement réussit");
            List<int> processesaftergen = getRunningProcesses();
            killProcesses(processesbeforegen, processesaftergen);
        }
        public List<int> getRunningProcesses()
        {
            List<int> ProcessIDs = new List<int>();
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (Process.GetCurrentProcess().Id == clsProcess.Id)
                    continue;
                if (clsProcess.ProcessName.Contains("WINWORD"))
                {
                    ProcessIDs.Add(clsProcess.Id);
                }
            }
            return ProcessIDs;
        }
        private void killProcesses(List<int> processesbeforegen, List<int> processesaftergen)
        {
            foreach (int pidafter in processesaftergen)
            {
                bool processfound = false;
                foreach (int pidbefore in processesbeforegen)
                {
                    if (pidafter == pidbefore)
                    {
                        processfound = true;
                    }
                }

                if (processfound == false)
                {
                    Process clsProcess = Process.GetProcessById(pidafter);
                    clsProcess.Kill();
                }
            }
        }
        public void Telecharger(string fileName,string SaveAs, Declaration Declaration)
        {

            CreateWordDocument(fileName, SaveAs,Declaration);
        }
    }
}
