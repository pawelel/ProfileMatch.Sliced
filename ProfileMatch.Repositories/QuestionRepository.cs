﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public QuestionRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<List<Question>> GetQuestionsWithCategories()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Questions.Include(c => c.Category).AsNoTracking().ToListAsync();
        }

        public async Task<Question> Create(Question question)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.Questions.AddAsync(question);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<Question> Delete(Question question)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.Questions.Remove(question).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }

        public async Task<Question> FindByName(string questionName)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Questions.SingleOrDefaultAsync(q => q.Name == questionName);
        }

        public async Task<Question> FindById(int questionId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Questions.SingleOrDefaultAsync(q => q.Id == questionId);
        }

        public async Task<List<Question>> GetQuestionsForCategory(int categoryId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Questions.Where(q=>q.CategoryId==categoryId).ToListAsync();
        }

        public async Task<List<Question>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Questions.ToListAsync();
        }

        public async Task<Question> Update(Question question)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var existing = await repositoryContext.Questions.FindAsync(question.Id);
            if (existing != null)
            {
                repositoryContext.Entry(existing).CurrentValues.SetValues(question);
                await repositoryContext.SaveChangesAsync();
                return existing;
            }
            else
            {
                return question;
            }
        }
    }
}