using Godot;

namespace Scripts
{
	/// <summary>
	/// Provides input for a player character.
	/// </summary>
	public partial class PlayerInputController : CharacterInput
	{
		[Export]
		private string JUMP_NAME = "jump";
		[Export]
		private string DASH_NAME = "dash";
		[Export]
		private string ATTACK_NAME = "shoot";
		[Export]
		private string LEFT_NAME = "left";
		[Export]
		private string RIGHT_NAME = "right";

		/// <summary>
		/// On input attempts to fire actions that need to be invoked as soon as they happen.
		/// </summary>
		/// <param name="event">The input event that happened</param>
		public override void _Input(InputEvent @event)
		{
			if (@event.IsActionPressed(JUMP_NAME))
			{
				JumpAction?.Invoke();
				GD.Print("Jumped");
			}
			if (@event.IsActionPressed(DASH_NAME))
			{
				DashAction?.Invoke();
				GD.Print("Dashed");
			}
			if (@event.IsActionPressed(ATTACK_NAME))
			{
				AttackAction?.Invoke();
				GD.Print("Attacked");
			}
		}

		/// <summary>
		/// Attempts to fire actions that only need to occur during the physics update.
		/// </summary>
		/// <param name="delta">Change in time since the last call</param>
		public override void _PhysicsProcess(double delta)
		{
			float moveVelocity = Input.GetAxis(LEFT_NAME, RIGHT_NAME);
			HorizontalMovementAction?.Invoke(moveVelocity, delta);
			GD.Print("Horizontal Movement: " + moveVelocity + "  Delta:" + delta);
		}
	}
}