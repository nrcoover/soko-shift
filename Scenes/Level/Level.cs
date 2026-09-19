using Godot;

public partial class Level : Node
{
	public enum TileAtlass
	{
		primary = 0,
	}

	[Export] private TileMapLayer _floor;
	public override void _Ready()
	{
		GD.Print(_floor.GetUsedCells());
		GD.Print(_floor.GetUsedRect());
		GD.Print(_floor.GetUsedRect().GetCenter());
		
		var targetSetLocation = new Vector2I(3,1);
		var tileAtlassCoordinate = new Vector2I(1,0);

		// We can use the "Atlas Id" of a TileSet's srpitesheet and then the Atlass Coordinate to programmatically select a tile from a tileset for dynamically adding tiles to a map through code.
		_floor.SetCell(targetSetLocation, (int)TileAtlass.primary, tileAtlassCoordinate);

		var targetEraseLocation = new Vector2I(2,1);

		_floor.EraseCell(targetEraseLocation);

		// The following would clear all tiles from the map:
		// _floor.Clear();
	}
}
