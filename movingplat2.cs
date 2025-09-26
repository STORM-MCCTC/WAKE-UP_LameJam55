using Godot;
using System;

public class movingplat2 : StaticBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		var ap = GetNode("AnimationPlayer") as AnimationPlayer;
		ap.Play("moving plat2");
		GD.Print("Moving plat2: Animation Loaded");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
