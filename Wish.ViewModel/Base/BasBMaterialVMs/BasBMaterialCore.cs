using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;
using Wish.ViewModel.Base.BasBDepartmentVMs;
using Microsoft.EntityFrameworkCore;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.Common.Dtos;

namespace Wish.ViewModel.Base.BasBMaterialVMs
{
    public partial class BasBMaterialVM
    {
        /// <summary>
        /// 检查物料编号在物料表里是否存在
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public bool IsMaterialExist(string materialCode)
        {
            var flag = DC.Set<BasBMaterial>().Where(x => x.MaterialCode == materialCode).Any();
            return flag;
        }
        public async Task<bool> IsMaterialExistAsync(string materialCode)
        {
            var flag = await DC.Set<BasBMaterial>().Where(x => x.MaterialCode == materialCode).AnyAsync();
            return flag;
        }

        /// <summary>
        /// 筛选出在物料表中不存在的物料
        /// </summary>
        /// <param name="maerialNos"></param>
        /// <returns></returns>
        public List<string> SelectNotExistMaterials(List<string> materialCodes)
        {
            var materials = DC.Set<BasBMaterial>();
            var query = from m in materials
                        where (materialCodes.Contains(m.MaterialCode))
                        select m.MaterialCode;
            var existMaterials = query.ToList();
            return materialCodes.Where(x => existMaterials.Contains(x) == false).ToList();
            //var objs = DC.Set<BasBMaterial>().ToList();
            //var basbMaterial = DC.Set<BasBMaterial>();
            //var query = from o in materialCodes
            //            where !(from m in basbMaterial select m.materialCode)
            //            .Contains(o)
            //            select o;
            //return query.ToList();
        }
        public async Task<List<string>> SelectNotExistMaterialsAsync(List<string> materialCodes)
        {
            // var materials =await DC.Set<BasBMaterial>().ToListAsync();
            var query = from m in DC.Set<BasBMaterial>()
                        where (materialCodes.Contains(m.MaterialCode))
                        select m.MaterialCode;
            var existMaterials = await query.ToListAsync();
            return materialCodes.Where(x => existMaterials.Contains(x) == false).ToList();
            //var objs = DC.Set<BasBMaterial>().ToList();
            //var basbMaterial = DC.Set<BasBMaterial>();
            //var query = from o in materialCodes
            //            where !(from m in basbMaterial select m.materialCode)
            //            .Contains(o)
            //            select o;
            //return query.ToList();
        }
        /// <summary>
        /// 检查物料是否是对应的物料标记
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public bool CheckMaterailHasFlag(string materialCode, DictonaryHelper.MaterialFlag materialFlag)
        {
            return false;
        }

