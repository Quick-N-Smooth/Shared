namespace TextInterpolationService;

public interface IKeywordResolver<T> 
	where T : class
{
	string Resolve(T entity);
}
