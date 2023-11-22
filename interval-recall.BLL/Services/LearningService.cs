using interval_recall.BLL.DTOs;
using interval_recall.BLL.Interfaces;
using interval_recall.DAL.Entities;

namespace interval_recall.BLL.Services
{
    public class LearningService : ILearningService
    {
        public LearningService() { }


        public static QuestionDTO SpacedRepetitionAlgorithm(QuestionDTO question)
        {

            var delay = (DateTime.Now - question.RepetitionDate).Days;
            if (question.Qualities[2] == false)// Incorrect(0)
            {
                question.EasyFactor = Math.Round(Math.Max(1.3, question.EasyFactor - 0.2), 2, MidpointRounding.AwayFromZero);
                question.Interval = (int)Math.Round(question.Interval * question.NewInterval, 0, MidpointRounding.AwayFromZero);
            }
            else if (question.Qualities[2] == true)// Correct(1)
            {
                question.EasyFactor = Math.Round(question.EasyFactor, 2, MidpointRounding.AwayFromZero);
                question.Interval = (int)Math.Max(question.Interval + 1, Math.Round((question.Interval + delay / 4) * question.EasyFactor * question.IntervalModifier, 0, MidpointRounding.AwayFromZero));
            }
            else if (question.Qualities[0] == true && question.Qualities[1] == true && question.Qualities[2] == true)// Correct 3 times 
            {
                question.EasyFactor = Math.Round(Math.Min(5, question.EasyFactor + 0.15), 2, MidpointRounding.AwayFromZero);
                question.Interval = (int)Math.Max(question.Interval + 1, (question.Interval + delay) * question.EasyFactor * question.IntervalModifier * question.EasyBonus);
            }

            question.Repetitions++;
            //question.RepetitionDate = question.RepetitionDate.AddDays(question.Interval);
            question.RepetitionDate = DateTime.Now.AddDays(question.Interval);

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
