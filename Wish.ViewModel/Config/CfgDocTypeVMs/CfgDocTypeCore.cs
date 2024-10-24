using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;
using Microsoft.EntityFrameworkCore;
using WISH.Helper.Common;
using Wish.ViewModel.Config.CfgDocTypeDtlVMs;
using Wish.ViewModel.Common.Dtos;
using WISH.Helper.Common.Dictionary.DictionaryHelper;

namespace Wish.ViewModel.Config.CfgDocTypeVMs
{
    public partial class CfgDocTypeVM
    {
        /// <summary>
        /// 根据单据类型编号 获取 单据类型对象
        /// </summary>
        /// <param name="docTypeCode"></param>
        /// <returns></returns>
        public CfgDocTypeDto GetCfgDocType(string docTypeCode)
        {
            CfgDocTypeDto cfgDocTypeView = new CfgDocTypeDto();
            CfgDocType cfgDocType = DC.Set<CfgDocType>().Where(x => x.docTypeCode == docTypeCode).FirstOrDefault();
            cfgDocTypeView.cfgDocType = cfgDocType;

            if (cfgDocType != null)
            {
                List<CfgDocTypeDtlDto> cfgDocTypeDtlViews = DC.Set<CfgDocTypeDtl>().Where(x => x.docTypeCode == docTypeCode)
                    .Select(x => new CfgDocTypeDtlDto { cfgDocTypeDtl = x }).ToList();

                List<CfgBusiness> cfgBusiness = DC.Set<CfgBusiness>().ToList();
                List<CfgBusinessModule> cfgBusinessModule = DC.Set<CfgBusinessModule>().ToList();
                List<CfgBusinessParam> cfgBusinessParam = DC.Set<CfgBusinessParam>().ToList();
                List<CfgBusinessParamValue> cfgBusinessParamValue = DC.Set<CfgBusinessParamValue>().ToList();
                foreach (var item in cfgDocTypeDtlViews)
                {
                    item.cfgBusiness = cfgBusiness.Where(x => x.businessCode == item.cfgDocTypeDtl.businessCode).FirstOrDefault();
                    item.cfgBusinessModule = cfgBusinessModule.Where(x => x.businessModuleCode == item.cfgDocTypeDtl.businessModuleCode)
                        .FirstOrDefault();
                    item.cfgBusinessParam = cfgBusinessParam.Where(x => x.paramCode == item.cfgDocTypeDtl.paramCode).FirstOrDefault();
                    //item.cfgBusinessParamValue = DC.Set<CfgBusinessParamValue>().Where(x => x.paramValueCode == item.cfgDocTypeDtl.paramValueCode).FirstOrDefault();
                    item.cfgBusinessParamValue = cfgBusinessParamValue
                        .Where(x => x.paramValueCode == item.cfgDocTypeDtl.paramValueCode && x.paramCode == item.cfgDocTypeDtl.paramCode)
                        .FirstOrDefault();
                }

                cfgDocTypeView.cfgDocTypeDtls = cfgDocTypeDtlViews;
            }

            return cfgDocTypeView;
        }

