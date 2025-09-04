using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Meta_ILAMembers_Link
{
    public class MetaILA_ILAMemberVM
    {
        public int? ILAId { get; set; }
        public int? ProviderId { get; set; }
        public int? MetaILAConfigPublishOptionId { get; set; }
        public bool? ILAActive { get; set; }
        public string? DeliveryMethodName { get; set; }
        public string? ILAImage { get; set; }
        public string? ILANickName { get; set; }
        public string? ILAName { get; set; }
        public string? ILANumber { get; set; }
        public string? ProviderName { get; set; }
        public string? MetaILAConfigPublishOptionDescription { get; set; }
        public int? SequenceNumber { get; set; }
        public DateTime? StartDate { get; set; }

        public MetaILA_ILAMemberVM(){}

        public MetaILA_ILAMemberVM(int? ilaId,int? providerId,int? metaILAConfigPublishOptionId,bool? ilaActive,string? deliveryMethodName,string? ilaImage,string ilaNickName,string? ilaName,string? ilaNumber,string? providerName,string? metaILAConfigPublishOptionDescription,int? sequenceNumber,DateTime? startDate)
        {
            ILAId = ilaId;
            ProviderId = providerId;
            MetaILAConfigPublishOptionId = metaILAConfigPublishOptionId;
            ILAActive = ilaActive;
            DeliveryMethodName = deliveryMethodName ?? string.Empty;
            ILAImage = ilaImage ?? string.Empty;
            ILANickName = ilaNickName ?? string.Empty;
            ILAName = ilaName ?? string.Empty;
            ILANumber = ilaNumber ?? string.Empty;
            ProviderName = providerName ?? string.Empty;
            MetaILAConfigPublishOptionDescription = metaILAConfigPublishOptionDescription ?? string.Empty;
            SequenceNumber = sequenceNumber;
            StartDate = startDate;
        }
    }

}
