namespace GitRepository.Repository.Model
{
	public class Snapshot
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int FileId { get; set; }
		public RepoFile File { get; set; }
		public string FileSystemPath { get; set; }

		public Snapshot PreviousSnapshot { get; set; }
		public int PreviousSnapshotId { get; set; }
		public ICollection<BranchAssociation> FilePushAssociation { get; set; }
	}


}