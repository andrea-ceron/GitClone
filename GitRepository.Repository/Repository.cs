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


		public async Task<Branch> CreateBranch(int projectId, string name, CancellationToken ct = default)
		{
			//Verifico che transaction sia nullo
			//bool TransactionBool = transaction == null;
			//if (TransactionBool)
			//{
			//	// se è nullo creo una nuova transaction
			//	transaction = new TransactionUnit(dbContext);
			//	await transaction.BeginTransactionAsync();
			//}
			//try
			//{
				// Creazione della Branch
				Branch NewBranch = new()
				{
					Name = name,
					ProjectId = projectId

				};
				await dbContext.Branches.AddAsync(NewBranch, ct);
				await dbContext.SaveChangesAsync(ct);
				//await CreatePush(NewBranch.Id, "","", transaction, ct);

				//if (TransactionBool)
				//{
				//	// se ho creato la transazione allora la chiudo 
				//	await transaction.CommitAsync();
				//}

				return NewBranch;
			//}
			//catch
			//{
			//	if (TransactionBool)
			//	{
			//		// se ho creato la transazione all interno di Branch se si verifica un errore eseguo la rollback
			//		await transaction.RollbackAsync();
			//	}
			//	throw;
			//}
			//return null;
		}

		public async Task<Project> CreateProject(int owner, string name, CancellationToken ct = default)
		{
			// Creo una transazione per gestire diverse operazioni verso il DB attraverso la classe TransactionUnit
			//using TransactionUnit transaction = new (dbContext);
			// Faccio aprtire la transazione
			//await transaction.BeginTransactionAsync();
			//try
			//{
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
				//await CreateBranch(NewProject.Id, "", transaction, ct);
				//await transaction.CommitAsync();
				return NewProject;

			//}
			//catch
			//{
			//	await transaction.RollbackAsync();
			//	throw;
			//}
			////Creacione branch main
			//return null;
		}

		public async Task<Push> CreatePush( string title = "Nuova Branch", string message = "Creazione di una nuova Branch", CancellationToken ct = default)
		{
			// Creazione push di inizializzazione senza file;
			Push NewPush = new()
			{
				Title = title,
				Message = message,
				Upload = new DateTime()
			};
			await dbContext.Pushes.AddAsync(NewPush, ct);
			return NewPush;
			//Creazione di una push con file;		
			//
		}

		public async Task<RepoFile> CreateRepoFile(string name, string path, CancellationToken ct = default)
		{
			RepoFile NewFile = new()
			{
				Name = name,
				FileSystemPath = path
			};
			await dbContext.Files.AddAsync(NewFile, ct);
			return NewFile;
		}

		public async Task<Snapshot> CreateSnapshot(string name, int fileId, string path, CancellationToken ct = default)
		{
			Snapshot NewSnapshot = new()
			{
				Name = name,
				FileId = fileId,
				FileSystemPath = path
			};
			await dbContext.Snapshots.AddAsync(NewSnapshot, ct);
			return NewSnapshot;
		}

		public async Task DeleteBranch(int branchId, CancellationToken ct = default)
		{
			Branch? Branch = await GetBranchById(branchId, ct);

			if(Branch == null)
			{
				return;
			}

			dbContext.Branches.Remove(Branch);
		}

		public async Task DeleteProject(int id, CancellationToken ct = default)
		{
			Project? Project = await GetProjectById(id, ct);

			if (Project == null)
			{
				return;
			}

			dbContext.Projects.Remove(Project);
		}

		public async Task DeletePush(int pushId, CancellationToken ct = default)
		{
			Push? Push = await GetPushById(pushId, ct);

			if (Push == null)
			{
				return;
			}

			dbContext.Pushes.Remove(Push);
		}

		public async  Task DeleteRepoFile(int id, CancellationToken ct = default)
		{
			RepoFile? File = await GetRepoFileById(id, ct);

			if (File == null)
			{
				return;
			}

			dbContext.Files.Remove(File);
		}

		public async Task DeleteSnapshot(int id, CancellationToken ct = default)
		{
			Snapshot? Snapshot = await GetSnapshotById(id, ct);

			if (Snapshot == null)
			{
				return;
			}

			dbContext.Snapshots.Remove(Snapshot);
		}

		public async Task<ICollection<Branch>> GetAllBranchByProjectId(int id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<Project>>? GetAllProjectByUserId(int id, CancellationToken ct = default)
		{
			return await dbContext.Projects
				.Where(p => p.UserId == id)
				.ToListAsync();
		}

		public async  Task<ICollection<Push>> GetAllPushByBranchId(int id, CancellationToken ct = default)
		{
			return await dbContext.BranchAssociations
							.Where(ba => ba.BranchId == id)
							.Select(ba => ba.Push)
							.Distinct()
							.ToListAsync(ct);

		}

		public async Task<ICollection<Snapshot>> GetAllSnapshotFromFileId(int fileId, CancellationToken ct = default)
		{
			return await dbContext.Snapshots.
							Where(s => s.FileId == fileId)
							.ToListAsync(ct);
							
		}

		public async  Task<Branch>? GetBranchById(int id, CancellationToken ct = default)
		{
			return await dbContext.Branches
				.Where(b => b.Id == id)
				.SingleOrDefaultAsync(ct);
		}

		public Task<Branch> GetLasPushOfABranch(int branchId, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public async  Task<Push?> GetPreviousPush(int id, CancellationToken ct = default)
		{
			return await dbContext.Pushes
				.Include(p=>p.PreviousPush)
				.Where(p => p.Id == id)
				.Select(p => p.PreviousPush)
				.SingleOrDefaultAsync(ct);
		}

		public async Task<Snapshot?> GetPreviousSnapshot(int id, CancellationToken ct = default)
		{
			return await dbContext.Snapshots
				.Include(p => p.PreviousSnapshot)
				.Where(p => p.Id == id)
				.Select(p => p.PreviousSnapshot)
				.SingleOrDefaultAsync(ct);
		}

		public async Task<Project> GetProjectById(int id, CancellationToken ct = default)
		{
			return await dbContext.Projects
				.Where(p => p.Id == id)
				.SingleOrDefaultAsync(ct);
		}

		public async Task<Push>? GetPushById(int id, CancellationToken ct = default)
		{
			return await dbContext.Pushes
				.Where(p => p.Id == id)
				.SingleOrDefaultAsync(ct);
		}

		public async Task<ICollection<BranchAssociation?>?> GetRepoFileAndSnapshotFromPushId(int id, CancellationToken ct = default)
		{
			return await dbContext.BranchAssociations
								.Where(ba => ba.PushId == id)
								.ToListAsync(ct);
		}

		public async Task<RepoFile>? GetRepoFileById(int id, CancellationToken ct = default)
		{
			return await dbContext.Files
				.Where(f => f.Id == id)
				.SingleOrDefaultAsync(ct);
		}

		public async Task<Snapshot>? GetSnapshotById(int id, CancellationToken ct = default)
		{
			return await dbContext.Snapshots
				.Where(s => s.Id == id)
				.SingleOrDefaultAsync(ct);

		}

		public async Task<User>? GetUser(int id, CancellationToken ct = default)
		{
			return await dbContext.Users
				.Where(u => u.Id == id)
				.SingleOrDefaultAsync(ct);
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
