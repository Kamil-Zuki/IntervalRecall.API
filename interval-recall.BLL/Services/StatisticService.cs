﻿using interval_recall.BLL.Interfaces;
using interval_recall.DAL.EF;
using interval_recall.DAL.Entities;
using interval_recall.DAL.Models;
using interval_recall.Models.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interval_recall.BLL.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IntervalRecallContext _dataContext;
        public StatisticService(IntervalRecallContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task RemoveStatisticAsync(Guid questionGroupId)
        {
            IQueryable<Question> questions = _dataContext.Questions.Where(q => q.QuestionGroupId == questionGroupId);
            if (!questions.Any())
                throw new Exception("There is no such group of questions");
            foreach(var question in questions)
            {
                question.Interval = TimeSpan.FromMinutes(1);
                question.EasyFactor = 2.5;
                question.Repetitions = 0;
                question.RepetitionDate = DateTime.Now;
                question.State = Enum.GetName(typeof(States), States.New)!;
                question.Step = TimeSpan.FromMinutes(1);
            }
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<QuestionGroupStatistic>> GetStatisticAsync(Guid? questionGroupId)
        {
            return _dataContext.QuestionGroups.Where(qGroup => (questionGroupId == null ? true : qGroup.Id == questionGroupId))
                .Include(qGroup => qGroup.Questions.OrderByDescending(x => x.RepetitionDate)).Adapt<List<QuestionGroupStatistic>>();
        }
    }
}