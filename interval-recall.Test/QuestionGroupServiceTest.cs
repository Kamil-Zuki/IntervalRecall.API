using interval_recall.BLL.DTOs;
using interval_recall.BLL.Interfaces;
using interval_recall.BLL.Services;
using interval_recall.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace interval_recall.Test
{
    public class QuestionGroupServicFixture : IDisposable
    {
        private readonly IServiceScope _scope;
        public IConfiguration _configuration;
        private readonly IntervaRecallContext _dataContext;
        public IConfigurationBuilder _configurationBuilder;

        public QuestionGroupServicFixture()
        {
            var serviceCollection = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder();

            _configuration = configBuilder.Build();
            serviceCollection.AddSingleton(_configuration);  // Register the configuration as a singleton

            serviceCollection.AddDbContext<IntervaRecallContext>(options =>
            {
                options.UseSqlite(_configuration.GetConnectionString(@"C:\Users\karat\Desktop\Projects\interval-recall\interval-recall.API\wwwroot")!);
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

    public class QuestionGroupServiceTest : IClassFixture<QuestionGroupServicFixture>
    {
        private readonly IQuestionGroupService _questionGroupService;
        public QuestionGroupServiceTest(QuestionGroupServicFixture questionGroupServicFixture)
        {
            _questionGroupService = new QuestionGroupService(questionGroupServicFixture.GetService<IntervaRecallContext>());
        }
        [Fact]
        public void Create_ShouldCreateGroupService()
        {
            InQuestionGroupDTO questionGroupDTO = new InQuestionGroupDTO()
            {
                Title = "100 most common used English words",
                IntervalModifier = 0.8,
                EasyBonus = 1.3,
                NewInterval = 0.2,
            };
            _questionGroupService.CreateAsync(questionGroupDTO);
        }
    }
}
