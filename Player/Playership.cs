using Godot;
using System;

public partial class Playership : CharacterBody2D
{
	private const float _SPEED = 300.0f;
	private const float _FIRERATE = 10;
	private PackedScene _bulletScene = ResourceLoader.Load<PackedScene>("res://Player/Bullet.tscn");
	private float _bulletSpawnDelay = 1/_FIRERATE;
	private double del_sum;
	double remainder;
	private int _health = 3;
	private bool _shooting = false;
	[Signal] public delegate void healthChangedEventHandler(int newHealth);
	private BulletStats _bulletSpawn;
	private int _bulletsToSpawn;

	public override void _Ready()
	{
		_bulletSpawn = GetNode<BulletStats>("BulletSpawn");
	}
	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("Shoot"))
		{
			_shooting = true;
		} 
		if(Input.IsActionJustReleased("Shoot"))
		{
			_shooting = false;
		} 
		if(_shooting)
		{
			del_sum += delta;
			if(del_sum > _bulletSpawnDelay)
			{
				remainder = del_sum % _bulletSpawnDelay;
				_bulletsToSpawn = Mathf.CeilToInt((del_sum - remainder) / _bulletSpawnDelay);
				FireBullet(_bulletsToSpawn);
				del_sum = remainder;
			}
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "MoveUp", "MoveDown");
		
		if (direction != Vector2.Zero)
		{
			velocity.Y = direction.Y * _SPEED;
		}
		else
		{
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, _SPEED);
		}

		Velocity = velocity;
		MoveAndSlide();
		this.Position = new Vector2(71, this.Position.Y);
	}

	public void DamagePlayer()
	{
		_health -= 1;

		EmitSignal(SignalName.healthChanged, _health);

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

	public int GetHealth()
	{
		return _health;
	}

	private void FireBullet(int bullet_num)
	{
		for (int i = 0; i < bullet_num; i++)
		{
			Bullet bullet = (Bullet)_bulletScene.Instantiate();
			GetTree().Root.GetNode("World").GetNode<Node2D>("Bullets").AddChild(bullet);
			bullet.SetStats(_bulletSpawn);
			Vector2 spawnPosition = new Vector2(this.Position.X + 40, this.Position.Y);
			bullet.Position = spawnPosition;
		}
	}
}
