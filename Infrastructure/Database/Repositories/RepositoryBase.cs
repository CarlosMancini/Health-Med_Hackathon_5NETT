﻿using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public Task Atualizar(T entidade)
        {
            throw new NotImplementedException();
        }

        public async Task Cadastrar(T entidade)
        {
            await _dbSet.AddAsync(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var entidade = await ObterPorId(id);
            if (entidade is not null)
            {
                _dbSet.Remove(entidade);
                await _context.SaveChangesAsync();
            };
        }

        public async Task<T> ObterPorId(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<IList<T>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
