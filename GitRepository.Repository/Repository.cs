using GitRepository.Repository.Abstraction;
using GitRepository.Repository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Transactions;
using System.Xml.Linq;

namespace GitRepository.Repository
{
	public class Repository(GitRepoDbContext dbContext) : IRepository
	{


		public async Task<Branch> CreateBranch(int projectId, string name, ITransactionUnit? transaction =null, CancellationToken ct = default)
		{
			//Verifico che transaction sia nullo
			bool TransactionBool = transaction == null;
			if (TransactionBool)
			{
				// se è nullo creo una nuova transaction
				transaction = new TransactionUnit(dbContext);
				await transaction.BeginTransactionAsync();
			}
			try
			{
				// Creazione della Branch
				Branch NewBranch = new()
				{
					Name = name,
					ProjectId = projectId

				};
				await dbContext.Branches.AddAsync(NewBranch, ct);
				await dbContext.SaveChangesAsync(ct);
				return NewBranch;

				if (TransactionBool)
				{
					// se ho creato la transazione allora la chiudo 
					await transaction.CommitAsync();
				}

			}
			catch
			{
				if (TransactionBool)
				{
					// se ho creato la transazione all interno di Branch se si verifica un errore eseguo la rollback
					await transaction.RollbackAsync();
				}
				throw;
			}
			return null;
		}

		public async Task<Project> CreateProject(int owner, string name, CancellationToken ct = default)
		{
			// Creo una transazione per gestire diverse operazioni verso il DB attraverso la classe TransactionUnit
			using TransactionUnit transaction = new (dbContext);
			// Faccio aprtire la transazione
			await transaction.BeginTransactionAsync();
			try
			{
				//Creo il modello
				Project NewProject = new()
				{
					UserId = owner,
					Name = name

				};
				// eseguo salvo la query
				await dbContext.Projects.AddAsync(NewProject, ct);
				await dbContext.SaveChangesAsync(ct);

				// creo il branch Main
				await CreateBranch(NewProject.Id, "", transaction, ct);
				await transaction.CommitAsync();
				return NewProject;

			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}
			//Creacione branch main
			return null;
		}



		public async Task<Push> CreatePush(int branchId, string title, string message, ITransactionUnit? transaction = null, CancellationToken ct = default)
		{
			// Creazione push di inizializzazione senza file;
			Push NewPush = new()
			{
				Title = title,
				Message = message,
				BranchId = branchId

			};
			await dbContext.Push.AddAsync(NewPush, ct);
			return NewPush;
			//Creazione di una push con file;		
			//
		}

		public Task<RepoFile> CreateRepoFile(string name, string path, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Snapshot> CreateSnapshot(string name, string fileId, string path, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task DeleteBranch(int branchId, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task DeleteProject(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task DeletePush(int pushId, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task DeleteRepoFile(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task DeleteSnapshot(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<Branch>> GetAllBranchByProjectId(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<Project>> GetAllProjectByUserId(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<Push>> GetAllPushByBranchId(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<Snapshot>> GetAllSnapshotFromFileId(int fileId, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Branch> GetBranchById(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Branch> GetLasPushOfABranch(int branchId, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Push> GetPreviousPush(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Snapshot> GetPreviousSnapshot(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Project> GetProjectById(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Push> GetPushById(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<BranchAssociation>> GetRepoFileAndSnapshotFromPushId(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<RepoFile> GetRepoFileById(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Snapshot> GetSnapshotById(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetUser(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public async Task<int> SaveChanges(CancellationToken ct = default)
		{
			return await dbContext.SaveChangesAsync(ct);
		}

		public Task<Branch> UpdateBranch(Branch branch, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Project> UpdateProject(Project project, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Push> UpdatePush(Push push, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<RepoFile> UpdateRepoFile(RepoFile file, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<Snapshot> UpdateSnapshot(Snapshot snapshot, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}
	}
}
