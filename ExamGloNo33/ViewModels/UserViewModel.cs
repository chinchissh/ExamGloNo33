using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ExamGloNo33.DataContext;
using ExamGloNo33.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamGloNo33.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _users;
        private string _filterText;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                ApplyFilter();
            }
        }

        public UserViewModel()
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new ApplicationContext())
            {
                Users = new ObservableCollection<User>(context.Users.Include(u => u.Employee).Include(u => u.Role).ToList());
            }
        }

        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                LoadUsers();
            }
            else
            {
                using (var context = new ApplicationContext())
                {
                    Users = new ObservableCollection<User>(
                        context.Users.Include(u => u.Employee).Include(u => u.Role)
                        .Where(u => u.Login.Contains(FilterText) || u.Employee.FullName.Contains(FilterText))
                        .ToList());
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
