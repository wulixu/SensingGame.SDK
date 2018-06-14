using StackExchange.Redis;
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


            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("139.224.23.171");

            //int databaseNumber = 1
            //object asyncState = 
            IDatabase db = redis.GetDatabase();
            db.StringSet("name", "william");


            string value = db.StringGet("name");
            Console.WriteLine(value); 

        }
    }
}
