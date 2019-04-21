/*
'	프로그램명	: DevCommonCheck
'	작성자		: DevOpsFlux
'	작성일		: 2015-05-20
'	설명		: DevCommonCheck CommonLib
*/
using System;
using System.Text;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;

using System.EnterpriseServices;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

/* 실행 시 관리자 권한 상승을 위해 추가*/
using System.Security.Principal;


namespace DevCommonCheck
{
    [Transaction(TransactionOption.NotSupported)]
    [JustInTimeActivation(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class CheckLib : ServicedComponent
    {
        #region # PingCheck 
        public bool PingCheck(string ip)
        {
            bool result = false;
            try
            {
                Ping pp = new Ping();
                PingOptions po = new PingOptions();

                po.DontFragment = true;

                byte[] buf = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaa");

                PingReply reply = pp.Send(
                    IPAddress.Parse(ip),
                    10, buf, po
                );

                if (reply.Status == IPStatus.Success)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region # TelnetCheck 
        public bool TelnetCheck(string ip, int port)
        {
            bool result = false;

            Socket socket = null;
            try
            {
                socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                    );

                socket.SetSocketOption(
                    SocketOptionLevel.Socket,
                    SocketOptionName.DontLinger,
                    false
                    );


                IAsyncResult ret = socket.BeginConnect(ip, port, null, null);

                result = ret.AsyncWaitHandle.WaitOne(100, true);
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (socket != null)
                {
                    socket.Close();
                }
            }
            return result;
        }
        #endregion

        #region UDP Client
        public bool UDPClient(string ServerIP, string ServerPort, string SendMsg, string LogPath)
        {
            bool result = false;
            string strPath = LogPath;
            //WriteErrLog(strPath, " UDP/TCP Client Start ");

            try
            {
                string strServerIP = ServerIP;
                Int32 intPort = Convert.ToInt32(ServerPort);
                string strMsg = SendMsg;


                // (1) UdpClient 객체 성성
                UdpClient client = new UdpClient();

                byte[] datagram = Encoding.UTF8.GetBytes(strMsg);

                // (2) 데이타 송신
                client.Send(datagram, datagram.Length, strServerIP, intPort);

                //// (3) 데이타 수신
                //IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
                //byte[] bytes = client.Receive(ref epRemote);

                //string responseData = System.Text.Encoding.UTF8.GetString(bytes);
                //Console.WriteLine(string.Format("Received: {0}", responseData));

                // (4) UdpClient 객체 닫기
                client.Close();
            }
            catch (SocketException ex)
            {
                WriteErrLog(ex.ToString());
            }
            finally
            {
                result = true;
                WriteErrLog(string.Format("SendMsg : {0}", SendMsg));
                //WriteErrLog(strPath, " UDP/TCP Client End finally ");
            }

            return result;
        }
        #endregion


        #region # ShellExecute
        [AutoComplete(true)]
        public void ShellExecute(string strOperation, string strParameters)
        {
            try
            {
                // Simple version
                Process.Start(strOperation, strParameters);

                /*
                ProcessStartInfo psi = new ProcessStartInfo("testFlux.exe");
                //psi.Arguments = @"update D:\UnitTest\";
                psi.Arguments = @"update D:\UnitTest\";
                psi.UseShellExecute = true;
                psi.Verb = "runas";
                Process.Start(psi);
                */

                /*
                string cmd = "testFlux.exe";
                string arguments = "update";
                int ms = 5000;
                ProcessStartInfo psi = new ProcessStartInfo(cmd);
                psi.Arguments = arguments;
                psi.RedirectStandardOutput = true;
                psi.WindowStyle = ProcessWindowStyle.Normal;
                psi.UseShellExecute = false;
                Process proc = Process.Start(psi);
                StreamReader output = new StreamReader(proc.StandardOutput.BaseStream, Encoding.UTF8);
                proc.WaitForExit(ms);
                if (proc.HasExited)
                {
                    return output.ReadToEnd();
                }
                */
            }
            catch (Exception ex)
            {
                WriteErrLog(ex.ToString());
            }
        }

        [AutoComplete(true)]
        public void ShellExecuteTest(string strKind, string strOperation, string strParameters)
        {
            try
            {

                /* 실행 시 관리자 권한 상승을 위한 코드 시작 */
                /*
                if (IsAdministrator() == false)
                {
                    try
                    {
                        ProcessStartInfo procInfo = new ProcessStartInfo();
                        procInfo.UseShellExecute = true;
                        procInfo.FileName = strOperation;
                        procInfo.Arguments = strParameters;
                        procInfo.WorkingDirectory = Environment.CurrentDirectory;
                        procInfo.Verb = "runas";
                        Process.Start(procInfo);
                    }
                    catch (Exception ex)
                    {
                        WriteErrLog(ex.Message.ToString());
                    }
                }
                */

                WriteErrLog("Call End");
                
            }
            catch (Exception ex)
            {
                WriteErrLog(ex.ToString());
            }
        }
        #endregion


        #region # IsAdministrator
        /* 실행 시 관리자 권한 상승을 위한 함수 시작 */
        public static bool IsAdministrator()
        {
	        WindowsIdentity identity = WindowsIdentity.GetCurrent();
	        if (null != identity)
	        {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                 return principal.IsInRole(WindowsBuiltInRole.Administrator);
	        }
            return false;
        }
        #endregion

        public static void WriteErrLog(string msg)
        {
            StreamWriter sw = null;
            //sw = new StreamWriter(@"D:\Dev\CI\testlog.log", true);
            sw = new StreamWriter(@"D:\Dev\CI\Com\testlog.log", true);
            
            sw.WriteLine(DateTime.Now.ToString() + " : " + msg.Trim());
            sw.Flush();
            sw.Close();
        }
        
    }
}
