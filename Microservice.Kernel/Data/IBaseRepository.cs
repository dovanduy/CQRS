using System.Collections.Generic;

namespace Microservice.Kernel.Data
{
	public interface IBaseRepository<T>
	{
		T GetByID(int id);
		List<T> GetMultiple(List<int> ids);
		bool Exists(int id);
		void Save(T item);
	}
}
