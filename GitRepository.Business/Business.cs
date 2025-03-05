using GitRepository.Business.Abstraction;
using GitRepository.Repository.Model;

namespace GitRepository.Business
{
	public class Business : IBusiness
	{
		public Task<Project> CreateProjectAsync(string name, User owner, List<User>? programmers, bool isPrivate = false)
		{
			throw new NotImplementedException();
		}
	}
}
