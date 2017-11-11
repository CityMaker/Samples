using System.Xml;
using Gvitech.CityMaker.Common;
using System;

namespace Authorization
{
    public class NetAuthorization
    {
        public static string m_sNetAuthorizationXml;

        public static string m_sNetHost;

        public static string m_sNetPort;

        public static string m_sNetPwd;

        public static bool CheckNetworkLicense()
        {
            //解析端口号
            uint port = 0;
            uint.TryParse(m_sNetPort, out port);

            //连接网络服务器并验证
            ILicenseServer licenseServer = new LicenseServer();
            licenseServer.SetHost(m_sNetHost, port, m_sNetPwd);

            bool bSucceed = licenseServer.HasPermission();

            return bSucceed;
        }

        /// <summary>
        /// 更新配置Xml信息
        /// </summary>
        /// <param name="sCfgFile">配置xml路径</param>
        /// <param name="sHost">服务器IP</param>
        /// <param name="sPort">端口</param>
        /// <param name="sPwd">密码</param>
        public static void UpdateServerInfo(string sHost,string sPort,string sPwd)
        {
            m_sNetHost = sHost;
            m_sNetPort = sPort;
            m_sNetPwd = sPwd;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_sNetAuthorizationXml);

            xmlDoc.SelectSingleNode("LicenseServer/Host").InnerText = m_sNetHost;
            xmlDoc.SelectSingleNode("LicenseServer/Port").InnerText = m_sNetPort;
            xmlDoc.SelectSingleNode("LicenseServer/Password").InnerText = m_sNetPwd;

            xmlDoc.Save(m_sNetAuthorizationXml);
        }

        /// <summary>
        /// 读取配置XML信息
        /// </summary>
        /// <param name="sCfgFile">配置xml路径</param>
        /// <param name="sHost">服务器IP</param>
        /// <param name="sPort">端口</param>
        /// <param name="sPwd">密码</param>
        public static void ReadServerInfo()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_sNetAuthorizationXml);

            m_sNetHost = xmlDoc.SelectSingleNode("LicenseServer/Host").InnerText;
            m_sNetPort = xmlDoc.SelectSingleNode("LicenseServer/Port").InnerText;
            m_sNetPwd = xmlDoc.SelectSingleNode("LicenseServer/Password").InnerText;
        }
    }
}
