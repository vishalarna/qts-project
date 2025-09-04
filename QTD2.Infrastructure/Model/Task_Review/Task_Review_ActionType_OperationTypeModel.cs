using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_List_Review
{
    public class Task_Review_ActionType_OperationTypeModel
    {
        public string ActionItemType { get; set; }
        public string ActionItemOperationName { get; set; }

        public Task_Review_ActionType_OperationTypeModel(string actionItemType, string actionItemOperationName)
        {
            ActionItemType = actionItemType;
            ActionItemOperationName = actionItemOperationName;
        }

    }
}
