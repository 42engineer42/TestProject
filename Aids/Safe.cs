namespace Nullam.Aids {
    public static class Safe {
        public static T? Run<T>(Func<T> function, T? defaultResult = default) { try { return function(); } catch { return defaultResult; } }
        public static void Run(Action a) { try { a(); } catch { } }
    }
}
