namespace TextInterpolationService.Tests;

public class TextInterpolationServiceTests
{
	[Fact]
	public void TextInterpolationService_HappyFlow()
	{ 
		var target = new TextInterpolationService<DomainDatasource>();

		var template = "This is a template for ::number:: ::name:: edition: ::date::";

		var dataSource = new DomainDatasource
		{
			Date = new DateTime(2023, 04, 13),
			Name = "Dalmatian",
			Number = 101
		};

		var result = target.InterpolateTemplate(dataSource, template);

		result.ShouldBe("This is a template for 101 Dalmatian edition: 2023-04-13 00:00:00");
	}

	[Fact]
	public void TextInterpolationService_HappyFlowWithManualResolver()
	{
		var target = new TextInterpolationService<DomainDatasource>();

		target.Resolvers.Add("date", new KeywordResolver<DomainDatasource>(i => i.Date.ToString("yyyy-MM-dd")));

		var template = "This is a template for ::number:: ::name:: edition: ::date::";

		var dataSource = new DomainDatasource
		{
			Date = new DateTime(2023, 04, 13),
			Name = "Dalmatian",
			Number = 101
		};

		var result = target.InterpolateTemplate(dataSource, template);

		result.ShouldBe("This is a template for 101 Dalmatian edition: 2023-04-13");
	}

	[Fact]
	public void TextInterpolationService_WhenKeyNotFound_ThrowExceptionWithKeyName()
	{
		var target = new TextInterpolationService<DomainDatasource>();

		var template = "This is a template for ::not_exist_key:: ::name:: edition: ::date::";

		var dataSource = new DomainDatasource
		{
			Date = new DateTime(2023, 04, 13),
			Name = "Dalmatian",
			Number = 101
		};

		Should.Throw<InvalidOperationException>(() => target.InterpolateTemplate(dataSource, template))
			.Message.ShouldContain("Failed to resolve key not_exist_key");
	}
}
