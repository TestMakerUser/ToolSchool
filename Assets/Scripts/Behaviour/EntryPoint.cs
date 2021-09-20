using SmartTek.ToolSchool.Helpers;
using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour
{
    /// <summary>
    /// The main entrypoint of all behaviour.
    /// </summary>
    public class EntryPoint : MonoBehaviour
    {
        private void Start()
        {
            ServicesReferences.SceneService.LoadScene(Services.SceneService.SceneType.Lobby, null);
        }
    }
}