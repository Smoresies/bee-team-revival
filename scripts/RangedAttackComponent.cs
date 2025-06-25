using BeeTeamRevival.scripts;
using Godot;
using System;
namespace BeeTeamRevival.scripts
{
    public partial class RangedAttackComponent : AttackComponent
    {
        [Export]
        private PackedScene _projectile;
        [Export]
        private float _projectileSpeed = 10f;
		[Export]
		private Vector2 _projectileSpawnOffset = new Vector2(5, 0);
        protected override void DoAttack(Character character)
        {
            Projectile projectile = (Projectile)_projectile.Instantiate();
            projectile.Position = character.GlobalPosition + _projectileSpawnOffset;
            projectile.InitProjectile(_projectileSpeed, Vector2.Right, _attackData);
            character.AddChild(projectile);
            GD.Print("Do attack");
        }
    }
}