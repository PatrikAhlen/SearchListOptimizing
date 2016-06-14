using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace SearchListOptimizing.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable, ICloneable
    {
        private bool _hasHiddenInformation;
        private readonly WeakReference _window = new WeakReference(null);

        protected ViewModelBase()
        {
            Mediator.Mediator.Instance.Register(this);
            ResizeMode = ResizeMode.NoResize;
            IsDisposed = false;
            HasHiddenInformation = false;

            IsDefaultPlugin = false;
            PluginSortOrder = null;
        }

        public virtual ResizeMode ResizeMode { get; set; }

        public bool HideWindowFunctions { get; set; }

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName { get; set; }

        public bool IsDefaultPlugin { get; protected set; }
        public int? PluginSortOrder { get; protected set; }

        public virtual bool HasHiddenInformation
        {
            get { return _hasHiddenInformation; }
            set
            {
                _hasHiddenInformation = value;
                OnPropertyChanged("HasHiddenInformation");
            }
        }

        #region Id

        /// <summary>
        /// Returns the Id this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string Id { get; protected set; }

        #endregion // Id

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }

                Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; set; }

        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

        protected void SetProperty<T>(Expression<Func<T>> propertyExpression, Action<T> propertySetter, T newValue)
        {
            SetProperty(propertyExpression, propertySetter, newValue, null);
        }

        protected void SetProperty<T>(Expression<Func<T>> propertyExpression, Action<T> propertySetter, T newValue,
            Action afterSetterInvokedAction)
        {
            var propertyName = PropertyUtil.GetPropertyName(propertyExpression);
            var type = GetType();

            var propertyInfo = type.GetProperty(propertyName);

            object existingValue = propertyInfo.GetValue(this, BindingFlags.GetProperty, null, null,
                CultureInfo.CurrentCulture);

            propertySetter.Invoke(newValue);

            if (existingValue != null && !existingValue.Equals(newValue) || existingValue == null && newValue != null)
            {
                OnPropertyChanged(propertyName);
                if (afterSetterInvokedAction != null)
                {
                    afterSetterInvokedAction.Invoke();
                }
            }
        }

        public void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertyUtil.GetPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }

        #region IDisposable Members

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        public bool IsDisposed { get; private set; }

        public Window Window
        {
            get { return (Window) _window.Target; }
            set { _window.Target = value; }
        }

        protected virtual void OnDispose()
        {
            IsDisposed = true;
        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        //~ViewModelBase()
        //{
        //string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
        //System.Diagnostics.Debug.WriteLine(msg);
        //}
#endif

        #endregion // IDisposable Members

        #region ICloneable Members
        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        protected void DoWork(Action work)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                work();
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        public void BeginWork()
        {
            Mouse.OverrideCursor = Cursors.Wait;
        }

        public void EndWork()
        {
            Mouse.OverrideCursor = null;
        }
    }
}
