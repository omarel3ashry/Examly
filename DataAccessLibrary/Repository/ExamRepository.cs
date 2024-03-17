using DataAccessLibrary.Data;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly ESContext _context;

        public ExamRepository(ESContext context)
        {
            _context = context;
        }
        public List<Exam> GetAll()
        {
            return _context.Exams.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Exam>> GetAllAsync()
        {
            return _context.Exams.Where(e => !e.IsDeleted).ToListAsync();
        }

        public List<Exam> GetAllWithIncludes()
        {
            return _context.Exams
                .Where(e => !e.IsDeleted)
                .Include(e => e.Course)
                .Include(e => e.Questions)
                .Include(e => e.Students)
                .Include(e => e.StudentAnswers)
                .ToList();
        }

        public Task<List<Exam>> GetAllWithIncludesAsync()
        {
            return _context.Exams
                .Where(e => !e.IsDeleted)
                .Include(e => e.Course)
                .Include(e => e.Questions)
                .Include(e => e.Students)
                .Include(e => e.StudentAnswers)
                .ToListAsync();
        }

        public Exam? GetById(int id)
        {
            return _context.Exams.Find(id);
        }

        public async Task<Exam?> GetByIdAsync(int id)
        {
            return await _context.Exams.FindAsync(id);
        }

        public Exam? GetByIdWithIncludes(int id)
        {
            return _context.Exams
                .Include(e => e.Course)
                .Include(e => e.Questions)
                .ThenInclude(e=>e.Choices)
                .Include(e => e.Students)
                .Include(e => e.StudentAnswers)
                .FirstOrDefault(e => e.Id == id);
        }

        public List<ExamTaken> GetExamGradesWithIncludes(int examId)
        {
            return _context.ExamsTaken
                        .Include(e => e.Exam)
                        .Include(e => e.Student)
                        .Where(e => !e.IsDeleted && e.ExamId == examId)
                        .ToList();
        }

/*        public Exam? GetExamWithStdAnswersIncluded(int examId, int stdId)
        {
            return _context.Exams
                            .Include(e=>e.Course)
                            .Include(e=>e.Questions)
                                .ThenInclude(e=>e.Choices)
                            .Include(e => e.StudentAnswers.Where(e=>e.StudentId==stdId))
                                .ThenInclude(e=>e.Choice)
                            .Where(e=>!e.IsDeleted&&e.Id == examId)
                            .FirstOrDefault();
        }*/

        public Task<Exam?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Exams
                .Include(e => e.Course)
                .Include(e => e.Questions)
                .Include(e => e.Students)
                .Include(e => e.StudentAnswers)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public int Add(Exam entity)
        {
            _context.Exams.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public bool AddExamQuestions(List<ExamQuestion> examQuestions)
        {
            _context.ExamQuestions.AddRange(examQuestions);
            return _context.SaveChanges() == examQuestions.Count;
        }

        public bool UpdateTotalGrade(int id, int totalGrade)
        {
            var entity = _context.Exams.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (entity != null)
            {
                entity.TotalGrade = totalGrade;
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<int> AddAsync(Exam entity)
        {
            _context.Exams.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Exam entity)
        {
            if (entity != null)
            {
                _context.Exams.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Exam entity)
        {
            if (entity != null)
            {
                _context.Exams.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var exam = _context.Exams.Find(id);
            if (exam != null)
            {
                exam.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exam = _context.Exams.Find(id);
            if (exam != null)
            {
                exam.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public Exam? Select(Expression<Func<Exam, bool>> predicate)
        {
            return _context.Exams.Where(predicate).FirstOrDefault();
        }

        public Task<Exam?> SelectAsync(Expression<Func<Exam, bool>> predicate)
        {
            return _context.Exams.Where(predicate).FirstOrDefaultAsync();
        }

        public List<Exam> SelectAll(Expression<Func<Exam, bool>> predicate)
        {
            return _context.Exams.Where(predicate).ToList();
        }

        public Task<List<Exam>> SelectAllAsync(Expression<Func<Exam, bool>> predicate)
        {
            return _context.Exams.Where(predicate).ToListAsync();
        }

        public List<Exam> GetInstructorExam(int instructorId)
        {
            return _context.Exams.Include(e => e.Questions).Where(e=>e.Questions.ElementAt(0).InstructorId==instructorId).ToList();
        }

        public List<Exam> GetDeptExams(int deptId)
        {
            var exams= _context.Exams.Include(e => e.Course)
                .ThenInclude(e => e.DepartmentCourses)
                .ToList();
            var deptExams= new List<Exam>();
            foreach (var exam in exams)
            {
                foreach (var deptCourse in exam.Course.DepartmentCourses)
                {
                    if (deptCourse.DepartmentId == deptId)
                    {
                        deptExams.Add(exam);
                    }
                }
            }
            return deptExams;
        }
    }
}
