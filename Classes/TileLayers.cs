using System.Collections.Generic;
using System.Linq;
using Godot;
using Newtonsoft.Json;

public class TileLayers
{
    [JsonProperty("Floor")]
    public List<TileCoordinate> FloorCoordinates { get; set; }
    
    [JsonProperty("Walls")]
    public List<TileCoordinate> WallCoordinates { get; set; }
    
    [JsonProperty("Targets")]
    public List<TileCoordinate> TargetsCoordinates { get; set; }
    
    [JsonProperty("TargetBoxes")]
    public List<TileCoordinate> TargetBoxesCoordinates { get; set; }
    
    [JsonProperty("Boxes")]
    public List<TileCoordinate> BoxesCoordinates { get; set; }

    private Dictionary<TileLayerType, List<Vector2I>> _layers;

    public void Convert()
    {
        _layers = new Dictionary<TileLayerType, List<Vector2I>>
        {
            { TileLayerType.Floor, FloorCoordinates.Select(tileCoordinates => tileCoordinates.ToVector2I()).ToList() },
            { TileLayerType.Walls, WallCoordinates.Select(tileCoordinates => tileCoordinates.ToVector2I()).ToList() },
            { TileLayerType.Targets, TargetsCoordinates.Select(tileCoordinates => tileCoordinates.ToVector2I()).ToList() },
            { TileLayerType.TargetBoxes, TargetBoxesCoordinates.Select(tileCoordinates => tileCoordinates.ToVector2I()).ToList() },
            { TileLayerType.Boxes, BoxesCoordinates.Select(tileCoordinates => tileCoordinates.ToVector2I()).ToList() },
            
        };
    }

    public List<Vector2I> GetLayerTiles(TileLayerType layer)
    {
        return _layers[layer];
    }
}