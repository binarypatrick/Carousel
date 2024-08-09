namespace PhotoCarousel;

public static class Extensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> elements)
    {
        T[] el = elements.ToArray();
        Random.Shared.Shuffle(el);

        return el;
    }
}
