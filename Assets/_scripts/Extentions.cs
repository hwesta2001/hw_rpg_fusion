using System;
using System.Threading.Tasks;

public static class Extentions
{
    public static void ToLog(this string text)
    {
        DebugLogs.Ins.ToLog(text);
    }

    public static CharNW GetChar(this int id)
    {
        return CharManager.ins.GetChar(id);
    }

    public static async Task WaitUntil(Func<bool> predicate, int sleep = 50)
    {
        while (!predicate())
        {
            await Task.Delay(sleep);
        }
    }

}
