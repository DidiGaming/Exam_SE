using Godot;
using System;

public partial class Camera2D : Godot.Camera2D
{
	private Label _label;
	private Label _healthDisplay;
	private Label _countdown;
	private Timer _timer;
	private Button _restartButton;
	private Button _exitButton;
	private Playership _playership;
	private int _timeleft = 60; 
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		_healthDisplay = GetNode<Label>("PlayerHealthInterface");
		_countdown = GetNode<Label>("Countdown");
		_countdown.Text = "Time Left: " + _timeleft;
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += ReduceTimeLeft;
		_restartButton = _label.GetNode<Button>("RestartButton");
		_restartButton.Pressed += OnRestartButtonPressed;
		_exitButton = _label.GetNode<Button>("ExitButton");
		_exitButton.Pressed += OnExitButtonPressed;
		_playership = GetTree().Root.GetNode("World").GetNode<Playership>("Playership");
		_playership.healthChanged += UpdateHealthDisplayed;
		_healthDisplay.Text = "Health: " + _playership.GetHealth();
	}

	public void ShowGameOverText()
	{
		_label.Visible = true;
		_restartButton.Disabled = false;
		_exitButton.Disabled = false;
	}

	void OnRestartButtonPressed()
	{
		GetTree().ReloadCurrentScene();
	}

	void OnExitButtonPressed()
	{
		GetTree().Quit();
	}

	private void UpdateHealthDisplayed(int newHealth)
	{
		_healthDisplay.Text = "Health: " + newHealth;
	}

	public void ReduceTimeLeft()
	{
		_timeleft -= 1;
		_countdown.Text = "Time Left: " + _timeleft;

		if(_timeleft <= 0)
		{
			_timer.Timeout -= ReduceTimeLeft;
			_countdown.Text = "Time Left: " + 0;

			_label.Text = "Victory";
			_label.LabelSettings.FontColor = new Color(0, 0, 255);
			_label.Visible = true;
			_restartButton.Disabled = false;
			_exitButton.Disabled = false;
			GetTree().Root.GetNode("World").GetNode<AstroidPool>("Astroidpool").QueueFree();
			_playership.FlyAway();
		}		
	}
}
