using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Sensing.Mqtt
{
    public class SensingStoreMqttClient
    {
        public const string DeviceControllerTopic = ".device.controller";
        public const string DeviceLogTopic = ".device.log";

        public MqttClient _client;
        public void Connect(string connectString,string clientId)
        {
            try
            {
                _client = new MqttClient("139.196.240.230");
                _client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                _client.ConnectionClosed += Client_ConnectionClosed;
                _client.Connect(clientId, "wulixu", "1qaz@WSX");
                _client.Subscribe(new string[] { DeviceControllerTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
            catch(Exception ex)
            {

            }
        }

        public void DisConnect()
        {
            if(_client.IsConnected)
            {
                _client.Disconnect();
            }
        }

        private void Client_ConnectionClosed(object sender, EventArgs e)
        {
            //todo: need to consider the retry again.
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Message);
            Console.WriteLine(message);
        }

        public void Publish(string msg, string topic)
        {
            if(_client!= null && _client.IsConnected)
            {
                var body = Encoding.UTF8.GetBytes(msg);
                _client.Publish(topic, body);
            }
        }
    }
}
