using GitRepository.Repository.Abstraction;
using GitRepository.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepository.Business.Abstraction
{
    public interface IBusiness
    {
        Task<Project> CreateProjectAsync(string name, User owner, int action, List<int> programmers, bool isPrivate = false,  CancellationToken ct = default);
        Task AppendUser(int projectId, List<int> programmers, int ownerId, CancellationToken ct = default);
        Task<Project> ShowUserProjects(User user, User owner, CancellationToken ct = default);
        Task<Project> ShowProject(User owner, User user, Branch branch, CancellationToken ct = default);
        Task<RepoFile> ShowFile(Project project, User owner, User user, Branch branch, CancellationToken ct = default);
        Task<User?> FindUser(string username, CancellationToken ct = default);
        Task<RepoFile?> FindFile(string filename, CancellationToken ct = default);
        Task ExecutePush(Project project, Branch branch, List<RepoFile> FilesToUpdate, string title, string description, ITransactionUnit transaction, int action, int projectId, string branchName = "master", CancellationToken ct = default);
        //Task<Project> ExecuteClone(Project project, CancellationToken ct = default);
        //Task ExecutePull(Project project, Branch branch, CancellationToken ct = default);

    }
}
