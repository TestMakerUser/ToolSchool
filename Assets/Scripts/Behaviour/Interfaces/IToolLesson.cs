using SmartTek.ToolSchool.Components;
using System;
using System.Collections.Generic;

namespace SmartTek.ToolSchool.Behaviour.Intefaces
{
    public interface IToolLesson : IDisposable
    {
        IReadOnlyList<BaseTool> ToolsPrefabs { get; }
        string Name { get; }
        string Description { get; }
    }
}