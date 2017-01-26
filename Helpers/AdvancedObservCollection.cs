﻿//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Collections.Specialized;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;

//namespace GameTech.Elite.Client.Modules.B3Center.Helpers
//{
//   public class AdvancedObservCollection<T> : ObservableCollection<T>
//    {
//        private bool _suppressNotification = false;

//        public bool AllowNotifications { get { return _suppressNotification; } }

//        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
//        {
//            if (!_suppressNotification)
//                base.OnCollectionChanged(e);
//        }

//        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
//        {
//            if (!_suppressNotification)
//                base.OnPropertyChanged(e);
//        }

//        public void ActivateNotifications()
//        {
//            _suppressNotification = false;
//        }

//        public void DesactivateNotifications()
//        {
//            _suppressNotification = true;
//        }

//        public void AddRange(IEnumerable<T> list)
//        {
//            if (list == null)
//                throw new ArgumentNullException("list");

//            _suppressNotification = true;

//            foreach (T item in list)
//            {
//                Add(item);
//            }
//            _suppressNotification = false;
//            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
//        }


//    }
//}
