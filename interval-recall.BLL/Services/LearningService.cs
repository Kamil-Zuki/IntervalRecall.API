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
        private const int maximumInterval = 36500;
        private const double easyFactorMinValue = 1.2;
        private const double easyFactorMaxValue = 5;
        private const double easyFactorDroppedValue = 0.2;
        private const double easyFactorIncreasedValue = 0.2;
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
            //calculate delay
            int delay = question.State == "New" ? 0 : (question.RepetitionDate - DateTime.Now).Days;

            if (question.Qualities[^1] == false)
            {
                HandleIncorrectAnswer(question, delay);
            }
            else if (IsCorrectThreeTimes(question))
            {
                HandleCorrectThreeTimes(question, delay);
            }
            else if (question.Qualities[^1] == true)
            {
                HandleCorrectOnce(question, delay);
            }

            UpdateRepetitionsAndDate(question);

            return question;
        }


        private static void HandleIncorrectAnswer(QuestionDTO question, int delay)
        {
            question.EasyFactor = Math.Round(Math.Max(easyFactorMinValue, question.EasyFactor - easyFactorDroppedValue), 2, MidpointRounding.AwayFromZero);
            if (question.State == Enum.GetName(typeof(States), States.New) || question.State == Enum.GetName(typeof(States), States.Learning))
            {
                //Set interval and step to minimum
                question.Interval = question.Step = question.Step < TimeSpan.FromDays(1) ?
                    TimeSpan.FromMinutes(1) : TimeSpan.FromMinutes(10);

                question.State = Enum.GetName(typeof(States), States.Learning)!;
            }
            else
            {
                //Adjust interval
                question.Interval = question.Interval * question.NewInterval;
                question.State = Enum.GetName(typeof(States), States.Learning)!;
            }
        }

        private static bool IsCorrectThreeTimes(QuestionDTO question)
        {
            return question.Qualities.Count() > 2 && question.State == Enum.GetName(typeof(States), States.Graduated) && question.Qualities[^3..^1].All(q => q == true);
        }

        private static void HandleCorrectThreeTimes(QuestionDTO question, int delay)
        {
            question.EasyFactor = Math.Round(Math.Min(easyFactorMaxValue, question.EasyFactor + easyFactorIncreasedValue), 2, MidpointRounding.AwayFromZero);
            question.Interval = TimeSpan.FromDays(Math.Min(36500, (question.Interval.Days + delay) * question.EasyFactor * question.IntervalModifier * question.EasyBonus));
        }

        private static void HandleCorrectOnce(QuestionDTO question, int delay)
        {
            question.State = question.State != Enum.GetName(typeof(States), States.Graduated) ?
                Enum.GetName(typeof(States), States.Learning)! :
                Enum.GetName(typeof(States), States.Graduated)!;

            if (question.Step == TimeSpan.FromHours(23))
            {
                question.Interval = TimeSpan.FromDays(Math.Min(maximumInterval, (question.Interval.Days + (int)Math.Round(delay / 4.0, 0, MidpointRounding.AwayFromZero)) * question.EasyFactor * question.IntervalModifier));
                question.State = Enum.GetName(typeof(States), States.Graduated)!;
            }
            else if (question.Step == TimeSpan.FromMinutes(10))
            {
                question.Interval = TimeSpan.FromDays(1);
                question.Step = TimeSpan.FromHours(23);
            }
            else if (question.Step == TimeSpan.FromMinutes(1))
            {
                question.Interval = question.Step = TimeSpan.FromMinutes(10);
            }
        }

        private static void UpdateRepetitionsAndDate(QuestionDTO question)
        {
            question.Repetitions++;
            question.RepetitionDate = DateTime.Now + question.Interval;
        }

        private static bool CorrectnessVerification(List<Answer> userAnswers, int correctAnswers)
        {
            if (userAnswers.Count != correctAnswers)
                return false;

            bool allAnswersCorrect = userAnswers.All(answer => answer.IsCorrect);
            return allAnswersCorrect;
        }
    }
}
