using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class GetB3AccountNumber
    {
        private bool m_result;

        public GetB3AccountNumber(int SessNum)
        {
            m_result = false;
            SqlConnection sc = new SqlConnection(Properties.Resources.SQLB3Connection);
            try
            {

                sc.Open();
                using (SqlCommand cmd = new SqlCommand(@"spB3_rptGetAccountNumber @SessNum", sc))
                {
                    AccountNumberList = new ObservableCollection<int>();
                    cmd.Parameters.AddWithValue("SessNum", SessNum);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int AccountNumber = (int)reader.GetInt32(0);
                        AccountNumberList.Add(AccountNumber);
                    }

                    if (AccountNumberList.Count != 0)
                    {
                        m_result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                sc.Close();
            }
        }

        public bool Result
        {
            get { return m_result; }
            set { m_result = value; }
        }

        public ObservableCollection<int> AccountNumberList;

    }
}
