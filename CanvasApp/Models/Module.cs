using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CanvasApp.Models
{
    [Table("Modules")]
    public class Module : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string description;
        private int courseId;  

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


        [Indexed]
        public int CourseId
        {
            get => courseId;
            set
            {
                if (courseId != value)
                {
                    courseId = value;
                    OnPropertyChanged(nameof(CourseId));
                }
            }
        }

        [Ignore]
        public ObservableCollection<ContentItem> Content { get; set; } = new ObservableCollection<ContentItem>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

