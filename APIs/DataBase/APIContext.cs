using APIs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIs.DataBase
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : 
            base(options)
        {

        }

        public DbSet<Palavra> Palavras { get; set; }
    }
}
