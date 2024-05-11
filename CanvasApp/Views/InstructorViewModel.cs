using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using CanvasApp.Models;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using CanvasApp.Services;


namespace CanvasApp.ViewModels
{
    public partial class InstructorViewModel : ObservableObject
    {

        [ObservableProperty]
        private Course selectedCourseToDelete;

        [ObservableProperty]
        private Person selectedStudentToDelete;

        [ObservableProperty]
        private string courseName;

        [ObservableProperty]
        private string courseCode;

        [ObservableProperty]
        private string courseDescription;

        [ObservableProperty]
        private string studentName;

        [ObservableProperty]
        private string studentClassification;

        [ObservableProperty]
        private Course selectedCourse;

        [ObservableProperty]
        private Person selectedStudent;

        [ObservableProperty]
        private string moduleName;
        [ObservableProperty]
        private string moduleDescription;
        [ObservableProperty]
        private string assignmentName;
        [ObservableProperty]
        private string assignmentDescription;
        [ObservableProperty]
        private string assignmentDueDate;

        [ObservableProperty]
        private Module selectedModuleToAdd;

        [ObservableProperty]
        private Assignment selectedAssignmentToAdd;

        private readonly DatabaseHelper _databaseHelper;


        private readonly StudentDataService _studentDataService;
        public ObservableCollection<Person> Students => _studentDataService.Students;
        public ObservableCollection<Course> Courses => _studentDataService.Courses;
        public ObservableCollection<Module> Modules => _studentDataService.Modules;
        public ObservableCollection<Assignment> Assignments => _studentDataService.Assignments;


        public ICommand AddCourseCommand { get; }
        public ICommand AddStudentCommand { get; }
        public ICommand LinkStudentToCourseCommand { get; }
        public ICommand AddModuleCommand { get; }
        public ICommand AddAssignmentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand DeleteCourseCommand { get; }




        public InstructorViewModel(StudentDataService studentDataService, DatabaseHelper databaseHelper)
        {
            _studentDataService = studentDataService;
            _databaseHelper = databaseHelper;

            AddCourseCommand = new RelayCommand(AddCourse);
            AddStudentCommand = new RelayCommand(AddStudent);
            LinkStudentToCourseCommand = new RelayCommand(LinkStudentToCourse);
            AddModuleCommand = new RelayCommand(AddModule);
            AddAssignmentCommand = new RelayCommand(AddAssignment);
            DeleteCourseCommand = new RelayCommand(DeleteCourse, () => SelectedCourseToDelete != null);
            DeleteStudentCommand = new RelayCommand(DeleteStudent, () => SelectedStudentToDelete != null);
        }

        private void AddCourse()
        {
            var newCourse = new Course
            {
                Name = CourseName,
                Code = CourseCode,
                Description = CourseDescription
            };

            _studentDataService.Courses.Add(newCourse);
            _databaseHelper.SaveCourse(newCourse);

            CourseName = string.Empty;
            CourseCode = string.Empty;
            CourseDescription = string.Empty;
        }
        private void DeleteCourse()
        {
            if (SelectedCourseToDelete != null)
            {
                Courses.Remove(SelectedCourseToDelete);
                SelectedCourseToDelete = null; 
            }
        }

        private void AddStudent()
        {
            var newStudent = new Person
            {
                Name = studentName,
                Classification = studentClassification
            };

            _studentDataService.Students.Add(newStudent);
            _databaseHelper.SavePerson(newStudent);

            StudentName = string.Empty;
            StudentClassification = string.Empty;
        }

        private void DeleteStudent()
        {
            if (SelectedStudentToDelete != null)
            {
                Students.Remove(SelectedStudentToDelete);
                SelectedStudentToDelete = null;
            }
        }

        private void LinkStudentToCourse()
        {
            if (SelectedStudent != null && SelectedCourse != null)
            {
                SelectedCourse.Roster.Add(SelectedStudent);
                _databaseHelper.UpdateCourse(SelectedCourse);

                var tempCourse = SelectedCourse;
                SelectedCourse = null;
                SelectedCourse = tempCourse;

                SelectedStudent = null;
            }
        }




        private void AddModule()
        {
            if (SelectedCourse != null)
            {
                var newModule = new Module
                {
                    Name = ModuleName,
                    Description = ModuleDescription
                };

                

                SelectedCourse.Modules.Add(newModule);
                _studentDataService.Modules.Add(newModule);
                _databaseHelper.SaveModule(newModule);

                ModuleName = string.Empty;
                ModuleDescription = string.Empty;
            }
        }

        private void AddAssignment()
        {
            if (SelectedCourse != null)
            {
                var newAssignment = new Assignment
                {
                    Name = AssignmentName,
                    Description = AssignmentDescription,
                    DueDate = AssignmentDueDate
                };

                SelectedCourse.Assignments.Add(newAssignment);
                _studentDataService.Assignments.Add(newAssignment);
                _databaseHelper.SaveAssignment(newAssignment);

                AssignmentName = string.Empty;
                AssignmentDescription = string.Empty;
                AssignmentDueDate = string.Empty;
            }
        }


    }
}



