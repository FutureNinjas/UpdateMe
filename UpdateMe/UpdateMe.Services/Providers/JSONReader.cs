using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;
using UpdateMe.Data.Models;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Services.Providers
{
    public class JSONReader : IReader
    {
        public Course ReadFile(HttpPostedFileBase file)
        {
            Course course = null;
            try
            {
                using (StreamReader reader = new StreamReader(file.InputStream))
                {
                    string readFile = reader.ReadToEnd();

                    course = JsonConvert.DeserializeObject<Course>(readFile);

                    course.DateCreated = DateTime.Now;

                    return course;

                }
            }
            catch (FileNotFoundException)
            {

            }

            return null;
        }
    }
}
