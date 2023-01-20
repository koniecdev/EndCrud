namespace CrudApp.Models;
public class AdminHeaderModel
{
    public AdminHeaderModel()
    {
        Title = string.Empty;
        Buttons = new();
    }
    public AdminHeaderModel(string title, List<Tuple<string, string>> buttons)
    {
        Title = title;
        Buttons = buttons;
    }

    public string Title { get; set; }
    public List<Tuple<string, string>> Buttons { get; set; }
}