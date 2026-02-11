using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Repositories;
using AgroSolutions.IoT.IngestaoDados.Domain.Entities;
using AgroSolutions.IoT.IngestaoDados.Infrastructure.Data;

namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.Repositories;

public class LeituraSensorTalhaoRepository : ILeituraSensorTalhaoRepository
{
    private readonly DataContext _context;
    
    public LeituraSensorTalhaoRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task AdicionarAsync(LeituraSensorTalhao leituraSensorTalhao)
    {
        await _context.LeiturasSensorTalhao.AddAsync(leituraSensorTalhao);
        await _context.SaveChangesAsync();
    }
}
