using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDetails.Core
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid ProfessionId { get; set; }

        public virtual Profession Profession { get; set; }


    }
}
