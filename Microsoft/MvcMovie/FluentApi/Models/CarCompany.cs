namespace FluentApi.Models
{
    public class CarCompany
    {
        public int CarCompanyId { get; set; }

        public string? Name { get; set; }

        //Relationship: One to One
        public CarModel? CarModel { get; set; } //Navigation Property

    }
}
