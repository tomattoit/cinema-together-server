using Domain.Common;

namespace Domain.Entities;

public class City : IBaseEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid CountryId { get; set; }

    public Country Country { get; set; }

    public List<User> Users { get; set; }
}
