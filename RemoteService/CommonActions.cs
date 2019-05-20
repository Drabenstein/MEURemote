using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace RemoteService
{
    public static class CommonActions
    {
        /// <summary>
        /// Zwraca lokalny adres IP komputera
        /// </summary>
        /// <returns>Lokalny adres IP komputera</returns>
        public static IPAddress GetLocalIP()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        }

        /// <summary>
        /// Zamyka komputer z podanymi parametrami
        /// </summary>
        /// <param name="arguments">Parametry zamknięcia systemu. Jeżeli nie podane, wybrane jest /s /f /t 0</param>
        public static bool Shutdown(string arguments = null)
        {
            try
            {
                string options = arguments == null ? "/s /f /t 0" : arguments;
                ProcessStartInfo proc = new ProcessStartInfo("shutdown", options);
                proc.CreateNoWindow = true;
                proc.UseShellExecute = false;
                Process.Start(proc);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Sprawdza czy dany proces jest uruchomiony w systemie
        /// </summary>
        /// <param name="processName">Nazwa procesu do sprawdzenia</param>
        /// <returns></returns>
        public static bool CheckProcessRunning(string processName)
        {
            try
            {
                int nNumOfOccurances = 0;
                Process[] procs = Process.GetProcesses();
                foreach (var proc in procs)
                {
                    if(proc.ProcessName.Contains(processName))
                    {
                        nNumOfOccurances++;
                    }
                }

                if(nNumOfOccurances == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw new Exception(":SVR_CHECK_PROCESS_ERROR");
            }
        }

        /// <summary>
        /// Zamyka podane procesy
        /// </summary>
        /// <param name="processesToKill">Procesy do zamknięcia</param>
        /// <returns>True - jeśli zamknięto False - jeśli wystąpił błąd</returns>
        public static bool KillProcess(string processToKill)
        {
            if (processToKill.Length == 0)
            {
                throw new Exception(":SVR_RUN_INVALID_ARG");
            }

            Process[] procs = Process.GetProcesses();
            int nOccurances = 0;
            foreach (var proc in procs)
            {
                if (proc.ProcessName.Contains(processToKill))
                {
                    nOccurances++;
                    try
                    {
                        proc.Kill();
                    }
                    catch (Exception)
                    {
                    } 
                }
            }

            if(nOccurances == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Uruchamia program bez ukazywania UI i zwraca obiekt Process programu
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Obiekt Process uruchomionego programu. Null jeśli błąd</returns>
        public static bool RunExecutable(string filename, string primaryLocation)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(primaryLocation);
            bool isPrimaryLocation = false;
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (file.Name == filename)
                {
                    isPrimaryLocation = true;
                    break;
                }
            }

            if (isPrimaryLocation)
            {
                filename.Insert(0, primaryLocation + Path.DirectorySeparatorChar);
            }
            else if(!File.Exists(filename))
            {
                throw new Exception(":SVR_RUN_FILE_NOT_FOUND");
            }

            try
            {
                ProcessStartInfo info = new ProcessStartInfo(filename);
                info.UseShellExecute = false;
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(info);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
