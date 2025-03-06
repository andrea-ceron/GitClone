namespace GitRepository.Repository.Model
{
	public class RepoFile
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string FileSystemPath { get; set; }
		public string OldFileSystemPath { get; set; }

		public ICollection<Snapshot> SnapshotList { get; set; }
		public ICollection<BranchAssociation> PushSnapshotAssociation { get; set; }


	}


}