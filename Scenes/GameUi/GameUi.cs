using Godot;

public partial class GameUi : Control
{
	[Export] private Label _levelLabel;
	[Export] private Label _movesLabel;
	[Export] private Label _bestLabel;

	private int _movesMade = 0;

	public override void _Ready()
	{
		SetUi();
	}

	private void SetUi()
	{
		_movesMade = 0;
		_movesLabel.Text = "0";
		_levelLabel.Text = GameManager.SelectedLevel;
	}

	public void IncrementMoves()
	{
		_movesMade++;
		_movesLabel.Text = _movesMade.ToString();
	}
}
