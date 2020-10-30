using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;


public class LevelLoader
{
    private const string levelPath = "Assets/Experimental/SpawnSystem/Levels/";
    private string[] content;
    private int stepIndex;
    public LevelLoader()
    {
        //???
    }

    public void ReadContent(string level)
    {

        content = Resources.Load<TextAsset>($"Levels/{level}").text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        //content = File.ReadAllLines(levelPath + $"{level}.lvl");
        stepIndex = 0;
    }

    public bool GetNextStep(out string step, out string args)
    {
        if (stepIndex < content.Length)
        {
            var line = content[stepIndex].Split(new []{ "," }, StringSplitOptions.RemoveEmptyEntries);
            step = line[0];
            args = line[1];
            stepIndex++;
            return true;
        }
        
        step = null;
        args = null;
        return false;
    }
}


