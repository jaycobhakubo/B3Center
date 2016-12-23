using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GameTech.Elite.Client.Modules.B3Center.UI
{
    /// <summary>
    /// Use to bypass gtiserver.
    /// </summary>
    public class SqlConnectionToByPassGtiServerApp
    {
        public static SqlConnection m_sqlConnection;
          
        public SqlConnectionToByPassGtiServerApp()
        {
            m_sqlConnection = new SqlConnection(Properties.Resources.SQLB3Connection);        
            try
            {
                m_sqlConnection.Open();
            }
            catch
            {
                m_sqlConnection = new SqlConnection(Properties.Resources.SQLGtiConnection);
                try
                {
                    m_sqlConnection.Open();
                }
                catch
                {
                    //No B3-Server or GtiServer
                }
            }
            m_sqlConnection.Close();
        }
    }
}


//SQL Direct Call REF 
#region GetB3AccountNumber
//private bool m_result;

//public GetB3AccountNumber(int SessNum)
//{
//    m_result = false;
//    SqlConnection sc = UI.SqlConnectionToByPassGtiServerApp.m_sqlConnection;
//    try
//    {

//        sc.Open();
//        using (SqlCommand cmd = new SqlCommand(@"spB3_rptGetAccountNumber @SessNum", sc))
//        {
//            AccountNumberList = new ObservableCollection<int>();
//            cmd.Parameters.AddWithValue("SessNum", SessNum);
//            SqlDataReader reader = cmd.ExecuteReader();

//            while (reader.Read())
//            {
//                int AccountNumber = (int)reader.GetInt32(0);
//                AccountNumberList.Add(AccountNumber);
//            }

//            if (AccountNumberList.Count != 0)
//            {
//                m_result = true;
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        System.Windows.MessageBox.Show(ex.Message);
//    }
//    finally
//    {
//        sc.Close();
//    }
//}

//public bool Result
//{
//    get { return m_result; }
//    set { m_result = value; }
//}

//public ObservableCollection<int> AccountNumberList;
#endregion
#region  GetB3MathGamePlay
//private bool m_result;

//public GetB3MathGamePlay()
//{
//    m_result = false;
//    SqlConnection sc = UI.SqlConnectionToByPassGtiServerApp.m_sqlConnection;
//    try
//    {

//        sc.Open();
//        using (SqlCommand cmd = new SqlCommand(@"spB3_GetMathPayTable ", sc))
//        {
//            ListB3MathGamePlay = new List<B3MathGamePay>();
//          SqlDataReader reader = cmd.ExecuteReader();

//            while (reader.Read())
//            {
//                B3MathGamePay data = new B3MathGamePay();
//                data.MathPackageID = (int)reader.GetInt32(0);
//                data.GameID = (int)reader.GetInt32(1);
//                data.PackageDesc = reader.GetString(2);
//                data.IsRNG = (bool)reader.GetBoolean(3);
//                ListB3MathGamePlay.Add(data);
//            }

//            if (ListB3MathGamePlay.Count != 0)
//            {
//                m_result = true;
//            }



//        }
//    }
//    catch (Exception ex)
//    {
//        System.Windows.MessageBox.Show(ex.Message);
//    }
//    finally
//    {
//        sc.Close();

//    }
//}

//public bool Result
//{
//    get { return m_result; }
//    set { m_result = value; }
//}

//public List<B3MathGamePay> ListB3MathGamePlay;
#endregion
#region SetGameEnableSQL
//public SetGameEnableSQL(string GameName, bool Enable)
  //      {
  //          SqlConnection sc = UI.SqlConnectionToByPassGtiServerApp.m_sqlConnection;
  //          try
  //          {
  //              sc.Open();
  //              using (SqlCommand cmd = new SqlCommand(@"spB3SetEnableDisableGame @GameName, @Enable", sc))
  //              {
  //                  cmd.Parameters.AddWithValue("GameName", GameName);
  //                  cmd.Parameters.AddWithValue("Enable", Enable);
  //                  cmd.ExecuteNonQuery();
  //              }
  //          }
  //          catch (Exception ex)
  //          {
  //              System.Windows.MessageBox.Show(ex.Message);
  //          }
  //          finally
  //          {
  //              sc.Close();
  //          }
//      }
#endregion 
