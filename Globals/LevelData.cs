using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

public partial class LevelData : Node
{
	public static LevelData Instance {get; private set;}

	private const string LEVEL_DATA_PATH = "res://Data/LevelData.json";

	public Dictionary<string, LevelLayout> LevelDataDictionary { get; private set; } = new();

	public override void _EnterTree()
	{
		Instance = this;
		LoadLevelData();
	}

	private void LoadLevelData()
	{
		if (!FileAccess.FileExists(LEVEL_DATA_PATH))
		{
			GD.PrintErr("LEVEL_DATA_PARTH file not found!");
			
			return;
		}

		string jsonDataString = FileAccess.GetFileAsString(LEVEL_DATA_PATH);
		
		if (string.IsNullOrEmpty(jsonDataString))
		{
			GD.PrintErr("jsonDataString empty!");
			
			return;
		}

		LevelDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, LevelLayout>>(jsonDataString);

		foreach (var layout in LevelDataDictionary.Values)
		{
			layout.TileLayers.Convert();
		}

		GD.Print("Data loaded!");
	}
}
