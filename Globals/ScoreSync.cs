using Godot;

public partial class ScoreSync : Node
{
	public static ScoreSync Instance {get; private set;}

	public override void _EnterTree()
	{
		Instance = this;
	}
}
