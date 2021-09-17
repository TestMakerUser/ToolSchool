using System;

namespace SmartTek.ToolSchool.Services.Intefaces
{
    public interface ISceneService
    {
        void LoadScene(SceneService.SceneType scene, Action onComplete);
    }
}