namespace CompanyContext.Application.Interface;

public interface IApplicationQuery
{
    public IQueryable<T> Query<T>() where T: class;
}