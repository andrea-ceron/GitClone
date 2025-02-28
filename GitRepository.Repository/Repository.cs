using GitRepository.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace GitRepository.Repository
{
	public class Repository(GitRepoDbContext db) :IRepository
	{
		public Task<Project> CreateProject(CancellationToken ct, int owner, string name)
		{
			throw new NotImplementedException();
		}
	}
}
