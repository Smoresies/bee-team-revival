using Godot;

namespace BeeTeamRevival.scripts
{
	public partial class RangedAttack : Attack
	{
		[Export]
		private PackedScene _projectile;
		[Export]
		private float _projectileSpeed = 10f;

		protected override void DoAttack()
		{
			Projectile projectile = (Projectile)_projectile.Instantiate();
			projectile.Position = _attackStartLocation.GlobalPosition;
			projectile.InitProjectile(_projectileSpeed, Vector2.Right, _attackData);
			AddChild(projectile);
			GD.Print("Do attack");
		}
	}
}
 