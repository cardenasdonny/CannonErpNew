using Serilog;

namespace Common.Logger
{
    public static class Logs
    {
        public static void Information(string titulo, string mensaje, string usuario = "System.Cannon", object? objeto = null)
        {
            if (objeto != null)
                Log.Information(titulo + " {Mensaje} - Usuario: {Usuario} - Objeto: {@Objeto}", mensaje, usuario, objeto);
            else
                Log.Information(titulo + " {Mensaje} - Usuario: {Usuario}", mensaje, usuario);
        }

        public static void Warning(string titulo, string mensaje, string usuario = "System.Cannon", object? objeto = null)
        {
            if (objeto != null)
                Log.Warning(titulo + " {Mensaje} - Usuario: {Usuario} - Objeto: {@Objeto}", mensaje, usuario, objeto);
            else
                Log.Warning(titulo + " {Mensaje} - Usuario: {Usuario}", mensaje, usuario); ;
        }

        public static void Error(string titulo, string mensaje, string usuario = "System.Cannon", object? objeto = null)
        {
            if (objeto != null)
                Log.Error(titulo + " {Mensaje} - Usuario: {Usuario} - Objeto: {@Objeto}", mensaje, usuario, objeto);
            else
                Log.Error(titulo + " {Mensaje} - Usuario: {Usuario}", mensaje, usuario);
        }

        public static void FatalError(string titulo, string mensaje, string usuario = "System.Cannon", object? objeto = null)
        {
            if (objeto != null)
                Log.Fatal(titulo + " {Mensaje} - Usuario: {Usuario} - Objeto: {@Objeto}", mensaje, usuario, objeto);
            else
                Log.Fatal(titulo + " {Mensaje} - Usuario: {Usuario}", mensaje, usuario);
        }

        public static void Debug(string titulo, string mensaje, string usuario = "System.Cannon", object? objeto = null)
        {
            if (objeto != null)
                Log.Debug(titulo + " {Mensaje} - Usuario: {Usuario} - Objeto: {@Objeto}", mensaje, usuario, objeto);
            else
                Log.Debug(titulo + " {Mensaje} - Usuario: {Usuario}", mensaje, usuario);
        }
    }
}