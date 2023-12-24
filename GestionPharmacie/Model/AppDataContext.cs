using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace GestionPharmacie.Model
{
    class AppDataContext
    {


        //SELECT
        string ChaineConnexion = "data source=AppData.db";
        public AppDataContext()
        {
        }
        public bool VerifiTrial()
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(ChaineConnexion))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("select * from admin", con))
                    {
                        con.Open();
                        DateTime dateFin = new DateTime();
                        string KeyOriginal = "AZSEDRGFTHYHKF?34R3467722134";
                        string keyEnregistrer = "";
                        SQLiteDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            dateFin = DateTime.Parse(dr[1].ToString());
                            keyEnregistrer = dr[0].ToString();
                        }
                        dr.Close();
                        con.Close();
                        if ((DateTime.Now <= dateFin && DateTime.Now>=DateTime.Parse("16/08/2016")) || keyEnregistrer == KeyOriginal)
                            return true;
                        else
                            return false;
                    }

                }
            }
            catch (Exception )
            {

                return false;
            }
            
        }
        public void  EntrerKey(string Key)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(ChaineConnexion))
                {
                    string req = "update admin set key='" + Key + "';";
                    using (SQLiteCommand cmd = new SQLiteCommand(req, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        
                    }

                }
            }
            catch (Exception)
            {

      
            }
        }
        //GeTmouvement
        public List<Mouvement> GetMouvement()
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                List<Mouvement> Mouvement = new List<Mouvement>();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Mouvement; ", cn);

            cn.Open();
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
                Mouvement.Add(new Mouvement(dr.GetInt32(0), dr[1].ToString(),DateTime.Parse(dr[2].ToString())));

            return Mouvement;


            }
           
        }
        //getMedicament
        public List<Medicament> GetMedicament()
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            { List<Medicament> Medicament = new List<Medicament>();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Medicament; ", cn);

            cn.Open();
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
                Medicament.Add(new Medicament(dr.GetInt32(0),dr[1].ToString(),DateTime.Parse(dr[2].ToString()),dr.GetInt32(3)));

            return Medicament;

            }
           
         }
        //getMedecin
        public List<Medecin> GetMedecint()
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                List<Medecin> Medecin = new List<Medecin>();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Medecin; ", cn);

            cn.Open();
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
                Medecin.Add(new Medecin(dr.GetInt32(0),dr[1].ToString(),dr[2].ToString(),dr[3].ToString()));

            return Medecin;

            }
            
        }
        //getDetaillMouvement
        public List<DetailMouvement> GetDetailMouvement(int IdMouevemnt)
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                List<DetailMouvement> DetailMouvement = new List<DetailMouvement>();

            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM DetailsMouvement where idmouvement={0} ; ",IdMouevemnt), cn);

            cn.Open();
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
                DetailMouvement.Add(new DetailMouvement(dr.GetInt32(0),dr.GetInt32(1),dr.GetInt32(2)));

            return DetailMouvement;

            }
            
        }
        public List<DetailMouvement> GetDetailMouvement()
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                List<DetailMouvement> DetailMouvement = new List<DetailMouvement>();

                SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM DetailsMouvement  ; "), cn);

                cn.Open();
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    DetailMouvement.Add(new DetailMouvement(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2)));

                return DetailMouvement;

            }

        }
        //getDeclarationMouvement
        public List<DeclarationMouvement> DeclarationMouvement()
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                List<DeclarationMouvement> DeclarationMouvement = new List<DeclarationMouvement>();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM DeclarationMouvement; ", cn);

            cn.Open();
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
                DeclarationMouvement.Add(new DeclarationMouvement(dr.GetInt32(0), dr.GetInt32(1), dr[2].ToString(),dr[3].ToString()));

            return DeclarationMouvement;

            }
            
        }
        //GetDeclaration 
        public List<Declaration> GetDeclaration()
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {List<Declaration> Declaration = new List<Declaration>();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Declaration; ", cn);

            cn.Open();
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
                {
                    Declaration d = new Declaration();
                    d.Id = dr.GetInt32(0);
                    d.IdPersonnel= dr.GetInt32(1);
                    d.IdMedecin= dr.GetInt32(2);
                    d.TypeDeclaration = dr.GetString(3);
                    d.TotalFraisEngage = double.Parse(dr[4].ToString());
                    DateTime dateConsultation;
                    if(DateTime.TryParse(dr[5].ToString(),out dateConsultation))
                        d.DateConsultation = dateConsultation;
                    d.NomPrenomMalade = dr[6].ToString();
                    d.AgeMalade = dr.GetInt32(7);
                    d.LienParente = dr[8].ToString();
                    d.NatureMaladie = dr[9].ToString();
                    d.NumCertificat= dr[10].ToString();
                    Declaration.Add(d);
                }

            return Declaration;

            }
            
        }
        //getPersonelle
        public List<Personelle> getPersonelle()
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
            List<Personelle> Personelle = new List<Personelle>();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Personnel; ", cn);

            cn.Open();
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
                Personelle.Add(new Personelle(dr.GetInt32(0),dr[1].ToString(),dr[2].ToString(),dr[3].ToString(),dr[4].ToString(),dr[5].ToString(),dr[6].ToString(),dr[7].ToString(),DateTime.Parse(dr[8].ToString())));
                dr.Close();
                cn.Close();


            return Personelle;
            }
            
        }
        public Personelle GetPersonelByMouvemnt(int IdMouvement)
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                Personelle Personelle=null;

                SQLiteCommand cmd = new SQLiteCommand(string.Format(@"select id,matricule,nom,prenom,tel,situation,adresse,numcontrat,datecontrat from personnel ,declarationmouvement where
                                          id = iddeclaration and idmouvement = {0} ",IdMouvement), cn);

                cn.Open();
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                    Personelle=new Personelle(dr.GetInt32(0), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), DateTime.Parse(dr[8].ToString()));
                dr.Close();
                cn.Close();


                return Personelle;
            }

        }
        public Personelle GetPersonnelById(int id)
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                Personelle Personelle = new Personelle();

                SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM Personnel where id={0}; ",id), cn);

                cn.Open();
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                    Personelle=new Personelle(dr.GetInt32(0), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), DateTime.Parse(dr[8].ToString()));
                dr.Close();
                cn.Close();


                return Personelle;
            }
        }
        public Medecin GetMedecinById(int id)
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                Medecin Medecin = new Medecin();

                SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM Medecin where id={0}; ",id), cn);

                cn.Open();
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                    Medecin=new Medecin(dr.GetInt32(0), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());

                return Medecin;

            }

        }
        public Seuil GetSeuil()
        {
            Seuil s = new Seuil();
            using (SQLiteConnection con=new SQLiteConnection(ChaineConnexion))
            {
                string req = "select * from seuil ;";
                using (SQLiteCommand cmd=new SQLiteCommand(req,con))
                {
                    con.Open();
                    using (SQLiteDataReader dr=cmd.ExecuteReader())
                    {
                        if(dr.Read())
                        {
                            s.Nbjour = dr.GetInt32(0);
                            s.NbQuantite = dr.GetInt32(1);
                        }
                        dr.Close();
                        con.Close();

                    }
                }
            }
            return s;
        }
        public void UpdateSeuil(Seuil seuil)
        {
            using (SQLiteConnection con = new SQLiteConnection(ChaineConnexion))
            {
                string req = string.Format("update seuil set NbJour ={0},NbQuantite={1};"
                    ,seuil.Nbjour,seuil.NbQuantite);
                using (SQLiteCommand cmd = new SQLiteCommand(req, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<DeclarationMouvement> GetDeclarationMouvementByPersonnel(int id)
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                List<DeclarationMouvement> DeclarationMouvement = new List<DeclarationMouvement>();

                SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM DeclarationMouvement where idDeclaration={0}; ",id), cn);

                cn.Open();
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    DeclarationMouvement.Add(new DeclarationMouvement(dr.GetInt32(0), dr.GetInt32(1), dr[2].ToString(), dr[3].ToString()));

                return DeclarationMouvement;

            }
        }
        public List<Declaration> GetDeclarationByPersonnel(int id)
        {
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                List<Declaration> Declaration = new List<Declaration>();

                SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT * FROM Declaration where idPersonnel={0}  ; ",id), cn);

                cn.Open();
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Declaration d = new Declaration();
                    d.Id = dr.GetInt32(0);
                    d.IdPersonnel = dr.GetInt32(1);
                    d.IdMedecin = dr.GetInt32(2);
                    d.TypeDeclaration = dr.GetString(3);
                    d.TotalFraisEngage = double.Parse(dr[4].ToString());
                    DateTime dateConsultation;
                    if (DateTime.TryParse(dr[5].ToString(), out dateConsultation))
                        d.DateConsultation = dateConsultation;
                    d.NomPrenomMalade = dr[6].ToString();
                    d.AgeMalade = int.Parse(dr[7].ToString());
                    d.LienParente = dr[8].ToString();
                    d.NatureMaladie = dr[9].ToString();
                    d.NumCertificat = dr[10].ToString();
                    Declaration.Add(d);
                }

                return Declaration;

            }
        }
        public List<Mouvement> GetMouvementByPersonnel(int id)
        {
            string Req = string.Format(@"select mouvement.id,[type],mouvement.date from mouvement , declarationmouvement where
                                       mouvement.id = declarationmouvement.idmouvement and declarationmouvement.iddeclaration ={0}
            ", id);
            using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
            {
                List<Mouvement> Mouvement = new List<Mouvement>();

                SQLiteCommand cmd = new SQLiteCommand(Req, cn);

                cn.Open();
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    Mouvement.Add(new Mouvement(dr.GetInt32(0), dr[1].ToString(), DateTime.Parse(dr[2].ToString())));

                return Mouvement;


            }

        }
        

        //INSERT 

        //insret Personelle 
        public bool insertPersonelle(Personelle personelle)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                string req;
                    req = @"INSERT INTO `Personnel`(`Matricule`
                                                    ,`Nom`
                                                    ,`Prenom`
                                                    ,`Tel`
                                                    ,`Situation`,
                                                    `Adresse`,
                                                    `NumContrat`,
                                                    `DateContrat`)
                VALUES ('" + personelle.Matricule + @"'
                        ,'" + personelle.Nom + @"'
                        ,'" + personelle.Prenom + @"'
                        ,'" + personelle.Tel + @"',
                        '" + personelle.Situation + @"',
                        '" + personelle.Adresse + @"'
                        ,'" + personelle.NumContrat + @"'
                        ,'" + personelle.DateContrat.ToShortDateString() + "');";

                SQLiteCommand cmd = new SQLiteCommand(req, cn);
                
                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cn.Close();
                        return true;
                    }
                    return false;
                }
               
            }
            catch (Exception)
            {
                return false;

            }

        }
        //insert Medicament
        public bool insertMedicament(Medicament medicament)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
