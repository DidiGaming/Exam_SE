using Godot;
using System;

public partial class Astroid : CharacterBody2D
{
	private float _speed = (float)GD.RandRange(80f, 120f);
	private Playership _playership;
	private Sprite2D _sprite;
	private bool _isMoving = false;
	public override void _Ready()
	{
		_sprite = this.GetNode<Sprite2D>("Sprite2D");
		_playership = GetTree().Root.GetNode("World").GetNode<Playership>("Playership");
		_isMoving = true;
	}

	public override void _Process(double delta)
	{
	}
	public override void _PhysicsProcess(double delta)
	{
		if(_isMoving)
		{
			Vector2 velocity = Velocity;
			
			velocity.X = -_speed;

			Velocity = velocity;
			MoveAndSlide();

			GD.Print(GetSlideCollisionCount());
			for(int i = 0; i < GetSlideCollisionCount(); i++)
			{
				var collision = GetSlideCollision(i);
				if(((Node)collision.GetCollider()).Name == "Playership")
				{
					_playership.DamagePlayer();
					ReturnToPool();
					break;
				}
			}
		}
	}

	private void ReturnToPool()
	{
		SetIsMoving(false);
		SetVisibilty(false);
		ReturnToPoolLocation();
	}

	public void SetIsMoving(bool _bool)
	{
		_isMoving = _bool;
	}
	public void SetVisibilty(bool _bool)
	{
		_sprite.Visible = _bool;
	}
	private void ReturnToPoolLocation()
	{
		this.Position = new Vector2 (-100, -100);
	}
}
