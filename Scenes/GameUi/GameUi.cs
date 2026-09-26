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
		SubscribeToSignals();
		SetUi();
	}

	public override void _ExitTree()
	{
		UnSubscribeFromSignals();
	}

  private void SubscribeToSignals()
  {
    SignalManager.Instance.IncrementMoves += OnIncrementMoves;
		SignalManager.Instance.GameOver += OnGameOver;
  }

	private void UnSubscribeFromSignals()
  {
    SignalManager.Instance.IncrementMoves -= OnIncrementMoves;
		SignalManager.Instance.GameOver -= OnGameOver;
  }

	private void OnIncrementMoves()
	{
		_movesMade++;
		_movesLabel.Text = _movesMade.ToString();
	}

	private void OnGameOver()
	{
		_gameOverPanel.Show();
		ScoreSync.LevelCompleted(GameManager.SelectedLevel, _movesMade);
	}

  private void SetUi()
	{
		_gameOverPanel.Hide();
		_movesMade = 0;
		_movesLabel.Text = "0";
		_levelLabel.Text = GameManager.SelectedLevel;
		_bestLabel.Text = ScoreSync.GetLevelBestScore(GameManager.SelectedLevel).ToString();
	}
}
