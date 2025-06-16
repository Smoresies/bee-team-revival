using Godot;
using System;

namespace BeeTeamRevival.scripts
{
	public partial class MovementController : CharacterBody2D
	{
		[Export]
		private int _maxSpeed = 200;
		[Export]
		private int _jumpVelocity = -300;
		[Export]
		private int _acceleration = 1500;
		[Export]
		private int _dashPower = 2000;
		[Export(PropertyHint.Range, "0, 1, .05")]
		private float _dashTime = .1f;
		[Export(PropertyHint.Range, "0, 10, .1")]
		private float _decelerationTime = .1f;
		[Export]
		private int _numberOfExtraJumpsAllowed = 1;

		private int _currentExtraJump = 0;
		private Direction _currentDirection = Direction.RIGHT;

		public override void _PhysicsProcess(double delta)
		{
			if (!IsOnFloor())
			{
				this.Velocity += GetGravity() * (float) delta;
			}
			else
			{
				_currentExtraJump = 0;
			}
			GD.Print("Vel:" + this.Velocity);
			MoveAndSlide();
		}


		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (IsOnFloor())
			{
				_currentExtraJump = 0;
			}
		}

		public void Jump()
		{
			if (IsOnFloor())
			{
				Vector2 vector2 = this.Velocity;
				vector2.Y = _jumpVelocity;
				this.Velocity = vector2;
			}
			else if (_currentExtraJump < _numberOfExtraJumpsAllowed)
			{
				Vector2 vector2 = this.Velocity;
				vector2.Y = _jumpVelocity;
				this.Velocity = vector2;
				_currentExtraJump++;
			}
		}

		public void MoveHorizontally(float direction, double delta)
		{
			if (direction > 0)
			{
				_currentDirection = Direction.RIGHT;
			}
			else if (direction < 0)
			{
				_currentDirection = Direction.LEFT;
			}
			if (Math.Abs(this.Velocity.X) > _maxSpeed)
			{
				int velocityDirection = _currentDirection == Direction.RIGHT ? 1 : -1;
				Vector2 vector2 = this.Velocity;
				vector2.X = _maxSpeed * velocityDirection / 2f;
				this.Velocity = vector2;

			}
			else if (Math.Abs(direction) < .00001f)
			{
				Vector2 vector2 = this.Velocity;
				vector2.X = Mathf.Lerp(this.Velocity.X, 0f, _decelerationTime);
				this.Velocity = vector2;
			}
			else
			{
				Vector2 vector2 = this.Velocity;
				vector2.X = Mathf.MoveToward(this.Velocity.X, direction * _maxSpeed, _acceleration * (float) delta);
				this.Velocity = vector2;
			}
		}

	}
}
