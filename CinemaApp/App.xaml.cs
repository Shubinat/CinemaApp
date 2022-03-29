using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.CinemaDatabaseEntities Context { get; private set; } = new Entities.CinemaDatabaseEntities();
        public static Entities.User AuthUser { get; set; }
    }
}
