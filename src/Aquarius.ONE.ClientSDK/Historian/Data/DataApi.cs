﻿using System;
using System.Collections.Generic;
using ONE.Utilities;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using ONE.Models.CSharp;

namespace ONE.Common.Historian

{
    public class DataApi
    {
        public DataApi(PlatformEnvironment environment, bool continueOnCapturedContext, RestHelper restHelper)
        {
            _environment = environment;
            _continueOnCapturedContext = continueOnCapturedContext;
            _restHelper = restHelper;
        }
        private PlatformEnvironment _environment;
        private bool _continueOnCapturedContext;
        private RestHelper _restHelper;
        public event EventHandler<ClientApiLoggerEventArgs> Event = delegate { };

        private List<HistorianData> ConvertToHistorianDataList(TimeSeriesDatas timeSeriesDatas)
        {
            List<HistorianData> historianDatas = new List<HistorianData>();
            if (timeSeriesDatas != null && timeSeriesDatas.Items != null)
            foreach (var timeSeriesData in timeSeriesDatas.Items)
            {
                historianDatas.Add(new HistorianData 
                {
                     DateTimeUTC = timeSeriesData.DateTimeUTC,
                     Id = timeSeriesData.Id,
                     PropertyBag = timeSeriesData.PropertyBag,
                     RecordAuditInfo = timeSeriesData.RecordAuditInfo,
                     StringValue = timeSeriesData.StringValue,
                     Value = timeSeriesData.Value
                });
            }
            return historianDatas;
        }
        private TimeSeriesDatas ConvertToTimeSeriesDatas(string telemetryTwinRefId, HistorianDatas historianDatas)
        {
            TimeSeriesDatas timeSeriesDatas = new TimeSeriesDatas();
            if (historianDatas != null && historianDatas.Items != null)
            {
                foreach (var historianData in historianDatas.Items)
                {
                    timeSeriesDatas.Items.Add(new TimeSeriesData
                    {
                        DateTimeUTC = historianData.DateTimeUTC,
                        Id = historianData.Id,
                        PropertyBag = historianData.PropertyBag,
                        RecordAuditInfo = historianData.RecordAuditInfo,
                        StringValue = historianData.StringValue,
                        TelemetryTwinRefId = telemetryTwinRefId,
                        Value = historianData.Value
                    });
                }
            }
            return timeSeriesDatas;
        }
        public async Task<List<HistorianData>> GetDataAsync(string telemetryTwinRefId, DateTime startDate, DateTime endDate)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var requestId = Guid.NewGuid();

            List<HistorianData> historianData = new List<HistorianData>();
            try
            {
                string sDate = startDate.ToString("MM/dd/yyyy HH:mm:ss");
                string eDate = endDate.ToString("MM/dd/yyyy HH:mm:ss");
                var respContent = await _restHelper.GetRestProtocolBufferAsync(requestId, $"timeSeries/data/v1/{telemetryTwinRefId}/timeSeriesData?startDate={sDate}&endDate={eDate}&requestId={requestId}").ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var results = ConvertToHistorianDataList(respContent.ApiResponse.Content.TimeSeriesDatas);
                    
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "DataApi", Message = $"GetDataAsync Success" });
                    return results;
                }
                else
                {
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "DataApi", Message = $"GetDataAsync Failed" });
                    return null;
                }
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "DataApi", Message = $"GetDataAsync Failed - {e.Message}" });
                throw;
            }
        }

        public async Task<List<HistorianData>> SaveDataAsync(string telemetryTwinRefId, HistorianDatas historianDatas)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var requestId = Guid.NewGuid();
            var endpoint = $"timeseries/data/v1/{telemetryTwinRefId}/timeSeriesData";
            var timeseriesDatas = ConvertToTimeSeriesDatas(telemetryTwinRefId, historianDatas);
            var json = JsonConvert.SerializeObject(timeseriesDatas, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            try
            {
                var respContent = await _restHelper.PostRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var results = ConvertToHistorianDataList(respContent.ApiResponse.Content.TimeSeriesDatas);

                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "DataApi", Message = $"SaveDataAsync Success" });
                    return results;
                }
                else
                {
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "DataApi", Message = $"SaveDataAsync Failed" });
                    return null;
                }

            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "DataApi", Message = $"SaveDataAsync Failed - {e.Message}" });
                throw;
            }
        }
    }
}
