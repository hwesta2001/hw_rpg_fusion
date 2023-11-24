using Fusion;

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
}
