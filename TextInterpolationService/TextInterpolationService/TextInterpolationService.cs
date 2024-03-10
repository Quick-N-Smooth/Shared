using System.Reflection;
using System.Text.RegularExpressions;

namespace TextInterpolationService;

/// <summary>
/// The base lightweight text interpolation service object. It parses the template file for
/// the following pattern: ::key:: and tries to find the value for the key in the datasource 
/// domain object.
/// When it founds it it replaces the field in the template with the value.
/// </summary>
/// <typeparam name="T">Domain object that serves as a datasource</typeparam>
/// <remarks>
/// This is a lighytwight class for interpolation for with some limitations. It expects that one 
/// domain object holds all the values used by the template.
/// It uses reflection technik to find key property in the dataSource domain object. However it 
/// is possible to add a manual method by adding a binder object the resolvers dictionary. If 
/// the manual method is found that is used instead of the reflection for the particular key.
/// </remarks>
public class TextInterpolationService<T> : ITextInterpolationService<T>
	where T : class
{
	public Dictionary<string, IKeywordResolver<T>> Resolvers { get; set; }
		= new Dictionary<string, IKeywordResolver<T>>();


	public string InterpolateTemplate(T dataSource, string template)
	{
		var keys = GetKeys(template);
		foreach (var key in keys)
		{
			var value = ResolveKeyValue(dataSource, key);
			template = template.Replace($"::{key}::", value, StringComparison.OrdinalIgnoreCase);
		}

		return template;
	}

	private string? ResolveKeyValue(T dataSource, string key)
	{
		try
		{
			// first try to find a manual resolver
			IKeywordResolver<T>? resolver; 
			var found = Resolvers.TryGetValue(key, out resolver);

			if (found)
			{
				return resolver!.Resolve(dataSource);
			}

			// if not found try with reflection
			Type dataSourceType = typeof(T);

			var field = dataSourceType.GetProperty(key, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

			if (field is null)
			{
				throw new Exception("Property {key} not exists");
			}

			var value = field.GetValue(dataSource);

			return value?.ToString() ?? null;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to resolve key {key}", ex);
		}
	}

	private IEnumerable<string> GetKeys(string textTemplate)
	{
		var res = new List<string>();
		var matches = Regex.Matches(textTemplate ?? string.Empty, @"\::(?<key>[a-zA-Z\d_.]+)::");

		foreach (Match? m in matches)
		{
			var key = m?.Groups["key"];
			if (key != null)
			{
				res.Add(key.Value.ToLower());
			}
		}

		return res.Distinct();
	}
}
