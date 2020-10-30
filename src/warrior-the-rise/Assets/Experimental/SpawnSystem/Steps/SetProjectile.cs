using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SetProjectile : ISpawnStep
{
    private SpawnSystem system;

    public SetProjectile(SpawnSystem system)
    {
        this.system = system;
    }

    public void Run(string args)
    {
        int argi;
        if (int.TryParse(args, out argi))
        {
            if (argi < system.projectilePrefabs.Length)
            {
                system.selectedProjectilePrefab = system.projectilePrefabs[argi];
            }
            
        }
    }
}

