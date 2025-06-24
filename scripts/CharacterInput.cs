using Godot;
using System;

namespace BeeTeamRevival.scripts
{
	public abstract partial class CharacterInput : Resource
	{
		[Signal]
		public delegate void OnHorizontalMovementEventHandler(Character character, float velocity, double delta);
		[Signal]
		public delegate void OnVerticalMovementEventHandler(Character character, float velocity, double delta);
		[Signal]
		public delegate void OnJumpEventHandler(Character character);
		[Signal]
		public delegate void OnAttackEventHandler(Character character);
		[Signal]
		public delegate void OnDashEventHandler(Character character);

		public abstract void OnInput(Character character, InputEvent @event);
		public abstract void OnPhysicsProcess(Character character, double delta);

	}
}
