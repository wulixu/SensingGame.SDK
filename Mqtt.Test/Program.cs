using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mqtt.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Sensing.Mqtt.SensingStoreMqttClient client = new Sensing.Mqtt.SensingStoreMqttClient();
            client.Connect("139.196.240.230", "123123");
            Console.ReadLine();
        }
    }
}
