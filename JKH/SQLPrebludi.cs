using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JKH
{
    class SQLPrebludi
    {
        public string Account { get; set; }
        private static SqlConnection connection { get; set; }
        static SQLPrebludi()
        { 
            connection = new SqlConnection(new SqlConnectionStringBuilder()
            {
                DataSource = @".\SQLExpress",
                InitialCatalog = "testDiplom",
                IntegratedSecurity = true
            }.ConnectionString);
            connection.Open();
        }
        public  SqlConnection getConnection()
        {
            return new SqlConnection(new SqlConnectionStringBuilder()
            {
                DataSource = @".\SQLExpress",
                InitialCatalog = "testDiplom",
                IntegratedSecurity = true
            }.ConnectionString);
        }
        public string TakeNameStreet()
        {
            return "SELECT [Name] FROM [Street]";
        }
        public string TakeNameMonth()
        {
            return "SELECT [NeedMonth] FROM [TimeMonth] ORDER BY [Id]";
        }
        public string TakeNameYear()
        {
            return "SELECT [NeedYear] FROM [TimeYear]";
        }
        public string TakeNumberHome(string word)
        {
            return "SELECT Number FROM [Home] Where StreetName = " + "'" + word + "'";
        }
        public string TakeNumberFlat(string word)
        {
            return "SELECT FlatNumber FROM [Flat] Where HomeNumber = " + "'" + word + "'";
        }
        public string TakePersonalAccount(string flatNumber,int homeNumber)
        {
            return "SELECT FlatPersonalAccount FROM [Flat] Where FlatNumber = " + "'" + flatNumber + "' AND HomeNumber = " + "'" + homeNumber + "'";
        }
        public string TakeConsumerInfo(string accountNumber)
        {
            return "SELECT * FROM [Consumer] Where PersonalAccount = " + "'" + accountNumber + "'";
        }
        public string TakeRentInfo(string accountNumber, int selectYear, string selectMonth)
        {
            return "SELECT * FROM [Rent] Where PersonalAccountRent = " + "'" + accountNumber + "'" + " AND NeedYear = " + "'" + selectYear + "'" + " AND NeedMonth = " + "'" + selectMonth + "'";
        }
        public string TakeDebtorRent(string accountNumber, int selectYear)
        {
            return "SELECT * FROM [Rent] Where Debtor = " + "'1' AND PersonalAccountRent = " + "'" + accountNumber + "'" + " AND NeedYear = " + "'" + selectYear + "'";
        }

        public DebtorVal TakeDebtor(string method)
        {
            DebtorVal debtor = new DebtorVal();

                
                SqlCommand command = new SqlCommand(method, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Rent rent = new Rent();
                    rent.ColdWaterSupply = float.Parse(reader[1].ToString());
                    rent.HotWaterSupply = float.Parse(reader[2].ToString());
                    rent.WaterDisposal = float.Parse(reader[3].ToString());
                    rent.Heating = float.Parse(reader[4].ToString());
                    rent.PowerSupply = float.Parse(reader[5].ToString());
                    rent.SolidMunicipalWasteManagement = float.Parse(reader[6].ToString());
                    rent.GasSupply = float.Parse(reader[7].ToString());
                    rent.DeptorType = Convert.ToBoolean(reader[8]);
                    rent.PaidUp = float.Parse(reader[9].ToString());
                    rent.NeededYear = Convert.ToInt32(reader[10]);
                    rent.NeededMonth = reader[11].ToString();
                    rent.PersonalAccountRent = Convert.ToInt32(reader[12]);
                    debtor.DebtorRent.Add(rent);
                }
                reader.Close();
                //connection.Dispose();
                //connection.Close();
                return debtor;
            
        }
        public NeedMonth TakeMonth(string method)
        {
            NeedMonth month = new NeedMonth();
            string query = method;
            using (SqlConnection connection = new SQLPrebludi().getConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    month.AllMonth.Add(reader[0].ToString());
                }
                reader.Close();
                connection.Dispose();
                connection.Close();
                return month;
            }
        }
        public NeedYear TakeYear(string method)
        {
            NeedYear year = new NeedYear();
            string query = method;
            using (SqlConnection connection = new SQLPrebludi().getConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    year.AllYear.Add(Convert.ToInt32(reader[0]));
                }
                reader.Close();
                connection.Dispose();
                connection.Close();
                return year;
            }
        }
        public Home TakeHome(string method)
        {
            Home home = new Home();
            string query = method;
            using (SqlConnection connection = new SQLPrebludi().getConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    home.NumberHome.Add(Convert.ToInt32(reader[0]));
                }
                reader.Close();
                connection.Dispose();
                connection.Close();
                return home;
            }
        }
        public Flat TakeFlat(string method)
        {
            Flat flat = new Flat();
            string query = method;
            using (SqlConnection connection = new SQLPrebludi().getConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    flat.NumberFlat.Add(Convert.ToInt32(reader[0]));
                }
                reader.Close();
                connection.Dispose();
                connection.Close();
                return flat;
            }
        }
        public Street TakeStreet(string method)
        {
            Street street = new Street();
            string query = method;

            using (SqlConnection connection = new SQLPrebludi().getConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    street.nameStreet.Add((reader[0].ToString()));
                }
                reader.Close();
                connection.Dispose();
                connection.Close();
                return street;
            }
        }
        public void TakeAccount(string method)
        {
            string query = method;
            try
            {
                using (SqlConnection connection = new SQLPrebludi().getConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Account = reader[0].ToString();
                    }
                    reader.Close();
                    connection.Dispose();
                    connection.Close();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        public Consumer TakeConsumer(string method)
        {
            Consumer consuk = new Consumer();
            string query = method;

            using (SqlConnection connection = new SQLPrebludi().getConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    consuk.Name = reader[1].ToString();
                    consuk.Surname = reader[2].ToString();
                    consuk.Patronymic = reader[3].ToString();
                    consuk.PersonalAccount = Convert.ToInt32(reader[4]);
                }
                reader.Close();
                connection.Dispose();
                connection.Close();
                return consuk;
            }
        }
        public Rent TakeRent(string method)
        {
            Rent rent = new Rent();
            string query = method;

            using (SqlConnection connection = new SQLPrebludi().getConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    rent.ColdWaterSupply = float.Parse(reader[1].ToString());
                    rent.HotWaterSupply = float.Parse(reader[2].ToString());
                    rent.WaterDisposal = float.Parse(reader[3].ToString());
                    rent.Heating = float.Parse(reader[4].ToString());
                    rent.PowerSupply = float.Parse(reader[5].ToString());
                    rent.SolidMunicipalWasteManagement = float.Parse(reader[6].ToString());
                    rent.GasSupply = float.Parse(reader[7].ToString());
                    rent.DeptorType = Convert.ToBoolean(reader[8]);
                    rent.PaidUp = float.Parse(reader[9].ToString());
                    rent.NeededYear = Convert.ToInt32(reader[10]);
                    rent.NeededMonth = reader[11].ToString(); 
                    rent.PersonalAccountRent = Convert.ToInt32(reader[12]);
                }
                reader.Close();
                connection.Dispose();
                connection.Close();
                return rent;
            }
        }
    }
}

