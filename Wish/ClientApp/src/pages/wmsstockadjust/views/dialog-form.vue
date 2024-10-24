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
             "Entity.adjustDesc":{
                 label: this.$t("wmsstockadjust.adjustDesc"),
                 rules: [],
                    type: "input"
            },
             "Entity.adjustOperate":{
                 label: this.$t("wmsstockadjust.adjustOperate"),
                 rules: [{ required: true, message: this.$t("wmsstockadjust.adjustOperate")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.adjustType":{
                 label: this.$t("wmsstockadjust.adjustType"),
                 rules: [{ required: true, message: this.$t("wmsstockadjust.adjustType")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.packageBarcode":{
                 label: this.$t("wmsstockadjust.packageBarcode"),
                 rules: [],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsstockadjust.palletBarcode"),
                 rules: [{ required: true, message: this.$t("wmsstockadjust.palletBarcode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsstockadjust.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsstockadjust.stockCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsstockadjust.whouseNo"),
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
