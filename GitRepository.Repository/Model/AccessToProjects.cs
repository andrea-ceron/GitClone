using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepository.Repository.Model
{
    public class AccessToProjects
    {
        public int Id { get; set; }
        public User Programmer { get; set; }
        public int ProgrammerId { get; set; }
		public bool Owner { get; set; }
		public Project Project { get; set; }
		public int ProjectId { get; set; }

	}
}
