using CoreApi.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreApi.Dtos
{
    public class JobDto
    {
        public class Job
        {
            public long Id { get; set; }

            [Required]
            public string Description { get; set; }

            [DefaultValue(JobStatus.Created)]
            public JobStatus Status { get; set; }
        }
    }
}
