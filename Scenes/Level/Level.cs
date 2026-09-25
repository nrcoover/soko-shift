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
	[Export] private AnimatedSprite2D _player;
	[Export] private GameUi _gameUi;

	private Vector2I _playerTile;
	private bool _gameOver = false;

	public override void _Ready()
	{
		SetupLevel();
	}

  public override void _UnhandledInput(InputEvent @event)
  {
		if(@event.IsActionPressed("ui_cancel"))
		{
			GameManager.LoadLevelSelect();
			return;
		}

		if(@event.IsActionPressed("reload"))
		{
			GetTree().ReloadCurrentScene();
			return;
		}

    Vector2I moveInput = GetMoveInput(@event);
		MovePlayer(moveInput);
  }

	private void CheckGameState()
	{
		var targetsTiles = _targetsTiles.GetUsedCells();
		foreach (Vector2I cell in targetsTiles)
		{
			if (!CellIsBox(cell)) {
				return;
			}

			_gameOver = true;
			_gameUi.GameOver();
		}
	}

	private Vector2I GetMoveInput(InputEvent @event)
	{
		Vector2I moveDirection = Vector2I.Zero;

		if (_gameOver)
		{
			return moveDirection;
		}
		
		if (Input.IsActionJustPressed("left"))
		{
			moveDirection = Vector2I.Left;
			_player.Play("left");
		}
		else if (Input.IsActionJustPressed("right"))
		{
			moveDirection = Vector2I.Right;
			_player.Play("right");
		}
		else if (Input.IsActionJustPressed("up"))
		{
			moveDirection = Vector2I.Up;
			_player.Play("up");
		}
		else if (Input.IsActionJustPressed("down"))
		{
			moveDirection = Vector2I.Down;
			_player.Play("down");
		}

		return moveDirection;
	}

	private void MovePlayer(Vector2I moveInput)
	{
		if (Vector2I.Zero == moveInput)
		{
			return;
		}
		
		Vector2I destinationTile = _playerTile + moveInput;

		if (CellIsWall(destinationTile))
		{
			return;
		}

		if (CellIsBox(destinationTile) && !BoxCanMove(destinationTile, moveInput))
		{
			return;
		}

		if (CellIsBox(destinationTile)) {
			MoveBox(destinationTile, moveInput);	
		}

		PlacePlayerOnTile(destinationTile);

		_gameUi.IncrementMoves();

		CheckGameState();
	}

	private void MoveBox(Vector2I boxCell, Vector2I direction)
	{
		Vector2I destinationCell = boxCell + direction;
		_boxesTiles.EraseCell(boxCell);

		TileLayerType layerType = TileLayerType.Boxes;

		if (_targetsTiles.GetUsedCells().Contains(destinationCell))
		{
			layerType = TileLayerType.TargetBoxes;
		}
		
		_boxesTiles.SetCell(destinationCell, (int)TileAtlass.primary, GetAtlasCoordinate(layerType));
	}

	private bool CellIsWall(Vector2I cell)
	{
		return _wallsTiles.GetUsedCells().Contains(cell);
	}

	private bool CellIsBox(Vector2I cell)
	{
		return _boxesTiles.GetUsedCells().Contains(cell);
	}

	private bool CellIsEmpty(Vector2I cell)
	{
		return !CellIsWall(cell) && !CellIsBox(cell);
	}

	private bool BoxCanMove(Vector2I boxCell, Vector2I direction)
	{
		return CellIsEmpty(boxCell + direction);
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
		_player.GlobalPosition = _floorTiles.ToGlobal(_floorTiles.MapToLocal(tileCoordinate));

		_playerTile = tileCoordinate;
	}

	private void SetupLevel()
	{
		ClearTiles();

		LevelLayout levelLayout = LevelData.GetLevelData(GameManager.SelectedLevel);

		SetupLayer(TileLayerType.Floor, _floorTiles, levelLayout);
		SetupLayer(TileLayerType.Walls, _wallsTiles, levelLayout);
		SetupLayer(TileLayerType.Targets, _targetsTiles, levelLayout);
		SetupLayer(TileLayerType.Boxes, _boxesTiles, levelLayout);
		SetupLayer(TileLayerType.TargetBoxes, _boxesTiles, levelLayout);

		CenterMap();
		PlacePlayerOnTile(levelLayout.PlayerStart.ToVector2I());
	}
}
