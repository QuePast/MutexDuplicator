using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace MutexKiller
{
    class Program
    {
        private static readonly string BaseMutexName = @"\Sessions\1\BaseNamedObjects\usbhub_Perf_Library_Lock_PlD";

        static void Main()
        {
            try
            {
                DuplicateMutexHandles();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private static void DuplicateMutexHandles()
        {
            Process notepadProcess = Process.GetProcessesByName("python").FirstOrDefault();
            if (notepadProcess == null)
            {
                Console.WriteLine("python.exe is not running.");
                return;
            }

            var leagueProcesses = Process.GetProcesses()
                                         .Where(p => p.ProcessName.StartsWith("Leagueb"))
                                         .ToList();

            foreach (var process in leagueProcesses)
            {
                Console.WriteLine("Checking in Process: " + process.ProcessName);

                List<Win32API.SYSTEM_HANDLE_INFORMATION> handles = Win32Processes.GetHandles(process, "Mutant");
                foreach (var handle in handles)
                {
                    string objectName = Win32Processes.GetObjectName(handle, process);
                    if (!string.IsNullOrWhiteSpace(objectName) && objectName.StartsWith(BaseMutexName))
                    {
                        if (DuplicateHandleToPython(notepadProcess, process, handle))
                        {
                            Console.WriteLine($"Duplicated handle from {process.ProcessName} to python.exe.");
                        }
                    }
                }
            }
        }

        private static bool DuplicateHandleToPython(Process notepadProcess, Process sourceProcess, Win32API.SYSTEM_HANDLE_INFORMATION handle)
        {
            IntPtr duplicatedHandle;
            return Win32API.DuplicateHandle(
                sourceProcess.Handle,
                handle.Handle,
                notepadProcess.Handle,
                out duplicatedHandle,
                0,
                false,
                Win32API.DUPLICATE_SAME_ACCESS);
        }
    }
}
