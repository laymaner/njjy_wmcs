<template>
    <wtm-dialog-box :is-show.sync="isShow" :status="status" :events="formEvent">
        <wtm-create-form :ref="refName" :status="status" :options="formOptions" ></wtm-create-form>
    </wtm-dialog-box>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Action, State } from "vuex-class";
import formMixin from "@/vue-custom/mixin/form-mixin";
import UploadImg from "@/components/page/UploadImg.vue";


@Component({
    mixins: [formMixin()]
})
export default class Index extends Vue {

    // 表单结构
    get formOptions() {
        const filterMethod = (query, item) => {
            return item.label.indexOf(query) > -1;
        };
        return {
            formProps: {
                "label-width": "100px"
            },
            formItem: {
                "Entity.ID": {
                    isHidden: true
                },
             "Entity.BarcodeFlag":{
                 label: "BarcodeFlag",
                 rules: [{ required: true, message: "BarcodeFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Brand":{
                 label: "Brand",
                 rules: [],
                    type: "input"
            },
             "Entity.BuyerCode":{
                 label: "BuyerCode",
                 rules: [],
                    type: "input"
            },
             "Entity.BuyerName":{
                 label: "BuyerName",
                 rules: [],
                    type: "input"
            },
             "Entity.EmaterialVtime":{
                 label: "EmaterialVtime",
                 rules: [],
                    type: "input"
            },
             "Entity.ErpBinNo":{
                 label: "ErpBinNo",
                 rules: [],
                    type: "input"
            },
             "Entity.Material":{
                 label: "Material",
                 rules: [],
                    type: "input"
            },
             "Entity.MaterialCategoryCode":{
                 label: "MaterialCategoryCode",
                 rules: [],
                    type: "input"
            },
             "Entity.MaterialName":{
                 label: "MaterialName",
                 rules: [{ required: true, message: "MaterialName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.MaterialNameAlias":{
                 label: "MaterialNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.MaterialNameEn":{
                 label: "MaterialNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.MaterialCode":{
                 label: "MaterialCode",
                 rules: [{ required: true, message: "MaterialCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.MaterialSpec":{
                 label: "MaterialSpec",
                 rules: [],
                    type: "input"
            },
             "Entity.MaterialTypeDesc":{
                 label: "MaterialTypeDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.MaterialTypeCode":{
                 label: "MaterialTypeCode",
                 rules: [],
                    type: "input"
            },
             "Entity.MaxDelayTimes":{
                 label: "MaxDelayTimes",
                 rules: [],
                    type: "input"
            },
             "Entity.MaxDriedTimes":{
                 label: "MaxDriedTimes",
                 rules: [],
                    type: "input"
            },
             "Entity.MinPkgQty":{
                 label: "MinPkgQty",
                 rules: [],
                    type: "input"
            },
             "Entity.ProjectDrawingNo":{
                 label: "ProjectDrawingNo",
                 rules: [],
                    type: "input"
            },
             "Entity.ProprietorCode":{
                 label: "ProprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.QcFlag":{
                 label: "QcFlag",
                 rules: [],
                    type: "input"
            },
             "Entity.SharedFalg":{
                 label: "SharedFalg",
                 rules: [],
                    type: "input"
            },
             "Entity.SkuRuleNo":{
                 label: "SkuRuleNo",
                 rules: [],
                    type: "input"
            },
             "Entity.SluggishTime":{
                 label: "SluggishTime",
                 rules: [],
                    type: "input"
            },
             "Entity.TechParm":{
                 label: "TechParm",
                 rules: [],
                    type: "input"
            },
             "Entity.UnitCode":{
                 label: "UnitCode",
                 rules: [],
                    type: "input"
            },
             "Entity.UnitWeight":{
                 label: "UnitWeight",
                 rules: [{ required: true, message: "UnitWeight"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.UsedFlag":{
                 label: "UsedFlag",
                 rules: [{ required: true, message: "UsedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.VirtualFlag":{
                 label: "VirtualFlag",
                 rules: [{ required: true, message: "VirtualFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.WarnOverdueLen":{
                 label: "WarnOverdueLen",
                 rules: [],
                    type: "input"
            },
             "Entity.WhouseNo":{
                 label: "WhouseNo",
                 rules: [],
                    type: "input"
            },
             "Entity.CompanyCode":{
                 label: "CompanyCode",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend1":{
                 label: "Extend1",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend2":{
                 label: "Extend2",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend3":{
                 label: "Extend3",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend4":{
                 label: "Extend4",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend5":{
                 label: "Extend5",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend6":{
                 label: "Extend6",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend7":{
                 label: "Extend7",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend8":{
                 label: "Extend8",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend9":{
                 label: "Extend9",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend10":{
                 label: "Extend10",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend11":{
                 label: "Extend11",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend12":{
                 label: "Extend12",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend13":{
                 label: "Extend13",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend14":{
                 label: "Extend14",
                 rules: [],
                    type: "input"
            },
             "Entity.Extend15":{
                 label: "Extend15",
                 rules: [],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
