using Godot;
using Newtonsoft.Json;
using System.Collections.Generic;

public partial class ScoreSync : Node
{
	private const string SCORE_FILE = "user://sokoshift.json";
	private const int DEFAULT_SCORE = 9999;

	public static ScoreSync Instance { get; private set; }

	public Dictionary<string, int> LevelScores { get; private set; } = new();

	public override void _Ready()
	{
		Instance = this;
		LoadScores();
	}

	public static int GetLevelBestScore(string level)
	{
		return Instance.LevelScores.GetValueOrDefault(level, DEFAULT_SCORE);
	}

	public static void LevelCompleted(string level, int moves)
	{
		if (GetLevelBestScore(level) > moves)
			Instance.LevelScores[level] = moves;
			Instance.SaveScores();
	}

	private void LoadScores()
	{       
		string jsonString = FileAccess.GetFileAsString(SCORE_FILE);
		if (!string.IsNullOrEmpty(jsonString))
			LevelScores = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonString);
	}

	private void SaveScores()
	{
		using var file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Write);
		if (file == null)
			return;

		string jsonString = JsonConvert.SerializeObject(LevelScores);
		file.StoreString(jsonString);
	}
}
