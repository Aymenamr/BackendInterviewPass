﻿namespace InterviewPass.WebApi.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int MinScore {  get; set; }
        public int MaxScore {  get; set; }
    }
}
