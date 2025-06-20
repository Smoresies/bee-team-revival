using Godot;
using System;
namespace BeeTeamRevival.scripts
{
	public partial class Projectile : Area2D
	{
		private float _speed;
		private Vector2 _direction;
		private AttackData _attackData;

	
        public override void _PhysicsProcess(double delta)
		{
			Position += _speed * _direction * (float)delta;
		}

		public void InitProjectile(float speed, Vector2 direction, AttackData attackData)
		{
			GD.Print("projectile init");
			_speed = speed;
			_direction = direction;
			_attackData = attackData;
			BodyEntered += Collide;
		}

		private void Collide(Node2D node2D) {
			GD.Print("Collide");
			QueueFree();
		}
	}
}