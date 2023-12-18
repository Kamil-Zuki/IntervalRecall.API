using interval_recall.BLL.Interfaces;
using interval_recall.DAL.EF;
using interval_recall.DAL.Entities;
using interval_recall.DAL.Models;
using interval_recall.Models.DTOs;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace interval_recall.BLL.Services
{
    public class LearningService : ILearningService
    {

        private readonly IQuestionService _questionService;
        private readonly IntervalRecallContext _dbContext;
        private readonly IMapper _mapper;
        public LearningService(IQuestionService questionService, IntervalRecallContext dbContext, IMapper mapper)
        {
            _questionService = questionService;
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<(int,int)> RecallAsync(List<InUserResponceDTO> userResponces)
        {
            try
            {
                int correct = 0;
                int incorrect = 0;
                foreach (var userResponce in userResponces)
                {
                    Question? question = _dbContext.Questions
                        .Include(q => q.QuestionGroup)
                        .Include(q => q.Answers)
                        .Include(q => q.DecisionQualities)
                        .FirstOrDefault(x => x.Id == userResponce.QuestionId);

                    var answers = question.Answers;
                    var correctAnswersAmount = question.Answers.Where(x => x.IsCorrect == true).Count();
                    var userAnswers = answers.Where(answer => userResponce.AnswerIds.Contains(answer.Id)).ToList();
                    var lastThreeQualies = question.DecisionQualities.TakeLast(2).Select(x => x.Value).ToList();

                    bool decisionQuality = CorrectnessVerification(userAnswers, correctAnswersAmount);
                    if (decisionQuality == true)
                        correct++;
                    else
                        incorrect++;
                    question.DecisionQualities.Add(new DecisionQuality()
                    {
                        Value = decisionQuality
                    });
                    lastThreeQualies.Add(decisionQuality);

                    QuestionDTO questionDTO = SpacedRepetitionAlgorithm(new QuestionDTO()
                    {
                        Qualities = lastThreeQualies,
                        Interval = question.Interval,
                        IntervalModifier = question.QuestionGroup.IntervalModifier,
                        NewInterval = question.QuestionGroup.NewInterval,
                        RepetitionDate = question.RepetitionDate,
                        State = question.State,
                        Step = question.Step,
                        EasyFactor = question.EasyFactor,
                        EasyBonus = question.QuestionGroup.EasyBonus,
                        Repetitions = question.Repetitions
                    });
                    questionDTO.Adapt(question);
                    _dbContext.Questions.Update(question);

                    await _dbContext.SaveChangesAsync();

                }
                return (correct, incorrect);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public static QuestionDTO SpacedRepetitionAlgorithm(QuestionDTO question)
        {
            //var delay = (DateTime.Now - question.RepetitionDate).Days;// Actual
            var delay = (question.RepetitionDate - DateTime.Now).Days;// Test



            if (question.Qualities[^1] == false)// Incorrect(0)
            {
                question.EasyFactor = Math.Round(Math.Max(1.3, question.EasyFactor - 0.2), 2, MidpointRounding.AwayFromZero);
                if (question.State == Enum.GetName(typeof(States), States.New) || question.State == Enum.GetName(typeof(States), States.Learning))
                {
                    question.Interval = question.Step = question.Step < TimeSpan.FromDays(1) ?
                        TimeSpan.FromMinutes(1) : TimeSpan.FromMinutes(10);

                    question.State = Enum.GetName(typeof(States), States.Learning)!;
                }
                else
                {
                    question.Interval = question.Interval * question.NewInterval;
                    question.State = Enum.GetName(typeof(States), States.Learning)!;
                }

            }
            else if (question.Qualities.Count() > 2 && question.State == Enum.GetName(typeof(States), States.Graduated) && question.Qualities[^1] == true && question.Qualities[^2] == true && question.Qualities[^3] == true)// Correct 3 times 
            {
                question.EasyFactor = Math.Round(Math.Min(5, question.EasyFactor + 0.15), 2, MidpointRounding.AwayFromZero);
                //question.Interval = Math.Min(36500, (int)Math.Max(question.Interval + 1, (question.Interval + delay) * question.EasyFactor * question.IntervalModifier * question.EasyBonus));

                question.Interval = TimeSpan.FromDays(Math.Min(36500, (question.Interval.Days + delay) * question.EasyFactor * question.IntervalModifier * question.EasyBonus));

            }
            else if (question.Qualities[^1] == true)// Correct(1)
            {
                question.EasyFactor = Math.Round(question.EasyFactor, 2, MidpointRounding.AwayFromZero);
                question.State = question.State != Enum.GetName(typeof(States), States.Graduated) ?
                    Enum.GetName(typeof(States), States.Learning)! :
                    Enum.GetName(typeof(States), States.Graduated)!;

                if (question.Step == TimeSpan.FromHours(23))
                {
                    question.Interval = TimeSpan.FromDays(Math.Min(36500, (question.Interval.Days + (int)Math.Round(delay / 4.0, 0, MidpointRounding.AwayFromZero)) * question.EasyFactor * question.IntervalModifier));
                    //question.Step = TimeSpan.FromDays(1);
                    question.State = Enum.GetName(typeof(States), States.Graduated)!;
                }

                else if (question.Step == TimeSpan.FromMinutes(10))
                {
                    question.Interval = TimeSpan.FromDays(1);
                    question.Step = TimeSpan.FromHours(23);
                }

                else if (question.Step == TimeSpan.FromMinutes(1))
                    question.Interval = question.Step = TimeSpan.FromMinutes(10);

            }

            question.Repetitions++;
            question.RepetitionDate = DateTime.Now + question.Interval;

            return question;
        }


        public static bool CorrectnessVerification(List<Answer> userAnswers, int correctAnswers)
        {
            if (userAnswers.Count != correctAnswers)
                return false;

            bool allAnswersCorrect = userAnswers.All(answer => answer.IsCorrect);

            return allAnswersCorrect;
        }
    }
}
