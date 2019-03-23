﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TinyCMS.Models.Data.DTO;

namespace TinyCMS.Models.ViewModels.Pages
{
    public class PageViewModel
    {
        public PageViewModel()
        {
        }

        public PageViewModel(Page row)
        {
            Id = row.Id;
            Title = row.Title;
            Slug = row.Slug;
            Body = row.Body;
            Sorting = row.Sorting;
            HasSidebar = row.HasSidebar;
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3)]
        public String Title { get; set; }
        public String Slug { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public String Body { get; set; }
        public int Sorting { get; set; }
        public bool HasSidebar { get; set; }
    }
}