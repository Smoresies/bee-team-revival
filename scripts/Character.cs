using Godot;
using System;

namespace BeeTeamRevival.scripts
{
	public partial class Character : CharacterBody2D, IStatusable
	{
		[Export]
		protected CharacterInput _characterInput;
		[Export]
		protected MovementController _movementController;
		[Export]
		protected HealthAndStatus _healthAndStatus;
		[Export]
		protected AttackComponent _attackComponent;

		public HealthAndStatus GetHealthAndStatus()
		{
			return _healthAndStatus;
		}

		public override void _Ready()
		{
			_characterInput.OnHorizontalMovement += _movementController.MoveHorizontally;
			_characterInput.OnJump += _movementController.Jump;
			_characterInput.OnDash += _movementController.Dash;
			_characterInput.OnAttack += _attackComponent.TryAttack;
			_healthAndStatus.Ready();
			_attackComponent.Ready(this);
			_healthAndStatus.OnDeath += OnDeath;
		}  
        public override void _Input(InputEvent @event)
        {
			_characterInput.OnInput(this, @event);
        }

		public override void _PhysicsProcess(double delta)
		{
			_characterInput.OnPhysicsProcess(this, delta);
			_movementController.OnPhysicsProcess(this, delta);
        }

		private void OnDeath()
		{
			QueueFree();
		}

	}
}
