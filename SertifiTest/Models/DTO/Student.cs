using System;
using System.Collections.Generic;
using SertifiTest.Models.Domain;

namespace SertifiTest.Models.DTO
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public List<double> GPARecord { get; set; }
    }
}
