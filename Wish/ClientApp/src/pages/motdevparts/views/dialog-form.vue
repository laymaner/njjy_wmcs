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
             "Entity.AreaNo":{
                 label: "AreaNo",
                 rules: [],
                    type: "input"
            },
             "Entity.DevNo":{
                 label: "DevNo",
                 rules: [],
                    type: "input"
            },
             "Entity.PartNo":{
                 label: "PartNo",
                 rules: [],
                    type: "input"
            },
             "Entity.PartLocNo":{
                 label: "PartLocNo",
                 rules: [],
                    type: "input"
            },
             "Entity.DevRunMode":{
                 label: "DevRunMode",
                 rules: [],
                    type: "input"
            },
             "Entity.SrmRoadway":{
                 label: "SrmRoadway",
                 rules: [],
                    type: "input"
            },
             "Entity.SrmForkType":{
                 label: "SrmForkType",
                 rules: [],
                    type: "input"
            },
             "Entity.SrmExecStep":{
                 label: "SrmExecStep",
                 rules: [],
                    type: "input"
            },
             "Entity.IsInSitu":{
                 label: "IsInSitu",
                 rules: [],
                    type: "input"
            },
             "Entity.IsAlarming":{
                 label: "IsAlarming",
                 rules: [],
                    type: "input"
            },
             "Entity.AlarmCode":{
                 label: "AlarmCode",
                 rules: [],
                    type: "input"
            },
             "Entity.IsFree":{
                 label: "IsFree",
                 rules: [],
                    type: "input"
            },
             "Entity.IsHasGoods":{
                 label: "IsHasGoods",
                 rules: [],
                    type: "input"
            },
             "Entity.CmdNo":{
                 label: "CmdNo",
                 rules: [],
                    type: "input"
            },
             "Entity.PalletNo":{
                 label: "PalletNo",
                 rules: [],
                    type: "input"
            },
             "Entity.OldPalletNo":{
                 label: "OldPalletNo",
                 rules: [],
                    type: "input"
            },
             "Entity.ReadPalletNo":{
                 label: "ReadPalletNo",
                 rules: [],
                    type: "input"
            },
             "Entity.StationNo":{
                 label: "StationNo",
                 rules: [],
                    type: "input"
            },
             "Entity.CurrentX":{
                 label: "CurrentX",
                 rules: [],
                    type: "input"
            },
             "Entity.CurrentY":{
                 label: "CurrentY",
                 rules: [],
                    type: "input"
            },
             "Entity.CurrentZ":{
                 label: "CurrentZ",
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
