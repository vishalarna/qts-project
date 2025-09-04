using QTD2.Application.Interfaces.Factories;
using QTD2.Infrastructure.Model.TreeDataVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Factories
{
    public class TPReviewSheetTreeItemViewModelBuilder : ITreeItemViewModelBuilder
    {
        private readonly Domain.Interfaces.Service.Core.ITrainingProgramTypeService _trainingProgramTypeService;

        public TPReviewSheetTreeItemViewModelBuilder(Domain.Interfaces.Service.Core.ITrainingProgramTypeService trainingProgramTypeService)
        {
            _trainingProgramTypeService = trainingProgramTypeService;
        }

        public async Task<Infrastructure.Model.TreeDataVMs.TreeItemViewModel> BuildModel()
        {
            var result = new Infrastructure.Model.TreeDataVMs.TreeItemViewModel();
            var dataset = await _trainingProgramTypeService.GetTPTypeWithProgramsAndReviews();
            result =
                new TreeItemViewModel()
                {
                    Label = "Training Program Type",
                    Searchable = false,
                    TreeItemOptions = dataset.Where((tp) => tp.Active).Select(tp => new TreeItemOptionViewModel()
                    {
                        Id = tp.Id,
                        Display = tp.TrainingProgramTypeTitle,
                        SubTreeItem =
                        new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
                        {
                            Label = "Version",
                            TreeItemOptions = tp.TrainingPrograms.Where((tp) => tp.Active).OrderByDescending(tp => tp.StartDate).Select((trainingProgram) =>
                             new TreeItemOptionViewModel
                             {
                                 Id = trainingProgram.Id,
                                 Display = trainingProgram.Version,
                                 SubTreeItem =
                                 new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
                                 {
                                     Label = "Training Program Review",
                                     TreeItemOptions = trainingProgram.TrainingProgramReviews.Where((tpReviews) => tpReviews.Active).Select((trainingProgramReview) =>
                                     new TreeItemOptionViewModel()
                                     {
                                         Id = trainingProgramReview.Id,
                                         Display = trainingProgramReview.Title
                                     }).ToList()
                                 }
                             }).ToList()
                        }
                    }).ToList()
                };
                        

            return result;
        }

    }
}
