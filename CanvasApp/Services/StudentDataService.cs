using System.Collections.ObjectModel;
using CanvasApp.Models;

namespace CanvasApp.Services
{
    public class StudentDataService
    {
        public ObservableCollection<Person> Students { get; } = new ObservableCollection<Person>();
        public ObservableCollection<Course> Courses { get; } = new ObservableCollection<Course>();
        public ObservableCollection<Module> Modules { get; } = new ObservableCollection<Module>();
        public ObservableCollection<Assignment> Assignments { get; } = new ObservableCollection<Assignment>();

    }
}


