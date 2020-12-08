using System.Threading.Tasks;

namespace FinancialManager.Infra.CrossCutting.Core.Data
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}
}
