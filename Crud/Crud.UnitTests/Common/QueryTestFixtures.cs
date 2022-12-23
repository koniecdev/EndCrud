using AutoMapper;
using Crud.Persistance;
using Crud.Shared.Common.Mappings;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Crud.UnitTests;

public class QueryTestFixtures : IDisposable
{
	public CrudDbContext Context { get; private set; }
	public IMapper Mapper { get; private set; }
	protected CancellationToken _token { get; private set; }

	public QueryTestFixtures()
	{
		Context = CrudDbContextFactory.Create().Object;
		var configurationProvider = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile<MappingProfile>();
		});
		Mapper = configurationProvider.CreateMapper();
		_token = CancellationToken.None;
	}
	public void Dispose()
	{
		CrudDbContextFactory.Destroy(Context);
	}
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixtures> {  }