namespace FluentApi.Models.Entity
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string? Name { get; set; }

        //RelationShip: One to Many: one doctor many patient

        public List<Patient>? Patients { get; set; }
    }
}
