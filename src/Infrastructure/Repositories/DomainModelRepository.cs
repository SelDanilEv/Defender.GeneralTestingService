using Defender.Common.Configuration.Options;
using Defender.Common.DB.Repositories;
using Defender.GeneralTestingService.Application.Common.Interfaces.Repositories;
using Defender.GeneralTestingService.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Defender.GeneralTestingService.Infrastructure.Repositories.DomainModels;

public class DomainModelRepository : BaseMongoRepository<DomainModel>, IDomainModelRepository
{
    public DomainModelRepository(IOptions<MongoDbOptions> mongoOption) : base(mongoOption.Value)
    {
    }

    public async Task<DomainModel> GetDomainModelByIdAsync(Guid id)
    {
        return await GetItemAsync(id);
    }
}
