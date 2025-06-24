using Godot;
using System;
namespace BeeTeamRevival.scripts
{
    public abstract partial class AttackComponent : Resource 
    {
        [Export]
        protected AttackData _attackData;
        [Export]
        protected float _attackCooldown = .25f;
        protected Timer _cooldownTimer;
        protected bool _canAttack = true;

        public void Ready(Character character)
        {
            _cooldownTimer = new Timer();
            _cooldownTimer.Timeout += ResetAttack;
            _cooldownTimer.WaitTime = _attackCooldown;
            _cooldownTimer.Autostart = false;
            _cooldownTimer.OneShot = true;
            character.AddChild(_cooldownTimer);
        }

        public void TryAttack(Character character)
        {
            if (_canAttack)
            {
                _canAttack = false;
                _cooldownTimer.Start();
                GD.Print("Attack");
                DoAttack(character);
            }
        }
        protected abstract void DoAttack(Character character);

        private void ResetAttack()
        {
            _canAttack = true;

            GD.Print("Reset Attack");
        }
    }
}