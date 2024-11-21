namespace SectionViewExample.Models
{
    public class CommentService
    {
        public static async Task<List<Comment>> GetRecentCommentsAsync()
        {
            await Task.Delay(500);
            return new List<Comment>
            {
                new Comment(){Text = "This is a great post!", User= "Alice"},
                new Comment(){Text = "Very informative, thanks for sharing.", User= "Bob"},
                new Comment(){Text = "I had a similar experience.", User= "Charlie"}
            };
        }
    }
}
