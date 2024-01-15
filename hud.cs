using Godot;

public partial class hud : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	public void ShowMessage(string text)
	{
		Label message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();
		GetNode<Timer>("MessageTimer").Start();
	}

	public void ButtonLeftDown()
	{
		Input.ActionPress("move_left");
	}
	public void ButtonLeftUp()
	{
		Input.ActionRelease("move_left");
	}

	public void ButtonRightDown()
	{
		Input.ActionPress("move_right");
	}

	public void ButtonRightUp()
	{
		Input.ActionRelease("move_right");
	}
	public void ButtonDownDown()
	{
		Input.ActionPress("move_down");
	}

	public void ButtonDownUp()
	{
		Input.ActionRelease("move_down");
	}

	public void ButtonUpUp()
	{
		Input.ActionRelease("move_up");
	}
	public void ButtonUpDown()
	{
		Input.ActionPress("move_up");
	}


	public void ShowHideMobileControls(bool Visible, bool Force = false)
	{
		string osname = OS.GetName();

		if (osname.ToLower().Contains("droid") || Force)
		{
			if (Visible)
			{
				//show touch controls on mobile
				GetNode<Button>("ButtonLeft").Show();
				GetNode<Button>("ButtonRight").Show();
				GetNode<Button>("ButtonDown").Show();
				GetNode<Button>("ButtonUp").Show();
			}
			else
			{
				//hide touch controls on mobile
				GetNode<Button>("ButtonLeft").Hide();
				GetNode<Button>("ButtonRight").Hide();
				GetNode<Button>("ButtonDown").Hide();
				GetNode<Button>("ButtonUp").Hide();
			}
		}
	}

	async public void ShowGameOver()
	{
		ShowMessage("Game Over");
		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, Timer.SignalName.Timeout);

		var message = GetNode<Label>("Message");
		message.Text = "Dodge the creeps!";
		message.Show();

		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		GetNode<Button>("StartButton").Show();
	}
	public void MessageTimerTimeout()
	{
		GetNode<Label>("Message").Hide();
	}

	public void StartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		ShowHideMobileControls(true);
		EmitSignal(SignalName.StartGame);
	}

	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}

}
