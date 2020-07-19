using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string Description { get; set; }

        [DefaultValue(JobStatus.Created)]
        public JobStatus status { get; set; }
    }
}
