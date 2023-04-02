using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;
using Sankusa.unity1week202303.Domain;
using SankusaLib.SceneManagementLib;
using UnityEngine.SceneManagement;
using SankusaLib;

namespace Sankusa.unity1week202303.Presentation
{
    public class InGameLoop : IInitializable, IDisposable
    {
        private readonly SceneArgStore sceneArgStore;
        private readonly Faith faith;
        private readonly GameTimer gameTimer;
        private readonly FinishFlag finishFlag;
        private readonly HumanManager humanManager;
        private readonly FinishPanel finishPanel;
        private readonly DiContainer diContainer;

        private readonly CancellationTokenSource source = new CancellationTokenSource();
        
        [Inject]
        public InGameLoop(SceneArgStore sceneArgStore,
            Faith faith,
            GameTimer gameTimer,
            FinishFlag finishFlag,
            HumanManager humanManager,
            FinishPanel finishPanel,
            DiContainer diContainer)
        {
            this.sceneArgStore = sceneArgStore;
            this.faith = faith;
            this.gameTimer = gameTimer;
            this.finishFlag = finishFlag;
            this.humanManager = humanManager;
            this.finishPanel = finishPanel;
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            StartAsync(source.Token).Forget();
        }

        private async UniTask StartAsync(CancellationToken token)
        {
            await UniTask.Yield(source.Token);

            // シーン引数取り出し
            InGameArg inGameArg = sceneArgStore.Pop<InGameArg>();
            StageDataContainer stageDataContainer = null;
            if(inGameArg == null)
            {
                stageDataContainer = StageMaster.Instance.StageDataContainers[0];
            }
            else 
            {
                stageDataContainer = StageMaster.Instance.StageDataContainers[inGameArg.StageIndex];
            }

            // ステージロード
            ResourceRequest request = Resources.LoadAsync<GameObject>(stageDataContainer.StagePrefabPath);
            await request.WithCancellation(token);
            
            GameObject stagePrefab = request.asset as GameObject;
            diContainer.InstantiatePrefab(stagePrefab);

            faith.Initialize(stageDataContainer.TargetFaith);
            gameTimer.SetTimeLimit(stageDataContainer.TimeLimit);

            // スタート
            bool success = false;
            gameTimer.Start();
            while(true)
            {
                await UniTask.Yield(cancellationToken: token);

                float faithProduce = humanManager.HumanCores.Where(x => x.Human.Finished).Select(x => x.Human.FaithProduce * Time.deltaTime).Sum();
                faith.AddValue(faithProduce);

                if(finishFlag.Value)
                {
                    success = true;
                    break;
                }
            }

            gameTimer.Stop();

            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(faith.Value);

            finishPanel.Show();
        }

        public void Dispose()
        {
            source.Cancel();
        }
    }
}