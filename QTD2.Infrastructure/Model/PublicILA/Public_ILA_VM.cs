using QTD2.Infrastructure.Model.ClassSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.PublicILA
{
    public class Public_ILA_VM
    {
        public int Id { get; set; }
        public int? DeliveryMethodId { get; set; } = 0;
        public string ILANumber { get; set; }
        public string ILAName { get; set; }
        public string ILANickName { get; set; }
        public double? CreditHours { get; set; }
        public string DeliveryMethodName { get; set; }
        public int ClassesCount {  get; set; }
        public double TotalTrainingHours { get; set; }
        public string Description {  get; set; }

        public bool IsPubliclyAvailable { get; set; }
        public Public_ILA_VM()
        {
            
        }

        public Public_ILA_VM(int id, int deliveryMethodId, string ilaNumber, string ilaName, double creditHours, string deliveryMethodName, bool isPubliclyAvailable, double totalTrainingHours, string description)
        {
            Id = id;
            DeliveryMethodId = deliveryMethodId;
            ILANumber = ilaNumber;
            ILAName = ilaName;
            CreditHours = creditHours;
            DeliveryMethodName = deliveryMethodName;
            IsPubliclyAvailable = isPubliclyAvailable;
            TotalTrainingHours = totalTrainingHours;
            Description = description;
        }
    }
}
