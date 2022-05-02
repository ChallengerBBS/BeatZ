namespace BeatZ.Domain.Dtos
{
    public class TrackListDto
    {
        public int TrackId { get; set; }

        public string TrackName { get; set; } = string.Empty;

        public List<string> Artists { get; set; } = new List<string>();
    }
}
