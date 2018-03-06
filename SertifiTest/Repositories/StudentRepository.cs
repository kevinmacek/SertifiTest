using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using dm = SertifiTest.Models.Domain;
using dto = SertifiTest.Models.DTO;
using System.Net.Http.Headers;
using AutoMapper;

namespace SertifiTest.Repositories
{
    public class StudentRepository
    {
        //TODO Should implement Interface contract so we can test consistently
        //TODO Have a factory for the HTTP client
        //TODO Move the base url to the factory, get this base url from a config file

        public static async Task<List<dm.Student>> GetStudents()
        {
            HttpClient client = new HttpClient();
            List<dto.Student> students = new List<dto.Student>();
            HttpResponseMessage response = await client.GetAsync("http://apitest.sertifi.net/api/Students");

            if (response.IsSuccessStatusCode)
            {
                students = response.Content.ReadAsAsync<List<dto.Student>>().Result;
            }

            return Mapper.Map<List<dm.Student>>(students);
        }
    }
}
