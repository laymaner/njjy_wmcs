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
                 label: this.$t("baswregion.areaNo"),
                 rules: [{ required: true, message: this.$t("baswregion.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.manualFlag":{
                 label: this.$t("baswregion.manualFlag"),
                 rules: [{ required: true, message: this.$t("baswregion.manualFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletMgt":{
                 label: this.$t("baswregion.palletMgt"),
                 rules: [{ required: true, message: this.$t("baswregion.palletMgt")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.pickupMethod":{
                 label: this.$t("baswregion.pickupMethod"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionName":{
                 label: this.$t("baswregion.regionName"),
                 rules: [{ required: true, message: this.$t("baswregion.regionName")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
            //  "Entity.regionNameAlias":{
            //      label: this.$t("baswregion.regionNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.regionNameEn":{
            //      label: this.$t("baswregion.regionNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.regionNo":{
                 label: this.$t("baswregion.regionNo"),
                 rules: [{ required: true, message: this.$t("baswregion.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.regionTypeCode":{
                 label: this.$t("baswregion.regionTypeCode"),
                 rules: [{ required: true, message: this.$t("baswregion.regionTypeCode")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.sdType":{
                 label: this.$t("baswregion.sdType"),
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: this.$t("baswregion.usedFlag"),
                 rules: [{ required: true, message: this.$t("baswregion.usedFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.virtualFlag":{
                 label: this.$t("baswregion.virtualFlag"),
                 rules: [{ required: true, message: this.$t("baswregion.virtualFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("baswregion.whouseNo"),
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
