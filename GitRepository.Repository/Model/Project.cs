using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GitRepository.Repository.Model
{
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public List<Branch> BranchList { get; set;}
	}


}
