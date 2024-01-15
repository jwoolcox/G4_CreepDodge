using Godot;

public partial class mob : RigidBody2D
{

	public void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		QueueFree();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();

		animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
