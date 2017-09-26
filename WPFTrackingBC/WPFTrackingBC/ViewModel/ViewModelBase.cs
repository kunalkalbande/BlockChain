using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTrackingBC.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region public Event
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <remarks></remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INotifyPropertyChanged implementor
        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <remarks></remarks>
        protected void NotifyPropertyChanged(string propertyName)
        {
            CheckPropertyNameIsValid(propertyName);
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Checks the property name is valid.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <remarks></remarks>
        [Conditional("DEBUG")]
        private void CheckPropertyNameIsValid(string propertyName)
        {
            // INotifyPropertyChanged notifies via strings, so use 
            // this helper to check that the string is accurate in debug builds.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                new Exception("UI : Invalid property name: " + propertyName);
        }
        #endregion
    }
}
