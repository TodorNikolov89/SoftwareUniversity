namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Homework
    {
        public int HomeworkId { get; set; }

        public string Content { get; set; }

        public EnumHomeworkContent ContentType { get; set; }

        public DateTime SubmissionTime { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

    }

    public enum EnumHomeworkContent
    {
        Application,
        Pdf,
        Zip
    }
}
