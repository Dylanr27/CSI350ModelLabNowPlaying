namespace NowPlaying.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string RunTime { get; set; }
        public string ImageURL { get; set; }
        public ICollection<Ticket> Tickets { get; }

    }
}
