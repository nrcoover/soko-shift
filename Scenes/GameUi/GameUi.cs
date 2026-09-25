using Godot;

public partial class GameUi : Control
{
	[Export] private Label _levelLabel;
	[Export] private Label _movesLabel;
	[Export] private Label _bestLabel;
	[Export] private PanelContainer _gameOverPanel;

	private int _movesMade = 0;

	public override void _Ready()
	{
		SetUi();
	}

	private void SetUi()
	{
		_gameOverPanel.Hide();
		_movesMade = 0;
		_movesLabel.Text = "0";
		_levelLabel.Text = GameManager.SelectedLevel;
		_bestLabel.Text = ScoreSync.GetLevelBestScore(GameManager.SelectedLevel).ToString();
	}

	public void IncrementMoves()
	{
		_movesMade++;
		_movesLabel.Text = _movesMade.ToString();
	}

	public void GameOver()
	{
		_gameOverPanel.Show();
		ScoreSync.LevelCompleted(GameManager.SelectedLevel, _movesMade);
	}
}
