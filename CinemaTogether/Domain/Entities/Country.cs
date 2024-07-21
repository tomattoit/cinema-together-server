using Domain.Common;

namespace Domain.Entities;

public class Country : IBaseEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<City> Cities { get; set; }
}
