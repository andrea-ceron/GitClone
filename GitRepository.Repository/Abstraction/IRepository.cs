using GitRepository.Repository.Model;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepository.Repository.Abstraction {

	public interface IRepository
	{
		Task<int>  SaveChanges(CancellationToken ct = default);
		//Users
		// Azioni relative all host vengono fatte da una altro microservizio, per questo qui viene fatta solo la get
		Task<User> GetUser(int id, CancellationToken ct = default);

		//Projects
		Task<Project> CreateProject(int owner, string name, CancellationToken ct = default);
		Task DeleteProject(int id, CancellationToken ct = default);
		Task<Project> UpdateProject(Project project, CancellationToken ct = default);
		Task<Project> GetProjectById(int id, CancellationToken ct = default);
		Task<ICollection<Project>> GetAllProjectByUserId(int id, CancellationToken ct = default);

		// Branches
		Task<Branch> CreateBranch(int projectId, string name, CancellationToken ct = default);
		Task DeleteBranch(int branchId, CancellationToken ct = default);
		Task<Branch> UpdateBranch(Branch branch, CancellationToken ct = default);
		Task<Branch> GetBranchById(int id, CancellationToken ct = default);
		Task<ICollection<Branch>> GetAllBranchByProjectId(int id, CancellationToken ct = default);

		//Push
		Task<Push> CreatePush( string title, string message, CancellationToken ct = default);
		Task DeletePush(int pushId, CancellationToken ct = default);
		Task<Push> UpdatePush(Push push, CancellationToken ct = default);
		Task<Push> GetPushById(int id, CancellationToken ct = default);
		Task<ICollection<Push>> GetAllPushByBranchId(int id, CancellationToken ct = default);
		Task<Branch> GetLasPushOfABranch(int branchId, CancellationToken ct = default);
		Task<Push> GetPreviousPush(int id, CancellationToken ct = default);

		//File
		Task<RepoFile> CreateRepoFile(string name, string path, CancellationToken ct = default);
		Task DeleteRepoFile(int id, CancellationToken ct = default);
		Task<RepoFile> UpdateRepoFile(RepoFile file, CancellationToken ct = default);
		Task<RepoFile> GetRepoFileById(int id, CancellationToken ct = default);

		//Snapshot
		Task<Snapshot> CreateSnapshot(string name, int fileId, string path, CancellationToken ct = default);
		Task DeleteSnapshot(int id, CancellationToken ct = default);
		Task<Snapshot> UpdateSnapshot(Snapshot snapshot, CancellationToken ct = default);
		Task<Snapshot> GetSnapshotById(int id, CancellationToken ct = default);
		Task<ICollection<Snapshot>> GetAllSnapshotFromFileId(int fileId, CancellationToken ct = default);
		Task<Snapshot> GetPreviousSnapshot(int id, CancellationToken ct = default);

		//BranchAssociation
		Task<ICollection<BranchAssociation>> GetRepoFileAndSnapshotFromPushId(int id, CancellationToken ct = default);





	}

}