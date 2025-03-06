using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitRepository.Repository.Model;

namespace GitRepository.Repository
{
	public class GitRepoDbContext : DbContext
	{
		protected override void OnModelCreating(ModelBuilder mb)
		{
			mb.Entity<User>().HasKey(u => u.Id);
			mb.Entity<User>().HasMany(p => p.Projects)
				.WithOne(b => b.Programmer)
				.HasForeignKey(b => b.ProgrammerId);

			mb.Entity<Project>().HasKey(p => p.Id);
			mb.Entity<Project>().HasMany(p => p.BranchList)
				.WithOne(b => b.Project)
				.HasForeignKey(b => b.ProjectId)
				.OnDelete(DeleteBehavior.Cascade);
			mb.Entity<Project>().HasMany(p => p.AccessFromUsers)
				.WithOne(b => b.Project)
				.HasForeignKey(b => b.ProjectId);

			mb.Entity<Branch>().HasKey(b => b.Id);
			mb.Entity<Branch>().HasMany(b => b.PushHistory)
				.WithOne(p => p.Branch)
				.HasForeignKey(p => p.BranchId)
				.OnDelete(DeleteBehavior.Cascade);
			//mb.Entity<Branch>().HasOne(b => b.LastPush)
			//	.WithOne()
			//	.HasForeignKey<Branch> (p => p.LastPushId)
			//	.OnDelete(DeleteBehavior.Cascade);

			mb.Entity<Push>().HasKey(p => p.Id);
			mb.Entity<Push>().HasOne(p => p.PreviousPush)
				.WithMany()
				.HasForeignKey(p => p.PreviousPushId)
				.OnDelete(DeleteBehavior.Restrict);
			mb.Entity<Push>().HasMany(p => p.AssociationFileSnapshot)
				.WithOne(b => b.Push)
				.HasForeignKey(b => b.PushId);

			mb.Entity<RepoFile>().HasKey(r => r.Id);
			mb.Entity<RepoFile>().HasMany(r=>r.SnapshotList)
				.WithOne(s=>s.File)
				.HasForeignKey(s=>s.FileId)
				.OnDelete(DeleteBehavior.Cascade);
			mb.Entity<RepoFile>().HasMany(r=>r.PushSnapshotAssociation)
				.WithOne(p=>p.File)
				.HasForeignKey(p=>p.FileId);

			mb.Entity<Snapshot>().HasKey(s => s.Id);
			mb.Entity<Snapshot>().HasMany(s => s.FilePushAssociation)
				.WithOne(p => p.Snapshot)
				.HasForeignKey(p => p.SnapshotId);
			mb.Entity<Snapshot>().HasOne(s => s.PreviousSnapshot)
				.WithMany()
				.HasForeignKey(s=>s.PreviousSnapshotId);

			mb.Entity<BranchAssociation>().HasKey(b => b.Id);

			mb.Entity<AccessToProjects>().HasKey(b => b.Id);


		}

		public DbSet<User> Users { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<AccessToProjects> AccessesToProjects { get; set; }

		public DbSet<Branch> Branches { get; set; }
		public DbSet<Push> Pushes { get; set; }
		public DbSet<RepoFile> Files { get; set; }
		public DbSet<Snapshot> Snapshots { get; set; }
		public DbSet<BranchAssociation> BranchAssociations { get; set; }


	}
}
