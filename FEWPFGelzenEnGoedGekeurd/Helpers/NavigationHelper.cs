using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FEWPFGelzenEnGoedGekeurd.Helpers
{
    public static class NavigationHelper
    {
        public static void NavigateTo<T>(Window currentWindow) where T : Window
        {
            var window = App.ServiceProvider.GetService<T>();
            window.Left = currentWindow.Left;
            window.Top = currentWindow.Top;
            //todo eric fix
            if (currentWindow is MainWindow) //typeof(T) == typeof(MainWindow) &&
            {
                currentWindow.Hide();
            }
            else
            {
                currentWindow.Close();
            }

            if (!window.IsVisible)
            {
                window.Show();
            }
            else
            {
                window.Activate();
            }

        }
    }
}
