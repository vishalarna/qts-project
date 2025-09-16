using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.QtdUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IQTDService
    {
        public  Task<List<QtdUserVM>> GetAllActiveAsync();
        public  Task<List<QtdUserVM>> GetAllActiveWithEmployeeData();
        public Task<QtdUserVM> CreateAsync(QtdUserVM qtdUserOption, bool isReturnConflictExp = false);
        public Task<QtdUserVM> UpdateAsync(int id, QtdUserVM qtdUserOption);
        public Task<QtdUserVM> ActivateAsync(int id);
        public Task<QtdUserVM> DeactivateAsync(int id);
        Task<QTDUser> GetQTDUserByUsernameAsync(string username);
    }
}
