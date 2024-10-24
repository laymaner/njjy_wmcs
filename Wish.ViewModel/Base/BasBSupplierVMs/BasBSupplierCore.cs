using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;
using Microsoft.EntityFrameworkCore;


namespace Wish.ViewModel.Base.BasBSupplierVMs
{
    public partial class BasBSupplierVM
    {
        /// <summary>
        /// 检查供应商编号在供应商表里是否存在
        /// </summary>
        /// <param name="supplierCode"></param>
        /// <returns></returns>
        public bool IsSupplierExist(string supplierCode)
        {
            var flag = DC.Set<BasBSupplier>().Where(x => x.supplierCode == supplierCode).Any();
            return flag;
        }

        /// <summary>
        /// 根据供应商编号获取供应商信息
        /// </summary>
        /// <param name="supplierCode"></param>
        /// <returns></returns>
        public BasBSupplier GetSupplier(string supplierCode)
        {
            return DC.Set<BasBSupplier>().Where(x => x.supplierCode == supplierCode && x.usedFlag == 1).FirstOrDefault();
        }
        public async Task<BasBSupplier> GetSupplierAsync(string supplierCode)
        {
            return await DC.Set<BasBSupplier>().Where(x => x.supplierCode == supplierCode && x.usedFlag == 1).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据供应商编号集合查找对应所有供应商集合
        /// </summary>
        /// <param name="supplierCodes"></param>
        /// <returns></returns>
        public List<BasBSupplier> GetSupplierListByNos(List<string> supplierCodes)
        {
            return DC.Set<BasBSupplier>().Where(x => supplierCodes.Contains(x.supplierCode) && x.usedFlag == 1).ToList();
        }

        public async Task<List<BasBSupplier>> GetSupplierListByNosAsync(List<string> supplierCodes)
        {
            return await DC.Set<BasBSupplier>().Where(x => supplierCodes.Contains(x.supplierCode) && x.usedFlag == 1).ToListAsync();
        }
    }
}
