using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TinyCMS.Models.Data.DTO;

namespace TinyCMS.Models.Data
{
    public class TinyCMSDB : DbContext
    {
        public DbSet<Page> Pages { get; set; }
    }
}