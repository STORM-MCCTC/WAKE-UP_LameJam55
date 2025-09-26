using Godot;
using System;

public class DeathZone : Area2D
{
	[Export] public NodePath PlayerSpawnPath;   // assign PlaySpawn (Position2D/Node2D) in inspector

	private Node2D _playSpawn;

	public override void _Ready()
	{
		GD.Print("DeathZone: _Ready");
		if (PlayerSpawnPath != null && PlayerSpawnPath != String.Empty)
		{
			try
			{
				_playSpawn = GetNode<Node2D>(PlayerSpawnPath);
				GD.Print($"DeathZone: PlaySpawn found at {_playSpawn.GlobalPosition}");
			}
			catch (Exception e)
			{
				GD.PrintErr("DeathZone: Failed to get PlaySpawn node: " + e.Message);
				_playSpawn = null;
			}
		}
		else
		{
			GD.PrintErr("DeathZone: PlayerSpawnPath not assigned in inspector!");
		}

		// ensure this Area2D is monitoring (usually true by default, but check)
		Monitoring = true;
		Connect("body_entered", this, nameof(OnBodyEntered));
	}

	private void OnBodyEntered(Node body)
	{
			GD.Print($"DeathZone: body_entered -> name:{body.Name} type:{body.GetType()}");

	if (_playSpawn == null)
	{
		GD.PrintErr("DeathZone: play spawn is null, aborting teleport.");
		return;
	}

	if (body.IsInGroup("player"))
	{
		GD.Print("DeathZone: body is in group 'player' - teleporting.");
		var playerNode2d = body as Node2D;
		if (playerNode2d != null)
		{
			// Prefer calling Respawn if available
			var playerScript = body as CharacterController;
			if (playerScript != null)
				playerScript.Respawn(_playSpawn.GlobalPosition);
			else
				playerNode2d.GlobalPosition = _playSpawn.GlobalPosition;
		}
		return;
	}

	GD.Print("DeathZone: Ignored non-player body (not in 'player' group).");
	}
}
