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
             "Entity.areaNo":{
                 label: "areaNo",
                 rules: [{ required: true, message: "areaNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binNo":{
                 label: "binNo",
                 rules: [{ required: true, message: "binNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.docTypeCode":{
                 label: "docTypeCode",
                 rules: [],
                    type: "input"
            },
             "Entity.loadedType":{
                 label: "loadedType",
                 rules: [{ required: true, message: "loadedType"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.orderNo":{
                 label: "orderNo",
                 rules: [],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: "palletBarcode",
                 rules: [{ required: true, message: "palletBarcode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.pickupMethod":{
                 label: "pickupMethod",
                 rules: [{ required: true, message: "pickupMethod"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.putdownNo":{
                 label: "putdownNo",
                 rules: [{ required: true, message: "putdownNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.putdownStatus":{
                 label: "putdownStatus",
                 rules: [{ required: true, message: "putdownStatus"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: "regionNo",
                 rules: [{ required: true, message: "regionNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: "stockCode",
                 rules: [{ required: true, message: "stockCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.targetLocNo":{
                 label: "targetLocNo",
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
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
