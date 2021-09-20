using SmartTek.ToolSchool.Behaviour.Interfaces;
using SmartTek.ToolSchool.Components;
using System.Collections.Generic;
using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour
{
    public class PneumaticDrillLesson : BaseToolLesson
    {
        [SerializeField]
        private BaseTool[] _toolsPrefabs;

        public override IReadOnlyList<BaseTool> ToolsPrefabs => _toolsPrefabs;

        public override string Name => "Pneumatic drill lesson";

        public override  string Description => string.Empty;

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