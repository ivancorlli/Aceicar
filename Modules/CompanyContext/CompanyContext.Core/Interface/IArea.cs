namespace CompanyContext.Core.Interface;

public abstract record IArea
{
    public Guid TypeId { get;}
        /// <summary>
    /// If Specialization is null means that is related for all specifications type's, otherwise it is related only to a specific specialization 
    /// </summary>
    /// <value></value> 
    public Guid? SpecializationId { get;} 
    internal IArea (Guid typeId){
        TypeId = typeId;
    }
    internal IArea(Guid typeId, Guid specializationId){
        TypeId = typeId;
        SpecializationId = specializationId;
    }
}