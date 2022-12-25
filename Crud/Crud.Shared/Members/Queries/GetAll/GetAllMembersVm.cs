namespace Crud.Shared.Members.Queries;
public class GetAllMembersVm
{
	public GetAllMembersVm()
	{
		Members = new List<GetAllMembersDto>();
	}
	public ICollection<GetAllMembersDto> Members { get; set; }
}