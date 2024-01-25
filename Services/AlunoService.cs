using BlzFluentUICrud.Context;
using BlzFluentUICrud.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlzFluentUICrud.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Aluno> AddAluno(Aluno aluno)
        {
            if (aluno == null)
            {
                throw new ArgumentNullException(nameof(aluno));
            }

            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<Aluno> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                throw new InvalidOperationException($"Aluno with ID {id} not found.");
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<Aluno> GetAluno(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var aluno = await _context.Alunos.FindAsync(id.Value);
            if (aluno == null)
            {
                throw new InvalidOperationException($"Aluno with ID {id} not found.");
            }

            return aluno;
        }

        public async Task<List<Aluno>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task<Aluno> UpdateAluno(Aluno aluno)
        {
            if (aluno == null)
            {
                throw new ArgumentNullException(nameof(aluno));
            }

            var existingAluno = await _context.Alunos.FindAsync(aluno.Id);
            if (existingAluno == null)
            {
                throw new InvalidOperationException($"Aluno with ID {aluno.Id} not found.");
            }

            // Atualiza propriedades do aluno existente com as do aluno fornecido.
            _context.Entry(existingAluno).CurrentValues.SetValues(aluno);

            await _context.SaveChangesAsync();
            return existingAluno;
        }
    }
}
