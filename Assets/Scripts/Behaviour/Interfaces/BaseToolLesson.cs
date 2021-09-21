using SmartTek.ToolSchool.Components;
using SmartTek.ToolSchool.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour.Interfaces
{
    public abstract class BaseToolLesson : MonoBehaviour, IToolLesson
    {
        public virtual IReadOnlyList<BaseTool> ToolsPrefabs => throw new System.NotImplementedException();

        public virtual string Name => throw new System.NotImplementedException();

        public virtual string Description => throw new System.NotImplementedException();

        protected IReadOnlyList<BaseTool> _instantiatedTools;

        public bool IsLaunching { get; protected set; }

#if UNITY_EDITOR
        private void OnGUI()
        {
            if(!IsLaunching)
            {
                return;
            }
            
            if(GUILayout.Button("Finish and return to lobby"))
            {
                ServicesReferences.LessonsService.FinishAndReturnToLobby();
            }
        }
#endif

        public virtual void Dispose()
        {
            DestroyTools();
            IsLaunching = false;
        }

        public virtual void LaunchLesson(LessonContext context)
        {
            InstantiateTools();
            IsLaunching = true;
        }

        protected virtual void InstantiateTools()
        {
            _instantiatedTools = ToolsPrefabs.Select(t => Instantiate(t)).ToList();
        }

        protected virtual void DestroyTools()
        {
            if(_instantiatedTools == null)
            {
                return;
            }

            foreach(var t in _instantiatedTools)
            {
                Destroy(t.gameObject);
            }

            _instantiatedTools = null;
        }

        protected virtual T GetToolInstance<T>() where T : BaseTool
        {
            return _instantiatedTools.First(t => { Debug.Log(t.name); return t is T; }) as T;
        }
    }
}