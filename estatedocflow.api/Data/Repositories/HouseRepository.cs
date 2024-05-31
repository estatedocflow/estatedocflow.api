using estatedocflow.api.Data.Repositories.Base;
using estatedocflow.api.Data.Repositories.Interfaces;
using estatedocflow.api.Infrastructure;
using estatedocflow.api.Models.Entities;

namespace estatedocflow.api.Data.Repositories;

public class HouseRepository(RealEstateDbContext dbContext) : RepositoryBase<House>(dbContext), IHouseRepository
{
}