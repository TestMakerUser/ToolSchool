using System;

namespace SmartTek.ToolSchool.Services.Interfaces
{
    /// <summary>
    /// Loading and unloading of scenes, which are included to gameplay.
    /// </summary>
    public interface ISceneService
    {
        void LoadScene(SceneService.SceneType scene, Action onComplete);
    }
}