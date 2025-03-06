

namespace GitRepository.Repository.Model
{
	public  class Push
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public int BranchId { get; set; }
		public Branch Branch { get; set; }
		public DateTime Upload {  get; set; }
		public Push? PreviousPush { get; set; }
		public int? PreviousPushId { get; set; }
		public int Action { get; set; }
		public List<BranchAssociation>? AssociationFileSnapshot { get; set; }
	}
}
