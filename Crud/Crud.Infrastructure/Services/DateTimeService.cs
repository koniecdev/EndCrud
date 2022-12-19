using Crud.Application.Common.Interfaces;

namespace Crud.Infrastructure.Services
{
	public class DateTimeService : IDateTime
	{
		public DateTime Now => DateTime.Now.ToUniversalTime();
	}
}
