namespace DataAccessLibrary.Model
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<Department> Departments { get; } = new List<Department>();
        public ICollection<Instructor> Instructors { get; } = new HashSet<Instructor>();
    }
}