        /// <summary>
        /// 根据物料编号获取物料信息（含物料大类、物料分类）
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public BasBMaterialDto GetBasBMaterial(string materialCode)
        {
            BasBMaterialDto basBMaterialView = new BasBMaterialDto();
            basBMaterialView.basBMaterial = DC.Set<BasBMaterial>().Where(x => x.MaterialCode == materialCode).FirstOrDefault();
            basBMaterialView.basBMaterialCategory =
                DC.Set<BasBMaterialCategory>().Where(x => x.materialCategoryCode == basBMaterialView.basBMaterial.MaterialCategoryCode).FirstOrDefault();
            basBMaterialView.basBMaterialType = DC.Set<BasBMaterialType>().Where(x => x.materialTypeCode == basBMaterialView.basBMaterial.MaterialTypeCode).FirstOrDefault();
            return basBMaterialView;
        }
        public async Task<BasBMaterialDto> GetBasBMaterialAsync(string materialCode)
        {
            BasBMaterialDto basBMaterialView = new BasBMaterialDto();
            basBMaterialView.basBMaterial = await DC.Set<BasBMaterial>().Where(x => x.MaterialCode == materialCode).FirstOrDefaultAsync();
            basBMaterialView.basBMaterialCategory = await
                DC.Set<BasBMaterialCategory>().Where(x => x.materialCategoryCode == basBMaterialView.basBMaterial.MaterialCategoryCode).FirstOrDefaultAsync();
            basBMaterialView.basBMaterialType = await DC.Set<BasBMaterialType>().Where(x => x.materialTypeCode == basBMaterialView.basBMaterial.MaterialTypeCode).FirstOrDefaultAsync();
            return basBMaterialView;
        }
        /// <summary>
        /// 根据物料编号获取物料信息（含物料大类、物料分类）
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public List<BasBMaterialDto> GetBasMaterials(List<string> materialCode)
        {
            List<BasBMaterialDto> list = new List<BasBMaterialDto>();
            List<BasBMaterial> matrials = DC.Set<BasBMaterial>().Where(x => materialCode.Contains(x.MaterialCode)).ToList();
            foreach (var item in matrials)
            {
                BasBMaterialDto basBMaterialView = new BasBMaterialDto();
                basBMaterialView.basBMaterial = item;
                basBMaterialView.basBMaterialCategory = DC.Set<BasBMaterialCategory>().Where(x => x.materialCategoryCode == item.MaterialCategoryCode).FirstOrDefault();
                basBMaterialView.basBMaterialType = DC.Set<BasBMaterialType>().Where(x => x.materialTypeCode == item.MaterialTypeCode).FirstOrDefault();
                list.Add(basBMaterialView);
            }

            return list;
        }
        public async Task<List<BasBMaterialDto>> GetBasMaterialsAsync(List<string> materialCode)
        {
            List<BasBMaterialDto> list = new List<BasBMaterialDto>();
            List<BasBMaterial> matrials = await DC.Set<BasBMaterial>().Where(x => materialCode.Contains(x.MaterialCode)).ToListAsync();
            foreach (var item in matrials)
            {
                BasBMaterialDto basBMaterialView = new BasBMaterialDto();
                basBMaterialView.basBMaterial = item;
                basBMaterialView.basBMaterialCategory = await DC.Set<BasBMaterialCategory>().Where(x => x.materialCategoryCode == item.MaterialCategoryCode).FirstOrDefaultAsync();
                basBMaterialView.basBMaterialType = await DC.Set<BasBMaterialType>().Where(x => x.materialTypeCode == item.MaterialTypeCode).FirstOrDefaultAsync();
                list.Add(basBMaterialView);
            }

            return list;
        }
        /// <summary>
        /// 根据物料编号获取物料信息
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public BasBMaterial GetBasBMaterialByNo(string materialCode)
        {
            return DC.Set<BasBMaterial>().Where(x => x.MaterialCode == materialCode).FirstOrDefault();
        }

        public async Task<BasBMaterial> GetBasBMaterialByNoAsync(string materialCode)
        {
            return await DC.Set<BasBMaterial>().Where(x => x.MaterialCode == materialCode).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 根据物料编号集合获取物料信息集合（仅物料表）
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public List<BasBMaterial> GetBasBMaterialList(List<string> materialCodes)
        {
            return DC.Set<BasBMaterial>().Where(x => materialCodes.Contains(x.MaterialCode) && x.UsedFlag == 1).ToList();
        }
        public async Task<List<BasBMaterial>> GetBasBMaterialListAsync(List<string> materialCodes)
        {
            return await DC.Set<BasBMaterial>().Where(x => materialCodes.Contains(x.MaterialCode) && x.UsedFlag == 1).ToListAsync();
        }
        /// <summary>
        /// 根据物料大类号获取物料大类信息
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        public BasBMaterialCategory GetBasBMaterialCategoryByCode(string categoryCode)
        {
            return DC.Set<BasBMaterialCategory>().Where(x => x.materialCategoryCode == categoryCode && x.usedFlag == 1).FirstOrDefault();
        }
        /// <summary>
        /// 根据物料大类号获取物料大类信息
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        public async Task<BasBMaterialCategory> GetBasBMaterialCategoryByCodeAsync(string categoryCode)
        {
            return await DC.Set<BasBMaterialCategory>().Where(x => x.materialCategoryCode == categoryCode && x.usedFlag == 1).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 取出所有的物料大类记录(因为记录数并不多所以可以全部查询出来再相应处理即可)
        /// </summary>
        /// <returns></returns>
        public List<BasBMaterialCategory> GetBasBMaterialCategoryAll()
        {
            return DC.Set<BasBMaterialCategory>().Where(x => x.usedFlag == 1).ToList();
        }
    }
}
