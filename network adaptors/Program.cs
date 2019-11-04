using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace network_adaptors
{
    class Program
    {
        static void Main(string[] args)
        {
            //edited in github for practice
            ManagementScope wmiSrv;
            ManagementObjectSearcher objSearcher;
            ManagementObjectCollection objColl;
            ObjectQuery objQuery;

            ConnectionOptions connOpts = new ConnectionOptions();
            objQuery = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter WHERE AdapterTypeID <> NULL");
            wmiSrv = new ManagementScope(@"root\cimv2", connOpts);
            wmiSrv.Connect();
            objSearcher = new ManagementObjectSearcher(wmiSrv, objQuery);
            objSearcher.Options.Timeout = new TimeSpan(0, 0, 0, 0, 7000);
            objColl = objSearcher.Get();
            foreach (ManagementObject mo in objColl)
            {
                Console.WriteLine("Device ID: " + mo["DeviceID"].ToString());
                Console.WriteLine("   Adapter type: " + mo["AdapterType"].ToString());
                Console.WriteLine("   Description: " + mo["Description"].ToString());
                Console.WriteLine("   MAC address: " + mo["MACAddress"].ToString());
                Console.WriteLine("   Manufacturer: " + mo["Manufacturer"].ToString());
                Console.WriteLine();
            }
            Console.ReadKey(true);
        }
    }
}
