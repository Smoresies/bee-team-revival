using Godot;
using System;
namespace BeeTeamRevival.scripts
{
    public abstract partial class Attack : Node
    {
        [Export]
        protected AttackData _attackData;
        [Export]
        protected float _attackCooldown = .25f;
        [Export]
        protected Marker2D _attackStartLocation;
        protected Timer _cooldownTimer;
        protected bool _canAttack = true;

        public override void _Ready()
        {
            _cooldownTimer = new Timer();
            _cooldownTimer.Timeout += ResetAttack;
            _cooldownTimer.WaitTime = _attackCooldown;
            _cooldownTimer.Autostart = false;
            _cooldownTimer.OneShot = true;
            AddChild(_cooldownTimer);
        }

        public void TryAttack()
        {
            if (_canAttack)
            {
                _canAttack = false;
                _cooldownTimer.Start();
                GD.Print("Attack");
                DoAttack();
            }
        }
        protected abstract void DoAttack();

        private void ResetAttack()
        {
            _canAttack = true;

            GD.Print("Reset Attack");
        }
    }
}