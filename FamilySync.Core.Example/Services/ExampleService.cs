using FamilySync.Core.Abstractions.Exceptions;
using FamilySync.Core.Example.Models.DTOs;
using FamilySync.Core.Example.Models.Requests;
using FamilySync.Core.Example.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FamilySync.Core.Example.Services;

public interface IExampleService
{
    public Task<ExampleDTO> Create(CreateExampleRequest request, CancellationToken cancellationToken);
    public Task<ExampleDTO> Get(Guid id, CancellationToken cancellationToken);
    public Task Delete(Guid id, CancellationToken cancellationToken);
    public Task<ExampleDTO> Update(Guid id, UpdateExampleRequest request, CancellationToken cancellationToken);
}

public class ExampleService : IExampleService
{
    private readonly ExampleContext _context;

    public ExampleService(ExampleContext context)
    {
        _context = context;
    }

    public async Task<ExampleDTO> Create(CreateExampleRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Models.Entity.Example>();

        _context.Examples.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Adapt<ExampleDTO>();
    }

    public async Task<ExampleDTO> Get(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Examples.FirstOrDefaultAsync(x => x.ID == id);

        if (entity is null)
        {
            throw new NotFoundException($"Could not find entity of type {typeof(Models.Entity.Example)} with id {id}");
        }

        return entity.Adapt<ExampleDTO>();
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = _context.Examples.FirstOrDefault(x => x.ID == id);

        if (entity is null)
        {
            return;
        }

        _context.Examples.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ExampleDTO> Update(Guid id, UpdateExampleRequest request, CancellationToken cancellationToken)
    {
        var entity = _context.Examples.FirstOrDefault(x => x.ID == id);

        if (entity is null)
        {
            throw new NotFoundException($"Could not find entity of type {typeof(Models.Entity.Example)} with id {id}");
        }

        request.Adapt(entity);
        _context.Examples.Update(entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Adapt<ExampleDTO>();
    }
}