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
                 label: this.$t("wmsstock.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsstock.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binNo":{
                 label: this.$t("wmsstock.binNo"),
                 rules: [{ required: true, message: this.$t("wmsstock.binNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.errFlag":{
                 label: this.$t("wmsstock.errFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.errMsg":{
                 label: this.$t("wmsstock.errMsg"),
                 rules: [],
                    type: "input"
            },
             "Entity.height":{
                 label: this.$t("wmsstock.height"),
                 rules: [],
                    type: "input"
            },
             "Entity.invoiceNo":{
                 label: this.$t("wmsstock.invoiceNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.loadedType":{
                 label: this.$t("wmsstock.loadedType"),
                 rules: [],
                    type: "input"
            },
             "Entity.locAllotGroup":{
                 label: this.$t("wmsstock.locAllotGroup"),
                 rules: [],
                    type: "input"
            },
             "Entity.locNo":{
                 label: this.$t("wmsstock.locNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.palletBarcode":{
                 label: this.$t("wmsstock.palletBarcode"),
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: this.$t("wmsstock.proprietorCode"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("wmsstock.regionNo"),
                 rules: [{ required: true, message: this.$t("wmsstock.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.roadwayNo":{
                 label: this.$t("wmsstock.roadwayNo"),
                 rules: [{ required: true, message: this.$t("wmsstock.roadwayNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.specialFlag":{
                 label: this.$t("wmsstock.specialFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.specialMsg":{
                 label: this.$t("wmsstock.specialMsg"),
                 rules: [],
                    type: "input"
            },
             "Entity.stockCode":{
                 label: this.$t("wmsstock.stockCode"),
                 rules: [{ required: true, message: this.$t("wmsstock.stockCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.stockStatus":{
                 label: this.$t("wmsstock.stockStatus"),
                 rules: [{ required: true, message: this.$t("wmsstock.stockStatus")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.weight":{
                 label: this.$t("wmsstock.weight"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("wmsstock.whouseNo"),
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
