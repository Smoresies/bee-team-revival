using Godot;
using System;

namespace BeeTeamRevival.scripts
{
	public partial class Character : Node, IStatusable
	{
		[Export]
		protected CharacterInput _characterInput;
		[Export]
		protected MovementController _movementController;
		[Export]
		protected HealthAndStatus _healthAndStatus;
		[Export]
		protected Attack _attack;

		public HealthAndStatus GetHealthAndStatus()
		{
			return _healthAndStatus;
		}

		public override void _Ready()
		{
			_characterInput.HorizontalMovementAction += _movementController.MoveHorizontally;
			_characterInput.JumpAction += _movementController.Jump;
			_characterInput.DashAction += _movementController.Dash;
			_characterInput.AttackAction += _attack.TryAttack;
			_healthAndStatus.OnDeath += OnDeath;
		}

		private void OnDeath()
		{
			QueueFree();
		}

	}
}
