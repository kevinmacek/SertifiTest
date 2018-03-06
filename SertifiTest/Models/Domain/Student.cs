using System;
using System.Collections.Generic;

namespace SertifiTest.Models.Domain
{
    public class Student
    {      
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public List<double> GPARecord { get; set; }
        public List<YearlyGrade> YearlyGrades { get { return CalculateYearlyGrades(); } }

        private List<YearlyGrade> CalculateYearlyGrades ()
        {
            List<YearlyGrade> yearlyGrades = new List<YearlyGrade>();
            YearlyGrade yearlyGrade;
            var startingYear = StartYear;

            foreach (var grade in GPARecord)
            {
                yearlyGrade = new YearlyGrade
                {
                    GPA = grade,
                    Year = startingYear
                };

                yearlyGrades.Add(yearlyGrade);
                startingYear++;
            }

            return yearlyGrades;
        }
    }
}

