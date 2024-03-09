namespace DataAccessLibrary.Model
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Department> Departments { get; set; }
    }
}
