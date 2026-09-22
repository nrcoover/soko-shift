using Godot;
using Newtonsoft.Json;

public class TileCoordinate
{
    [JsonProperty("x")]
    public int XCoordinate { get; set; }

    [JsonProperty("y")]
    public int YCoordinate { get; set; }

    public Vector2I ToVector2I()
    {
        return new Vector2I(XCoordinate, YCoordinate);
    }
}
