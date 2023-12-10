using System.Reflection;

namespace Pyther.Collections;

internal static class Extensions
{
    internal static string Repeat(this string text, uint n)
    {
        var spanSrc = text.AsSpan();
        int length = spanSrc.Length;
        var spanDst = new Span<char>(new char[length * (int)n]);
        for (var i = 0; i < n; i++)
        {
            spanSrc.CopyTo(spanDst.Slice(i * length, length));
        }
        return spanDst.ToString();
    }

}
