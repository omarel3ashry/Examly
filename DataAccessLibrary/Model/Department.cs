namespace DataAccessLibrary.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Branch Branch { get; set; }
    }
}
