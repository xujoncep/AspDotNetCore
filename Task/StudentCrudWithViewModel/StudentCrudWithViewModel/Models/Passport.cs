namespace StudentCrudWithViewModel.Models
{
    public class Passport
    {
        public int PassportId { get; set; }
        public int PassportNumber { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}
