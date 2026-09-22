using Newtonsoft.Json;

public class LevelLayout
{
    [JsonProperty("tiles")]
    public TileLayers TileLayers { get; set; }

    [JsonProperty("player_start")]
    public TileCoordinate PlayerStart { get; set; }
}