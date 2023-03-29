using Godot;
using System;

public partial class Playership : CharacterBody2D
{
	public const float Speed = 300.0f;
	private int _health = 300;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "MoveUp", "MoveDown");
		
		if (direction != Vector2.Zero)
		{
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		this.Position = new Vector2(71, this.Position.Y);

		//GD.Print(_health);
	}

	public void DamagePlayer()
	{
		_health -= 1;

		if(_health <= 0)
		{
			Death();
		}
	}
	
	private void Death()
	{
		GetNode<Sprite2D>("Sprite2D").Visible = false;
		Camera2D camera = GetTree().Root.GetNode("World").GetNode<Camera2D>("Camera2D");
		camera.ShowGameOverText();
	}
}
