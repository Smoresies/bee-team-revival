using Godot;
using System;
namespace BeeTeamRevival.scripts
{
    public interface IStatusable
    {
        public HealthAndStatus GetHealthAndStatus();
    }
}