using SmartTek.ToolSchool.Services.Interfaces;

namespace SmartTek.ToolSchool.Helpers
{
    public static class ServicesReferences
    {
        public static ILessonsService LessonsService { get => Services.LessonsService.Instance; }
        public static ISceneService SceneService { get => Services.SceneService.Instance; }
    }
}