using Godot;

public partial class SignalManager : Node
{
	public static SignalManager Instance {get; private set;}

	[Signal] public delegate void IncrementMovesEventHandler();
	[Signal] public delegate void GameOverEventHandler();

	public override void _EnterTree()
	{
		Instance = this;
	}

	public static void EmitIncrementMoves()
	{
		Instance.EmitSignal(SignalName.IncrementMoves);
	}

	public static void EmitGameOver()
	{
		Instance.EmitSignal(SignalName.GameOver);
	}
}
