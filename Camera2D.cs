using Godot;
using System;

public partial class Camera2D : Godot.Camera2D
{
	private Label label;
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
	}

	public void ShowGameOverText()
	{
		label.Visible = true;
	}
}
