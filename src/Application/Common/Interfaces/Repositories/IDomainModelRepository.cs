using Defender.GeneralTestingService.Domain.Entities;

namespace Defender.GeneralTestingService.Application.Common.Interfaces.Repositories;

public interface IDomainModelRepository
{
    Task<DomainModel> GetDomainModelByIdAsync(Guid id);
}
