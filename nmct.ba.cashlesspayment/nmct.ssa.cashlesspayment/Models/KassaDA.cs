using nmct.ssa.cashlesspayment.helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlesspayment.Models
{
    public class KassaDA
    {
        public static List<Kassa> GetRegisters()
        {
            string sql = "SELECT * FROM Registers";
            List<Kassa> resultaat = new List<Kassa>();
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("DefaultConnection"), sql);
                while (reader.Read())
                {
                    resultaat.Add(new Kassa()
                    {
                        ID = Int32.Parse(reader["ID"].ToString()),
                        RegisterName = reader["RegisterName"].ToString(),
                        Device = reader["Device"].ToString(),
                        PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                        ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"]),
                    });
                }
                return resultaat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<Organisate_Kassa> GetRegistersbyOrg()
        {
            string sql = "SELECT * FROM Organisation_Register";
            List<Organisate_Kassa> resultaat = new List<Organisate_Kassa>();
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("DefaultConnection"), sql);
                while (reader.Read())
                {
                    resultaat.Add(new Organisate_Kassa()
                    {
                        OrganisationID = OrganisatieDA.GetOrganisationByid(Int32.Parse(reader["OrganisationID"].ToString())),
                        RegisterID = GetKassaByID(Int32.Parse(reader["RegisterID"].ToString())),
                        FromDate = Convert.ToDateTime(reader["FromDate"]),
                        UntilDate = Convert.ToDateTime(reader["UntilDate"]),
                    });
                }
                return resultaat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static int AddRegisterToDatabase(Organisate_Kassa orgreg)
        {
            string sql = "INSERT INTO Register VALUES(@RegisterName, @Device)";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@RegisterName", orgreg.RegisterID.RegisterName);
            DbParameter par2 = Database.AddParameter("ConnectionString", "@Device", orgreg.RegisterID.Device);
            int id = Database.InsertData(Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"BRECHT", orgreg.OrganisationID.DbName, orgreg.OrganisationID.DbLogin, orgreg.OrganisationID.DbPassword)), sql, par1, par2);

            return id;
        }
        public static int AddRegisterToTable(Organisate_Kassa orgreg)
        {
            string sql = "INSERT INTO Organisation_Register VALUES(@OrganisationID,@RegisterID,@FromDate,@UntilDate)";
            DbParameter par1 = Database.AddParameter("DefaultConnection", "@OrganisationID", orgreg.OrganisationID.ID);
            DbParameter par2 = Database.AddParameter("DefaultConnection", "@RegisterID", orgreg.RegisterID.ID);
            DbParameter par3 = Database.AddParameter("DefaultConnection", "@FromDate", orgreg.FromDate);
            DbParameter par4 = Database.AddParameter("DefaultConnection", "@UntilDate", orgreg.UntilDate);
            int id = Database.InsertData(Database.GetConnection("DefaultConnection"), sql, par1, par2, par3, par4);

            return id;
        }
        public static int InsertRegister(Kassa newkassa)
        {
            string sql = "INSERT INTO Registers VALUES(@RegisterName,@Device,@PurchaseDate,@ExpiresDate)";
            DbParameter par1 = Database.AddParameter("DefaultConnection", "@RegisterName", newkassa.RegisterName);
            DbParameter par2 = Database.AddParameter("DefaultConnection", "@Device", newkassa.Device);
            DbParameter par3 = Database.AddParameter("DefaultConnection", "@PurchaseDate", newkassa.PurchaseDate);
            DbParameter par4 = Database.AddParameter("DefaultConnection", "@ExpiresDate", newkassa.ExpiresDate);
            int id = Database.InsertData(Database.GetConnection("DefaultConnection"), sql, par1, par2, par3, par4);

            return id;
        }
        public static int ChangeRegister(Kassa changereg)
        {
            string sql = "UPDATE Registers SET RegisterName = @RegisterName, Device = @Device, PurchaseDate = @PurchaseDate, ExpiresDate = @ExpiresDate WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter("DefaultConnection", "@RegisterName", changereg.RegisterName);
            DbParameter par2 = Database.AddParameter("DefaultConnection", "@Device", changereg.Device);
            DbParameter par3 = Database.AddParameter("DefaultConnection", "@PurchaseDate", changereg.PurchaseDate);
            DbParameter par4 = Database.AddParameter("DefaultConnection", "@ExpiresDate", changereg.ExpiresDate);
            DbParameter par5 = Database.AddParameter("DefaultConnection", "@ID", changereg.ID);
            int id = Database.InsertData(Database.GetConnection("DefaultConnection"), sql, par1, par2, par3, par4, par5);
            return id;

        }
        public static Kassa GetKassaByID(int id)
        {
            string sql = "SELECT * FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("DefaultConnection", "@ID", id);
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("DefaultConnection"), sql, par1);
                reader.Read();
                return new Kassa()
                {
                        ID = Int32.Parse(reader["ID"].ToString()),
                        RegisterName = reader["RegisterName"].ToString(),
                        Device = reader["Device"].ToString(),
                        PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                        ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"]),
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}