using Godot;
using System;

public partial class BehaviourTestFirerate : Node
{
	private Playership _playership;
	private float playerFirerate = 10f;
	public override void _Ready()
	{
		if(GetTree().Root.GetNode("World").FindChild("TestRunnerFirerate") != null)
		{
			_playership = GetTree().Root.GetNode("World").GetNode<Playership>("Playership");
			_playership.SetBulletSpawnDelay(playerFirerate);
			_playership.SetShooting(true);
			GD.Print("Test Script not finised / Only the automatic shooting is coded / Pressing Space cancels the autofire");
		}
	}

	public override void _Process(double delta)
	{
		// loop test per level reload with different firerates
	}
}
