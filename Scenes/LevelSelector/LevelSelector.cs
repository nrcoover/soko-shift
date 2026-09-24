using Godot;

public partial class LevelSelector : Control
{
	[Export] private PackedScene _levelButtonScene;
	[Export] private GridContainer _gridContainer;

	public override void _Ready()
	{
		foreach(var item in LevelData.GetLevelNumbers())
		{
			var levelButton = _levelButtonScene.Instantiate<LevelButton>();
			levelButton.Setup(item);
			_gridContainer.AddChild(levelButton);
		}
	}
}
