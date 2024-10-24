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
             "Entity.whouseNo":{
                 label: "whouseNo",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.areaNo":{
                 label: "areaNo",
                 rules: [{ required: true, message: "areaNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.moveNo":{
                 label: "moveNo",
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: "regionNo",
                 rules: [],
                    type: "input"
            },
             "Entity.roadwayNo":{
                 label: "roadwayNo",
                 rules: [{ required: true, message: "roadwayNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binNo":{
                 label: "binNo",
                 rules: [{ required: true, message: "binNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: "stockCode",
                 rules: [{ required: true, message: "stockCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: "palletBarcode",
                 rules: [],
                    type: "input"
            },
             "Entity.moveDtlStatus":{
                 label: "moveDtlStatus",
                 rules: [{ required: true, message: "moveDtlStatus"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.loadedType":{
                 label: "loadedType",
                 rules: [],
                    type: "input"
            },
             "Entity.moveQty":{
                 label: "moveQty",
                 rules: [],
                    type: "input"
            },
             "Entity.createBy":{
                 label: "createBy",
                 rules: [],
                    type: "input"
            },
             "Entity.createTime":{
                 label: "createTime",
                 rules: [],
                    type: "datePicker"
            },
             "Entity.updateBy":{
                 label: "updateBy",
                 rules: [],
                    type: "input"
            },
             "Entity.updateTime":{
                 label: "updateTime",
                 rules: [],
                    type: "datePicker"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
