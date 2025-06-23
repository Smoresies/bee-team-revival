using Godot;
using System;
namespace BeeTeamRevival.scripts
{
    public partial class HealthAndStatus : Resource
    {
        [Export]
        private int _maxHealth = 100;
        private int _currentHealth;
        [Signal]
        public delegate void OnDeathEventHandler();

        public void Ready()
        {
            OnDeath += Die;
        }
        public void TakeDamage(AttackData attackData)
        {
            _currentHealth -= attackData.Damage;
            if (_currentHealth <= 0)
            {
                EmitSignal(SignalName.OnDeath);
            }
        }
        private static void Die()
        {
            GD.Print("I am dead");
        }

    }
}