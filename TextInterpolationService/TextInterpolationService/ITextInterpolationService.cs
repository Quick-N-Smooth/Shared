namespace TextInterpolationService;

public interface ITextInterpolationService<T> 
	where T : class
{
	string InterpolateTemplate(T dataSource, string template);
}
