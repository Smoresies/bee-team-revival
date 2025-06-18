using Godot;
using System;

namespace BeeTeamRevival.scripts
{
	public partial class Character : Node
	{
		[Export]
		private CharacterInput _characterInput;
		[Export]
		private MovementController _movementController;

		public override void _Ready()
		{
			_characterInput.HorizontalMovementAction += _movementController.MoveHorizontally;
			_characterInput.JumpAction += _movementController.Jump;
			_characterInput.DashAction += _movementController.Dash;
        }

	}
}
