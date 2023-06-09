using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDark = false;
        
        public void ChangeTheme(Uri uri)
        {
            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }
    }
}
