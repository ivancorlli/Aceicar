namespace CompanyContext.Core.Interface;

public interface IArea<T>
{
    public Guid TypeId { get;} 
    public Guid? SpecializationId { get;} 
    abstract static T InType(Guid typeId);
    abstract static T InSpecialization(Guid typeId, Guid specializationId);
}