using System.Collections.ObjectModel;
using CanvasApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Windows.Input;
using CanvasApp.Services;
using Microsoft.Maui.Storage;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CanvasApp.ViewModels
{
    public partial class StudentViewModel : ObservableObject
    {


        private readonly HttpClient _httpClient;

        private readonly StudentDataService _studentDataService;

        private string _pickedFilePath;

        [ObservableProperty]
        private string pickedFileName;

        [ObservableProperty]
        private Person selectedStudent;

        partial void OnSelectedStudentChanged(Person value)
        {
            UpdateStudentCourses();
        }

        [ObservableProperty]
        private bool isFilePicked;


        [ObservableProperty]
        private Course selectedCourse;

        partial void OnSelectedCourseChanged(Course value)
        {
            ChooseStudentClass();
        }

        [ObservableProperty]
        private Module selectedCourseModules;

        [ObservableProperty]
        private Assignment selectedAssignments;

        public ObservableCollection<Module> StudentModules { get; } = new ObservableCollection<Module>();
        public ObservableCollection<Assignment> StudentAssignments { get; } = new ObservableCollection<Assignment>();

        public ObservableCollection<Course> StudentCourses { get; } = new ObservableCollection<Course>();

        public ObservableCollection<Person> Students => _studentDataService.Students;
        public ObservableCollection<Course> Courses => _studentDataService.Courses;
        public ObservableCollection<Module> Modules => _studentDataService.Modules;
        public ObservableCollection<Assignment> Assignments => _studentDataService.Assignments;

        public ICommand SelectStudentCommand { get; }
        public ICommand ChooseStudentClassCommand { get; }
        public ICommand SubmitAssignmentCommand { get; }
        public ICommand PickFileCommand { get; }

        public StudentViewModel(StudentDataService studentDataService)
        {
            _studentDataService = studentDataService;
            SelectStudentCommand = new RelayCommand(UpdateStudentCourses);
            ChooseStudentClassCommand = new RelayCommand(ChooseStudentClass);
            SubmitAssignmentCommand = new RelayCommand(SubmitAssignment, () => IsFilePicked);
            PickFileCommand = new RelayCommand(PickFile);
        }


        private void UpdateStudentCourses()
        {
            if (SelectedStudent == null)
                return;
        
            StudentCourses.Clear();

            var registeredCourses = Courses.Where(course => course.Roster.Contains(SelectedStudent));

            foreach (var course in registeredCourses)
            {
                StudentCourses.Add(course);
            }
        }

        private void ChooseStudentClass()
        {
            if (SelectedCourse == null) return;

            StudentModules.Clear();
            StudentAssignments.Clear();

            foreach (var module in SelectedCourse.Modules)
            {
                StudentModules.Add(module);
            }

            foreach (var assignment in SelectedCourse.Assignments)
            {
                StudentAssignments.Add(assignment);
            }
        }

        private async void SubmitAssignment()
        {
            if (!IsFilePicked || string.IsNullOrEmpty(_pickedFilePath))
            {
                // Optionally alert the user that no file is picked
                await App.Current.MainPage.DisplayAlert("Error", "Please pick a file before submitting.", "OK");
                return;
            }

            var success = true;
            if (success)
            {
                IsFilePicked = true;
                _pickedFilePath = null;
                await App.Current.MainPage.DisplayAlert("Success", "File uploaded successfully.", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to upload the file.", "OK");
            }
        }

        private async void PickFile()
        {
            try
            {
                PickOptions options = new();

                var fileResult = await FilePicker.PickAsync(options);

                if (fileResult != null)
                {
                    _pickedFilePath = fileResult.FullPath;
                    PickedFileName = fileResult.FileName;  
                    IsFilePicked = true; 

                    var result = await Processfile(fileResult);



                    if (result)
                        await App.Current.MainPage.DisplayAlert("File Uploaded Successfully", "The file has been uploaded successfully.", "OK");
                    else
                        await App.Current.MainPage.DisplayAlert("An error has occured.", "The file has not been uploaded successfully.", "OK");
                }
            }
            catch
            {

            }
        }


        private static async Task<bool> Processfile(FileResult fileResult)
        {
            if (fileResult == null)
                return false;

            using var fileStream = File.OpenRead(fileResult.FullPath);

            byte[] bytes;

            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                bytes = memoryStream.ToArray();
            }

            using var fileContent = new ByteArrayContent(bytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            using var form = new MultipartFormDataContent
            {
                {fileContent,"fileContent",Path.GetFileName(fileResult.FullPath) }
            };

            return await UploadFile(form);
        }

        public static async Task<bool> UploadFile(MultipartFormDataContent form)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return false;
            }

            var client = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(5),
                BaseAddress = new Uri("http://localhost:5060")

            };

            try
            {
                var response = await client.PostAsync("uploadFile", form);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }

}

