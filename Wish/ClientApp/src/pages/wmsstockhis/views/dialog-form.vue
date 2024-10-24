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
                 label: this.$t("wmsstockhis.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsstockhis.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binNo":{
                 label: this.$t("wmsstockhis.binNo"),
                 rules: [{ required: true, message: this.$t("wmsstockhis.binNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.errFlag":{
                 label: this.$t("wmsstockhis.errFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.errMsg":{
                 label: this.$t("wmsstockhis.errMsg"),
                 rules: [],
                    type: "input"
            },
             "Entity.height":{
                 label: this.$t("wmsstockhis.height"),
                 rules: [],
                    type: "input"
            },
             "Entity.invoiceNo":{
                 label: this.$t("wmsstockhis.invoiceNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.loadedType":{
                 label: this.$t("wmsstockhis.loadedType"),
                 rules: [],
                    type: "input"
            },
             "Entity.locAllotGroup":{
                 label: this.$t("wmsstockhis.locAllotGroup"),
                 rules: [],
                    type: "input"
            },
             "Entity.locNo":{
                 label: this.$t("wmsstockhis.locNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsstockhis.palletBarcode"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsstockhis.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("wmsstockhis.regionNo"),
                 rules: [{ required: true, message: this.$t("wmsstockhis.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.roadwayNo":{
                 label: this.$t("wmsstockhis.roadwayNo"),
                 rules: [{ required: true, message: this.$t("wmsstockhis.roadwayNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.specialFlag":{
                 label: this.$t("wmsstockhis.specialFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.specialMsg":{
                 label: this.$t("wmsstockhis.specialMsg"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsstockhis.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsstockhis.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockStatus":{
                 label: this.$t("wmsstockhis.stockStatus"),
                 rules: [{ required: true, message: this.$t("wmsstockhis.stockStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.weight":{
                 label: this.$t("wmsstockhis.weight"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsstockhis.whouseNo"),
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
