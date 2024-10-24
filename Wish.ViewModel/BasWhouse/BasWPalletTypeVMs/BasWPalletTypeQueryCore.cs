using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;
using Microsoft.EntityFrameworkCore;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.Text.RegularExpressions;
using Wish.ViewModel.Common.Dtos;
using WISH.Helper.Common.Dictionary.DictionaryHelper;

namespace Wish.ViewModel.BasWhouse.BasWPalletTypeVMs
{
    public partial class BasWPalletTypeVM
    {

        /// <summary>
        /// 根据载体或者库位编号判断是 托盘/料箱/货位
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public BasWPalletTypeDto GetPalletType(string barCode)
        {
            BasWPalletTypeDto basWPalletTypeView = new BasWPalletTypeDto();
            basWPalletTypeView.palletType = PalletTypeExtend.UnKnown;
            List<BasWPalletType> basWPalletTypes = DC.Set<BasWPalletType>().Where(x => x.usedFlag == 1).ToList();
            foreach (var palletType in basWPalletTypes)
            {
                // 校验公式
                string checkFormula = palletType.checkFormula;
                string palletTypeExtend = palletType.palletTypeCode;
                //ST  钢托盘 ST + 8位流水
                //PL  塑料托盘 PL+8位流水
                //BX  料箱 BX+8位流水
                //bool isST = Regex.IsMatch(barCode, "^ST[0-9]{8}$") && checkFormula == "^ST[0-9]{8}$";
                //bool isPL = Regex.IsMatch(barCode, "^PL[0-9]{8}$") && checkFormula == "^PL[0-9]{8}$";
                //bool isBX = Regex.IsMatch(barCode, "^BX[0-9]{8}$") && checkFormula == "^BX[0-9]{8}$";
                //bool isST = Regex.IsMatch(barCode, "^ST\\d{8}$") && checkFormula == "^ST\\d{8}$";
                //bool isPL = Regex.IsMatch(barCode, "^PL\\d{8}$") && checkFormula == "^PL\\d{8}$";
                //bool isBX = Regex.IsMatch(barCode, "^BX[0-9]{8}$") && checkFormula == "^BX[0-9]{8}$";
                bool isMatch = Regex.IsMatch(barCode, checkFormula);

                if (isMatch)
                {
                    basWPalletTypeView.basWPalletType = palletType;

                    if (palletTypeExtend == PalletTypeExtend.Pallet.GetCode())
                    {
                        basWPalletTypeView.palletType = PalletTypeExtend.Pallet;
                    }
                    else if (palletTypeExtend == PalletTypeExtend.Box.GetCode())
                    {
                        basWPalletTypeView.palletType = PalletTypeExtend.Box;
                    }
                    else if (palletTypeExtend == PalletTypeExtend.Steel.GetCode())
                    {
                        basWPalletTypeView.palletType = PalletTypeExtend.Steel;
                    }
                    break;
                }
            }
            return basWPalletTypeView;

        }

        public async Task<BasWPalletTypeDto> GetPalletTypeAsync(string barCode)
        {
            BasWPalletTypeDto basWPalletTypeView = new BasWPalletTypeDto();
            basWPalletTypeView.palletType = PalletTypeExtend.UnKnown;
            List<BasWPalletType> basWPalletTypes = await DC.Set<BasWPalletType>().Where(x => x.usedFlag == 1).ToListAsync();
            foreach (var palletType in basWPalletTypes)
            {
                // 校验公式
                string checkFormula = palletType.checkFormula;
                string palletTypeExtend = palletType.palletTypeCode;
                bool isMatch = Regex.IsMatch(barCode, checkFormula);

                if (isMatch)
                {
                    basWPalletTypeView.basWPalletType = palletType;

                    if (palletTypeExtend == PalletTypeExtend.Pallet.GetCode())
                    {
                        basWPalletTypeView.palletType = PalletTypeExtend.Pallet;
                    }
                    else if (palletTypeExtend == PalletTypeExtend.Box.GetCode())
                    {
                        basWPalletTypeView.palletType = PalletTypeExtend.Box;
                    }
                    else if (palletTypeExtend == PalletTypeExtend.Steel.GetCode())
                    {
                        basWPalletTypeView.palletType = PalletTypeExtend.Steel;
                    }
                    break;
                }
            }
            return basWPalletTypeView;

        }
    }
}
