using Godot;
using System;

public partial class Test : Node
{
	public class TestInvalid : Exception
	{
		public TestInvalid(string message){}
	}
	public override void _Ready()
	{
		// Run Unit Test 1:
		GD.Print("Unit Test 1 / Is player isAlive bool set to false after three hits: " +
		CheckIfPlayerIsAliveBoolIsFalseAfterThreeHits(GetTree().Root.GetNode("World").GetNode<Playership>("Playership"))
		);

		// Run Unit Test 2:
		// Spawn Test Astroid
		Astroid astroid = (Astroid)ResourceLoader.Load<PackedScene>("res://Astroids/Astroid_Normal.tscn").Instantiate();
		AddChild(astroid);

		// Test Result
		GD.Print("Unit Test 2 / Is Astroid getting destroy after hits corresponding to its health: " +
		AstroidGetsDestroyAfterCorrespondingHits(astroid, GetTree().Root.GetNode("World").GetNode<Playership>("Playership").GetNode<BulletStats>("BulletSpawn"))
		);

		// Run Unit Test 3:
		GD.Print("Unit Test 3 / Game Over Menu gets shown when player is dead: " +
		GameOverMenuGetsShownWhenPlayerDies(GetTree().Root.GetNode("World").GetNode<Playership>("Playership"), GetTree().Root.GetNode("World").GetNode<Camera2D>("Camera2D"))
		);
	}

	// Unit Test 1
	public static bool CheckIfPlayerIsAliveBoolIsFalseAfterThreeHits(Playership player)
	{
		player.DamagePlayer();
		player.DamagePlayer();
		player.DamagePlayer();
		
		return !player.isAlive;
	}

	// Unit Test 2
	public static bool AstroidGetsDestroyAfterCorrespondingHits(Astroid astroid, BulletStats bulletStats)
	{
		int astroidHealth = astroid.GetHealth(); 
		for(int i = 0; i < astroidHealth; i++)
		{
			astroid.TakeDamage(bulletStats.GetDamage());
		}

		return astroid.GetHealth() <= 0 && !astroid.GetIsMoving();
	}

	// Unit Test 3

	public static bool GameOverMenuGetsShownWhenPlayerDies(Playership player, Camera2D camera2D)
	{
		if(player.isAlive == false)
		{
			return camera2D.GetMenu().Text == "Game Over" && camera2D.GetMenu().Visible == true;
		}
		else
		{
			throw new TestInvalid("Test invalid, Player not dead");
		}
	}
}
