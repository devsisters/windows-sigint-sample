using System;
using System.Threading;

namespace SignalHandlerTestManaged
{
    internal static class Program
    {
        public const int DefaultInterruptSignalWaitSeconds = (60 * 5);

        private static int waitSeconds = DefaultInterruptSignalWaitSeconds;
        private static NativeMethods.SignalFunctionDelegate previousSignalHandler = null;

        private static int Main()
        {
            string waitSecondsArgs = Environment.GetEnvironmentVariable("WAIT_SECONDS");
            if (!Int32.TryParse(waitSecondsArgs, out waitSeconds))
            {
                Console.Error.WriteLine("Falling back to default wait second value ({0}).", DefaultInterruptSignalWaitSeconds);
                waitSeconds = DefaultInterruptSignalWaitSeconds;
            }
            else
            {
                Console.Out.WriteLine("Wait second specified. ({0}).", waitSecondsArgs);
                waitSeconds = Math.Max(1, waitSeconds);
            }

            Console.Out.WriteLine("Wait second: {0} sec.", waitSeconds);

            Console.Out.WriteLine("Hello, World!");
            previousSignalHandler = NativeMethods.signal(
                NativeMethods.SIGINT,
                new NativeMethods.SignalFunctionDelegate(sigint_handler));

            Thread.Sleep(Timeout.Infinite);
            return 0;
        }

        public static void sigint_handler(int s)
        {
            if (s != NativeMethods.SIGINT)
                return;

            Console.Out.WriteLine("SIGINT({0}) received.", s);

            for (int i = waitSeconds; i > 0; i--)
            {
                Console.Out.WriteLine("{0} second(s) remained to interrupt.", i);
                Thread.Sleep(TimeSpan.FromSeconds(1d));
            }

            Console.Out.WriteLine("SIGINT({0}) processed.", s);
            NativeMethods.signal(NativeMethods.SIGINT, previousSignalHandler);
            Environment.Exit(0);
        }
    }
}
