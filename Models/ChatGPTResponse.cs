namespace Models;

public class ChatGPTResponse
{
    public List<Choice> choices { get; set; }
}

public class Choice
{
    public string text { get; set; }
}