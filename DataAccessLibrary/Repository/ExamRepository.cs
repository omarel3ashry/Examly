﻿using DataAccessLibrary.Data;
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
                .Include(e => e.Students)
                .Include(e => e.StudentAnswers)
                .FirstOrDefault(e => e.Id == id);
        }

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
    }
}