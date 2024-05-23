using estatedocflow.api.Data.Repositories.Base;
using estatedocflow.api.Data.Repositories.Interfaces;
using estatedocflow.api.Infrastructure;
using estatedocflow.api.Models.Entities;

namespace estatedocflow.api.Data.Repositories;
public class HouseAttachmentRepository : RepositoryBase<HouseAttachment>, IHouseAttachmentRepository
{
    private readonly RealEstateDbContext _dbContext;
    public HouseAttachmentRepository(RealEstateDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}