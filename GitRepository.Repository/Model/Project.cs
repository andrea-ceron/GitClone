

namespace GitRepository.Repository.Model
{
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Branch> BranchList { get; set;}
		public bool Scope { get; set; }
		public List<AccessToProjects>? AccessFromUsers { get; set; }

	}


}
