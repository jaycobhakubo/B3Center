using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using GameTech.Elite.Client.Modules.B3Center.Business;
using System.IO;

namespace GameTech.Elite.Client.Modules.B3Center.Messages
{
    class SetB3LoggedChanges
    {
        public SetB3LoggedChanges(int StaffId, int MachineId, int OperatorId, string Description)
        {
            SqlConnection sc = new SqlConnection(Properties.Resources.SQLB3Connection);
            try
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(@"spAddAuditLogEntry @AuditTypeId, @StaffId, @MachineId, @OperatorId, @Description", sc))
                {
                    cmd.Parameters.AddWithValue("AuditTypeId", 15);
                    cmd.Parameters.AddWithValue("StaffId", StaffId);
                    cmd.Parameters.AddWithValue("MachineId", MachineId);
                    cmd.Parameters.AddWithValue("Operatorid", OperatorId);
                    cmd.Parameters.AddWithValue("Description", Description);
                    cmd.ExecuteNonQuery();
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
    }

    class B3_GetIdDefinition
    {
        public string getSettingDefinition(int B3SettingId)
        {
            string B3SettingDefinition = "";
            switch (B3SettingId)
            {
                case 	1	:	B3SettingDefinition =	"Denom 1"	; break;
                case 	2	:	B3SettingDefinition =	"Denom 5"	; break;
                case 	3	:	B3SettingDefinition =	"Denom 10"	; break;
                case 	4	:	B3SettingDefinition =	"Denom 25"	; break;
                case 	5	:	B3SettingDefinition =	"Denom 50"	; break;
                case 	6	:	B3SettingDefinition =	"Denom 100"	; break;
                case 	7	:	B3SettingDefinition =	"Denom 200"	; break;
                case 	8	:	B3SettingDefinition =	"Denom 500"	; break;
                case 	9	:	B3SettingDefinition =	"Max Bet Level"	; break;
                case 	10	:	B3SettingDefinition =	"Max Cards"	; break;
                case 	11	:	B3SettingDefinition =	"Call Speed"	; break;
                case 	12	:	B3SettingDefinition =	"Auto Call"	; break;
                case 	13	:	B3SettingDefinition =	"Auto Play"	; break;
                case 	14	:	B3SettingDefinition =	"Hide Serial Number"	; break;
                case 	15	:	B3SettingDefinition =	"Single Offer Bonus"	; break;
                case 	16	:	B3SettingDefinition =	"Player Setting Calibrate Touch"	; break;
                case 	17	:	B3SettingDefinition =	"Player Setting Press to Collect"	; break;
                case 	18	:	B3SettingDefinition =	"Player Setting Announce Call"	; break;
                case 	19	:	B3SettingDefinition =	"Player Setting Screen Cursor"	; break;
                case 	20	:	B3SettingDefinition =	"Player Setting Time To Collect"	; break;
                case 	21	:	B3SettingDefinition =	"Player Setting Disclaimer"	; break;
                case 	22	:	B3SettingDefinition =	"Player Setting Disclaimer Text ID"	; break;
                case 	23	:	B3SettingDefinition =	"Player Setting Main Volume"	; break;
                case 	24	:	B3SettingDefinition =	"Screen Cursor"	; break;
                case 	25	:	B3SettingDefinition =	"Calibrate Touch"	; break;
                case 	26	:	B3SettingDefinition =	"Auto Print Session Report"	; break;
                case 	27	:	B3SettingDefinition =	"Page Printer"	; break;
                case 	28	:	B3SettingDefinition =	"Quick Sales"	; break;
                case 	29	:	B3SettingDefinition =	"Print Logo"	; break;
                case 	30	:	B3SettingDefinition =	"Allow in Session Ball Change"	; break;
                case 	31	:	B3SettingDefinition =	"Logging Enable"	; break;
                case 	32	:	B3SettingDefinition =	"Log Recycle Days"	; break;
                case 	33	:	B3SettingDefinition =	"Sales Main Volume"	; break;
                case 	34	:	B3SettingDefinition =	"Min Player"	; break;
                case 	35	:	B3SettingDefinition =	"Game Start Delay"	; break;
                case 	36	:	B3SettingDefinition =	"Consolotion Prize"	; break;
                case 	37	:	B3SettingDefinition =	"Game Recall Password"	; break;
                case 	38	:	B3SettingDefinition =	"Wait Count Down"	; break;
                case 	39	:	B3SettingDefinition =	"Payout Limit"	; break;
                case 	40	:	B3SettingDefinition =	"Jackpot Limit"	; break;
                case 	41	:	B3SettingDefinition =	"Enfore Mix"	; break;
                case 	42	:	B3SettingDefinition =	"Hand Payout Trigger"	; break;
                case 	43	:	B3SettingDefinition =	"Minimum Players"	; break;
                case 	44	:	B3SettingDefinition =	"Vip Poin Multi Player"	; break;
                case 	45	:	B3SettingDefinition =	"Mag Card Sentinel Start"	; break;
                case 	46	:	B3SettingDefinition =	"Mag Card Sentinel End"	; break;
                case 	47	:	B3SettingDefinition =	"Currency"	; break;
                case 	48	:	B3SettingDefinition =	"RNG Ball Call Time"	; break;
                case 	49	:	B3SettingDefinition =	"Player Pin Length"	; break;
                case 	50	:	B3SettingDefinition =	"Enable UK"	; break;
                case 	51	:	B3SettingDefinition =	"Dual Account"	; break;
                case 	52	:	B3SettingDefinition =	"Multi Operator"	; break;
                case 	53	:	B3SettingDefinition =	"Common RNG Ball Call"	; break;
                case 	54	:	B3SettingDefinition =	"North Dakota Mode"	; break;
                case 	55	:	B3SettingDefinition =	"Auto Session End"	; break;
                case 	56	:	B3SettingDefinition =	"Site Name"	; break;
                case 	57	:	B3SettingDefinition =	"System Main Volume"	; break;
                case 	58	:	B3SettingDefinition =	"MathPayTable"; break; //0 to 69; As 0 is Default"	
            }

            return B3SettingDefinition;
        }


        public string getMathPayTableDef(int MathPackageID)
        {
            string MathPayTableDef = "";

            SqlConnection sc = new SqlConnection(Properties.Resources.SQLB3Connection);
            try
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(@"select PackageNameDesc from B3MathPackageDef where MathPackageID = @MathPackageID", sc))
                {
                    cmd.Parameters.AddWithValue("MathPackageID", MathPackageID);
                    MathPayTableDef = cmd.ExecuteScalar().ToString();
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

            return MathPayTableDef;
        }
    }
}
