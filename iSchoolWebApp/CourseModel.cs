using Newtonsoft.Json;

namespace iSchoolWebApp.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    
    public class CourseRootModel
    {
        public CourseModel[] Course { get; set; }
    }

    public class CourseModel
    {
        public string courseID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
