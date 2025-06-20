using Godot;

namespace BeeTeamRevival.scripts
{
	/// <summary>
	/// Provides input for a player character.
	/// </summary>
	public partial class PlayerInputController : CharacterInput
	{
		private StringName _jumpName = "jump";
		private StringName _dashName = "dash";
		private StringName _attackName = "shoot";
		private StringName _leftName = "left";
		private StringName _rightName = "right";

		/// <summary>
		/// On input attempts to fire actions that need to be invoked as soon as they happen.
		/// </summary>
		/// <param name="event">The input event that happened</param>
		public override void _Input(InputEvent @event)
		{
			if (@event.IsActionPressed(_jumpName))
			{
				JumpAction?.Invoke();
				// GD.Print("Jumped");
			}
			if (@event.IsActionPressed(_dashName))
			{
				DashAction?.Invoke();
				// GD.Print("Dashed");
			}
			if (@event.IsActionPressed(_attackName))
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
			float moveVelocity = Input.GetAxis(_leftName, _rightName);
			HorizontalMovementAction?.Invoke(moveVelocity, delta);
			// GD.Print("Horizontal Movement: " + moveVelocity + "  Delta:" + delta);
		}
	}
}