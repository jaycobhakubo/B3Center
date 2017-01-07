using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;
using GameTech.Elite.Client.Modules.B3Center.Messages;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GameTech.Elite.Client.Modules.B3Center.UI.OperatorViews
{
    /// <summary>
    /// Interaction logic for CharityView.xaml
    /// </summary>
    public partial class CharityView : UserControl
    {
        //private readonly SaveCancelCrtl m_saveCancelCtrl;
        //private Operator m_operator = new Operator();
        //private string m_operatorname;
        //private string m_operatorDescription;
        //private string m_contactName;
        //private string m_address;
        //private  string m_city;
        //private  string m_state;
        //private string m_zipcode;
        //private string m_phoneNumber;
        //private  string m_faxNumber;
        //private  int m_iconColor;
        //private string m_colorDef;
        //private List<B3IconColor> m_b3IconColor = new List<B3IconColor>();
        //private Button m_btnSave = new Button();
        //private Button m_btnCancel =new Button();
        //private bool m_isNewOperator;
        //private bool m_isOperatorRename;
        //private int m_currentOperatorId;

        //public Button BtnSave
        //{
        //    get { return m_btnSave; }
        //    set { m_btnSave = value; }
        //}

        //public bool IsOperatorRenamed
        //{
        //    get { return m_isOperatorRename; }
        //    set { m_isOperatorRename = value; }
        //}

        //public bool NewOperator
        //{
        //    get { return m_isNewOperator; }
        //    set { m_isNewOperator = value; }
        //}

        //public int CurrentOperatorId
        //{
        //    get { return m_currentOperatorId; }
        //    set { m_currentOperatorId = value; }
        //}

        
        public CharityView()
        {
            InitializeComponent();
            DataContext = this;
            ////m_saveCancelCtrl = new SaveCancelCrtl();
            ////m_btnSave = m_saveCancelCtrl.btnSave;
            //m_btnSave.Click += new RoutedEventHandler(m_btnSave_Click);
            ////m_btnCancel = m_saveCancelCtrl.btnCancel;
            //m_btnCancel.Click += new RoutedEventHandler(m_btnCancel_Click);
            ////SaveCancelTransition.Content = (UserControl)m_saveCancelCtrl;
            ////SaveCancelTransition.Visibility = Visibility.Visible;
            //m_b3IconColor =  SettingViewModel.Instance.Settings.B3IconColor_;
            //cmbxIconColor.ItemsSource = m_b3IconColor.OrderBy(l => l.ColorValue).Select(l => l.ColorValue).ToList();
            //lblNewOperator = new Label();
            //lblNewOperator.Visibility = Visibility.Visible;
            //btnDelete = new Button();
            //btnDelete.Visibility = Visibility.Hidden;
            //lblSavedNotification = new Label();
            //lblSavedNotification.Visibility = Visibility.Hidden;

        }

        //public Button btnDelete { get; set; }
   
        public void LoadDataIntoVar(int operatorID)
        {
            //lblNewOperator.Visibility = Visibility.Hidden;
            //btnDelete.Visibility = Visibility.Visible;
            //m_operator = SettingViewModel.Instance.Operators.Single(l => l.OperatorId == operatorID);
            //m_operatorname = m_operator.OperatorName;
            //m_operatorDescription = m_operator.OperatorNameDescription;
            //m_contactName = m_operator.ContactName;
            //m_address = m_operator.Address;
            //m_city = m_operator.City;
            //m_state = m_operator.State;
            //m_zipcode = m_operator.ZipCode;
            //m_phoneNumber = m_operator.PhoneNumber;
            //m_faxNumber = m_operator.FaxNumber;
            //m_iconColor = m_operator.IconColor;
            //SetColorIntoTheControls(m_iconColor);
            //PopulateDataIntoControls();
        }
    
        private void UpdateCurrentOperator(Operator NewOperatorDetail)
        {
            //m_operator.OperatorName = NewOperatorDetail.OperatorName;
            //m_operator.OperatorNameDescription = NewOperatorDetail.OperatorNameDescription;
            //m_operator.ContactName = NewOperatorDetail.ContactName;
            //m_operator.Address = NewOperatorDetail.Address;
            //m_operator.City = NewOperatorDetail.City;
            //m_operator.State = NewOperatorDetail.State;
            //m_operator.ZipCode = NewOperatorDetail.ZipCode;
            //m_operator.PhoneNumber = NewOperatorDetail.PhoneNumber;
            //m_operator.FaxNumber = NewOperatorDetail.FaxNumber;
            //m_operator.IconColor = NewOperatorDetail.IconColor;
            
        }


        public void ClearSavedNotification()
        {
            //if (lblSavedNotification.Visibility != Visibility.Hidden) lblSavedNotification.Visibility = Visibility.Hidden;
        }

        private void SetBorderBrushToDefault()
        {
            //txtbxOperatorName.BorderBrush = Brushes.LightGray;
        }

        private void SetColorIntoTheControls(int m_iconColor)
        {
            //m_colorDef = m_b3IconColor.Single(l => l.ColorID == m_iconColor).ColorValue;
        }


        private void PopulateDataIntoControls()
        {
            //txtbxOperatorName.Text = m_operatorname;
            //txtbxOperatorNameDescription.Text = m_operatorDescription;
            //txtbxContactName.Text = m_contactName;
            //txtbxAddress.Text = m_address;
            //txtbxCity.Text = m_city;
            //txtbxState.Text = m_state;
            //txtbxZipCode.Text = m_zipcode.Trim();
            //txtbxPhoneNumber.Text = m_phoneNumber;
            //txtbxFaxNumber.Text = m_faxNumber;
            //cmbxIconColor.SelectedIndex = cmbxIconColor.Items.IndexOf(m_colorDef);
            //lblNewOperator.Content = m_operatorname;
        }

        //public Label lblNewOperator { get; set; }

        private bool IsModify(Operator NewOperatorSetting )
        {
            bool result = false;
            //m_isOperatorRename = false;
            //if (NewOperatorSetting.OperatorName != m_operator.OperatorName)
            //{
            //    result = true;
            //    m_isOperatorRename = true;
            //    SessionViewModel.Instance.IsOperatorListModify = true;
            //}
            //else if (NewOperatorSetting.OperatorNameDescription != m_operator.OperatorNameDescription)
            //{
            //    result = true;
            //}
            //else if (NewOperatorSetting.ContactName != m_operator.ContactName)
            //{
            //    result = true;
            //}
            //else if (NewOperatorSetting.Address != m_operator.Address)
            //{
            //    result = true;
            //}
            //else if (NewOperatorSetting.City != m_operator.City)
            //{
            //    result = true;
            //}
            //else if (NewOperatorSetting.State != m_operator.State)
            //{
            //    result = true;
            //}
            //else if(NewOperatorSetting.ZipCode != m_operator.ZipCode)
            //{
            //    result = true;
            //}
            //else if (NewOperatorSetting.PhoneNumber != m_operator.PhoneNumber)
            //{
            //    result = true;

            //}
            //else if (NewOperatorSetting.FaxNumber != m_operator.FaxNumber)
            //{
            //    result = true;
            //}
            //else if (NewOperatorSetting.IconColor != m_operator.IconColor)
            //{
            //    result = true;
            //}

            return result;

        }

        private bool ValidateUserInput()
        {
            bool result = true; //True = validation ok ; false validation not ok.
            //if (string.IsNullOrEmpty(txtbxOperatorName.Text) == true || string.IsNullOrWhiteSpace(txtbxOperatorName.Text) == true)
            //{
            //    result = false;
            //    txtbxOperatorName.BorderBrush = Brushes.Red;
            //}

                return result;
        }

        private void SetElementToEmpty(UIElement element)
        {
            //if (element is TextBox)
            //{
            //    TextBox txt = (TextBox)element;
            //    txt.Text = string.Empty;

            //}
            //else if (element is ComboBox)
            //{
            //    ComboBox cmbxCurrent = (ComboBox)element;
            //    cmbxCurrent.SelectedIndex = -1;
            //}
        }

        private void ClearAllDataIntoControls()
        {
            //foreach (UIElement element in gridCharity.Children)
            //{
            //    if (element is StackPanel)
            //    {
            //        StackPanel stkpnl = (StackPanel)element;
            //        foreach (UIElement elemStkPnl in stkpnl.Children)
            //        {
            //            SetElementToEmpty(elemStkPnl);
            //        }
            //    }
            //}

        }


        void m_btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Saved the current control sertting
            //Operator Set = new Operator();
            //Set.OperatorName = txtbxOperatorName.Text;
            //Set.OperatorNameDescription = txtbxOperatorNameDescription.Text;
            //Set.ContactName = txtbxContactName.Text;
            //Set.Address = txtbxAddress.Text;
            //Set.City = txtbxCity.Text;
            //Set.State = txtbxState.Text;
            //Set.ZipCode = txtbxZipCode.Text;
            //Set.PhoneNumber = txtbxPhoneNumber.Text;
            //Set.FaxNumber = txtbxFaxNumber.Text;
            //if (cmbxIconColor.SelectedIndex != -1)
            //{
            //    Set.IconColor = m_b3IconColor.Single(l => l.ColorValue.ToString() == cmbxIconColor.SelectedItem.ToString()).ColorID;
            //}
            //else
            //{
            //    //default red.
            //    Set.IconColor = 1;
            //}

            //if (ValidateUserInput() == false)
            //{
            //    return;
            //}

            //if (m_isNewOperator == false)//Update
            //{

            //    if (IsModify(Set))//Check if theres a changes made.
            //    {
            //        Set.OperatorId = m_operator.OperatorId;

            //        Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            //        SetB3OperatorMessage msg = new SetB3OperatorMessage(Set, 0);
            //        msg.Send();
            //        Mouse.OverrideCursor = null;

            //        if (msg.ReturnCode != ServerReturnCode.Success)
            //        {
            //            throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set operator detail failed.", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
            //        }
            //        else
            //        {
            //            UpdateCurrentOperator(Set);
            //            m_currentOperatorId = m_operator.OperatorId;
            //            lblSavedNotification.Visibility = Visibility.Visible;
            //        }
            //    }
            //}
            //else//New 
            //{
            //    Set.OperatorId = 0;
            //    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            //    SetB3OperatorMessage msg = new SetB3OperatorMessage(Set, 0);
            //    msg.Send();
            //    Mouse.OverrideCursor = null;

            //    if (msg.ReturnCode != ServerReturnCode.Success)
            //    {
            //        throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set operator detail failed.", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
            //    }
            //    else
            //    {
            //        Set.OperatorId = msg.OperatorID;
            //        m_currentOperatorId = Set.OperatorId;
            //        SettingViewModel.Instance.Operators.Add(Set);
            //        lblSavedNotification.Visibility = Visibility.Visible;
            //        SessionViewModel.Instance.IsOperatorListModify = true;
            //    }
            //}
        }

        //public Label lblSavedNotification { get; set; }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Operator Set = new Operator();
            //Set = m_operator;
           
            //SetB3OperatorMessage msg = new SetB3OperatorMessage(Set, 1);
            //msg.Send();

            //if (msg.ReturnCode != ServerReturnCode.Success)
            //{
            //    throw new B3CenterException(string.Format(CultureInfo.CurrentCulture, "B3 Set operator detail failed.", ServerErrorTranslator.GetReturnCodeMessage(msg.ReturnCode)));
            //}
            //else
            //{
            //    m_currentOperatorId = 0;
            //    SettingViewModel.Instance.Operators.Remove(m_operator);
            //    SessionViewModel.Instance.IsOperatorListModify = true;
            //}
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearSavedNotification();
            //SetBorderBrushToDefault();
        }

        void m_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //if (m_operator != null)
            //{
            //    LoadDataIntoVar(m_operator.OperatorId);
            //}
            //else
            //{
            //    ClearAllDataIntoControls();
            //}
        }

        private void txtbxNumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9]+");
            //e.Handled = regex.IsMatch(e.Text);
        }

        private void txtbx_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //bool notAllow = false;

            //if (e.Key == Key.Space)
            //{
            //    notAllow = true;
            //}
            //else
            //    if (e.Key == Key.Back)
            //    {
            //        notAllow = false;
            //    }
            //e.Handled = notAllow;
        }

    }
}
