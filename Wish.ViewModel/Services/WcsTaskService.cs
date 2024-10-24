using Wish.Model;
using Wish.Models.ImportDto;
using System;
using System.Collections.Generic;
using Wish.Service;
using WalkingTec.Mvvm.Mvc;
using System.Linq;
using Wish.TaskConfig.Model;
using Newtonsoft.Json;
using WalkingTec.Mvvm.Core;
using Wish.ViewModel.WcsCmd.SrmCmdVMs;
using WISH.Helper.Common;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.Reflection;
using Wish.ViewModel.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Wish.ViewModel.Interface.InterfaceSendBackVMs;


namespace Wish.Services
{
    public class WcsTaskService 
    {
        private WTMContext wtm;
        private void InitWTM()
        {
            var services = new ServiceCollection();
            try
            {
                services.AddWtmContextForConsole();
            }
            catch (Exception ex)
            {
                ContextService.Log.Error(ex);
            }
            ServiceProvider provider = services.BuildServiceProvider();
            wtm = provider.GetRequiredService<WTMContext>();
        }
        /// <summary>
        /// 获取堆垛机指令
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SrmTaskDto> GetSrmCmdAsync(GetSrmTaskDto input)
        {
            if (wtm == null)
            {
                InitWTM();
            }
            SrmCmdVM srmCmdVM = wtm.CreateVM<SrmCmdVM>();
            BusinessResult result = new BusinessResult();
            SrmTaskDto srmTaskDto = new SrmTaskDto();
            try
            {
                result = await srmCmdVM.GetSrmCmdInfoAsync(input);
                if (result.code == ResCode.OK)
                {
                    if (result.outParams != null)
                    {
                        srmTaskDto = CommonHelper.ConvertToEntity<SrmTaskDto>(result.outParams);
                    }
                    else
                    {
                        return null;
                    }
                    //List<ReturnTask> Datas = JsonConvert.DeserializeObject<List<ReturnTask>>(returnInfo.Data.ToString());
                }
                else
                {
                    return null;
                }
                return srmTaskDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取堆垛机出库指令
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SrmTaskDto> GetSrmOutCmdAsync(GetSrmTaskDto input)
        {
            if (wtm == null)
            {
                InitWTM();
            }
            SrmCmdVM srmCmdVM = wtm.CreateVM<SrmCmdVM>();
            BusinessResult result = new BusinessResult();
            SrmTaskDto srmTaskDto = new SrmTaskDto();
            try
            {
                result = await srmCmdVM.GetSrmOutCmdInfoAsync(input);
                if (result.code == ResCode.OK)
                {
                    if (result.outParams != null)
                    {
                        srmTaskDto = CommonHelper.ConvertToEntity<SrmTaskDto>(result.outParams);
                    }
                    else
                    {
                        return null;
                    }
                    //List<ReturnTask> Datas = JsonConvert.DeserializeObject<List<ReturnTask>>(returnInfo.Data.ToString());
                }
                else
                {
                    return null;
                }
                return srmTaskDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 处理堆垛机反馈信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> DealSrmTaskAsync(DealSrmTaskDto input)
        {
            if (wtm == null)
            {
                InitWTM();
            }
            SrmCmdVM srmCmdVM = wtm.CreateVM<SrmCmdVM>();
            BusinessResult result = new BusinessResult();
            try
            {
                result = await srmCmdVM.DealSrmTaskAsync(input, "");
                //if (result.code == ResCode.Error)
                //{
                //    throw new Exception(result.msg);
                //}
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 处理指令下发
        /// </summary>
        /// <param name="input"></param>
        public async Task<BusinessResult> DealCmdSendAsync(DealCmdSendDto input)
        {
            if (wtm == null)
            {
                InitWTM();
            }
            SrmCmdVM srmCmdVM = wtm.CreateVM<SrmCmdVM>();
            BusinessResult result = new BusinessResult();
            try
            {
                result = await srmCmdVM.DealCmdSendAsync(input, "");
                //if (result.code == ResCode.Error)
                //{
                //    throw new Exception(result.msg);
                //}
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
            return result;
        }
        
        /// <summary>
        /// 根据反馈的工单号生成接口请求信息
        /// </summary>
        /// <param name="input"></param>
        public async Task<BusinessResult> CreateOutRequest(string invoiceNo,string deviceNo)
        {
            if (wtm == null)
            {
                InitWTM();
            }
            var vm = wtm.CreateVM<InterfaceSendBackVM>();
            BusinessResult result = new BusinessResult();
            try
            {
                result = await vm.CreateOutRequest(invoiceNo, deviceNo);
                //if (result.code == ResCode.Error)
                //{
                //    throw new Exception(result.msg);
                //}
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取堆垛机指令
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task< SrmTaskDto> GetSrmCmdAsync1(GetSrmTaskDto input)
        {
            IDataContext dc = DCService.GetInstance().GetDC();
            if (dc.Database.CurrentTransaction == null)
            {
                dc.Database.BeginTransaction();
            }
            try
            {
                if (input == null)
                {
                    throw new Exception("获取堆垛机指令失败：入参为空");
                }
                if (string.IsNullOrWhiteSpace(input.deviceNo))
                {
                    throw new Exception("获取堆垛机指令失败：设备编号为空");
                }
                List<SrmCmd> srmTask = new List<SrmCmd>();
                SrmTaskDto result = new SrmTaskDto();

                srmTask = dc.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Exec_Status < 90).AsQueryable().ToList();
                if (srmTask.Any())
                {
                    int unFinish = srmTask.Where(x => x.Exec_Status > 0).Count();
                    if (unFinish > 0 && unFinish <= 1)
                    {
                        if (string.IsNullOrWhiteSpace(input.taskNo))
                        {
                            throw new Exception($"获取堆垛机指令失败：当前设备{input.deviceNo}存在未完成的指令，且堆垛机对应的指令号为空");
                        }
                        var query = new List<SrmCmd>();
                        if (srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 5).Any())
                        {
                            query = srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 5).ToList();
                        }
                        else if (srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 20).Any())
                        {
                            query = srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 20).ToList();
                        }
                        else if (srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 25).Any())
                        {
                            query = srmTask.Where(x => x.Exec_Status > 0 && x.Exec_Status == 25).ToList();
                        }
                        else
                        {
                            throw new Exception($"获取堆垛机指令失败：当前设备{input.deviceNo}不存在未完成交互的指令");
                        }
                        result = query.Select(x => new SrmTaskDto
                        {
                            TaskNo = x.SubTask_No,
                            SerialNo = x.Serial_No,
                            ActionType = x.Task_Cmd,
                            TaskType = x.Task_Type,
                            PalletBarcode = x.Pallet_Barcode,
                            SourceRow = x.From_ForkDirection,
                            SourceColumn = x.From_Column,
                            SourceLayer = x.From_Layer,
                            SourceStationNo = x.From_Station,
                            TargetStationNo = x.To_Station,
                            TargetRow = x.To_ForkDirection,
                            TargetColumn = x.To_Column,
                            TargetLayer = x.To_Layer,
                            AlarmCode = 0,
                            Station = 0,
                            Sign = 0,
                            CheckPoint = x.Check_Point,
                        }).FirstOrDefault();
                    }
                    else if (unFinish > 1)
                    {
                        throw new Exception($"获取堆垛机指令失败：当前设备{input.deviceNo}存在多条未完成的指令");
                    }
                    else if (unFinish == 0)
                    {
                        result = srmTask.Where(x => x.Exec_Status == 0).OrderBy(x => x.ID).Select(x => new SrmTaskDto
                        {
                            TaskNo = x.SubTask_No,
                            SerialNo = x.Serial_No,
                            ActionType = x.Task_Cmd,
                            TaskType = x.Task_Type,
                            PalletBarcode = x.Pallet_Barcode,
                            SourceRow = x.From_ForkDirection,
                            SourceColumn = x.From_Column,
                            SourceLayer = x.From_Layer,
                            SourceStationNo = x.From_Station,
                            TargetStationNo = x.To_Station,
                            TargetRow = x.To_ForkDirection,
                            TargetColumn = x.To_Column,
                            TargetLayer = x.To_Layer,
                            AlarmCode = 0,
                            Station = 0,
                            Sign = 0,
                            CheckPoint = x.Check_Point,
                        }).FirstOrDefault();
                    }
                }
                else
                {
                    throw new Exception($"获取堆垛机指令失败：当前设备{input.deviceNo}没有指令");
                }
                if (dc.Database.CurrentTransaction != null)
                {
                    dc.Database.CommitTransaction();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (dc.Database.CurrentTransaction != null)
                {
                    dc.Database.RollbackTransaction();
                }
                throw ex;
            }
        }
        /// <summary>
        /// 处理堆垛机反馈信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DealSrmTaskAsync1(DealSrmTaskDto input)
        {
            IDataContext dc = DCService.GetInstance().GetDC();
            if (dc.Database.CurrentTransaction == null)
            {
                dc.Database.BeginTransaction();
            }
            try
            {
                //SrmTaskDto result = new SrmTaskDto();
                if (input == null)
                {
                    throw new Exception("处理堆垛机反馈失败：入参为空");
                }
                if (string.IsNullOrWhiteSpace(input.deviceNo))
                {
                    throw new Exception("处理堆垛机反馈失败：设备编号为空");
                }
                if (input.actionType == null)
                {
                    throw new Exception("处理堆垛机反馈失败：动作类型为空");
                }
                if (input.palletBarcode == null)
                {
                    throw new Exception("处理堆垛机反馈失败：托盘码为空");
                }
                List<int> actionDealTypes = new List<int>() { 14, 15, 16, 19, 20 };
                if (actionDealTypes.Contains((int)input.actionType))
                {
                    //if (input.taskNo==0 || input.taskNo==null)
                    //{
                    //    throw new Exception($"处理堆垛机反馈失败：动作类型为{JsonConvert.SerializeObject(actionDealTypes)}时，堆垛机反馈的任务号为空");
                    //}
                }
                if (input.checkPoint == null)
                {
                    throw new Exception("处理堆垛机反馈失败：检验点为空");
                }

                /*处理逻辑
                 * 1、处理带有任务号的反馈，根据任务号，设备编码，托盘号，动作类型进行处理指令；
                 * （1）完成取货完成，再次下发放货任务；
                 * （2）完成放货任务，结束指令；
                 * （3）异常处理。
                 * 2、处理没有任务号的反馈，根据设备编码，托盘号，动作类型进行处理反馈；
                 * （1）申请任务，调用创建指令逻辑。
                 */
                if (input.palletBarcode != 0)
                    //if (string.IsNullOrWhiteSpace(input.palletBarcode))
                {
                    var srmCmd = dc.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Pallet_Barcode == input.palletBarcode).FirstOrDefault();
                    if (srmCmd == null)
                    {
                        throw new Exception($"处理堆垛机反馈失败：根据设备编码:{input.deviceNo}，托盘号：{input.palletBarcode}，流水号:{input.taskNo}，检验点：{input.checkPoint}；查询指令为空");
                    }
                    //取货完成
                    if (input.actionType == 19)
                    {
                        srmCmd.Task_Cmd = 6;//放货
                        srmCmd.Exec_Status = 20;
                        srmCmd.Pick_Date = DateTime.Now;
                        dc.UpdateEntity(srmCmd);
                        dc.SaveChanges();

                    }
                    //放货完成
                    else if (input.actionType == 20)
                    {
                        srmCmd.Exec_Status = 40;
                        srmCmd.Pick_Date = DateTime.Now;
                        dc.UpdateEntity(srmCmd);
                        dc.SaveChanges();
                        //TODO:指令转历史，处理WMS任务，WMS单据，WMS库存
                    }
                    else if (input.actionType == 0)//拒绝任务
                    {
                        //TODO:暂停未发送的指令，如已有发送的指令则不处理该动作
                    }
                    else
                    {
                        throw new Exception($"处理堆垛机反馈失败：暂无其他反馈逻辑，动作代码{input.actionType}");
                    }
                }
                else
                {
                    //入库申请
                    if (input.actionType == 5)
                    {
                        //TODO:查找WMS任务并生成堆垛机指令
                    }
                    else if (input.actionType == 7)//出库申请
                    {
                        //TODO:查找WMS任务并生成出库堆垛机指令
                    }
                    else if (input.actionType == 0)//拒绝任务
                    {
                        //TODO:暂停未发送的指令，如已有发送的指令则不处理该动作
                    }
                    else
                    {
                        throw new Exception($"处理堆垛机反馈失败：暂无其他申请逻辑，动作代码{input.actionType}");
                    }
                }
                if (dc.Database.CurrentTransaction != null)
                {
                    dc.Database.CommitTransaction();
                }
                //return result ;
            }
            catch (Exception ex)
            {
                if (dc.Database.CurrentTransaction != null)
                {
                    dc.Database.RollbackTransaction();
                }
                throw ex;
            }

        }

        /// <summary>
        /// 处理指令下发
        /// </summary>
        /// <param name="input"></param>
        public async Task DealCmdSendAsync1(DealCmdSendDto input)
        {
            IDataContext dc = DCService.GetInstance().GetDC();
            if (dc.Database.CurrentTransaction == null)
            {
                dc.Database.BeginTransaction();
            }
            try
            {
                if (input == null)
                {
                    throw new Exception("处理下发指令失败：入参为空");
                }
                if (string.IsNullOrWhiteSpace(input.deviceNo))
                {
                    throw new Exception("处理下发指令失败：设备编号为空");
                }
                if (input.palletBarcode == null)
                {
                    throw new Exception("处理下发指令失败：托盘码为空");
                }
                if (input.checkPoint == null)
                {
                    throw new Exception("处理下发指令失败：检验点为空");
                }

                var srmCmd = dc.Set<SrmCmd>().Where(x => x.Device_No == input.deviceNo && x.Pallet_Barcode == input.palletBarcode && x.Exec_Status < 30).FirstOrDefault();
                if (srmCmd == null)
                {
                    throw new Exception($"处理下发指令失败：根据设备编码:{input.deviceNo}，托盘号：{input.palletBarcode}，流水号:{input.taskNo}，检验点：{input.checkPoint}；查询指令为空");
                }
                if (srmCmd.Exec_Status == 0)
                {
                    srmCmd.Exec_Status = 5;
                }
                else if (srmCmd.Exec_Status == 5)
                {
                    srmCmd.Exec_Status = 10;
                    srmCmd.Begin_Date = DateTime.Now;
                }
                else if (srmCmd.Exec_Status == 20)
                {
                    srmCmd.Exec_Status = 25;
                }
                else if (srmCmd.Exec_Status == 25)
                {
                    srmCmd.Exec_Status = 30;
                }
                srmCmd.UpdateTime = DateTime.Now;
                srmCmd.UpdateBy = "WCS";
                dc.UpdateEntity(srmCmd);
                dc.SaveChanges();
                if (dc.Database.CurrentTransaction != null)
                {
                    dc.Database.CommitTransaction();
                }

            }
            catch (Exception ex)
            {
                if (dc.Database.CurrentTransaction != null)
                {
                    dc.Database.RollbackTransaction();
                }
                throw ex;
            }


        }
    }
}
