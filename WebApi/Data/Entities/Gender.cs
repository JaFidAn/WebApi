using System.Collections.Generic;

namespace WebApi.Data.Entities
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
