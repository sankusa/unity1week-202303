using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SankusaLib;

namespace Sankusa.unity1week202303.Domain
{
    public class IdClassGenerator
    {
        private const string AUTO_GENERATED_SCRIPTS_PATH = "Assets/Sankusa/Scripts/Domain/AutoGeneratedScripts";

        [MenuItem("WitchFruit/ParameterIdクラス更新")]
        private static void UpdateFruitParameterIdClass() {
            string scriptTemplate = @"
namespace Sankusa.unity1week202303.Domain
{
    public static class HumanParameterId
    {
#BODY#
    }
}
";
            string scriptBody = "";
            List<HumanParameterMaster> masters = AssetUtil.LoadAllAssets<HumanParameterMaster>();
            if(masters.Count != 1) {
                Debug.LogError("Master exist " + masters.Count);
            }
            foreach(HumanParameterData data in masters[0].ParameterDataList) {
                scriptBody += "        public const string " + data.ParameterId + " = \"" + data.ParameterId + "\";\r\n";
            }
            string script = scriptTemplate.Replace("#BODY#", scriptBody);

            string filePath = AUTO_GENERATED_SCRIPTS_PATH + "/HumanParameterId.cs";
            string assetPath = AssetDatabase.GenerateUniqueAssetPath(filePath);
            System.IO.File.WriteAllText(filePath, script);
            AssetDatabase.Refresh();
        }
    }
}