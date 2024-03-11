namespace WebAppProject.FakeModels
{
    public class FakeInstructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }

        public int BranchId { get; set; }
        public FakeBranch Branch { get; set; }

    }
}