        public async Task<CfgDocTypeDto> GetCfgDocTypeAsync(string docTypeCode)
        {
            CfgDocTypeDto cfgDocTypeView = new CfgDocTypeDto();
            CfgDocType cfgDocType = await DC.Set<CfgDocType>().Where(x => x.docTypeCode == docTypeCode).FirstOrDefaultAsync();
            cfgDocTypeView.cfgDocType = cfgDocType;

            if (cfgDocType != null)
            {
                List<CfgDocTypeDtlDto> cfgDocTypeDtlViews = await DC.Set<CfgDocTypeDtl>().Where(x => x.docTypeCode == docTypeCode)
                    .Select(x => new CfgDocTypeDtlDto { cfgDocTypeDtl = x }).ToListAsync();

                List<CfgBusiness> cfgBusiness = await DC.Set<CfgBusiness>().ToListAsync();
                List<CfgBusinessModule> cfgBusinessModule = await DC.Set<CfgBusinessModule>().ToListAsync();
                List<CfgBusinessParam> cfgBusinessParam = await DC.Set<CfgBusinessParam>().ToListAsync();
                List<CfgBusinessParamValue> cfgBusinessParamValue = await DC.Set<CfgBusinessParamValue>().ToListAsync();
                foreach (var item in cfgDocTypeDtlViews)
                {
                    item.cfgBusiness = cfgBusiness.Where(x => x.businessCode == item.cfgDocTypeDtl.businessCode).FirstOrDefault();
                    item.cfgBusinessModule = cfgBusinessModule.Where(x => x.businessModuleCode == item.cfgDocTypeDtl.businessModuleCode)
                        .FirstOrDefault();
                    item.cfgBusinessParam = cfgBusinessParam.Where(x => x.paramCode == item.cfgDocTypeDtl.paramCode).FirstOrDefault();
                    //item.cfgBusinessParamValue = DC.Set<CfgBusinessParamValue>().Where(x => x.paramValueCode == item.cfgDocTypeDtl.paramValueCode).FirstOrDefault();
                    item.cfgBusinessParamValue = cfgBusinessParamValue
                        .Where(x => x.paramValueCode == item.cfgDocTypeDtl.paramValueCode && x.paramCode == item.cfgDocTypeDtl.paramCode)
                        .FirstOrDefault();
                }

                cfgDocTypeView.cfgDocTypeDtls = cfgDocTypeDtlViews;
            }

            return cfgDocTypeView;
        }
        /// <summary>
        /// 创建入库单据类型10
        /// </summary>
        /// <param name="docTypeCode"></param>
        /// <returns></returns>
        public BusinessResult CreateInOrderCfgDocType(string docTypeCode, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string _vpoint = "";
            try
            {
                string yesCode = DictonaryHelper.YesNoCode.Yes.GetCode();
                string noCode = DictonaryHelper.YesNoCode.No.GetCode();
                string businessCode = DictonaryHelper.BusinessCode.In.GetCode();
                string businessModuleCode = DictonaryHelper.BusinessModuleCode.InOrder.GetCode();
                //string autoReceiptParamCode = DictonaryHelper.InOrderDocTypeParam.AutoReceipt.GetCode();
                //string generateUnnicodeParamCode = DictonaryHelper.InOrderDocTypeParam.GenerateUnnicode.GetCode();
                //string printParamCode = DictonaryHelper.InOrderDocTypeParam.Print.GetCode();
                //string autoReceiptParamValueCode = DictonaryHelper.BusinessParamValueYesNoValueCode.Yes.GetCode();
                //string generateUnnicodeParamValueCode = DictonaryHelper.BusinessParamValueYesNoValueCode.No.GetCode();
                //string printParamValueCode = DictonaryHelper.BusinessParamValueYesNoValueCode.No.GetCode();


                _vpoint = "检查在单据类型表中是否已存在";
                var flag = DC.Set<CfgDocType>().Where(x => x.docTypeCode == docTypeCode).Any();
                if (flag == true)
                {
                    throw new Exception($"单据类型 {docTypeCode} 已存在, 不可重复创建!");
                }

                _vpoint = $"检查 CfgBusiness 中 {businessCode} 参数是否已配置";
                flag = DC.Set<CfgBusiness>().Where(x => x.businessCode == businessCode).Any();
                if (flag == false)
                {
                    throw new Exception($"CfgBusiness 中未配置 {businessCode} 参数信息!");
                }

                _vpoint = $"检查 CfgBusinessModule 中 {businessModuleCode} 参数是否已配置";
                flag = DC.Set<CfgBusinessModule>().Where(x => x.businessCode == businessModuleCode && x.businessCode == businessCode).Any();
                if (flag == false)
                {
                    throw new Exception($"CfgBusinessModule 中未配置 {businessModuleCode} 参数信息!");
                }

                //_vpoint = $"检查 CfgBusinessParam 中参数是否已配置";
                //flag = DC.Set<CfgBusinessParam>()
                //    .Where(x => x.paramCode == autoReceiptParamCode || x.paramCode == generateUnnicodeParamCode || x.paramCode == printParamCode).ToList().Count == 3;
                //if (flag == false)
                //{
                //    throw new Exception($"CfgBusinessParam 中未配置 {autoReceiptParamCode} | {generateUnnicodeParamCode} | {printParamCode} 参数信息!");
                //}

                //_vpoint = $"检查 CfgBusinessParamValue 中参数是否已配置";
                //flag = DC.Set<CfgBusinessParamValue>()
                //    .Where(x => (x.paramCode == autoReceiptParamCode && x.paramValueCode == autoReceiptParamValueCode) 
                //                || (x.paramCode == generateUnnicodeParamCode && x.paramValueCode == generateUnnicodeParamValueCode) 
                //                || (x.paramCode == generateUnnicodeParamCode && x.paramValueCode == printParamValueCode)).ToList().Count == 3;
                //if (flag == false)
                //{
                //    throw new Exception($"CfgBusinessParamValue 中未配置 {autoReceiptParamCode} | {generateUnnicodeParamCode} | {printParamCode} 参数信息!");
                //}

                _vpoint = $"创建单据信息";
                CfgDocType cfgDocType = new CfgDocType();
                //cfgDocType.ID = Guid.NewGuid().ToString();
                cfgDocType.docTypeCode = docTypeCode;
                cfgDocType.externalDocTypeCode = docTypeCode;
                cfgDocType.docTypeName = docTypeCode;
                cfgDocType.docTypeDesc = docTypeCode;
                cfgDocType.docTypeNameEn = docTypeCode;
                cfgDocType.docTypeNameAlias = docTypeCode;
                cfgDocType.businessCode = DictonaryHelper.BusinessCode.In.GetCode();

                //// 自动收货
                //CfgDocTypeDtl autoReceiptCfgDocTypeDtl = new CfgDocTypeDtl();
                //autoReceiptCfgDocTypeDtl.ID = Guid.NewGuid().ToString();
                //autoReceiptCfgDocTypeDtl.docTypeCode = docTypeCode;
                //autoReceiptCfgDocTypeDtl.businessCode = cfgDocType.businessCode;
                //autoReceiptCfgDocTypeDtl.businessModuleCode = businessModuleCode;
                //autoReceiptCfgDocTypeDtl.paramCode = autoReceiptParamCode;
                //autoReceiptCfgDocTypeDtl.paramValueCode = autoReceiptParamValueCode;

                //// 自动收货
                //CfgDocTypeDtl genUnnicodeCfgDocTypeDtl = new CfgDocTypeDtl();
                //genUnnicodeCfgDocTypeDtl.ID = Guid.NewGuid().ToString();
                //genUnnicodeCfgDocTypeDtl.docTypeCode = docTypeCode;
                //genUnnicodeCfgDocTypeDtl.businessCode = cfgDocType.businessCode;
                //genUnnicodeCfgDocTypeDtl.businessModuleCode = businessModuleCode;
                //genUnnicodeCfgDocTypeDtl.paramCode = generateUnnicodeParamCode;
                //genUnnicodeCfgDocTypeDtl.paramValueCode = generateUnnicodeParamValueCode;

                //// 自动收货
                //CfgDocTypeDtl printCfgDocTypeDtl = new CfgDocTypeDtl();
                //printCfgDocTypeDtl.ID = Guid.NewGuid().ToString();
                //printCfgDocTypeDtl.docTypeCode = docTypeCode;
                //printCfgDocTypeDtl.businessCode = cfgDocType.businessCode;
                //printCfgDocTypeDtl.businessModuleCode = businessModuleCode;
                //printCfgDocTypeDtl.paramCode = printParamCode;
                //printCfgDocTypeDtl.paramValueCode = printParamValueCode;
                cfgDocType.CreateBy = invoker;
                cfgDocType.CreateTime = DateTime.Now;
                DC.AddEntity(cfgDocType);
                //DC.AddEntity(autoReceiptCfgDocTypeDtl);
                //DC.AddEntity(genUnnicodeCfgDocTypeDtl);
                //DC.AddEntity(printCfgDocTypeDtl);
                DC.SaveChanges();
            }
            catch (Exception ex)
            {
                result.code = DictonaryHelper.ResCode.Error;
                result.msg = $"CfgDocTypeApiVM->CreateInOrderCfgDocType 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
            }

            return result;
        }
        public async Task<BusinessResult> CreateInOrderCfgDocTypeAsync(string docTypeCode, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string _vpoint = "";
            try
            {
                string yesCode = DictonaryHelper.YesNoCode.Yes.GetCode();
                string noCode = DictonaryHelper.YesNoCode.No.GetCode();
                string businessCode = DictonaryHelper.BusinessCode.In.GetCode();
                string businessModuleCode = DictonaryHelper.BusinessModuleCode.InOrder.GetCode();
                //string autoReceiptParamCode = DictonaryHelper.InOrderDocTypeParam.AutoReceipt.GetCode();
                //string generateUnnicodeParamCode = DictonaryHelper.InOrderDocTypeParam.GenerateUnnicode.GetCode();
                //string printParamCode = DictonaryHelper.InOrderDocTypeParam.Print.GetCode();
                //string autoReceiptParamValueCode = DictonaryHelper.BusinessParamValueYesNoValueCode.Yes.GetCode();
                //string generateUnnicodeParamValueCode = DictonaryHelper.BusinessParamValueYesNoValueCode.No.GetCode();
                //string printParamValueCode = DictonaryHelper.BusinessParamValueYesNoValueCode.No.GetCode();


                _vpoint = "检查在单据类型表中是否已存在";
                var flag = await DC.Set<CfgDocType>().Where(x => x.docTypeCode == docTypeCode).AnyAsync();
                if (flag == true)
                {
                    throw new Exception($"单据类型 {docTypeCode} 已存在, 不可重复创建!");
                }

                _vpoint = $"检查 CfgBusiness 中 {businessCode} 参数是否已配置";
                flag = await DC.Set<CfgBusiness>().Where(x => x.businessCode == businessCode).AnyAsync();
                if (flag == false)
                {
                    throw new Exception($"CfgBusiness 中未配置 {businessCode} 参数信息!");
                }

                _vpoint = $"检查 CfgBusinessModule 中 {businessModuleCode} 参数是否已配置";
                flag = await DC.Set<CfgBusinessModule>().Where(x => x.businessCode == businessModuleCode && x.businessCode == businessCode).AnyAsync();
                if (flag == false)
                {
                    throw new Exception($"CfgBusinessModule 中未配置 {businessModuleCode} 参数信息!");
                }

                //_vpoint = $"检查 CfgBusinessParam 中参数是否已配置";
                //flag = DC.Set<CfgBusinessParam>()
                //    .Where(x => x.paramCode == autoReceiptParamCode || x.paramCode == generateUnnicodeParamCode || x.paramCode == printParamCode).ToList().Count == 3;
                //if (flag == false)
                //{
                //    throw new Exception($"CfgBusinessParam 中未配置 {autoReceiptParamCode} | {generateUnnicodeParamCode} | {printParamCode} 参数信息!");
                //}

                //_vpoint = $"检查 CfgBusinessParamValue 中参数是否已配置";
                //flag = DC.Set<CfgBusinessParamValue>()
                //    .Where(x => (x.paramCode == autoReceiptParamCode && x.paramValueCode == autoReceiptParamValueCode) 
                //                || (x.paramCode == generateUnnicodeParamCode && x.paramValueCode == generateUnnicodeParamValueCode) 
                //                || (x.paramCode == generateUnnicodeParamCode && x.paramValueCode == printParamValueCode)).ToList().Count == 3;
                //if (flag == false)
                //{
                //    throw new Exception($"CfgBusinessParamValue 中未配置 {autoReceiptParamCode} | {generateUnnicodeParamCode} | {printParamCode} 参数信息!");
                //}

                _vpoint = $"创建单据信息";
                CfgDocType cfgDocType = new CfgDocType();
                //cfgDocType.ID = Guid.NewGuid().ToString();
                cfgDocType.docTypeCode = docTypeCode;
                cfgDocType.externalDocTypeCode = docTypeCode;
                cfgDocType.docTypeName = docTypeCode;
                cfgDocType.docTypeDesc = docTypeCode;
                cfgDocType.docTypeNameEn = docTypeCode;
                cfgDocType.docTypeNameAlias = docTypeCode;
                cfgDocType.businessCode = DictonaryHelper.BusinessCode.In.GetCode();

                //// 自动收货
                //CfgDocTypeDtl autoReceiptCfgDocTypeDtl = new CfgDocTypeDtl();
                //autoReceiptCfgDocTypeDtl.ID = Guid.NewGuid().ToString();
                //autoReceiptCfgDocTypeDtl.docTypeCode = docTypeCode;
                //autoReceiptCfgDocTypeDtl.businessCode = cfgDocType.businessCode;
                //autoReceiptCfgDocTypeDtl.businessModuleCode = businessModuleCode;
                //autoReceiptCfgDocTypeDtl.paramCode = autoReceiptParamCode;
                //autoReceiptCfgDocTypeDtl.paramValueCode = autoReceiptParamValueCode;

                //// 自动收货
                //CfgDocTypeDtl genUnnicodeCfgDocTypeDtl = new CfgDocTypeDtl();
                //genUnnicodeCfgDocTypeDtl.ID = Guid.NewGuid().ToString();
                //genUnnicodeCfgDocTypeDtl.docTypeCode = docTypeCode;
                //genUnnicodeCfgDocTypeDtl.businessCode = cfgDocType.businessCode;
                //genUnnicodeCfgDocTypeDtl.businessModuleCode = businessModuleCode;
                //genUnnicodeCfgDocTypeDtl.paramCode = generateUnnicodeParamCode;
                //genUnnicodeCfgDocTypeDtl.paramValueCode = generateUnnicodeParamValueCode;

                //// 自动收货
                //CfgDocTypeDtl printCfgDocTypeDtl = new CfgDocTypeDtl();
                //printCfgDocTypeDtl.ID = Guid.NewGuid().ToString();
                //printCfgDocTypeDtl.docTypeCode = docTypeCode;
                //printCfgDocTypeDtl.businessCode = cfgDocType.businessCode;
                //printCfgDocTypeDtl.businessModuleCode = businessModuleCode;
                //printCfgDocTypeDtl.paramCode = printParamCode;
                //printCfgDocTypeDtl.paramValueCode = printParamValueCode;
                cfgDocType.CreateBy = invoker;
                cfgDocType.CreateTime = DateTime.Now;
                DC.AddEntity(cfgDocType);
                //DC.AddEntity(autoReceiptCfgDocTypeDtl);
                //DC.AddEntity(genUnnicodeCfgDocTypeDtl);
                //DC.AddEntity(printCfgDocTypeDtl);
                DC.SaveChanges();
            }
            catch (Exception ex)
            {
                result.code = DictonaryHelper.ResCode.Error;
                result.msg = $"CfgDocTypeApiVM->CreateInOrderCfgDocType 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
            }

            return result;
        }

