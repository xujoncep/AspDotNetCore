using System.ComponentModel.DataAnnotations.Schema;

namespace FluentApi.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string? Name { get; set; }

        // Relationship: Many to one : many patient one doctor

        [ForeignKey("DoctorId")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; } 


    }
}
