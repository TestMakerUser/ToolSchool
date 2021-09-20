using SmartTek.ToolSchool.Components;
using System;
using System.Collections.Generic;

namespace SmartTek.ToolSchool.Behaviour.Interfaces
{
    public interface IToolLesson : IDisposable
    {
        IReadOnlyList<BaseTool> ToolsPrefabs { get; }
        string Name { get; }
        string Description { get; }
        bool IsLaunching { get; }

        void LaunchLesson(LessonContext context);
    }
}