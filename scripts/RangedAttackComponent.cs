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

        protected override void DoAttack(Character character)
        {
            Projectile projectile = (Projectile)_projectile.Instantiate();
            projectile.Position = character.GlobalPosition;
            projectile.InitProjectile(_projectileSpeed, Vector2.Right, _attackData);
            character.AddChild(projectile);
            GD.Print("Do attack");
        }
    }
}