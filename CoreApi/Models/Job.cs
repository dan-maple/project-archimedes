namespace CoreApi.Models
{
    public enum JobStatus
    {
        Created,
        Scheduled,
        Started,
        Completed
    }

    public class Job
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public JobStatus status { get; set; }
    }
}
