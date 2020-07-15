using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sensing.SDK.Contract
{
    public class AdAndAppTimelineScheduleViewModel
    {
        public DateTime Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public  TimeSpan? EndTime { get; set; }
        public ScheduleModel ScheduleModel { get; set; }
        public List<ProgramItem> AdAndApps { get; set; }
    }

    public enum PlayMode
    {
        Strict = 0,
        NoStrict = 1
    }

    public class ScheduleModel
    {
        public SchedulingModelType Model { get; set; }
        public PlayMode PlayMode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Priority { get; set; }
        [JsonConverter(typeof(ListToStringJsonConverter))]
        public List<int> MonthDay { get; set; }
        public List<int> WeekdayList { get; set; }
    }

    public enum SchedulingModelType
    {
        TimeRange = 0,
        Daily = 1,
        Week = 2,
        Month = 3
    }

    public class AdsPlayListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var model = JsonConvert.DeserializeObject<List<ProgramItem>>(reader.Value as string);
                return model;
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                return serializer.Deserialize(reader, objectType);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(JsonConvert.SerializeObject(value));

        }
    }


    public class ScheduleModelConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var model = JsonConvert.DeserializeObject<ScheduleModel>(reader.Value as string);
                if (model.StartTime.HasValue)
                {
                    if (model.StartTime.Value.Kind == DateTimeKind.Utc)
                    {
                        model.StartTime = DateTime.SpecifyKind(model.StartTime.Value, DateTimeKind.Local);
                    }
                }
                if (model.EndTime.HasValue)
                {
                    if (model.EndTime.Value.Kind == DateTimeKind.Utc)
                    {
                        model.EndTime = DateTime.SpecifyKind(model.EndTime.Value, DateTimeKind.Local);
                    }
                }
                return model;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(JsonConvert.SerializeObject(value));
        }
    }

    public class ListToStringJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return (reader.Value as string).Split(',').Select(int.Parse).ToList();
            }
            else
            {
                return reader.Value;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<int> list = (List<int>)value;
            string text = string.Join(",", list);
            writer.WriteValue(text);
        }
    }
}
