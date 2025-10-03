using MA.Clean.Template.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MA.Clean.Template.Domain.Entities;

public sealed class Sample : EntityBase<Guid>, IAggregateRoot
{
    [MaxLength(DomainConstants.NameMaxLength)]
    public string Name { get; private set; } = default!;
    public SampleStatus Status { get; private set; } = SampleStatus.Active;

    private Sample() { }

    public Sample(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        MarkCreated();
    }

    public void UpdateName(string name)
    {
        Name = name;
        MarkModified();
    }
}