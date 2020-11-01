using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SetSpeed : ISpawnStep
{
    private SpawnSystem system;

    public SetSpeed(SpawnSystem system)
    {
        this.system = system;
    }

    public void Run(string args)
    {
        float argf;
        if (float.TryParse(args, out argf))
        {
            system.speed = argf;
            system.currentCountdown = system.TimeBetweenSteps;
        }
    }

}
