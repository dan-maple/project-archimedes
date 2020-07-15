using CoreApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi
{
    public static class DbInitialiser
    {
        public static void Initialise(ApiDbContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();
            if (!context.Jobs.Any())
            {
                context.Jobs.Add(new Job { Description = "Job 1" });
                context.Jobs.Add(new Job { Description = "Job 2" });
                context.Jobs.Add(new Job { Description = "Job 3" });
                context.SaveChanges();
            }
        }
    }
}
