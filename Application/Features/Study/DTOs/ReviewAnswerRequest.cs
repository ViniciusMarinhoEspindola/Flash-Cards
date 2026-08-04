namespace Application.Features.Study.DTOs
{
    public class ReviewAnswerRequest
    {
        public Guid SessionId { get; set; }
        public Guid CardId { get; set; }

        /// <summary>SM-2 rating: 0 = total fail, 5 = perfect recall.</summary>
        public int Rating { get; set; }
    }
}
