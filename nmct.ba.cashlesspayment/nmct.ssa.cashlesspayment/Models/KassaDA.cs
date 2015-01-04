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