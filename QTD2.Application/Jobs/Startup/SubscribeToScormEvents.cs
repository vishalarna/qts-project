using QTD2.Infrastructure.HttpClients;
using QTD2.Infrastructure.Jobs.Interfaces;
using QTD2.Infrastructure.Rustici.EngineApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using QTD2.Infrastructure.Database.Interfaces;
using Microsoft.Extensions.Logging;

namespace QTD2.Application.Jobs.Startup
{
    public class SubscribeToScormEvents : IJob
    {
        private readonly ScormEngineService _scormEngineService;
        private readonly Application.Settings.DomainSettings _domainSettings; 
        private ILogger<QTD2DatabaseSetup> _logger;

        public SubscribeToScormEvents(
            ScormEngineService scormEngineService, 
            IOptions<Application.Settings.DomainSettings> domainSettingsOptions,
            ILogger<QTD2DatabaseSetup> logger)
        {
            _scormEngineService = scormEngineService;
            _domainSettings = domainSettingsOptions.Value;
            _logger = logger;
        }
        public bool RunAtStartup { get { return true; } }

        public async Task ExecuteAsync()
        {
            try
            {
                SubscriptionDefinitionSchema subscriptionDefinition = new SubscriptionDefinitionSchema()
                {
                    Topic = TopicEnum.RegistrationChanged,
                    Subtopics = new List<SubTopicEnum> { SubTopicEnum.ScoreChanged, SubTopicEnum.CompletionChanged },
                    Enabled = true,
                    Url = new Uri(new Uri(_domainSettings.QTD), "scorm/registration").ToString()
                };
                var subscriptions = await _scormEngineService.GetSubscriptionsAsync("", subscriptionDefinition.Topic.ToString(), subscriptionDefinition.Subtopics[0].ToString());
                var matchedSubscriptions = subscriptions.Subscriptions.Where(r => r.SubscriptionEntrySchema.Select(s => s.Definition.Topic == subscriptionDefinition.Topic && s.Definition.Subtopics.Contains(subscriptionDefinition.Subtopics[0])).Count() > 0);
                if (matchedSubscriptions.Count() == 0)
                {
                    await _scormEngineService.CreateSubscriptionAsync(subscriptionDefinition);
                }

            }
            catch(Exception e)
            {
                _logger.LogError($"Subscribe to Scorm Events Failed {e}", e);
                throw e;
            }
        }
    }
}
