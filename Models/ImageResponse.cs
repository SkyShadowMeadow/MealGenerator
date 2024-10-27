namespace Models;

public class ImageResponse
{
    public List<ImageData> data { get; set; }
}

public class ImageData
{
    public string url { get; set; }
}