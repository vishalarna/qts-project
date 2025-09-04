using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.DeliveryMethod;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IDeliveryMethodService
    {
        public Task<List<DeliveryMethod>> GetAsync();

        public Task<DeliveryMethod> GetAsync(int id);

        public Task<List<DeliveryMethod>> GetNercAsync(bool isNerc);

        public Task<DeliveryMethod> CreateAsync(DeliveryMethodCreateOptions options);

        public Task<DeliveryMethod> UpdateAsync(int id, DeliveryMethodUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
