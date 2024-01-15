using Godot;
using Godot.NativeInterop;
using System;

public partial class main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private int _score;

	public void game_over()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();

		GetNode<hud>("HUD").ShowHideMobileControls(false);

		GetNode<AudioStreamPlayer>("Death").Play();

		hud h = GetNode<hud>("HUD");
		h.ShowGameOver();
	}

	public void NewGame()
	{
		GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
		_score = 0;

		var player = GetNode<player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		GetNode<Timer>("StartTimer").Start();

		hud h = GetNode<hud>("HUD");
		h.UpdateScore(0);
		h.ShowMessage("Get Ready!");
	}

	public void MobTimerTimeout()
	{
		mob mob = MobScene.Instantiate<mob>();

		// Choose a random location on Path2D.
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		mob.Position = mobSpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);
	}

	public void ScoreTimerTimeout()
	{
		_score++;
		hud h = GetNode<hud>("HUD");
		h.UpdateScore(_score);
	}

	public void StartTimerTimeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AudioStreamPlayer>("Music").Play();

		GetNode<hud>("HUD").ShowHideMobileControls(false, true);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
