namespace Server.Models.ViewModels
{
    public class TodolistViewModel
    {
        public int? Id { get; set; }
        public string? Subjects { get; set; }
        public string? Describe { get; set; }
        public string? ExpiryDate { get; set; }
        public int? TimeLeft { get; set; }
    }
}
