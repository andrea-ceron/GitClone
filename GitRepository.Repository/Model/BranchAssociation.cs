
namespace GitRepository.Repository.Model
{
	public class BranchAssociation
	{
		public int Id {  get; set; }
		public Push Push { get; set; }
		public RepoFile? File { get; set; }
		public Snapshot? Snapshot {  get; set; }
		public int PushId { get; set; }
		public int? FileId { get; set;}
		public int? SnapshotId { get; set; }
		public bool IsDeleted { get; set; }
	}
}
