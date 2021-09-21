using UnityEngine;

namespace SmartTek.ToolSchool.Components
{
    /// <summary>
    /// Common tool information. Tool - main entity, which is used in the lessons. One tool may be used in several lessons.
    /// </summary>
    public abstract class BaseTool : MonoBehaviour
    {
        public abstract string Name { get; }
    }
}