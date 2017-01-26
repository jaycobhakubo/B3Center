#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

namespace GameTech.Elite.Client.Modules.B3Center.Business
{
   class Operator
    {
        #region Member Variables
        #endregion

        #region Constructors
        public Operator(int operatorId, string operatorName, string operatordesc, string contactname, string address, string city, string state, string zipcode, string phonenumber, string faxnumber, int iconcolor)
        {
            OperatorId = operatorId;
            OperatorName = operatorName;
            OperatorNameDescription = operatordesc;
            ContactName = contactname;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipcode;
            PhoneNumber = phonenumber;
            FaxNumber = faxnumber;
            IconColor = iconcolor;
            //IconColorValue = //B3IconColor.Single(l => l.ColorID == value.IconColor);
        }

        //Session Stsart
        public Operator(int operatorId, string operatorName)
        {
            OperatorId = operatorId;
            OperatorName = operatorName;
        }


        public Operator()
        {
          
        }

        #endregion

        #region Member Methods
        /// <summary>
        /// Returns the operator name as a string
        /// </summary>
        public override string ToString()
        {
            return OperatorName;
        }
        #endregion

        #region Member Property

   
        public int OperatorId
        {
            get;
           set;
        }

        public string OperatorName
        {
            get;
            set;
        }

        public string OperatorNameDescription
        {
            get;
            set;
        }

        public string ContactName
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string ZipCode
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }

        public string FaxNumber
        {
            get;
            set;
        }

        public int IconColor
        {
            get;
            set;
        }

        public B3IconColor IconColorValue
        {
            get;
            set;
        }

        #endregion


    }
}
