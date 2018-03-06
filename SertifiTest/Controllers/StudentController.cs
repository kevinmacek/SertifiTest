
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SertifiTest.Repositories;
using dm = SertifiTest.Models.Domain;

namespace SertifiTest.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        //TODO: Could set the students as a private variable here, or cache them if they are going to be unchanging
        private static List<dm.Student> _students;


        // GET api/student
        [HttpGet]
        public List<dm.Student> Get()
        {
            //StudentRepository repo = new StudentRepository();
            _students = StudentRepository.GetStudents().Result;

            int YearWithHighestAttendance = FindYearWithHighestAttendance();

            double YearWithHighestOverallGPA = FindYearWithHighestOverallGpa();

            List<int> Top10StudentIdsWithHighestGpa = FindTop10StudentIdsWithHighestGpa();

            int StudentIdMostInconsistent = FindStudentIdMostInconsistent();

            return _students;
        }

        private static int FindYearWithHighestAttendance()
        {
            var grouped = _students.SelectMany(g => g.YearlyGrades)
                                .GroupBy(g => g.Year)
                                .Select(x => new { x.Key, Count = x.Count() });

            var mostYears = grouped.Max(x => x.Count);

            var highestYears = grouped.Where(x => x.Count == mostYears).Select(x => x.Key).ToList();

            return highestYears.Min(x => x);
        }

        private static double FindYearWithHighestOverallGpa()
        {
            return _students.SelectMany(g => g.YearlyGrades)
                                  .GroupBy(g => g.Year)
                                  .Select(x => new { Year = x.Key, GpaAverage = x.Average(i => i.GPA) })
                                  .OrderByDescending(x => x.GpaAverage)
                                  .Select(x => x.Year)
                                  .FirstOrDefault();
        }

        private static List<int> FindTop10StudentIdsWithHighestGpa() 
        {
            return _students.Select(x => new { StudentId = x.Id, GpaAverage = x.YearlyGrades.Average(y => y.GPA) })
                           .OrderByDescending(x => x.GpaAverage)
                           .Select(x => x.StudentId)
                           .Take(10)
                           .ToList();
        }

        private int FindStudentIdMostInconsistent()
        {
            return _students.Select(x => new { StudentId = x.Id, GpaDifferential = x.YearlyGrades.Max(y => y.GPA) - x.YearlyGrades.Min(z => z.GPA) })
                               .OrderByDescending(x => x.GpaDifferential)
                               .Select(x => x.StudentId)
                               .FirstOrDefault();
        }

    }
}
