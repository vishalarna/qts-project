using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Rustici.EngineApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class ScormHelpersService : IScormHelpersService
    {
        public CBT_ScormRegistration ProcessRuntimeInteraction(List<RuntimeInteractionSchema> runtimeInteractions, CBT_ScormRegistration registration, CBT_ScormUpload cbtScormUpload)
        {
            foreach (var runtimeInteraction in runtimeInteractions)
            {
                var correctChoices = runtimeInteraction.CorrectResponses.ToList();
                var choices = runtimeInteraction.CorrectResponses.ToList();
                var learnerResponse = runtimeInteraction.LearnerResponse ?? "";
                choices.Add(learnerResponse);

                var question = cbtScormUpload.AddQuestion((CBT_ScormUpload_Question_Type)Enum.Parse(typeof(CBT_ScormUpload_Question_Type), runtimeInteraction.Type.ToString()), runtimeInteraction.Id, runtimeInteraction.Description, choices, correctChoices);

                var choice = question.GetChoice(learnerResponse);
                bool isCorrect = choice != null && choice.CorrectChoice;

                if (choice == null)
                {
                    var userChoice = question.AddChoice(learnerResponse, isCorrect);
                    registration.AddResponse(userChoice);
                }
                else
                {
                    registration.AddResponse(choice);
                }
            }

            return registration;
        }
    }
}
