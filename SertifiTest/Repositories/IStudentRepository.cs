using System;
using dm = SertifiTest.Models.Domain;
using dto = SertifiTest.Models.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SertifiTest.Repositories
{
    public interface IStudentRepository
    {
        Task<List<dm.Student>> GetStudents();
    }
}
