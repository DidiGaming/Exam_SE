using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	private const float _SPEED = 1000.0f;
	private BulletStats _stats;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.X = _SPEED;
		Velocity = velocity;
		MoveAndSlide();

		for(int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);

			if(((Node)collision.GetCollider()).IsInGroup("Astroid"))
			{
				collision.GetCollider().Call("TakeDamage", _stats.GetDamage());
				DestroyBullet();
				break;
			}
			// else if(((Node)collision.GetCollider()).Name == "KillZone2")
			// {
			// 	DestroyBullet();
			// 	break;
			// }
			//else{}
		}
	}
	
	public void SetStats(BulletStats stats)
	{
		_stats = stats;
	}
	private void DestroyBullet()
	{
		QueueFree();
	}
}
