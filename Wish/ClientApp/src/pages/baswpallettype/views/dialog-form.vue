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
             "Entity.barcodeFlag":{
                 label: "barcodeFlag",
                 rules: [{ required: true, message: "barcodeFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.checkFormula":{
                 label: "checkFormula",
                 rules: [],
                    type: "input"
            },
             "Entity.checkPalletFlag":{
                 label: "checkPalletFlag",
                 rules: [{ required: true, message: "checkPalletFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.chekDesc":{
                 label: "chekDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.developFlag":{
                 label: "developFlag",
                 rules: [{ required: true, message: "developFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.emptyMaxQty":{
                 label: "emptyMaxQty",
                 rules: [],
                    type: "input"
            },
             "Entity.maxWeight":{
                 label: "maxWeight",
                 rules: [],
                    type: "input"
            },
             "Entity.palletHeight":{
                 label: "palletHeight",
                 rules: [],
                    type: "input"
            },
             "Entity.palletLength":{
                 label: "palletLength",
                 rules: [],
                    type: "input"
            },
             "Entity.palletTypeCode":{
                 label: "palletTypeCode",
                 rules: [{ required: true, message: "palletTypeCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletTypeFlag":{
                 label: "palletTypeFlag",
                 rules: [{ required: true, message: "palletTypeFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletTypeName":{
                 label: "palletTypeName",
                 rules: [{ required: true, message: "palletTypeName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletTypeNameAlias":{
                 label: "palletTypeNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.palletTypeNameEn":{
                 label: "palletTypeNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.palletWeight":{
                 label: "palletWeight",
                 rules: [],
                    type: "input"
            },
             "Entity.palletWidth":{
                 label: "palletWidth",
                 rules: [],
                    type: "input"
            },
             "Entity.positionCol":{
                 label: "positionCol",
                 rules: [],
                    type: "input"
            },
             "Entity.positionDirect":{
                 label: "positionDirect",
                 rules: [],
                    type: "input"
            },
             "Entity.positionFlag":{
                 label: "positionFlag",
                 rules: [],
                    type: "input"
            },
             "Entity.positionRow":{
                 label: "positionRow",
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: "usedFlag",
                 rules: [{ required: true, message: "usedFlag"+this.$t("form.notnull"),trigger: "blur" }],
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
