namespace GitRepository.Repository.Model
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public ICollection<Project>? Projects { get; set; }

	}


}