namespace TextInterpolationService;

public class KeywordResolver<T> : IKeywordResolver<T>
	where T : class
{
	private readonly Func<T, string?> accessor;

	public KeywordResolver(Func<T, string?> accessor)
	{
		this.accessor = accessor;
	}

	public string Resolve(T entity)
	{
		var value = accessor.Invoke(entity);

		if (value == null)
		{
			throw new Exception("Resolve failure");
		}

		return value;
	}
}
