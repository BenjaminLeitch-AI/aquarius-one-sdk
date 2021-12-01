﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ONE.Utilities;
using System.Collections.Generic;
using Operations.Spreadsheet.Protobuf.Models;
using Common.Core.Protobuf.Models;

namespace ONE.Operations.Spreadsheet
{
    public class SpreadsheetApi
    {
        public event EventHandler<ClientApiLoggerEventArgs> Event = delegate { };
        public SpreadsheetApi(PlatformEnvironment environment, bool continueOnCapturedContext, RestHelper restHelper)
        {
            _environment = environment;
            _continueOnCapturedContext = continueOnCapturedContext;
            _restHelper = restHelper;
        }
        private PlatformEnvironment _environment;
        private bool _continueOnCapturedContext;
        private RestHelper _restHelper;
        public async Task<Cell> CellValidate(string operationTwinReferenceId, EnumWorksheet worksheetType, Cell cell)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/validateCell?requestId={requestId}";
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(cell, jsonSettings);

            try
            {
                var respContent = await _restHelper.PostRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var result = apiResponse.Content.Cells.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinitionAsync Success" });
                    return result[0].Value;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"CellValidate Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"CellValidate Failed - {e.Message}" });
                throw;
            }
        }

        public async Task<List<Measurement>> ColumnGetByDay(string operationTwinReferenceId, EnumWorksheet worksheetType, uint columnId, DateTime date)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var worksheetDefinitionEndpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/column/{columnId}/byday/{date.Year}/{date.Month}/{date.Day}";

            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, worksheetDefinitionEndpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.Measurements.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinitionAsync Success" });
                    return result;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinitionAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinition Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<List<Measurement>> ColumnGetByMonth(string operationTwinReferenceId, EnumWorksheet worksheetType, uint columnId, DateTime date)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var worksheetDefinitionEndpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/column/{columnId}/bymonth/{date.Year}/{date.Month}";

            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, worksheetDefinitionEndpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.Measurements.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinitionAsync Success" });
                    return result;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinitionAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinition Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<List<Measurement>> ColumnGetByYear(string operationTwinReferenceId, EnumWorksheet worksheetType, uint columnId, DateTime date)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var worksheetDefinitionEndpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/column/{columnId}/byyear/{date.Year}";

            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, worksheetDefinitionEndpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.Measurements.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinitionAsync Success" });
                    return result;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinitionAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinition Failed - {e.Message}" });
                throw;
            }
        }

        public async Task<SpreadsheetComputation> ComputationCreateAsync(string operationTwinReferenceId, EnumWorksheet worksheetType, SpreadsheetComputation spreadsheetComputation)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/computation?requestId={requestId}";
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(spreadsheetComputation, jsonSettings);

            try
            {
                var respContent = await _restHelper.PostRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var result = apiResponse.Content.SpreadsheetComputations.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationCreateAsync Success" });
                    return result[0];
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationCreateAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"ComputationCreateAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<bool> ComputationDeleteAsync(string operationTwinReferenceId, EnumWorksheet worksheetType, string id)
        {

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            try
            {
                var respContent = await _restHelper.DeleteRestJSONAsync(requestId, $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/computation/{id}?requestId={requestId}").ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationDeleteAsync Success" });
                else
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationDeleteAsync Failed" });
                return respContent.ResponseMessage.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"ComputationDeleteAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<SpreadsheetComputation> ComputationExecuteAsync(string operationTwinReferenceId, EnumWorksheet worksheetType, uint startRow, uint endRow, DataSourceBinding dataSourceBinding)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/execute?startRow={startRow}&endRow={endRow}&requestId={requestId}";
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(dataSourceBinding, jsonSettings);

            try
            {
                var respContent = await _restHelper.PostRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var result = apiResponse.Content.SpreadsheetComputations.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationCreateAsync Success" });
                    return result[0];
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationCreateAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"ComputationCreateAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<List<SpreadsheetComputation>> ComputationGetManyAsync(string twinReferenceId, EnumWorksheet worksheetType)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{twinReferenceId}/worksheet/{(int)worksheetType}/computation?requestId={requestId}"; ;

            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.SpreadsheetComputations.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationGetManyAsync Success" });
                    return result;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationGetManyAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"ComputationGetManyAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<SpreadsheetComputation> ComputationGetOneAsync(string twinReferenceId, EnumWorksheet worksheetType, string id)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{twinReferenceId}/worksheet/{(int)worksheetType}/computation/{id}?requestId={requestId}"; ;

            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.SpreadsheetComputations.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationGetOneAsync Success" });
                    if (result.Count == 1)
                        return result[0];
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationGetOneAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"ComputationGetOneAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<SpreadsheetComputation> ComputationUpdateAsync(string twinReferenceId, EnumWorksheet worksheetType, SpreadsheetComputation spreadsheetComputation)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{twinReferenceId}/worksheet/{(int)worksheetType}/computation/{spreadsheetComputation.Computation.Id}?requestId={requestId}";

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(spreadsheetComputation, jsonSettings);

            try
            {
                var respContent = await _restHelper.PutRestJSONAsync(requestId, json.ToString(), endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.SpreadsheetComputations.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationGetOneAsync Success" });
                    if (result.Count == 1)
                        return result[0];
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationUpdateAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"ComputationUpdateAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<List<ApiError>> ComputationValidateAsync(string operationTwinReferenceId, EnumWorksheet worksheetType, SpreadsheetComputation spreadsheetComputation)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/computation/validate?requestId={requestId}";
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(spreadsheetComputation, jsonSettings);

            try
            {
                var respContent = await _restHelper.PostRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var result = apiResponse.Errors.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationCreateAsync Success" });
                    return result;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"ComputationCreateAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"ComputationCreateAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<bool> DeletePlantAsync(string operationTwinReferenceId)
        {

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            try
            {
                var respContent = await _restHelper.DeleteRestJSONAsync(requestId, $"operations/spreadsheet/v1/{operationTwinReferenceId}/plant").ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"DeletePlantAsync Success" });
                else
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"DeletePlantAsync Failed" });
                return respContent.ResponseMessage.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"DeletePlantAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<bool> FlushPlantAsync(string operationTwinReferenceId)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/plant/flush?requestId={requestId}";
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = "";

            try
            {
                var respContent = await _restHelper.PostRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    /*
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var result = apiResponse.Content.SpreadsheetComputations.Items.Select(x => x).ToList();
                    */
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"PlantFlushAsync Success" });
                    return true;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"PlantFlushAsync Failed" });
                return false;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"PlantFlushAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<Rows> GetRowsAsync(string twinReferenceId, EnumWorksheet worksheetType, uint startRow, uint endRow, string columnList = null, string viewId = null)
        {
            return await GetSpreadsheetRowsAsync(twinReferenceId, worksheetType, startRow, endRow, columnList, viewId);
        }
        public async Task<Rows> GetSpreadsheetRowsAsync(string twinReferenceId, EnumWorksheet worksheetType, uint startRow, uint endRow, string columnList = null, string viewId = null)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{twinReferenceId}/worksheet/{(int)worksheetType}/rows?startRow={startRow}&endRow={endRow}&columns={columnList}&viewid={viewId}";
            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetRowsAsync Success" });
                    return apiResponse.Content.Rows;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetRowsAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"GetRowsAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<Rows> GetRowsByDayAsync(string twinReferenceId, EnumWorksheet worksheetType, DateTime date)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{twinReferenceId}/worksheet/{(int)worksheetType}/rows/byday/{date.Year}/{date.Month}/{date.Day}";
            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetRowsAsync Success" });
                    return apiResponse.Content.Rows;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetRowsAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"GetRowsAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<Rows> GetRowsByMonthAsync(string twinReferenceId, EnumWorksheet worksheetType, DateTime date)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{twinReferenceId}/worksheet/{(int)worksheetType}/rows/bymonth/{date.Year}/{date.Month}";
            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetRowsAsync Success" });
                    return apiResponse.Content.Rows;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetRowsAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"GetRowsAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<SpreadsheetDefinition> GetSpreadsheetDefinitionAsync(string twinReferenceId)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{twinReferenceId}/definition?requestId={requestId}";

            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.SpreadsheetDefinitions.Items.Select(x => x).ToList();
                    if (result.Count == 1)
                    {
                        Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetSpreadsheetDefinitionAsync Success" });
                        return result[0];
                    }
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetSpreadsheetDefinitionAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"GetSpreadsheetDefinition Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<WorksheetDefinition> GetWorksheetDefinitionAsync(string twinReferenceId, EnumWorksheet worksheetType)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var worksheetDefinitionEndpoint = $"operations/spreadsheet/v1/{twinReferenceId}/worksheet/{(int)worksheetType}/definition?requestId={requestId}";

            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, worksheetDefinitionEndpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.WorksheetDefinitions.Items.Select(x => x).ToList();
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinitionAsync Success" });
                    return result[0];
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinitionAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"GetWorksheetDefinition Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<RowIndices> GetRowIndexesAsync(string operationTwinReferenceId, EnumWorksheet worksheetType, string relativeTime, DateTime utcTime, bool isInSpeed, bool isRowCooked, bool isColumnsCooked)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/index?relativeTime={relativeTime}&utcTime={utcTime}&isInSpeed={isInSpeed}&isRowCooked={isRowCooked}&isColumnsCooked={isColumnsCooked}&requestId={requestId}";

            try
            {
                var respContent = await _restHelper.GetRestJSONAsync(requestId, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.RowIndices;
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetSpreadsheetDefinitionAsync Success" });
                    return result;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"GetSpreadsheetDefinitionAsync Failed" });
                return null;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"GetSpreadsheetDefinition Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<bool> SaveRowsAsync(Rows rows, string operationTwinReferenceId, EnumWorksheet worksheetType)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/rows?requestId={requestId}";
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(rows, jsonSettings);

            try
            {
                var respContent = await _restHelper.PostRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"SaveRowsAsync Success" });
                    return true;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"SaveRowsAsync Failed" });
                return false;
            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"SaveRowsAsync Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<bool> SaveSpreadsheetDefinitionAsync(string operationTwinReferenceId, SpreadsheetDefinition spreadsheetDefinition)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/definition?requestId={requestId}";
            var json = JsonConvert.SerializeObject(spreadsheetDefinition);
            try
            {
                var respContent = await _restHelper.PostRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"SaveSpreadsheetDefinitionAsync Success" });
                    return true;
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"SaveSpreadsheetDefinitionAsync Failed" });
                return false;

            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"SaveSpreadsheetDefinition Failed - {e.Message}" });
                throw;
            }
        }
        public async Task<WorksheetDefinition> WorksheetAddColumnAsync(string operationTwinReferenceId, EnumWorksheet worksheetType, WorksheetDefinition worksheetDefinition)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/definition/columns?requestId={requestId}";
            var json = JsonConvert.SerializeObject(worksheetDefinition);
            try
            {
                var respContent = await _restHelper.PostRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"SaveWorksheetDefinitionAsync Success" });
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.WorksheetDefinitions.Items.Select(x => x).ToList();
                    return result[0];
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"SaveWorksheetDefinitionAsync Failed" });
                return null;

            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"SaveWorksheetDefinition Failed - {e.Message}" });
                throw;
            }
        }

        public async Task<WorksheetDefinition> WorksheetUpdateColumnAsync(string operationTwinReferenceId, EnumWorksheet worksheetType, WorksheetDefinition worksheetDefinition)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var requestId = Guid.NewGuid();
            var endpoint = $"operations/spreadsheet/v1/{operationTwinReferenceId}/worksheet/{(int)worksheetType}/definition/columns?requestId={requestId}";
            var json = JsonConvert.SerializeObject(worksheetDefinition);
            try
            {
                var respContent = await _restHelper.PutRestJSONAsync(requestId, json, endpoint).ConfigureAwait(_continueOnCapturedContext);
                if (respContent.ResponseMessage.IsSuccessStatusCode)
                {
                    Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Trace, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"SaveWorksheetDefinitionAsync Success" });
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(respContent.Result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = apiResponse.Content.WorksheetDefinitions.Items.Select(x => x).ToList();
                    return result[0];
                }
                Event(null, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Warn, HttpStatusCode = respContent.ResponseMessage.StatusCode, ElapsedMs = watch.ElapsedMilliseconds, Module = "SpreadsheetApi", Message = $"SaveWorksheetDefinitionAsync Failed" });
                return null;

            }
            catch (Exception e)
            {
                Event(e, new ClientApiLoggerEventArgs { EventLevel = EnumEventLevel.Error, Module = "SpreadsheetApi", Message = $"SaveWorksheetDefinition Failed - {e.Message}" });
                throw;
            }
        }


    }
}
