using Godot;
using System;

public class LevelEndZone : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	   GD.Print("LevelEndZone: _Ready");

		// ensure this Area2D is monitoring (usually true by default, but check)
		Monitoring = true;
		Connect("body_entered", this, nameof(OnBodyEntered));
	}
private void OnBodyEntered(Node body)
	{
			GD.Print($"LevelEndZone: body_entered -> name:{body.Name} type:{body.GetType()}");

	if (body.IsInGroup("player"))
	{
		GD.Print("LevelEndZone: body is in group 'player'");
		var playerNode2d = body as Node2D;
		if (playerNode2d != null)
		{
			GD.Print("LevelEndZone — loading Level2...");
			GetTree().ChangeScene("res://level2.tscn");
		}
		return;
	}

	GD.Print("LevelEndZone: Ignored non-player body (not in 'player' group).");
	}
}
