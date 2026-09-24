using Godot;

public partial class GameManager : Node
{
	public static GameManager Instance {get; private set;}

	private PackedScene _levelSelectScene = GD.Load<PackedScene>("res://Scenes/LevelSelector/LevelSelector.tscn");
	private PackedScene _levelScene = GD.Load<PackedScene>("res://Scenes/Level/Level.tscn");

	public static string SelectedLevel {get; private set;}

	public override void _EnterTree()
	{
		Instance = this;
	}

	private void LoadLevelSelectScene()
	{
		GetTree().ChangeSceneToPacked(_levelSelectScene);
	}

	private void LoadLevelScene(string levelNumber)
	{
		SelectedLevel = levelNumber;
		GetTree().ChangeSceneToPacked(_levelScene);
	}

	public static void LoadLevelSelect()
	{
		Instance.LoadLevelSelectScene();
	}

	public static void LoadLevel(string levelNumber)
	{
		Instance.LoadLevelScene(levelNumber);
	}
}
