
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SertifiTest.Repositories;
using dm = SertifiTest.Models.Domain;

namespace SertifiTest.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StudentController : Controller
    {
        //TODO: Could implement some sort of caching here
        private static List<dm.Student> _students;

        // PUT api/student/PutStudentAggregate
        [HttpPut]
        public ActionResult PutStudentAggregate()
        {
            _students = StudentRepository.GetStudents().Result;

            dm.StudentAggregate studentAggregate = new dm.StudentAggregate
            {
                YourName = "Kevin Macek", //Obviously we would never hard code this in real life
                YourEmail = "macekkevin@gmail.com", //Obviously we would never hard code this in real life
                YearWithHighestAttendance = FindYearWithHighestAttendance(),
                YearWithHighestOverallGpa = FindYearWithHighestOverallGpa(),
                Top10StudentIdsWithHighestGpa = FindTop10StudentIdsWithHighestGpa(),
                StudentIdMostInconsistent = FindStudentIdMostInconsistent()
            };

            //Do this since we may want to make the user aware it failed on the frontend
            if (!StudentRepository.PutStudentAggregate(studentAggregate))
            {
                return BadRequest();
            }

            return Ok();
        }

        //TODO: Move all of this to a domain level class to perform business logic, here for simplicity
        private static int FindYearWithHighestAttendance()
        {
            var grouped = _students.SelectMany(g => g.YearlyGrades)
                                .GroupBy(g => g.Year)
                                .Select(x => new { x.Key, Count = x.Count() });

            var mostYears = grouped.Max(x => x.Count);

            var highestYears = grouped.Where(x => x.Count == mostYears).Select(x => x.Key).ToList();

            return highestYears.Min(x => x);
        }

        private static int FindYearWithHighestOverallGpa()
        {
            return _students.SelectMany(g => g.YearlyGrades)
                                  .GroupBy(g => g.Year)
                                  .Select(x => new { Year = x.Key, GpaAverage = x.Average(i => i.GPA) })
                                  .OrderByDescending(x => x.GpaAverage)
                                  .FirstOrDefault()
                                  .Year;
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
