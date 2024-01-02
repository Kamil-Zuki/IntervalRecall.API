using interval_recall.BLL.Interfaces;
using interval_recall.BLL.Services;
using interval_recall.DAL.EF;
using interval_recall.Models.DTOs;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace interval_recall.Test
{
    public class LearningServiceFixture : IDisposable
    {
        private readonly IServiceScope _scope;
        private readonly IConfiguration _configuration;

        public LearningServiceFixture()
        {
            var serviceCollection = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = configBuilder.Build();
            serviceCollection.AddSingleton(_configuration);
            serviceCollection.AddMapster();
            serviceCollection.AddScoped<IQuestionService, QuestionService>();

            serviceCollection.AddDbContext<IntervalRecallContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")!);
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _scope = serviceProvider.CreateScope();
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

    public class LearningServiceTest : IClassFixture<LearningServiceFixture>
    {
        private readonly ILearningService _learningService;
        private readonly IStatisticService _statisticService;
        public LearningServiceTest(LearningServiceFixture learningServiceFixture)
        {
            _learningService = new LearningService(learningServiceFixture.GetService<IQuestionService>(), learningServiceFixture.GetService<IntervalRecallContext>(), learningServiceFixture.GetService<IMapper>());
            _statisticService = new StatisticService(learningServiceFixture.GetService<IntervalRecallContext>());
        }

        [Fact]
        public async Task SpacedRepetitionAlgorithm_ReturnValue()
        {



            List<InUserResponceDTO> userResponses = new List<InUserResponceDTO>()
            {
                new InUserResponceDTO()
                {
                    QuestionId = Guid.Parse("ad3cf19a-88b6-4785-5e59-08dbffdefadb"),
                    AnswerIds = new List<Guid>()
                    {
                        Guid.Parse("88871711-d482-4ecc-e6e7-08dbffdefae1")
                        //Guid.Parse("a568ea94-87c3-4c01-e6fc-08dbffdefae1"),
                        //Guid.Parse("d41d6e73-8d45-414a-e6fd-08dbffdefae1"),
                        //Guid.Parse("5fc24f65-df6c-4d50-e6fe-08dbffdefae1")
                    }
                }
            };

            for (int i = 0; i < 6; i++)
            {
                var responce = await _learningService.RecallAsync(userResponses);
            }
        }
    }
}