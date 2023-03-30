using Godot;
using System;

public partial class BulletKillZone : Area2D
{
	public override void _Ready()
	{
		this.BodyEntered += OnBody2DEntered;
	}
	public void OnBody2DEntered(Node2D body)
	{
		body.QueueFree();
	}
}
