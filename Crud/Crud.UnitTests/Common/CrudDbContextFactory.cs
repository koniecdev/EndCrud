using Crud.Persistance;
using Crud.Domain.Entities;
using Moq;
using System;
using Crud.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Crud.UnitTests;

public static class CrudDbContextFactory
{
	public static Mock<CrudDbContext> Create()
	{
		var dateTime = new DateTime(2000, 1, 1);
		var dateTimeMock = new Mock<IDateTime>();
		dateTimeMock.Setup(m => m.Now).Returns(dateTime);

		var currentUserMock = new Mock<ICurrentUserService>();
		currentUserMock.Setup(m => m.Email).Returns("mocked@user.pl");
		currentUserMock.Setup(m => m.Id).Returns("mockedUserId");
		currentUserMock.Setup(m => m.IsAuthenticated).Returns(true);

		var options = new DbContextOptionsBuilder<CrudDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

		var mock = new Mock<CrudDbContext>(options, dateTimeMock.Object, currentUserMock.Object) { CallBase = true };

		var context = mock.Object;

		context.Database.EnsureCreated();

		var member1 = new Member() { Id = 1, StatusId = 1, Email = "lmao@lmao.pl", UserId = "UserIdOfMember" };
		context.Members.Add(member1);
		var member2 = new Member() { Id = 2, StatusId = 1, Email = "mocked@user.pl", UserId = "mockedUserId" };
		context.Members.Add(member2);

		var category1 = new Category() { Id = 1, StatusId = 1, Name = "Uncategorized"};
		context.Categories.Add(category1);
		var category2 = new Category() { Id = 2, StatusId = 1, Name = "Sponsored" };
		context.Categories.Add(category2);


		var picture1 = new Picture() { Id = 1, StatusId = 1, RelativePath = "/2022/12/first.jpg" };
		context.Pictures.Add(picture1);
		var picture2 = new Picture() { Id = 2, StatusId = 1, RelativePath = "/2022/12/second.jpg" };
		context.Pictures.Add(picture2);

		var article1 = new Article() { 
			Id = 1,
			StatusId = 1,
			Header = "Szok i niedowierzanie! VAT 23% na paliwo wraca!",
			Content = "Lorem ipsum dolor sit amet is simple dummy text to fill the blank article.",
			MemberId = 1,
			CategoryId = 1,
			ThumbnailId = 1,
			Pictures = new List<Picture>() { picture1, picture2 }
		};
		context.Articles.Add(article1);
		var article2 = new Article()
		{
			Id = 2,
			StatusId = 1,
			Header = "#2 Szok i niedowierzanie! VAT 23% na paliwo wraca!",
			Content = "#2 Lorem ipsum dolor sit amet is simple dummy text to fill the blank article.",
			MemberId = 1,
			CategoryId = 2,
			ThumbnailId = 1,
			Pictures = new List<Picture>() { picture1, picture2 }
		};
		context.Articles.Add(article2);

		context.SaveChanges();

		return mock;
	}

	public static void Destroy(CrudDbContext context)
	{
		context.Database.EnsureDeleted();
		context.Dispose();
	}

}
