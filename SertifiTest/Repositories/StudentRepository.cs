using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using dm = SertifiTest.Models.Domain;
using dto = SertifiTest.Models.DTO;
using System.Net.Http.Headers;
using AutoMapper;
using SertifiTest.Models;

namespace SertifiTest.Repositories
{
    public class StudentRepository
    {
        //TODO Should implement Interface contract so we can test consistently
        public static async Task<List<dm.Student>> GetStudents()
        {
            SertifiHttpClient client = SertifiHttpClient.GetMyHttpClient();
            List<dto.Student> students = new List<dto.Student>();
            HttpResponseMessage response = await client.GetAsync("/api/Students");

            if (response.IsSuccessStatusCode)
            {
                students = response.Content.ReadAsAsync<List<dto.Student>>().Result;
            }

            return Mapper.Map<List<dm.Student>>(students);
        }

        internal static bool PutStudentAggregate(dm.StudentAggregate studentAggregate)
        {
            SertifiHttpClient client = SertifiHttpClient.GetMyHttpClient();
            var response = client.PutAsJsonAsync("/api/StudentAggregate", studentAggregate).Result;
            if (!response.IsSuccessStatusCode)
            {
                //Log this if we have a failure
                var log = response.StatusCode + " " + response.Content;
                return false;
            }

            return true;
        }
    }
}
