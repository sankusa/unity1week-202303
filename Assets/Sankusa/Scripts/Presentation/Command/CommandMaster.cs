using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SankusaLib.SingletonLib;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(CommandMaster), menuName = nameof(unity1week202303) + "/" + nameof(CommandMaster))]
    public class CommandMaster : SingletonScriptableObject<CommandMaster>, ICommandMaster
    {
        [SerializeField] private List<Command> commands;
        public IReadOnlyList<Command> Commands => commands;

        public Command FindByCommandId(string commandId)
        {
            return commands.Find(x => x.CommandId == commandId);
        }

        public int FindIndex(string commandId)
        {
            return commands.FindIndex(x => x.CommandId == commandId);
        }
    }
}