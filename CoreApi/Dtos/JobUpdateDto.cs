using CoreApi.Models;

namespace CoreApi.Dtos
{
    public class JobUpdateDto
    {
        public string Description { get; set; }

        public JobStatus Status { get; set; }
    }
}
