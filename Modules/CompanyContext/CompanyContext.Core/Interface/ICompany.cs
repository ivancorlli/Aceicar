using Common.Basis.Aggregate;
using Common.Basis.Utils;
using Common.Basis.ValueObject;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Enumerable;
using CompanyContext.Core.Error;
using CompanyContext.Core.Event;
using CompanyContext.Core.ValueObject;

namespace CompanyContext.Core.Interface;

public abstract class ICompany : IAggregate
{
    public CompanyName Name { get; protected set; } = default!;
    public CompanyStatus Status { get; protected set; } = default!;
    public bool Published { get; protected set; } = false;
    public DateTime? PublishedAt { get; protected set; } = default!;
    public Email? Email { get; protected set; } = default!;
    public Phone? Phone { get; protected set; } = default!;
    public CompanyPicture? Picture { get; protected set; } = default!;
    public CompanyDescription? Description { get; protected set; } = default!;
    public Location? Address { get; protected set; } = default!;
    public IList<CompanyArea> Areas { get; private set; } = new List<CompanyArea>();
    public IList<Access> Accesses { get; private set; } = new List<Access>();

    public ICompany Publish()
    {
        CompanyPublished @event = new(Id, DateTime.Now);
        Apply(@event);
        Raise(@event);
        return this;
    }
    private void Apply(CompanyPublished @event)
    {
        Id = @event.CompanyId;
        Published = @event.Publish;
        PublishedAt = @event.PublishAt;
    }
    public ICompany ChangeEmail(Email email)
    {
        EmailChanged @event = new(Id, email.Value);
        Apply(@event);
        Raise(@event);
        return this;
    }
    private void Apply(EmailChanged @event)
    {
        Email = Email.Create(@event.Email);
    }
    public ICompany ChangePhone(Phone phone)
    {
        PhoneChanged @event = new(Id, phone.Country, phone.Number);
        Apply(@event);
        Raise(@event);
        return this;
    }
    private void Apply(PhoneChanged @event)
    {
        Phone = Phone.Create(@event.Country, @event.Number);
    }
    public ICompany ChangePicture(CompanyPicture picture)
    {
        PictureChanged @event = new(Id, picture.Value);
        Apply(@event);
        Raise(@event);
        return this;
    }
    private void Apply(PictureChanged @event)
    {
        Picture = CompanyPicture.Create(@event.Picture);
    }
    public ICompany ChangeDescription(CompanyDescription description)
    {
        DescriptionChanged @event = new(Id, description.Value);
        Apply(@event);
        Raise(@event);
        return this;
    }
    private void Apply(DescriptionChanged @event)
    {
        Description = CompanyDescription.Create(@event.Description);
    }
    public ICompany ChangeLocation(Location location)
    {
        LocationChanged @event = new(Id, location.Country, location.City, location.State, location.PostalCode, location.Steet, location.StreetNumber, location.Floor, location.Apartment);
        Apply(@event);
        Raise(@event);
        return this;
    }
    private void Apply(LocationChanged @event)
    {
        if (@event.Floor != null && @event.Apartment != null)
        {
            Address = Location.Create(
                @event.Country,
                @event.City,
                @event.State,
                @event.PostalCode,
                @event.Street,
                @event.StreetNumber,
                @event.Floor,
                @event.Apartment
                );
        }
        else
        {
            Address = Location.Create(
                @event.Country,
                @event.City,
                @event.State,
                @event.PostalCode,
                @event.Street,
                @event.StreetNumber
                );
        }
    }
    public Result<CompanyArea> AddToArea(Guid typeId, Guid specializationId)
    {
        Result<CompanyArea> area = CreateArea(typeId, specializationId);
        if (area.IsSuccess)
        {
            AddedToArea @event = new(Id, typeId, specializationId);
            Apply(@event);
            Raise(@event);
            return Result.Ok(area.Value);
        }
        return Result.Fail<CompanyArea>(area.Error);
    }
    private Result<CompanyArea> CreateArea(Guid typeId, Guid specializationId)
    {
        if (Areas.Count <= 0)
        {
            return Result.Ok(new CompanyArea(typeId, specializationId));
        }
        return Result.Fail<CompanyArea>(new AreaAlreadyRegistered());
    }
    private void Apply(AccessCreated @event)
    {
        Access access = new(@event.AccessId,@event.UserId, @event.RoleId);
        Accesses.Add(access);
    }

    public Result<Access> AddAccess(Guid userId, Guid roleId)
    {
        Result<Access> access = CreateAccess(userId, roleId);
        if (access.IsSuccess)
        {
            AccessCreated @event = new(Id,access.Value.Id,access.Value.UserId, access.Value.RoleId);
            Apply(@event);
            Raise(@event);
            return Result.Ok(access.Value);
        }
        return Result.Fail<Access>(access.Error);
    }
    private Result<Access> CreateAccess(Guid userId,Guid roleId)
    {
        IList<Access> exists = Accesses.Where(x=>x.UserId == userId).ToList();
        if(exists.Count <= 0) return Result.Ok(new Access(Guid.NewGuid(),userId,roleId));
        return Result.Fail<Access>(new AccessAlreadyRegistered());
    }
    private void Apply(AddedToArea @event)
    {
        CompanyArea area = new(@event.TypeId, @event.SpecializationId);
        Areas.Add(area);
    }

}