﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace InitLoader
{
    public class InitLoader
    {
        [HandleProcessCorruptedStateExceptions]
        public static void Start()
        {
            if (!isPhotonDotNet)
            {
                Console.WriteLine("Module is bad. Error: 0x0000001F");
                return;
            }

            if (File.Exists("lic.ss"))
            {
                var msg = File.ReadAllText("lic.ss");
                foreach (var message in msg.Split('\n'))
                {
                    var mgs = message.Trim(new char[] { ' ', '\t', '\n', '\r', '{', '}' });
                    if (mgs.Length > 0)
                    {
                        if (string.IsNullOrEmpty(InitModule.login))
                        {
                            InitModule.login = mgs;
                            continue;
                        }
                        if (string.IsNullOrEmpty(InitModule.pass))
                        {
                            InitModule.pass = mgs;
                            continue;
                        }
                        if (string.IsNullOrEmpty(InitModule.PrivateKey))
                        {
                            InitModule.PrivateKey = mgs;
                            continue;
                        }
                    }
                }
            }
            if (InitModule.Web.WebRequest("anonimous", "", "checkauth", "xtest") is byte[])
                InitModule.isConnected = true;

            if (InitModule.isConnected)
            {
                byte[] bytes = File.ReadAllBytes("Modules\\InitManager.dll");
                var obj = InitModule.Web.WebRequest(InitModule.login, InitModule.pass, InitModule.PrivateKey, "" + bytes[0] + bytes[258] + bytes[333] + bytes[2] + bytes[303] + bytes[900] + "030");
                try
                {
                    // [SpecialName()]
                    var assembly = Assembly.Load((byte[])obj);
                    if (assembly == null)
                        throw new ArgumentNullException();

                    foreach (var type in assembly.GetTypes())
                    {
                        var method = type.GetMethods().FirstOrDefault(m => m.GetCustomAttributes(typeof(HandleProcessCorruptedStateExceptionsAttribute), true).Length > 0 && m.IsStatic && m.GetParameters().Length == 0);
                        if (method != null)
                        {
                            method.Invoke(null, null);
                            break;
                        }
                    }
                }
                catch {  }
            }
        }
        public static bool isPhotonDotNet
        {
            get
            {
                bool result = false;
                string szFileName = "Photon-DotNet.dll";
                if (!File.Exists(szFileName))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(InitModule.Web.MyRemoteCertificateValidationCallback);
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            client.DownloadFile("http://icefrag.ru/photon_dotnet", szFileName);
                        }
                        finally
                        {
                            result = File.Exists(szFileName);
                        }
                    }
                }
                else
                    result = true;
                return result;
            }
        }

    }
    public class InitModule
    {
        public static bool isConnected = false;

        public static string login { get; set; }

        public static string pass { get; set; }

        public static string PrivateKey { get; set; }
        // -----------
        public class Web
        {
            private static Uri httpRequestBase = new Uri("http://icefrag.ru");

            public static object WebRequest(string login, string pass, string privatekey, string uri)
            {
                object result = null;
                Uri newURI = new Uri(httpRequestBase + uri);

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(MyRemoteCertificateValidationCallback);
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add(HttpRequestHeader.UserAgent, "BlazeLoader");
                    webClient.Headers.Add("OS", Environment.OSVersion.Platform + ":" + Environment.OSVersion.Version.Major);
                    webClient.Headers.Add("login", login);
                    webClient.Headers.Add("pass", pass);
                    webClient.Headers.Add("PrivateKey", privatekey);
                    webClient.Headers.Add("mhash", mHash);
                    try
                    {
                        result = webClient.DownloadData(newURI);
                    }
                    catch
                    {
                        result = "Server is failed";
                    }
                }
                return result;
            }

            public static bool MyRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                if (sslPolicyErrors > SslPolicyErrors.None)
                {
                    for (int i = 0; i < chain.ChainStatus.Length; i++)
                    {
                        if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                            continue;

                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                        if (!chain.Build((X509Certificate2)certificate))
                            return false;
                    }
                }
                return true;
            }
        }

        public static string mHash
        {
            get
            {
                string result = string.Empty;
                try
                {
                    if (File.Exists("winmm.dll"))
                    {
                        MD5 md5 = new MD5CryptoServiceProvider();
                        byte[] retVal = md5.ComputeHash(File.ReadAllBytes("winmm.dll"));

                        for (int i = 0; i < retVal.Length; i++)
                        {
                            result += retVal[i].ToString("x2");
                            
                        }
                    }
                }
                catch
                {

                }
                return result;
            }
        }
        // -----------
    }
}
