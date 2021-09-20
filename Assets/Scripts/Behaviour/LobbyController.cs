using SmartTek.ToolSchool.Helpers;
using SmartTek.ToolSchool.Services.Interfaces;
using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour
{
    public class LobbyController : MonoBehaviour
    {
        private ILessonsService lessonsService;

        private void Start()
        {
            lessonsService = ServicesReferences.LessonsService;
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            foreach(var lesson in lessonsService.LessonsInstances)
            {
                if(GUILayout.Button(lesson.Name))
                {
                    lessonsService.Launch(lesson);
                }
            }    
        }
#endif
    }
}