using Godot;

public partial class Level : Node
{
	public enum TileAtlass
	{
		primary = 0,
	}

	[Export] private Node2D _tileLayers;
	[Export] private TileMapLayer _floorTiles;
	[Export] private TileMapLayer _wallsTiles;
	[Export] private TileMapLayer _targetsTiles;
	[Export] private TileMapLayer _boxesTiles;
	[Export] private Sprite2D _debug;

	public override void _Ready()
	{
		SetupLevel();
	}

	private void ClearTiles()
	{
		foreach (var tileLayer in _tileLayers.GetChildren())
		{
			if (tileLayer is TileMapLayer layer)
			{
				layer.Clear();
			}
		}
	}

	private Vector2I GetAtlasCoordinate(TileLayerType layerType)
	{
		switch (layerType)
		{
			case TileLayerType.Walls:
				return new Vector2I(0, 0);
			case TileLayerType.Floor:
				return new Vector2I(4, 0);
			case TileLayerType.Targets:
				return new Vector2I(3, 0);
			case TileLayerType.TargetBoxes:
				return new Vector2I(2, 0);
			case TileLayerType.Boxes:
				return new Vector2I(1, 0);
			default:
				return new Vector2I(0, 0);
		}
	}

	private void AddTile(TileLayerType layerType, Vector2I tileCoordinate, TileMapLayer mapLayer)
	{
		mapLayer.SetCell(tileCoordinate, (int)TileAtlass.primary, GetAtlasCoordinate(layerType));
	}

	private void SetupLayer(TileLayerType layerType, TileMapLayer mapLayer, LevelLayout levelLayout)
	{
		foreach (var tileCoordinate in levelLayout.TileLayers.GetLayerTiles(layerType))
		{
			AddTile(layerType, tileCoordinate, mapLayer);
		}
	}

	private void CenterMap()
	{
		Vector2I mapMiddle = _floorTiles.GetUsedRect().GetCenter();
		Vector2 mapMiddlePixel = _floorTiles.ToGlobal(_floorTiles.MapToLocal(mapMiddle));

		_tileLayers.Position = _floorTiles.GetViewportRect().GetCenter() - mapMiddlePixel;
	}

	private void PlacePlayerOnTile(Vector2I tileCoordinate)
	{
		_debug.GlobalPosition = _floorTiles.ToGlobal(_floorTiles.MapToLocal(tileCoordinate));	
	}

	private void SetupLevel()
	{
		ClearTiles();

		LevelLayout levelLayout = LevelData.GetLevelData("26");

		SetupLayer(TileLayerType.Floor, _floorTiles, levelLayout);
		SetupLayer(TileLayerType.Walls, _wallsTiles, levelLayout);
		SetupLayer(TileLayerType.Targets, _targetsTiles, levelLayout);
		SetupLayer(TileLayerType.Boxes, _boxesTiles, levelLayout);
		SetupLayer(TileLayerType.TargetBoxes, _boxesTiles, levelLayout);

		CenterMap();
		PlacePlayerOnTile(levelLayout.PlayerStart.ToVector2I());
	}
}
