using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage="enter stg")]
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
      

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}