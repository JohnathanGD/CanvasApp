using SQLite;
using CanvasApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

//Currently a work in progress attempting to make it so that save data persists
namespace CanvasApp.Services
{
    public class DatabaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseHelper(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
            _database.CreateTableAsync<Course>().Wait();
            _database.CreateTableAsync<Module>().Wait();
            _database.CreateTableAsync<Assignment>().Wait();
        }

        public Task<List<Module>> GetModulesByCourseId(int courseId)
        {
            return _database.Table<Module>().Where(m => m.CourseId == courseId).ToListAsync();
        }

        public Task<List<Assignment>> GetAssignmentsByCourseId(int courseId)
        {
            return _database.Table<Assignment>().Where(a => a.CourseId == courseId).ToListAsync();
        }

        public Task<List<Person>> GetStudentsByCourseId(int courseId)
        {
            return _database.Table<Person>().Where(p => p.CourseId == courseId).ToListAsync();
        }

        //Save methods
        public Task<int> SavePerson(Person person)
        {
            if (person.Id == 0)
                return _database.InsertAsync(person);
            else
                return _database.UpdateAsync(person);
        }

        public Task<int> SaveCourse(Course course)
        {
            if (course.Id == 0)
                return _database.InsertAsync(course);
            else
                return _database.UpdateAsync(course);
        }

        public Task<int> SaveModule(Module module)
        {
            if (module.Id == 0)
                return _database.InsertAsync(module);
            else
                return _database.UpdateAsync(module);
        }

        public Task<int> SaveAssignment(Assignment assignment)
        {
            if (assignment.Id == 0)
                return _database.InsertAsync(assignment);
            else
                return _database.UpdateAsync(assignment);
        }

        //Delete methods
        public Task<int> DeleteCourseAsync(Course course)
        {
            return _database.DeleteAsync(course);
        }

        public Task<int> DeletePersonAsync(Person person)
        {
            return _database.DeleteAsync(person);
        }

        public Task<int> DeleteModuleAsync(Module module)
        {
            return _database.DeleteAsync(module);
        }

        public Task<int> DeleteAssignmentAsync(Assignment assignment)
        {
            return _database.DeleteAsync(assignment);
        }


        // Generic Update
        public Task<int> UpdateCourse(Course course)
        {
            return _database.UpdateAsync(course);
        }

        // Retrieving lists
        public Task<List<Person>> GetAllPeopleAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        public Task<List<Course>> GetAllCoursesAsync()
        {
            return _database.Table<Course>().ToListAsync();
        }

        public Task<List<Module>> GetAllModulesAsync()
        {
            return _database.Table<Module>().ToListAsync();
        }

        public Task<List<Assignment>> GetAllAssignmentsAsync()
        {
            return _database.Table<Assignment>().ToListAsync();
        }
    }
}


