using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CanvasApp.Models
{
    [Table("People")]
    public class Person : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string classification;
        private double grade;
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        [MaxLength(50)]
        public string Classification
        {
            get => classification;
            set
            {
                if (classification != value)
                {
                    classification = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Grade
        {
            get => grade;
            set
            {
                if (grade != value)
                {
                    grade = value;
                    OnPropertyChanged();
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



