using System.Threading.Tasks;

namespace QTD2.Infrastructure.Jobs.Interfaces
{
    public interface IJob
    {
        bool RunAtStartup { get; }
        Task ExecuteAsync();
    }
}
