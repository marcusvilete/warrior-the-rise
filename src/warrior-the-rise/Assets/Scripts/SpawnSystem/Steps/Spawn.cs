using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Spawn : ISpawnStep
{
    private SpawnSystem system;

    public Spawn(SpawnSystem system)
    {
        this.system = system;
    }

    public void Run(string args)
    {
        Debug.Log($"[Spawn] [{args}]");

        string[] argArr = args.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

        int argInt = 0;
        for (int i = 0; i < argArr.Length; i++)
        {

            if (int.TryParse(argArr[i], out argInt))
            {
                switch (argInt)
                {
                    case 1: // projectile
                        system.Spawn(system.selectedProjectilePrefab, i);
                        break;
                    case 2: // collectible01?
                        system.Spawn(system.healthCollectible, i);
                        break;
                    case 3: // collectible02?
                        system.Spawn(system.shieldCollectible, i);
                        break;
                    case 4: // collectible03?
                        break;
                    //and so on?
                    default: //??
                        break;
                }
            }
        }
    }
}

