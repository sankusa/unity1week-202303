using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sankusa.unity1week202303.Domain
{
    public interface ICommandMaster
    {
        int FindIndex(string commandId);
    }
}