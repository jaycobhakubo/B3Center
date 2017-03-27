//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using GameTech.Elite.Client.Modules.B3Center.Business;
//using System.IO;

//namespace GameTech.Elite.Client.Modules.B3Center.Messages
//{
//    class SetB3LoggedChanges
//    {
//        public SetB3LoggedChanges(int StaffId, int MachineId, int OperatorId, string Description)
//        {
//            SqlConnection sc = UI.SqlConnectionToByPassGtiServerApp.m_sqlConnection;
//            try
//            {
//                sc.Open();
//                using (SqlCommand cmd = new SqlCommand(@"spAddAuditLogEntry @AuditTypeId, @StaffId, @MachineId, @OperatorId, @Description", sc))
//                {
//                    cmd.Parameters.AddWithValue("AuditTypeId", 15);
//                    cmd.Parameters.AddWithValue("StaffId", StaffId);
//                    cmd.Parameters.AddWithValue("MachineId", MachineId);
//                    cmd.Parameters.AddWithValue("Operatorid", OperatorId);
//                    cmd.Parameters.AddWithValue("Description", Description);
//                    //cmd.ExecuteNonQuery();
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Windows.MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                sc.Close();
//            }
//        }
//    }

//    class B3_GetIdDefinition
//    {
//        public string getSettingDefinition(int B3SettingId)
//        {
//            string B3SettingDefinition = "";

//            SqlConnection sc = UI.SqlConnectionToByPassGtiServerApp.m_sqlConnection;
//            try
//            {
//                sc.Open();
//                using (SqlCommand cmd = new SqlCommand(@"select SettingDesc from B3SettingsGlobal where SettingID = @B3SettingId", sc))
//                {
//                    cmd.Parameters.AddWithValue("B3SettingId", B3SettingId);
//                    B3SettingDefinition = cmd.ExecuteScalar().ToString();
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Windows.MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                sc.Close();
//            }

//            return B3SettingDefinition;
//        }


//        public string getMathPayTableDef(int MathPackageID)
//        {
//            string MathPayTableDef = "";

//            SqlConnection sc = UI.SqlConnectionToByPassGtiServerApp.m_sqlConnection;
//            try
//            {
//                sc.Open();
//                using (SqlCommand cmd = new SqlCommand(@"select PackageNameDesc from B3MathPackageDef where MathPackageID = @MathPackageID", sc))
//                {
//                    cmd.Parameters.AddWithValue("MathPackageID", MathPackageID);
//                    MathPayTableDef = cmd.ExecuteScalar().ToString();
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Windows.MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                sc.Close();
//            }

//            return MathPayTableDef;
//        }
//    }
//}
