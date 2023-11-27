﻿using System.ComponentModel.DataAnnotations;

namespace EBlog.DAL.Models
{
    public class BlogModel
    {
        [Key]
        public int? BlogId { get; set; }
        public string? BlogHeader { get; set; }
        public string? BlogContent { get; set; }
        public int UserId { get; set; }
    }
}
