using CinemaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaApp.Windows
{
    /// <summary>
    /// Interaction logic for SessionEditorWindow.xaml
    /// </summary>
    public partial class SessionEditorWindow : Window
    {
        private Session _session;
        public SessionEditorWindow(Session session)
        {
            InitializeComponent();
            if (session!=null)
            {
                
                CBoxHall.Text = session.Hall.Name;
                CBoxMovie.Text = session.Movie.Name;
                DatePickerSession.SelectedDate = session.DateTime;
                TBoxAmount.Text = session.Hall.PlacesCount.ToString();
               
                _session = session;
            }
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_session == null)
                {
                    _session = new Session()
                    {
                   
                    };
                    App.Context.Sessions.Add(_session);
                }
                else
                {
           
                }
                App.Context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch
            {
                _ = MessageBox.Show("Произошла ошибка. Проаерьте правильность заполнения полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
