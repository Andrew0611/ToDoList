using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Todolist
    {
        public int Id { get; set; }
        public string? Subjects { get; set; }
        public string? Describe { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? TimeLeft { get; set; }
    }
}
