namespace YATT.Libs.Extensions;


public static class TaskExtension
{
    public static T GetValue<T>(this Task<T> task)
    {
        return task.GetAwaiter().GetResult();
    }

}
