using AutoMapper;
using Crud.Application.Common.Interfaces;
using Crud.Persistance;
using Crud.Shared.Common.Mappings;
using Moq;
using System;
using System.Threading;

namespace Crud.UnitTests;

public class CommandTestBase : IDisposable
{
	protected CrudDbContext _db { get; private set; }
	protected IMapper _mapper { get; private set; }
	protected IDateTime _dateTime { get; private set; }
	protected CancellationToken _token { get; private set; }
	public CommandTestBase()
	{
		//_db = CrudDbContextFactory.Create().Object;
		var configurationProvider = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile<MappingProfile>();
		});
		_mapper = configurationProvider.CreateMapper();
		var dateTimeMock = new Mock<IDateTime>();
		dateTimeMock.Setup(m => m.Now).Returns(new DateTime(2025, 1, 1));
		_dateTime = dateTimeMock.Object;
		_token = CancellationToken.None;
	}
	public void Dispose()
	{
		CrudDbContextFactory.Destroy(_db);
	}
}
