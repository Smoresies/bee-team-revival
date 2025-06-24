using Godot;
using System;

namespace BeeTeamRevival.scripts
{
	public partial class MovementController : Resource
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

		public void OnPhysicsProcess(Character character, double delta)
		{
			if (character.IsOnFloor())
			{
				_currentExtraJump = 0;
				_canDash = !_dashing;
			}
			else
			{
				character.Velocity += character.GetGravity() * (float)delta;
			}
			character.MoveAndSlide();
		}

		public void Jump(Character character)
		{
			if (character.IsOnFloor())
			{
				character.Velocity = new Vector2(character.Velocity.X, _jumpVelocity);
			}
			else if (_currentExtraJump < _numberOfExtraJumpsAllowed)
			{
				character.Velocity = new Vector2(character.Velocity.X, _jumpVelocity);
				_currentExtraJump++;
			}
		}

		public void MoveHorizontally(Character character, float direction, double delta)
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
				if (Math.Abs(character.Velocity.X) > _maxSpeed)
				{
					character.Velocity = new Vector2(_maxSpeed * (int)_currentDirection / 2f, character.Velocity.Y);
				}
				else if (Math.Abs(direction) < .00001f)
				{
					character.Velocity = new Vector2(Mathf.Lerp(character.Velocity.X, 0f, _decelerationTime), character.Velocity.Y);
				}
				else
				{
					character.Velocity = new Vector2(Mathf.MoveToward(character.Velocity.X, direction * _maxSpeed, _acceleration * (float)delta), character.Velocity.Y);
				}
			}
		}

		public void Dash(Character character)
		{
			if (!_dashing && _canDash)
			{
				_dashing = true;
				_canDash = false;
				character.Velocity = new Vector2(_dashPower * (int)_currentDirection, 0);
				_dashTimer = new();
				_dashTimer.WaitTime = _dashTime;
				_dashTimer.Autostart = true;
				_dashTimer.Timeout += ResetDash;
				character.AddChild(_dashTimer);
			}
		}

		public void ResetDash()
		{
			_dashing = false;
			_dashTimer.QueueFree();
		}
    }
}
