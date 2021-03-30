using System.Runtime.InteropServices;

namespace SignalHandlerTestManaged
{
    internal static class NativeMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SignalFunctionDelegate(int signal);

        public const int SIGINT = 2;

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern SignalFunctionDelegate signal(int signal, SignalFunctionDelegate function);
    }
}
