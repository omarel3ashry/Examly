﻿using DataAccessLibrary.Data;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class ExamTakenRepository : IExamTakenRepository
    {
        private readonly ESContext _context;

        public ExamTakenRepository(ESContext context)
        {
            _context = context;
        }
        public List<ExamTaken> GetAll()
        {
            return _context.ExamsTaken.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<ExamTaken>> GetAllAsync()
        {
            return _context.ExamsTaken.Where(e => !e.IsDeleted).ToListAsync();
        }
        public List<ExamTaken> GetAllWithIncludes()
        {
            return _context.ExamsTaken
                .Where(e => !e.IsDeleted)
                .Include(e => e.Student)
                .Include(e => e.Exam)
                .ToList();
        }

        public Task<List<ExamTaken>> GetAllWithIncludesAsync()
        {
            return _context.ExamsTaken
                .Where(e => !e.IsDeleted)
                .Include(e => e.Student)
                .Include(e => e.Exam)
                .ToListAsync();
        }

        public ExamTaken? GetById(int examId)
        {
            return _context.ExamsTaken.Find(examId);
        }

        public ValueTask<ExamTaken?> GetByIdAsync(int examId)
        {
            return _context.ExamsTaken.FindAsync(examId);
        }

        public ExamTaken? GetByIdWithIncludes(int examId)
        {
            return _context.ExamsTaken
                .Include(e => e.Student)
                .Include(e => e.Exam)
                .FirstOrDefault(e => e.ExamId == examId);
        }

        public Task<ExamTaken?> GetByIdWithIncludesAsync(int examId)
        {
            return _context.ExamsTaken
                .Include(e => e.Student)
                .Include(e => e.Exam)
                .FirstOrDefaultAsync(e => e.ExamId == examId);
        }

        public int Add(ExamTaken entity)
        {
            _context.ExamsTaken.Add(entity);
            _context.SaveChanges();
            return entity.ExamId;
        }

        public async Task<int> AddAsync(ExamTaken entity)
        {
            _context.ExamsTaken.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ExamId;
        }

        public bool Update(ExamTaken entity)
        {
            if (entity != null)
            {
                _context.ExamsTaken.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(ExamTaken entity)
        {
            if (entity != null)
            {
                _context.ExamsTaken.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var examTaken = _context.ExamsTaken.Find(id);
            if (examTaken != null)
            {
                examTaken.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var examTaken = _context.ExamsTaken.Find(id);
            if (examTaken != null)
            {
                examTaken.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public ExamTaken? Select(Expression<Func<ExamTaken, bool>> predicate)
        {
            return _context.ExamsTaken.Where(e => !e.IsDeleted).Where(predicate).FirstOrDefault();
        }

        public Task<ExamTaken?> SelectAsync(Expression<Func<ExamTaken, bool>> predicate)
        {
            return _context.ExamsTaken.Where(e => !e.IsDeleted).Where(predicate).FirstOrDefaultAsync();
        }

        public List<ExamTaken> SelectAll(Expression<Func<ExamTaken, bool>> predicate)
        {
            return _context.ExamsTaken.Where(e => !e.IsDeleted).Where(predicate).ToList();
        }

        public Task<List<ExamTaken>> SelectAllAsync(Expression<Func<ExamTaken, bool>> predicate)
        {
            return _context.ExamsTaken.Where(e => !e.IsDeleted).Where(predicate).ToListAsync();
        }

        public ExamTaken? GetByStudentId(int studentId)
        {
            return _context.ExamsTaken.Find(studentId);
        }

        public async Task<ExamTaken?> GetByStudentIdAsync(int studentId)
        {
            return await _context.ExamsTaken.FindAsync(studentId);
        }

        public List<ExamTaken> GetAllByStudentIdWithIncludes(int studentId)
        {
            return _context.ExamsTaken
                        .Include(e => e.Student)
                        .Include(e => e.Exam)
                        .ThenInclude(e => e.Course)
                        .Where(e => !e.IsDeleted && e.StudentId == studentId)
                        .ToList();
        }

        public Task<List<ExamTaken>> GetAllByStudentIdWithIncludesAsync(int studentId)
        {
            return _context.ExamsTaken
                     .Include(e => e.Student)
                     .Include(e => e.Exam)
                     .ThenInclude(e => e.Course)
                     .Where(e => !e.IsDeleted && e.StudentId == studentId)
                     .ToListAsync();
        }

        public ExamTaken? GetExamTakenWithIncludes(int studentId, int examId)
        {
            return _context.ExamsTaken
                    .Include(e => e.Student)
                    .Include(e => e.Exam)
                    .FirstOrDefault(e => !e.IsDeleted && e.StudentId == studentId && e.ExamId == examId);
        }

        public Task<ExamTaken?> GetExamTakenWithIncludesAsync(int studentId, int examId)
        {
            return _context.ExamsTaken
                    .Include(e => e.Student)
                    .Include(e => e.Exam)
                    .FirstOrDefaultAsync(e => !e.IsDeleted && e.StudentId == studentId && e.ExamId == examId);
        }
    }
}
