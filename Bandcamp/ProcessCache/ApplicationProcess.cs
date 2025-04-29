using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandcamp.ProcessCache
{
    public class ApplicationProcess
    {
        public Process? _process;

        public void DefineProcessFfplay()
        {
            if (_process != null)
            {
                return;
            }

            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools", "ffplay.exe"),
                Arguments = "-i - -nodisp",
                RedirectStandardInput = true, // permite redirección de bytes desde streamstore
                RedirectStandardOutput = false,
                UseShellExecute = false, //Debe estar en false para permitir redirección de stdin
                CreateNoWindow = true // ocultar la ventana para depuración del proceso
            };

            _process = Process.Start(processInfo);

        }

        public void DestroyedProcess()
        {
            try
            {
                if (_process != null && !_process.HasExited)
                {
                    _process.Kill();
                    _process?.Dispose();
                }
                _process = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("cerrando _process: " + ex.Message);
            }
        }

        public void DestroyedProcessCache()
        {
            try
            {
                if (_process != null && !_process.HasExited)
                {
                    Debug.WriteLine("cerrando proceso activo ffplay #: "+_process.Id);
                    Process childProcess = Process.GetProcessById(_process.Id);
                    childProcess.CloseMainWindow();
                    if (!childProcess.HasExited) { 
                        childProcess.Kill();
                    }
                    childProcess.WaitForExit();
                    childProcess.Dispose();
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error cerrando _process cache: " + ex.Message);
            }
        }

    }
}
