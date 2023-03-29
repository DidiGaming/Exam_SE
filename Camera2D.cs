using Godot;
using System;

public partial class Camera2D : Godot.Camera2D
{
	private Label _label;
	private Button _restartButton;
	private Button _exitButton;
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		_restartButton = _label.GetNode<Button>("RestartButton");
		_restartButton.Pressed += OnRestartButtonPressed;
		_exitButton = _label.GetNode<Button>("ExitButton");
		_exitButton.Pressed += OnExitButtonPressed;
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
}
