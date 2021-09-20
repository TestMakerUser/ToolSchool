using SmartTek.ToolSchool.Behaviour.Intefaces;
using SmartTek.ToolSchool.Components;
using System.Collections.Generic;
using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour
{
    public class PneumaticDrillLesson : MonoBehaviour, IToolLesson
    {
        public IReadOnlyList<BaseTool> ToolsPrefabs => throw new System.NotImplementedException();

        public string Name => "Pneumatic drill";

        public string Description => string.Empty;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void LaunchLesson(LessonContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}