namespace Project.Models
{
    public class UpdateStudentViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Semester { get; set; }

        public string Specialization { get; set; }
    }
}
