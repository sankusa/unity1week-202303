using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Sankusa.unity1week202303.Domain;
using System.Linq;
using Zenject;

namespace Sankusa.unity1week202303.Presentation
{
    public class NPC_Mono : NPCBase
    {
        [Inject] private HumanManager humanManager;

        public override IReadOnlyList<string> TemporaryCommands
        {
            get
            {
                List<string> commands = new List<string>();
                commands.AddRange(temporaryCommands);
                if(talkCount > 4)
                {
                    commands.Add(CommandId.VillagerBig);
                }
                return commands;
            }
        }
        private int talkCount;

        public override async UniTask ReactionAsync(CommandArg commandArg)
        {
            if(commandArg.CommandId == CommandId.Greeting)
            {
                await humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・");
                if(commandArg.User.Human.FindParameter(HumanParameterId.Intelligence) == null) commandArg.User.Human.AddParameter(new HumanParameter(HumanParameterId.Intelligence, 0));
                commandArg.User.Human.FindParameter(HumanParameterId.Intelligence).AddBaseValue(100);
            }
            else if(commandArg.CommandId == CommandId.Punch)
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・").Forget();
                if(commandArg.User.Human.FindParameter(HumanParameterId.Intelligence) == null) commandArg.User.Human.AddParameter(new HumanParameter(HumanParameterId.Intelligence, 0));
                commandArg.User.Human.FindParameter(HumanParameterId.Intelligence).AddBaseValue(100);
            }
            else if(commandArg.CommandId == CommandId.StartBattle)
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・").Forget();
                if(commandArg.User.Human.FindParameter(HumanParameterId.Intelligence) == null) commandArg.User.Human.AddParameter(new HumanParameter(HumanParameterId.Intelligence, 0));
                commandArg.User.Human.FindParameter(HumanParameterId.Intelligence).AddBaseValue(100);
            }
            else if(commandArg.CommandId == CommandId.Inform)
            {
                await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・");
                if(commandArg.User.Human.FindParameter(HumanParameterId.Intelligence) == null) commandArg.User.Human.AddParameter(new HumanParameter(HumanParameterId.Intelligence, 0));
                commandArg.User.Human.FindParameter(HumanParameterId.Intelligence).AddBaseValue(100);
            }
            await UniTask.CompletedTask;
        }
    }
}