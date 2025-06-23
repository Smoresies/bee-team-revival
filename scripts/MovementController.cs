using Godot;
using System;

namespace BeeTeamRevival.scripts
{
	public partial class MovementController : CharacterBody2D, IStatusable
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
		private bool _canDash = true;
		private bool _dashing = false;
		private Timer _dashTimer;

		public override void _PhysicsProcess(double delta)
		{
			if (IsOnFloor())
			{
				_currentExtraJump = 0;
				_canDash = !_dashing;
			}
			else
			{
				Velocity += GetGravity() * (float)delta;
			}
			MoveAndSlide();
		}

		public void Jump()
		{
			if (IsOnFloor())
			{
				Velocity = new Vector2(Velocity.X, _jumpVelocity);
			}
			else if (_currentExtraJump < _numberOfExtraJumpsAllowed)
			{
				Velocity = new Vector2(Velocity.X, _jumpVelocity);
				_currentExtraJump++;
			}
		}

		public void MoveHorizontally(float direction, double delta)
		{
			if (!_dashing)
			{

				if (direction > 0)
				{
					_currentDirection = Direction.RIGHT;
				}
				else if (direction < 0)
				{
					_currentDirection = Direction.LEFT;
				}
				if (Math.Abs(Velocity.X) > _maxSpeed)
				{
					Velocity = new Vector2(_maxSpeed * (int)_currentDirection / 2f, Velocity.Y);
				}
				else if (Math.Abs(direction) < .00001f)
				{
					Velocity = new Vector2(Mathf.Lerp(Velocity.X, 0f, _decelerationTime), Velocity.Y);
				}
				else
				{
					Velocity = new Vector2(Mathf.MoveToward(Velocity.X, direction * _maxSpeed, _acceleration * (float)delta), Velocity.Y);
				}
			}
		}

		public void Dash()
		{
			if (!_dashing && _canDash)
			{
				_dashing = true;
				_canDash = false;
				Velocity = new Vector2(_dashPower * (int)_currentDirection, 0);
				_dashTimer = new();
				_dashTimer.WaitTime = _dashTime;
				_dashTimer.Autostart = true;
				_dashTimer.Timeout += ResetDash;
				AddChild(_dashTimer);
			}
		}

		public void ResetDash()
		{
			_dashing = false;
			_dashTimer.QueueFree();
		}

		public HealthAndStatus GetHealthAndStatus()
		{
			GD.Print("fuck");
			return null;
        }
    }
}