string req;
               
                 req=   @"INSERT INTO `Medicament`(`Designation`
                                                ,`DateExpiration`
                                                ,`QuantiteStock`) 
                                           VALUES ('" + medicament.Designation + 
                                           "','" + medicament.DateExpiration.ToShortDateString() +
                                           "'," + medicament.QuantiterStock + 
                                           ");";
                    
              SQLiteCommand cmd = new SQLiteCommand(req, cn);
                
                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                 return false;
                }
              
            }
            catch (Exception)
            {
                return false;

            }

        }
        //insert medecin
        public bool insertmedecin(Medecin medecin)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
SQLiteCommand cmd = new SQLiteCommand(@"INSERT INTO `Medecin`(`Nom`,`Prenom`,`Adresse`) VALUES ('"+medecin.Nom+"','"+medecin.Prenom+"','"+medecin.Adresse+"');", cn);

                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                return false;


                }
               
            }
            catch (Exception)
            {
                return false;

            }

        }
        //insert deteilmouvement
        public bool insertdeteilmouvement(List<Medicament> listMedicament,int IdMouvement,string TypeMouvement)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    cn.Open();
                    
                    string QueryInsert = "";
                    string QueryUpdate = "";

                    foreach (var item in listMedicament)
                    {
                        QueryInsert=QueryInsert + string.Format(@"INSERT INTO `DetailsMouvement`
                                (`IdMouvement`,`IdMedicament`,`Quantite`)
                                    VALUES ({0},{1},{2});",IdMouvement,
                                                          item.Id,item.QuantiterStock );
                        if (TypeMouvement == "Entrée")
                            QueryUpdate = QueryUpdate + string.Format("UPDATE `Medicament` set `QuantiteStock`=`QuantiteStock`+ {0} where id={1} ; "
                                                                      , item.QuantiterStock, item.Id );
                        else
                            QueryUpdate = QueryUpdate + string.Format("UPDATE `Medicament` set `QuantiteStock`=`QuantiteStock`-{0} where id={1};  "
                                                                       , item.QuantiterStock, item.Id);
                    }
                    SQLiteCommand cmd = new SQLiteCommand(QueryInsert+QueryUpdate, cn);
                    cmd.ExecuteNonQuery();
                        cn.Close();
                        return true;
                }


            }
            catch (Exception)
            {
               
                return false;

            }
        }
        public bool DeleteDeteilmouvement(List<Medicament> listMedicament, int IdMouvement, string TypeMouvement)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    cn.Open();

                    string QueryDelete = "";
                    string QueryUpdate = "";

                    foreach (var item in listMedicament)
                    {
                        QueryDelete = QueryDelete + string.Format(@"delete from detailsmouvement
                                        where idmouvement={0} and idmedicament={1};", IdMouvement,
                                                          item.Id);
                        if (TypeMouvement == "Entrée")
                            QueryUpdate = QueryUpdate + string.Format("UPDATE `Medicament` set `QuantiteStock`=`QuantiteStock`- {0} where id={1} ; "
                                                                      , item.QuantiterStock, item.Id);
                        else
                            QueryUpdate = QueryUpdate + string.Format("UPDATE `Medicament` set `QuantiteStock`=`QuantiteStock`+{0} where id={1};  "
                                                                       , item.QuantiterStock, item.Id);
                    }
                    SQLiteCommand cmd = new SQLiteCommand(QueryDelete + QueryUpdate, cn);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    return true;
                }


            }
            catch (Exception)
            {

                return false;

            }
        }

        public bool insertdeteilmouvement (DetailMouvement DetailMouvement)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {

                SQLiteCommand cmd = new SQLiteCommand(@"INSERT INTO `DetailsMouvement`(`IdMouvement`,`IdMedicament`,`Quantite`) VALUES ("+DetailMouvement.Idmouvement+","+DetailMouvement.Idmedicament+","+DetailMouvement.Quatiter+");", cn);

                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                return false;


                }
                

            }
            catch (Exception)
            {
                return false;

            }

        }
        //insert DeclarationMouvement
        public bool InsertDeclarationMouvement(DeclarationMouvement DeclarationMouvement)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {

                SQLiteCommand cmd = new SQLiteCommand(@"INSERT INTO `DeclarationMouvement`(`IdDeclaration`,`IdMouvement`,`Lieu`,`Service`) VALUES ("+DeclarationMouvement.IdDeclaration+","+DeclarationMouvement.IdMouvement+",'"
                    +DeclarationMouvement.Lieu+"','"+DeclarationMouvement.Service+"');", cn);

                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                return false;


                }

            }
            catch (Exception)
            {
                return false;

            }

        }
        //insert Declaration 
        public bool insertDeclaration(Declaration Declaration)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                { string req;
                if (Declaration.DateConsultation == null)
                    req = @"INSERT INTO `Declaration`(`IdPersonnel`,`IdMedecin`,`TypeDeclaration`,`TotalFraisEngage`,`DateConsultation`,`NomPrenomMalade`,`AgeMalade`,`LienParente`,`NatureMaladie`,`NumCertificat`) 
                 VALUES (" + Declaration.IdPersonnel + "," + Declaration.IdMedecin + ",'" + Declaration.TypeDeclaration + "'," + Declaration.TotalFraisEngage + ",'','" + Declaration.NomPrenomMalade + "'," + Declaration.AgeMalade + ",'" + Declaration.LienParente + "','" + Declaration.NatureMaladie + "','" + Declaration.NumCertificat + "');";
                else
                    req = @"INSERT INTO `Declaration`(`IdPersonnel`,`IdMedecin`,`TypeDeclaration`,`TotalFraisEngage`,`DateConsultation`,`NomPrenomMalade`,`AgeMalade`,`LienParente`,`NatureMaladie`,`NumCertificat`) 
                 VALUES (" + Declaration.IdPersonnel + "," + Declaration.IdMedecin + ",'" + Declaration.TypeDeclaration + "'," + Declaration.TotalFraisEngage + ",'" + Declaration.DateConsultation.ToShortDateString() + "','" + Declaration.NomPrenomMalade + "'," + Declaration.AgeMalade + ",'" + Declaration.LienParente + "','" + Declaration.NatureMaladie + "','" + Declaration.NumCertificat + "');";

                SQLiteCommand cmd = new SQLiteCommand(req, cn);

                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                return false;


                }
               
            }
            catch (Exception)
            {
                return false;

            }

        }
        //insert mouvement
        public int insetrMouvement (Mouvement Mouvement)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    string req;
                    int id=-1;
                    string req1 = "SELECT last_insert_rowid();";
                    req= "INSERT INTO `Mouvement`(`Type`,`Date`) VALUES ('" + Mouvement.Type + "','" + Mouvement.Date.ToShortDateString() + "');";
                    SQLiteCommand cmd = new SQLiteCommand(req+req1, cn);
                    cn.Open();            
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        id = dr.GetInt32(0);
                        dr.Close();
                        return id;
                    }
                    else
                        return -1;
                

                }


            }
            catch (Exception)
            {
                return -1;

            }

        }

        //update 

        //Update personelle
        public bool Updatepersonelle(Personelle Personelle)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    string req ;
                    req = @"UPDATE `Personnel` SET `Nom`='" + Personelle.Nom + @"' 
                                                   , prenom='" + Personelle.Prenom + @"'
                                                   ,tel='" + Personelle.Tel + @"'
                                                   ,Situation='" + Personelle.Situation + @"'
                                                   ,Adresse='" + Personelle.Adresse + @"'
                                                   ,NumContrat='" + Personelle.NumContrat + @"'
                                                   ,DateContrat='" + Personelle.DateContrat.ToShortDateString() 
                                               + "' WHERE id=" + Personelle.Id +";";
                
                     SQLiteCommand cmd = new SQLiteCommand(req, cn);
                   
                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cn.Close();
                        return true;
                    }
                    return false;
                
                }
              
            }
            catch (Exception)
            {
                return false;

            }
        }
        //update Mouvement
        public bool UpdateMouvement(Mouvement mouvement)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
 string req  ;
                if (mouvement.Date == null)
                    req = @"UPDATE `Mouvement` SET `Type`='" + mouvement.Type + "',date ='' WHERE `_rowid_`= '" + mouvement.Id + "';";
                else
                  req=  @"UPDATE `Mouvement` SET `Type`='" + mouvement.Type + "' ,date = '" + mouvement.Date.ToShortDateString() + "'WHERE `_rowid_`= '" + mouvement.Id + "';";

                SQLiteCommand cmd = new SQLiteCommand(req, cn);

                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                return false;

                }
               
            }
            catch (Exception)
            {
                return false;

            }
        }
        //update medicament
        public bool UpdateMedicament(Medicament medicament)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                   string req;
                    req = @" UPDATE `Medicament` SET `Designation`='"
                    + medicament.Designation + "',`DateExpiration`='"
                    +medicament.DateExpiration.ToShortDateString()+
                    "' ,`QuantiteStock`=" 
                    + medicament.QuantiterStock +" WHERE `id`= "+ medicament.Id +"; ";

                   SQLiteCommand cmd = new SQLiteCommand(req, cn);

                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                return false;

                }
                
            }
            catch (Exception)
            {
                return false;

            }
        }
        //update Medecin
        public bool UpdateMedecin(Medecin medecin)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    string req = string.Format("UPDATE `Medecin` SET `Nom`='{0}',`Prenom`='{1}', `Adresse`='{2}' WHERE `Id`={3};",
                        medecin.Nom, medecin.Prenom, medecin.Adresse, medecin.Id);
                SQLiteCommand cmd = new SQLiteCommand(req, cn);

                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                else
                return false;

                }
                
            }
            catch (Exception)
            {
                return false;

            }
        }
        //update Details Mouvement
        public bool updateDetailMouvement(DetailMouvement DetailMouvement)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
 string req = @"UPDATE `DetailsMouvement` SET `Quantite`="+DetailMouvement.Quatiter+"WHERE IdMedicament="+DetailMouvement.Idmedicament+" and IdMouvement="+DetailMouvement.Idmouvement+";";
                SQLiteCommand cmd = new SQLiteCommand(req, cn);

                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                return false;

            }
            }
            catch (Exception)
            {
                return false;

            }
                
               
        }
        //Update Declaration Mouvement
        public bool updateDeclarationMouvement(DeclarationMouvement declarationmouvement)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    string req = @"UPDATE `DeclarationMouvement` SET `Lieu`='" + declarationmouvement.Lieu + "', `Service`='" + declarationmouvement.Service + "' WHERE  `IdDeclaration`=" + declarationmouvement.IdDeclaration + " and`IdMouvement`=" + declarationmouvement.IdMouvement + ";";
                    SQLiteCommand cmd = new SQLiteCommand(req, cn);

                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cn.Close();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        // Update Declaration 
        public bool UpdateDeclaration(Declaration declaration)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    string req;
                    req = string.Format(@"UPDATE `Declaration` SET `IdPersonnel`={0}, 
                                                     `IdMedecin`={1}, 
                                                     `TypeDeclaration`='{2}',
                                                     `TotalFraisEngage`={3},
                                                     `DateConsultation`='{4}' ,
                                                     `NomPrenomMalade`='{5}',
                                                     `AgeMalade`={6},
                                                     `LienParente`='{7}' ,
                                                     `NatureMaladie`='{8}' ,
                                                     `NumCertificat`='{9}' WHERE `Id`={10};"
                                            , declaration.IdPersonnel,
                                            declaration.IdMedecin,
                                            declaration.TypeDeclaration,
                                            declaration.TotalFraisEngage,
                                            declaration.DateConsultation.ToShortDateString(),
                                            declaration.NomPrenomMalade,
                                            declaration.AgeMalade,
                                            declaration.LienParente,
                                            declaration.NatureMaladie,
                                            declaration.NumCertificat,
                                            declaration.Id);
                    SQLiteCommand cmd = new SQLiteCommand(req, cn);

                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cn.Close();
                        return true;
                    }
                    cn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        //DELETE
        //Delete Personelle
        public bool DeletePersonelle(int ID)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    SQLiteCommand cmd = new SQLiteCommand("delete from Personnel where id =" + ID, cn);

                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cn.Close();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        //Delete Mouvement
        public bool DeleteMouvement(Mouvement mouvement,List<Medicament> Medicament)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    string req = "delete from Mouvement where id=" + mouvement.Id;
                    DeleteDeteilmouvement(Medicament, mouvement.Id, mouvement.Type);
                    SQLiteCommand cmd = new SQLiteCommand(req, cn);
                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1 )
                    {
                        cn.Close();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        //Delete Medicament
        public bool DeleteMedicament(int ID)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {

                
                string req = "delete from Medicament where Id=" + ID;
               
                SQLiteCommand cmd = new SQLiteCommand(req, cn);

                cn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    cn.Close();
                    return true;
                }
                return false;
}
            }
            catch (Exception)
            {
                return false;

            }
        }
        //delete medecin
        public bool DeleteMedecin(int ID)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    SQLiteCommand cmd = new SQLiteCommand("delete from Medecin where Id=" + ID, cn);

                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cn.Close();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        //delete DetailMouvement
        public bool deleteDetailMouvement(int IDMouvement,int IDMedicament)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    SQLiteCommand cmd = new SQLiteCommand("delete from DetailsMouvement where Idmouvement=" + IDMouvement + " and idMedicament=" + IDMedicament, cn);

                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cn.Close();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        // delete declarationMouvement
        public bool deleteDeclarationMouvement(int IDMouvement)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    SQLiteCommand cmd = new SQLiteCommand("delete from declarationmouvement where Idmouvement=" + IDMouvement + ";", cn);

                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cn.Close();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        // delete Declaration
        public bool deleteDeclaration(int ID)
        {
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection(ChaineConnexion))
                {
                    SQLiteCommand cmd = new SQLiteCommand("delete from declaration where id=" + ID, cn);

                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cn.Close();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }



    }
}
