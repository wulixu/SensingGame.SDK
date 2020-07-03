﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sensing.SDK.Contract
{
    public class AdSchedule
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? EndTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? CreationTime { get; set; }
        public List<AdSchedulingContent> AdSchedulingContent { get; set; }
    }



    public class AdSchedulingContent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(ScheduleModelConverter))]
        public SchedulingModel Content { get; set; }
        [JsonConverter(typeof(AdsPlayListConverter))]
        public List<AdsPlayList> Ads { get; set; }
        public TimeSpan? StartTimeSpan { get; set; }
    }

    public enum PlayMode
    {
        Strict = 0,
        NoStrict = 1
    }

    public class SchedulingModel
    {
        public SchedulingModelType Model { get; set; }
        public PlayMode PlayMode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Priority { get; set; }
        [JsonConverter(typeof(ScheduleModelMonthDayConverter))]
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
                var model = JsonConvert.DeserializeObject<SchedulingModel>(reader.Value as string);
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
            else if (reader.TokenType == JsonToken.StartObject)
            {
                return serializer.Deserialize(reader, objectType);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }
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
                var model = JsonConvert.DeserializeObject<List<AdsPlayList>>(reader.Value as string);
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

        }
    }


    public class ScheduleModelMonthDayConverter : JsonConverter
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
            else if (reader.TokenType == JsonToken.StartArray)
            {
                return reader.Value;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }
    }
}