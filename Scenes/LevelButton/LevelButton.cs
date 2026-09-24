using System;
using Godot;

public partial class LevelButton : NinePatchRect
{
	[Export] private Label _label;
	
	private string _levelNumber;

	public override void _Ready()
	{
		_label.Text = _levelNumber;
		SubscribeToSignals();
	}

	private void SubscribeToSignals()
	{
		GuiInput += OnGuiInput;
	}

  private void OnGuiInput(InputEvent @event)
  {
    if(@event.IsActionPressed("click"))
		{
			GameManager.LoadLevel(_levelNumber);
		}
  }

  public void Setup(string levelNumber)
	{
		_levelNumber = levelNumber;
	}
}
