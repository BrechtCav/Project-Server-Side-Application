using nmct.ba.demo.encryptie;
using nmct.ssa.cashlesspayment.helper;
using nmct.ssa.cashlesspayment.PresentationModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlesspayment.Models
{
    public class OrganisatieDA
    {
        public static Organisatie GetOrganisationByLoginAndPassword(string username, string password)
        {
            string sql = "SELECT * FROM Organisations WHERE Login='%+@Login+%' AND Password='%+@Password+%'";
            DbParameter par1 = Database.AddParameter("DefaultConnection", "@Login", username);
            DbParameter par2 = Database.AddParameter("DefaultConnection", "@Password", password);

            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("DefaultConnection"), sql, par1, par2);
                reader.Read();
                return new Organisatie()
                {
                    ID = Int32.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = reader["Password"].ToString(),
                    DbName = reader["DbName"].ToString(),
                    DbLogin = reader["DbLogin"].ToString(),
                    DbPassword = reader["DbPassword"].ToString(),
                    OrganisationName = reader["OrganisationName"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static Organisatie GetOrganisationByid(int id)
        {
            string sql = "SELECT * FROM Organisations WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("DefaultConnection", "@ID", id);

            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("DefaultConnection"), sql, par1);
                reader.Read();
                return new Organisatie()
                {
                    ID = Int32.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = Cryptography.Decrypt(reader["Password"].ToString()),
                    DbName = Cryptography.Decrypt(reader["DbName"].ToString()),
                    DbLogin = Cryptography.Decrypt(reader["DbLogin"].ToString()),
                    DbPassword = Cryptography.Decrypt(reader["DbPassword"].ToString()),
                    OrganisationName = reader["OrganisationName"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<Errorlog> GetLogs()
        {
            string sql = "SELECT * FROM Errorlog";
            List<Errorlog> resultaat = new List<Errorlog>();
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("DefaultConnection"), sql);
                while (reader.Read())
                {
                    resultaat.Add(new Errorlog()
                    {
                        RegisterID = KassaDA.GetKassaByID(Convert.ToInt32(reader["RegisterID"])),
                        TimeStamp = Convert.ToDateTime(reader["Timestamp"]),
                        Message = reader["Message"].ToString(),
                        Stacktrace = reader["Stacktrace"].ToString(),
                        Organisatie = OrganisatieDA.GetOrganisationByid(Convert.ToInt32(reader["OrganisationID"]))
                    });
                }
                return resultaat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<Errorlog> GetLogsByOrg(int Id)
        {
            string sql = "SELECT * FROM Errorlog Where OrganisationID = @ID";
            DbParameter par1 = Database.AddParameter("DefaultConnection", "@ID", Id);
            List<Errorlog> resultaat = new List<Errorlog>();
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("DefaultConnection"), sql, par1);
                while(reader.Read())
                {
                    resultaat.Add(new Errorlog()
                    {
                        RegisterID = KassaDA.GetKassaByID(Convert.ToInt32(reader["RegisterID"])),
                        TimeStamp = Convert.ToDateTime(reader["Timestamp"]),
                        Message = reader["Message"].ToString(),
                        Stacktrace = reader["Stacktrace"].ToString(),
                    });
                }
                return resultaat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<Organisatie> GetOrganisations()
        {
            string sql = "SELECT * FROM Organisations";
            List<Organisatie> resultaat = new List<Organisatie>();
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("DefaultConnection"), sql);
                while(reader.Read())
                {
                    resultaat.Add(new Organisatie()
                    {
                        ID = Int32.Parse(reader["ID"].ToString()),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString(),
                        DbName = Cryptography.Decrypt(reader["DbName"].ToString()),
                        DbLogin = Cryptography.Decrypt(reader["DbLogin"].ToString()),
                        DbPassword = reader["DbPassword"].ToString(),
                        OrganisationName = reader["OrganisationName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString()
                    });
                }
                return resultaat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static int ChangeOrganisation(PMChangedOrganisation changedorg)
        {
            string sql = "UPDATE Organisations SET Login = @Login, Password = @Password, OrganisationName = @OrganisationName, Address = @Address, Email = @Email, Phone = @Phone WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter("DefaultConnection", "@Login", changedorg.Login);
            DbParameter par2 = Database.AddParameter("DefaultConnection", "@Password", Cryptography.Encrypt(changedorg.Password));
            DbParameter par3 = Database.AddParameter("DefaultConnection", "@OrganisationName", changedorg.OrganisationName);
            DbParameter par4 = Database.AddParameter("DefaultConnection", "@Address", changedorg.Address);
            DbParameter par5 = Database.AddParameter("DefaultConnection", "@Email", changedorg.Email);
            DbParameter par6 = Database.AddParameter("DefaultConnection", "@Phone", changedorg.Phone);
            DbParameter par7 = Database.AddParameter("DefaultConnection", "@ID", changedorg.ID);
            int id = Database.InsertData(Database.GetConnection("DefaultConnection"), sql, par1, par2, par3, par4,par5,par6,par7);
            return id;

        }

        public static int InsertOrganisation(Organisatie o)
        {
            string sql = "INSERT INTO Organisations VALUES(@Login,@Password,@DbName,@DbLogin,@DbPassword,@OrganisationName,@Address,@Email,@Phone)";
            DbParameter par1 = Database.AddParameter("DefaultConnection", "@Login", o.Login);
            DbParameter par2 = Database.AddParameter("DefaultConnection", "@Password",Cryptography.Encrypt(o.Password));
            DbParameter par3 = Database.AddParameter("DefaultConnection", "@DbName", Cryptography.Encrypt(o.DbName));
            DbParameter par4 = Database.AddParameter("DefaultConnection", "@DbLogin", Cryptography.Encrypt(o.DbLogin));
            DbParameter par5 = Database.AddParameter("DefaultConnection", "@DbPassword", Cryptography.Encrypt(o.DbPassword));
            DbParameter par6 = Database.AddParameter("DefaultConnection", "@OrganisationName", o.OrganisationName);
            DbParameter par7 = Database.AddParameter("DefaultConnection", "@Address", o.Address);
            DbParameter par8 = Database.AddParameter("DefaultConnection", "@Email", o.Email);
            DbParameter par9 = Database.AddParameter("DefaultConnection", "@Phone", o.Phone);
            int id = Database.InsertData(Database.GetConnection("DefaultConnection"), sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);

            CreateDatabase(o);
            return id;
        }

        private static void CreateDatabase(Organisatie o)
        {
            // create the actual database
            //string create = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/create.txt")); only for the web
            string create = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Data/create.txt"); // only for desktop
            string sql = create.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);
            foreach (string commandText in RemoveGo(sql))
            {
                Database.ModifyData(Database.GetConnection("DefaultConnection"), commandText);
            }

            // create login, user and tables
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction("DefaultConnection");

                //string fill = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/fill.txt")); // only for the web
                string fill = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Data/fill.txt"); // only for desktop
                string sql2 = fill.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);

                foreach (string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }
        

        private static string[] RemoveGo(string input)
        {
            //split the script on "GO" commands
            string[] splitter = new string[] { "\r\nGO\r\n" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }
    }
}