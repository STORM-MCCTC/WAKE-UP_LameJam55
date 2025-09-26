using Godot;

public class CharacterController : KinematicBody2D
{
	[Export] public int MoveSpeed = 350;
	[Export] public int JumpSpeed = 550;
	[Export] public int Gravity = 800;

	// Tweak these
	[Export] public float GroundAcceleration = 1500f;
	[Export] public float AirAcceleration = 600f;
	[Export] public float GroundDeceleration = 2000f;
	[Export] public float MinStopSpeed = 10f; // below this, snap to 0

	private Vector2 _velocity = Vector2.Zero;
	
	public override void _Ready()
	{
		AddToGroup("player");
		GD.Print("CharacterController: Added self to player group");
	}	

	public override void _PhysicsProcess(float delta)
	{
		// Gravity
		_velocity.y += Gravity * delta;

		// Input direction
		var direction = GetInputDirection();
		float target = direction * MoveSpeed;

		// Choose accel or decel depending on input and whether we're in the air
		bool onFloor = IsOnFloor();
		if (direction != 0)
		{
			float accel = onFloor ? GroundAcceleration : AirAcceleration;
			_velocity.x = Mathf.MoveToward(_velocity.x, target, accel * delta);
		}
		else
		{
			// Stronger braking on the ground
			float decel = onFloor ? GroundDeceleration : AirAcceleration;
			_velocity.x = Mathf.MoveToward(_velocity.x, 0, decel * delta);

			// Snap tiny values to zero so it doesn't feel floaty
			if (Mathf.Abs(_velocity.x) < MinStopSpeed)
				_velocity.x = 0;
		}

		// Jump
		if (onFloor && Input.IsActionJustPressed("jump"))
			_velocity.y = -JumpSpeed;

		// Move
		_velocity = MoveAndSlide(_velocity, Vector2.Up);
	}

	private float GetInputDirection()
	{
		float direction = 0;
		if (Input.IsActionPressed("move_right"))
			direction += 1;
		if (Input.IsActionPressed("move_left"))
			direction -= 1;
		return direction;
	}
	
	   public void Respawn(Vector2 spawnPos)
	{
		GlobalPosition = spawnPos;
		_velocity = Vector2.Zero;
		GD.Print($"Character Control: Player location {spawnPos}");
	}
}
