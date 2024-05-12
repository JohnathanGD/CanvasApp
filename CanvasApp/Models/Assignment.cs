using SQLite;
using System.ComponentModel;

namespace CanvasApp.Models
{
    [Table("Assignments")]
    public class Assignment : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string description;
        private int totalAvailablePoints;
        private string dueDate;
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

        public int TotalAvailablePoints
        {
            get => totalAvailablePoints;
            set
            {
                if (totalAvailablePoints != value)
                {
                    totalAvailablePoints = value;
                    OnPropertyChanged(nameof(TotalAvailablePoints));
                }
            }
        }

        [MaxLength(20)]
        public string DueDate
        {
            get => dueDate;
            set
            {
                if (dueDate != value)
                {
                    dueDate = value;
                    OnPropertyChanged(nameof(DueDate));
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



