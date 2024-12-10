using System.ComponentModel.DataAnnotations.Schema;

namespace FluentApi.Models
{
    public class CarModel
    {
        public int CarModelId { get; set; }

        public string? Model { get; set; }

        //Relationship: one to one

        [ForeignKey("CarCompanyId")]
        public int CarCompanyId { get; set; } //FK
        public CarCompany? CarCompany { get; set; }
    }
}
