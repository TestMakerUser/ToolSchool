using SmartTek.ToolSchool.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour
{
    public class LobbyController : MonoBehaviour
    {
        private void Start()
        {
            
        }

        private void OnGUI()
        {
            if(GUILayout.Button("Load workshop"))
            {
                ServicesReferences.SceneService.LoadScene(Services.SceneService.SceneType.Workshop, null);
            }
        }
    }
}