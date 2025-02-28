using GitRepository.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepository.Repository.Abstraction {

	public interface IRepository
	{
		//Users
		// Azioni relative all host vengono fatte da una altro microservizio, per questo qui viene fatta solo la get
		Task<User> GetUser(CancellationToken ct = default, int id);

		//Projects
		Task<Project> CreateProject(CancellationToken ct = default, int owner, string name);
		Task DeleteProject(CancellationToken ct = default, int id);
		Task<Project> UpdateProject(CancellationToken ct = default, Project project);
		Task<Project> GetProjectById(CancellationToken ct = default, int id);
		Task<ICollection<Project>> GetAllProjectByUserId(CancellationToken ct = default, int id);

		// Branches
		Task<Project> CreateBranch(CancellationToken ct = default, int projectId, string name);
		Task DeleteBranch(CancellationToken ct = default, int branchId);
		Task<Project> UpdateBranch(CancellationToken ct = default, Branch branch);
		Task<Project> GetBranchById(CancellationToken ct = default, int id);
		Task<ICollection<Project>> GetAllBranchByProjectId(CancellationToken ct = default, int id);

		//Push
		Task<Push> CreatePush(CancellationToken ct = default, int branchId, string title, string message);
		Task DeletePush(CancellationToken ct = default, int pushId);
		 Task<Push> UpdatePush(CancellationToken ct = default, Push push);
		Task<Push> GetPushById(CancellationToken ct = default, int id);
		Task<ICollection<Push>> GetAllPushByBranchId(CancellationToken ct = default, int id);
		Task<Branch> GetLasPushOfABranch(CancellationToken ct = default, int branchId);
		Task<Push> GetPreviousPush(CancellationToken ct = default, int id);

		//File
		 Task<RepoFile> CreateRepoFile(CancellationToken ct = default, string name, string path);
		Task DeleteRepoFile(CancellationToken ct = default, int id);
		Task<RepoFile> UpdateRepoFile(CancellationToken ct = default, RepoFile file);
		Task<RepoFile> GetRepoFileById(CancellationToken ct = default, int id);

		//Snapshot
		Task<Snapshot> CreateSnapshot(CancellationToken ct = default, string name, string fileId, string path);
		Task DeleteSnapshot(CancellationToken ct = default, int id);
		Task<Snapshot> UpdateSnapshot(CancellationToken ct = default, Snapshot snapshot);
		Task<Snapshot> GetSnapshotById(CancellationToken ct = default, int id);
		Task<ICollection<Snapshot>> GetAllSnapshotFromFileId(CancellationToken ct = default, int fileId);
		Task<Snapshot> GetPreviousSnapshot(CancellationToken ct = default, int id);

		//BranchAssociation
		Task<ICollection<BranchAssociation>> GetRepoFileAndSnapshotFromPushId(CancellationToken ct = default, int id);





	}

}