using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Business;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    public class GetB3MathGamePlay
    {

        private bool m_result;

        public GetB3MathGamePlay()
        {
            m_result = false;
            SqlConnection sc = new SqlConnection(Properties.Resources.SQLB3Connection);
            try
            {
  
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(@"spB3_GetMathPayTable ", sc))
                {
                    ListB3MathGamePlay = new List<B3MathGamePay>();
                  SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        B3MathGamePay data = new B3MathGamePay();
                        data.MathPackageID = (int)reader.GetInt32(0);
                        data.GameID = (int)reader.GetInt32(1);
                        data.PackageDesc = reader.GetString(2);
                        data.IsRNG = (bool)reader.GetBoolean(3);
                        ListB3MathGamePlay.Add(data);
                    }

                    if (ListB3MathGamePlay.Count != 0)
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

        public List<B3MathGamePay> ListB3MathGamePlay;
    }

}
