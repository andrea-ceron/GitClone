using GitRepository.Repository.Abstraction;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepository.Repository
{
	public class TransactionUnit : ITransactionUnit
	{
		private readonly GitRepoDbContext _context;
		private IDbContextTransaction _transaction;

		public TransactionUnit(GitRepoDbContext context)
		{
			_context = context;
		}
		public async Task BeginTransactionAsync()
		{
			_transaction = await _context.Database.BeginTransactionAsync();
		}

		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
			await _transaction.CommitAsync();
		}

		public void Dispose()
		{
			_transaction?.Dispose();
		}

		public async Task RollbackAsync()
		{
			await _transaction.RollbackAsync();
		}
	}
}
