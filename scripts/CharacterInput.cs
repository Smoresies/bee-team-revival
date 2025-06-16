using Godot;
using System;

namespace BeeTeamRevival.scripts
{
	public abstract partial class CharacterInput : Node
	{

		public Action<float, double> HorizontalMovementAction;
		public Action<float, double> VerticalMovementAction;
		public Action JumpAction;
		public Action AttackAction;
		public Action DashAction;
	}
}
