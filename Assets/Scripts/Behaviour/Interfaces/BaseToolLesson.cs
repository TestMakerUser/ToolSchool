﻿using SmartTek.ToolSchool.Components;
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

        public virtual void Dispose()
        {
            DestroyTools();
        }

        public virtual void LaunchLesson(LessonContext context)
        {
            InstantiateTools();
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
    }
}