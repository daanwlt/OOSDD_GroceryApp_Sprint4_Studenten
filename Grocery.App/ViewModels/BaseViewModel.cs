using CommunityToolkit.Mvvm.ComponentModel;

namespace Grocery.App.ViewModels
{
    /// <summary>
    /// Base class for all ViewModels in the application.
    /// Provides common functionality and properties that all ViewModels share.
    /// Follows HBO-ICT coding guidelines for inheritance and base classes.
    /// </summary>
    public abstract partial class BaseViewModel : ObservableObject
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title of the current view.
        /// Used for display purposes in the UI.
        /// </summary>
        [ObservableProperty]
        private string _title = string.Empty;

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Loads data for the ViewModel.
        /// Override this method in derived classes to implement specific loading logic.
        /// </summary>
        public virtual void Load() 
        {
            // Base implementation - can be overridden by derived classes
        }

        /// <summary>
        /// Called when the view appears.
        /// Override this method in derived classes to implement specific appearing logic.
        /// </summary>
        public virtual void OnAppearing() 
        {
            // Base implementation - can be overridden by derived classes
        }

        /// <summary>
        /// Called when the view disappears.
        /// Override this method in derived classes to implement specific disappearing logic.
        /// </summary>
        public virtual void OnDisappearing() 
        {
            // Base implementation - can be overridden by derived classes
        }

        #endregion
    }
}
