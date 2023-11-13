using BL.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEWPFGelzenEnGoedGekeurd
{
    public class MainWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public MainWindow CreateMainWindow()
        {
            return new MainWindow(_serviceProvider.GetRequiredService<ICustomerManager>());
        }
    }
}
