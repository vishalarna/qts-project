namespace QTD2.Infrastructure.Model.ILA
{
    public class ILADetailsVM
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int? DeliveryMethodId { get; set; } = 0;
        public string Number { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string ProviderName { get; set; }
        public bool IsSelfPaced { get; set; }
        public bool UseForEMP { get; set; }
        public bool IsPublished { get; set; }
        public bool? IsProviderNERC { get; set; }
        public string IsMeta { get; set; }
        public bool CBTRequiredForCourse { get; set; }
        public double? CreditHours { get; set; }
        public string DeliveryMethodName { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
        public int? ILATraineeEvaluationCount { get; set; }
        public bool IsPubliclyAvailable { get; set; }

        public ILADetailsVM() { }

        public ILADetailsVM(int id,string name,string number,string nickname,string image,string description,int providerId,int? deliveryMethodId,bool isSelfPaced,bool useForEMP,bool isPublished,bool cbtRequiredForCourse,string deliveryMethodName, string providerName,bool? isProviderNERC, int? iLATraineeEvaluationCount,bool active, bool isPubliclyAvailable)
        {
            Id = id;
            Name = name;
            Number = number;
            NickName = nickname;
            Image = image;
            Description = description;
            ProviderId = providerId;
            DeliveryMethodId = deliveryMethodId;
            IsSelfPaced = isSelfPaced;
            UseForEMP = useForEMP;
            IsPublished = isPublished;
            CBTRequiredForCourse = cbtRequiredForCourse;
            DeliveryMethodName = deliveryMethodName;
            ProviderName = providerName;
            IsProviderNERC = isProviderNERC;
            ILATraineeEvaluationCount = iLATraineeEvaluationCount;
            Active = active;
            IsPubliclyAvailable = isPubliclyAvailable;
        }
        public ILADetailsVM(int id,string name,string number,bool active) 
        {
            Id = id;
            Name = name;
            Number = number;
            Active = active;
        }
    }
}
