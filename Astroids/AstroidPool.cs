using Godot;
using System;

public partial class AstroidPool : Node2D
{
	private Astroid[] _astroidPool = new Astroid[40];
	private PackedScene _packedScene;
	public int _activeAstroids = 0;
	private float _timer = 0;
	private float _spawnDelay;
	public override void _Ready()
	{
		for(int i = 0; i < _astroidPool.Length; i++)
		{
			Astroid astroid = (Astroid)ResourceLoader.Load<PackedScene>("res://Astroids/Astroid_Normal.tscn").Instantiate();
			AddChild(astroid);
			_astroidPool[i] = astroid;
		}
	}

	public override void _Process(double delta)
	{
		if(_activeAstroids < _astroidPool.Length - 5)
		{
			_spawnDelay = (float)GD.RandRange(0.5f, 1.5f);
			if(_timer >= _spawnDelay)
			{
				StartAstroid();
				_timer = 0;
			}
		}
		_timer += (float)delta;
	}
	
	private void StartAstroid()
	{
		int random = (int)GD.RandRange(0, 39);
		
		while(_astroidPool[random].GetIsMoving() == true)
		{
			random = (int)GD.RandRange(0, 39);
		}

		Vector2 startPos = new Vector2(0, (int)GD.RandRange(30, GetViewport().GetVisibleRect().Size.Y - 30));
		_astroidPool[random].ResetHealth();
		_astroidPool[random].Position = startPos;
		_astroidPool[random].SetIsMoving(true);
		_astroidPool[random].SetVisibilty(true);
		_activeAstroids++;
	}
}
