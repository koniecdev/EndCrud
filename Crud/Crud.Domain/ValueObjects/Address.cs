using Crud.Domain.Common;
namespace Crud.Domain.ValueObjects;
public class Address : ValueObject
{
	public string City { get; set; } = "";
	public string Street { get; set; } = "";
	public string House { get; set; } = "";
	public string PostalCode { get; set; } = "";

	public override string ToString()
	{
		return $"{Street} {House}, {PostalCode} {City}";
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return City;
		yield return Street;
		yield return House;
		yield return PostalCode;
	}
}