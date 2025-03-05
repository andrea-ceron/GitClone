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
        Task<Project> CreateProjectAsync(string name, User owner, List<User>? programmers, bool isPrivate = false); 
    }
}
