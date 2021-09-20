using System;

namespace SmartTek.ToolSchool.Services.Interfaces
{
    public interface ISceneService
    {
        void LoadScene(SceneService.SceneType scene, Action onComplete);
    }
}