        /// <summary>
        /// 根据单据类型名称获取单据信息(用于校验外部接口)
        /// </summary>
        /// <param name="docTypeName"></param>
        /// <returns></returns>
        public CfgDocType GetCfgDocTypeByName(string docTypeName)
        {
            var cfgDocType = DC.Set<CfgDocType>().Where(x => x.docTypeName == docTypeName && x.usedFlag == 1).FirstOrDefault();
            return cfgDocType;
        }
        public async Task<CfgDocType> GetCfgDocTypeByNameAsync(string docTypeName)
        {
            var cfgDocType = await DC.Set<CfgDocType>().Where(x => x.docTypeName == docTypeName && x.usedFlag == 1).FirstOrDefaultAsync();
            return cfgDocType;
        }
        /// <summary>
        /// 根据单据类型名称或外部单据类型编码 匹配获取单据信息(用于校验外部接口)
        /// </summary>
        /// <param name="docTypeName"></param>
        /// <returns></returns>
        public CfgDocType GetCfgDocTypeByExternalName(string docTypeName)
        {
            var cfgDocType = DC.Set<CfgDocType>().Where(x => (x.externalDocTypeCode == docTypeName || x.docTypeName == docTypeName) && x.usedFlag == 1).FirstOrDefault();
            return cfgDocType;
        }
        public async Task<CfgDocType> GetCfgDocTypeByExternalNameAsync(string docTypeName)
        {
            var cfgDocType = await DC.Set<CfgDocType>().Where(x => (x.externalDocTypeCode == docTypeName || x.docTypeName == docTypeName) && x.usedFlag == 1).FirstOrDefaultAsync();
            return cfgDocType;
        }
        /// <summary>
        /// 根据单据类型名称和出入库类型获取单据类型（用于校验外部接口）
        /// </summary>
        /// <param name="docTypeName"></param>
        /// <returns></returns>
        public CfgDocType GetCfgDocTypeByNameAndIOType(string docTypeName, string businessCode)
        {
            var cfgDocType = DC.Set<CfgDocType>().Where(x => x.docTypeName == docTypeName && x.businessCode == businessCode && x.usedFlag == 1).FirstOrDefault();
            return cfgDocType;
        }
        public async Task<CfgDocType> GetCfgDocTypeByNameAndIOTypeAsync(string docTypeName, string businessCode)
        {
            var cfgDocType = await DC.Set<CfgDocType>().Where(x => x.docTypeName == docTypeName && x.businessCode == businessCode && x.usedFlag == 1).FirstOrDefaultAsync();
            return cfgDocType;
        }
        /// <summary>
        /// 根据单据类型编码获取单据信息(用于校验外部接口)
        /// </summary>
        /// <param name="docTypeCode"></param>
        /// <returns></returns>
        public CfgDocType GetCfgDocTypeByCode(string docTypeCode)
        {
            var cfgDocType = DC.Set<CfgDocType>().Where(x => x.docTypeCode == docTypeCode && x.usedFlag == 1).FirstOrDefault();
            return cfgDocType;
        }
        public async Task<CfgDocType> GetCfgDocTypeByCodeAsync(string docTypeCode)
        {
            var cfgDocType = await DC.Set<CfgDocType>().Where(x => x.docTypeCode == docTypeCode && x.usedFlag == 1).FirstOrDefaultAsync();
            return cfgDocType;
        }

