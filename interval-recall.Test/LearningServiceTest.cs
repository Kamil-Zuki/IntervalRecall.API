using interval_recall.BLL.DTOs;
using interval_recall.BLL.Models;
using interval_recall.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace interval_recall.Test
{
    public class LearningServiceFixture : IDisposable
    {
        private readonly IServiceScope _scope;
        public IConfiguration _configuration;

        public LearningServiceFixture()
        {
            //var serviceCollection = new ServiceCollection();
            //var configBuilder = new ConfigurationBuilder();
            //   .SetBasePath(Directory.GetCurrentDirectory())
            //   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //_configuration = configBuilder.Build();
            //var serviceProvider = serviceCollection.BuildServiceProvider();

            //_scope = serviceProvider.CreateScope();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public T GetService<T>()
        {
            return _scope.ServiceProvider.GetRequiredService<T>();
        }
    }


    public class LearningServiceTest
    {
        //private readonly ILearningService _learningService;
        public LearningServiceTest(/*ILearningService learningService*/)
        {
            //_learningService = learningService;
        }

        [Fact]
        public void SpacedRepetitionAlgorithm_ReturnValue()
        {

            Question question = new ()
            {
                Text = "В какой из этих столиц бывших союзных республик раньше других появилось метро?",
                Answers = new List<Answer>()
                {
                    new (){ Id = Guid.NewGuid(), Value = "Тбилиси" , IsCorrect = true},
                    new (){ Id = Guid.NewGuid(), Value = "Ереван", IsCorrect = false},
                    new (){ Id = Guid.NewGuid(), Value = "Баку", IsCorrect = false},
                    new (){ Id = Guid.NewGuid(), Value = "Минск", IsCorrect = false},
                }
            };
            bool quality = LearningService.CorrectnessVerification(new List<Answer>() { new Answer() { IsCorrect = true } }, 1);

            QuestionDTO testQuestionDTO = new QuestionDTO()
            {
                Qualities = new bool[] { true, false, true },
                EasyFactor = 2.5,
                Interval = 1,
                IntervalModifier = 1,
                NewInterval = 0.2,
                RepetitionDate = DateTime.Now,
                EasyBonus = 1.3,
                Repetitions = 0
            };
            int i = 1;
            while (true)
            {
                var questionDTO = LearningService.SpacedRepetitionAlgorithm(testQuestionDTO);
                if (i == 5)
                    testQuestionDTO.Qualities[2] = false;
                i ++;
            }
        }
    }
}