using System;
using System.ComponentModel;
using System.Windows;
using System.Deployment.Application;
using System.Diagnostics;

namespace SearchListOptimizing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationDeployment _currentDeployment;
        public MainWindow()
        {
            InitializeComponent();
            if (!ApplicationDeployment.IsNetworkDeployed)
            {
                Debug.WriteLine("Error, not Network Deployed");
            }
            else
            {
                MessageBox.Show($"Current version: {ApplicationDeployment.CurrentDeployment.CurrentVersion}");
                _currentDeployment = ApplicationDeployment.CurrentDeployment;
                _currentDeployment.CheckForUpdateCompleted += CurrentDeploymentUpdateCompleted;
                _currentDeployment.CheckForUpdateAsync();
            }
        }

        private void CurrentDeploymentUpdateCompleted(object sender, CheckForUpdateCompletedEventArgs args)
        {
            
            if (args.UpdateAvailable)
            {
                MessageBox.Show($"An update is available! Version:{args.AvailableVersion}");
                _currentDeployment.UpdateCompleted += CurrentDeploymentOnUpdateCompleted;
                _currentDeployment.UpdateAsync();
            }
            _currentDeployment.CheckForUpdateCompleted -= CurrentDeploymentUpdateCompleted;
        }

        private void CurrentDeploymentOnUpdateCompleted(object sender, AsyncCompletedEventArgs args)
        {
            MessageBox.Show(args.Error != null
                ? "An error occured!"
                : "Updated was a success! The application will now restart");
            _currentDeployment.UpdateCompleted -= CurrentDeploymentOnUpdateCompleted;
        }
    }
}
