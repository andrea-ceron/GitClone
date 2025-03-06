using GitRepository.Business.Abstraction;
using GitRepository.Repository;
using GitRepository.Repository.Abstraction;
using GitRepository.Repository.Model;


namespace GitRepository.Business
{
	public class Business(IRepository repository) : IBusiness
	{
		public async Task AppendUser(int projectId, List<int> programmersId,int ownerId, CancellationToken ct = default)
		{
			Project? fetchedProject = await repository.GetProjectById(projectId, ct);
			if(fetchedProject == null)
			{
				// gestire l errore
				return;
			}
			await repository.AppendUserToProject(programmersId, projectId, ownerId, ct);
			await repository.SaveChanges(ct);

		}


		public async Task<Project> CreateProjectAsync(string name, User owner, int action, List<int> programmers, bool isPrivate = false, CancellationToken ct = default)
		{
			using var transaction = repository.TransactionFactory();
			await transaction.BeginTransactionAsync();
			try
			{
				Project p = await repository.CreateProject(name, isPrivate, ct);
				await repository.SaveChanges(ct);

				await repository.AppendUserToProject(programmers, p.Id, owner.Id, ct);
				await repository.SaveChanges(ct);

				await ExecutePush(transaction, action, p.Id, "", "", "", ct);
				await repository.SaveChanges(ct);

				await transaction.CommitAsync();
				return p;
			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}
		}

		public async Task ExecutePush(int projectId, Branch? branch, List<RepoFile> FilesToUpdate, List<Snapshot> SnapshotToUpLoad, string title, string description, ITransactionUnit? transaction, int action, string branchName = "master", CancellationToken ct = default)
		{
			bool TransactionIsNUll = transaction == null;
			bool isBranchNull = branch == null;

			try
			{
				if ((!TransactionIsNUll && !isBranchNull))
				{
					throw new Exception("Inconsistenza");
				}

				if (TransactionIsNUll)
				{
					// se è nullo creo una nuova transaction
					transaction = repository.TransactionFactory();
					await transaction.BeginTransactionAsync();
				}

				if (action % 3 == 0)
				{
					// allora dobbiamo creare una branch quindi creo una push senza cercare 
					branch = await repository.CreateBranch(projectId, "",ct);
					await repository.SaveChanges(ct);
				}

				//Creazione della push
				Push? NewPush = null;
				if (branch == null)
					NewPush = await repository.CreatePush(null, title, description, ct);
				else
				{

					NewPush = await repository.CreatePush( title, description, ct);
				}
				await repository.SaveChanges(ct);



				if(action % 5 == 0)
				{
					// upload o modifica dei file
					foreach(var file in FilesToUpdate)
					{
						
					}
					
				}
				if (TransactionIsNUll)
				{
					// se ho creato la transazione allora la chiudo 
					await transaction.CommitAsync();
				}
				return;
			}
			catch
			{
				if (TransactionIsNUll)
				{
					// se ho creato la transazione all interno di Branch se si verifica un errore eseguo la rollback
					await transaction.RollbackAsync();
				}
				throw;
			}

		}

		public Task<RepoFile?> FindFile(string filename, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<User?> FindUser(string username, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<RepoFile> ShowFile(Project project, User owner, User user, Branch branch, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Project> ShowProject(User owner, User user, Branch branch, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Project> ShowUserProjects(User user, User owner, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}
	}
}
