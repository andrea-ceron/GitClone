namespace GitRepository.Repository.Model
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public List<AccessToProjects>? Projects { get; set; }

	}


}