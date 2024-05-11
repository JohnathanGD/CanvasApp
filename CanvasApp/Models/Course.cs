using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CanvasApp.Models
{
    [Table("Courses")]
    public class Course : INotifyPropertyChanged
    {
        private int id;
        private string code;
        private string name;
        private string description;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        [MaxLength(10)]
        public string Code
        {
            get => code;
            set
            {
                if (code != value)
                {
                    code = value;
                    OnPropertyChanged(nameof(Code));
                }
            }
        }

        [MaxLength(100)]
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        [Ignore]
        public ObservableCollection<Person> Roster { get; set; } = new ObservableCollection<Person>();

        [Ignore]
        public ObservableCollection<Module> Modules { get; set; } = new ObservableCollection<Module>();

        [Ignore]
        public ObservableCollection<Assignment> Assignments { get; set; } = new ObservableCollection<Assignment>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}




