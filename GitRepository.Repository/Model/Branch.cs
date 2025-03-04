namespace GitRepository.Repository.Model
{
	public class Branch
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ProjectId { get; set; }
		public Project Project { get; set; }
		public ICollection<BranchAssociation>? PushHistory { get; set;}
		
	}


}