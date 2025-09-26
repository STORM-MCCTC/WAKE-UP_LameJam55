using Godot;
using System;

public class Start : Button
{
	public override void _Ready()
	{
		// connect the pressed signal to your method
		Connect("pressed", this, nameof(OnStartButtonPressed));
	}

	private void OnStartButtonPressed()
	{
		GD.Print("Start button pressed — loading Level1...");
		GetTree().ChangeScene("res://level1.tscn");
	}
}
