using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SankusaLib.SceneManagementLib;

public class InGameArg : ISceneArg
{
    private readonly int stageIndex;
    public int StageIndex => stageIndex;

    public InGameArg(int stageIndex)
    {
        this.stageIndex = stageIndex;
    }
}
