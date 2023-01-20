namespace CrudApp.Models;
public class AdminLibraryModel
{
    public AdminLibraryModel(int chosenType)
    {
        ChosenType = chosenType;
    }
	public int ChosenType { get; set; }
}