using Godot;
using System;

public partial class Astroid : CharacterBody2D
{
	private float _speed = (float)GD.RandRange(80f, 150f);
	private Playership _playership;
	private Sprite2D _sprite;
	private AstroidPool _pool;
	private Area2D _killzone;
	private bool _isMoving = false;
	public override void _Ready()
	{
		_sprite = this.GetNode<Sprite2D>("Sprite2D");
		_playership = GetTree().Root.GetNode("World").GetNode<Playership>("Playership");
		_pool = GetTree().Root.GetNode("World").GetNode<AstroidPool>("Astroidpool");
		_killzone = GetTree().Root.GetNode("World").GetNode<Area2D>("KillZone");
		_killzone.BodyEntered += Kill;
		//_isMoving = true;
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

			for(int i = 0; i < GetSlideCollisionCount(); i++)
			{
				var collision = GetSlideCollision(i);
				GD.Print(((Node)collision.GetCollider()).Name);
				if(((Node)collision.GetCollider()).Name == "Playership")
				{
					_playership.DamagePlayer();
					ReturnToPool();
					break;
				}
				else if(((Node)collision.GetCollider()).Name == "KillZone")
				{
					ReturnToPool();
				}
			}
		}
	}

	private void ReturnToPool()
	{
		_pool._activeAstroids--;
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
		this.Position = _pool.Position;
	}

	public bool GetIsMoving()
	{
		return _isMoving;
	}

	private void Kill(Node2D node)
	{
		if(node == this)
		{
			ReturnToPool();
			GD.Print("asdasdasdasd");
		}
	}
}