        /// <summary>
        /// 根据单据类型编码和参数代码获取配置信息
        /// </summary>
        /// <param name="docTypeCode"></param>
        /// <param name="paramCode"></param>
        /// <returns></returns>
        public CfgDocTypeDtl GetDocTypeDtl(string docTypeCode, string paramCode)
        {
            CfgDocTypeDtl cfgDocTypeDtl = DC.Set<CfgDocTypeDtl>().Where(x => x.docTypeCode == docTypeCode && x.paramCode == paramCode).FirstOrDefault();
            return cfgDocTypeDtl;
        }

        /// <summary>
        /// 根据单据类型编码和参数代码获取配置信息
        /// </summary>
        /// <param name="docTypeCode"></param>
        /// <param name="paramCode"></param>
        /// <returns></returns>
        public async Task<CfgDocTypeDtl> GetDocTypeDtlAsync(string docTypeCode, string paramCode)
        {
            CfgDocTypeDtl cfgDocTypeDtl = await DC.Set<CfgDocTypeDtl>().Where(x => x.docTypeCode == docTypeCode && x.paramCode == paramCode).FirstOrDefaultAsync();
            return cfgDocTypeDtl;
        }

        /// <summary>
        /// 根据单据类型编码获取所有配置信息
        /// </summary>
        /// <param name="docTypeCode"></param>
        /// <returns></returns>
        public List<CfgDocTypeDtl> GetDocTypeDtlList(string docTypeCode)
        {
            return DC.Set<CfgDocTypeDtl>().Where(x => x.docTypeCode == docTypeCode).ToList();
        }

        public async Task<List<CfgDocTypeDtl>> GetDocTypeDtlListAsync(string docTypeCode)
        {
            return await DC.Set<CfgDocTypeDtl>().Where(x => x.docTypeCode == docTypeCode).ToListAsync();
        }

        public async Task<bool?> GetIsCheckStockByDocTypeCodeAsync(string docTypeCode)
        {
            var cfgDocDtl = await DC.Set<CfgDocTypeDtl>().Where(t => t.docTypeCode == docTypeCode && t.paramCode == "IS_CHECK_STOCK").FirstOrDefaultAsync();
            bool? isCheckStock = null;
            if (cfgDocDtl == null)
            {
                return null;
            }
            if (cfgDocDtl.paramValueCode == "1")
            {
                isCheckStock = true;
            }
            else
            {
                isCheckStock = false;
            }
            return isCheckStock;
        }
    }
}
