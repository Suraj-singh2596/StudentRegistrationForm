using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class StudentInsertRequet
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? AboutYourself { get; set; } 
        public string? inputGroupFile01 { get; set; }
    }

    public class StudentInsertResponse
    {
        public int? code { get; set;}
        public string? Data { get; set;}
    }

    public class StudentRequest
    {
        public string? StudentId { get; set; }
    }


    public class UpdateRequet
    {
        public string? StudentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? AboutYourself { get; set; }
        public string? inputGroupFile01 { get; set; }
    }

}

