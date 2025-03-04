

namespace GitRepository.Repository.Model
{
	public  class Push
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public DateTime Upload {  get; set; }
		public Branch Branch { get; set; }
		public int BranchId { get; set; }
		public Push? PreviousPush { get; set; }
		public int? PreviousPushId { get; set; }
		public ICollection<BranchAssociation> AssociationFileSnapshot { get; set; }
	}
}
