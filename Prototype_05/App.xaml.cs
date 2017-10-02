using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using log4net;
using System.Reflection;

namespace Prototype_05
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Global Logservice
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Default constructor
        /// </summary>
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += (CurrentDomainUnhandledException);
        }

        /// <summary>
        /// Event CurrentDomainUnhandledException
        /// </summary>
        /// <param name="sender">Parameter sender</param>
        /// <param name="e">Parameter UnhandledExceptionEventArgs</param>
        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            Logger.Error(exception.Message);
        }
    }
}