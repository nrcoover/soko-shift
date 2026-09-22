using Godot;

public partial class SignalManager : Node
{
	public static SignalManager Instance {get; private set;}

	public override void _EnterTree()
	{
		Instance = this;
	}
}
