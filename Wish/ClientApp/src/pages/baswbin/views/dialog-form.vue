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
                 label: this.$t("baswbin.areaNo"),
                 rules: [{ required: true, message: this.$t("baswbin.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.bearWeight":{
                 label: this.$t("baswbin.bearWeight"),
                 rules: [],
                    type: "input"
            },
             "Entity.binCol":{
                 label: this.$t("baswbin.binCol"),
                 rules: [{ required: true, message: this.$t("baswbin.binCol")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binErrFlag":{
                 label: this.$t("baswbin.binErrFlag"),
                 rules: [{ required: true, message: this.$t("baswbin.binErrFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binErrMsg":{
                 label: this.$t("baswbin.binErrMsg"),
                 rules: [],
                    type: "input"
            },
             "Entity.binGroupIdx":{
                 label: this.$t("baswbin.binGroupIdx"),
                 rules: [],
                    type: "input"
            },
             "Entity.binGroupNo":{
                 label: this.$t("baswbin.binGroupNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.binHeight":{
                 label: this.$t("baswbin.binHeight"),
                 rules: [],
                    type: "input"
            },
             "Entity.binLayer":{
                 label: this.$t("baswbin.binLayer"),
                 rules: [{ required: true, message: this.$t("baswbin.binLayer")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binLength":{
                 label: this.$t("baswbin.binLength"),
                 rules: [],
                    type: "input"
            },
             "Entity.binName":{
                 label: this.$t("baswbin.binName"),
                 rules: [{ required: true, message: this.$t("baswbin.binName")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
            //  "Entity.binNameAlias":{
            //      label: this.$t("baswbin.binNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.binNameEn":{
            //      label: this.$t("baswbin.binNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.binNo":{
                 label: this.$t("baswbin.binNo"),
                 rules: [{ required: true, message: this.$t("baswbin.binNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binPriority":{
                 label: this.$t("baswbin.binPriority"),
                 rules: [{ required: true, message: this.$t("baswbin.binPriority")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binRow":{
                 label: this.$t("baswbin.binRow"),
                 rules: [{ required: true, message: this.$t("baswbin.binRow")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.binType":{
                 label: this.$t("baswbin.binType"),
                 rules: [],
                    type: "input"
            },
             "Entity.binWidth":{
                 label: this.$t("baswbin.binWidth"),
                 rules: [],
                    type: "input"
            },
             "Entity.capacitySize":{
                 label: this.$t("baswbin.capacitySize"),
                 rules: [{ required: true, message: this.$t("baswbin.capacitySize")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.extensionGroupNo":{
                 label: this.$t("baswbin.extensionGroupNo"),
                 rules: [],
                    type: "input"
            },
             "Entity.extensionIdx":{
                 label: this.$t("baswbin.extensionIdx"),
                 rules: [],
                    type: "input"
            },
             "Entity.fireFlag":{
                 label: this.$t("baswbin.fireFlag"),
                 rules: [],
                    type: "input"
            },
             "Entity.isInEnable":{
                 label: this.$t("baswbin.isInEnable"),
                 rules: [{ required: true, message: this.$t("baswbin.isInEnable")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.isOutEnable":{
                 label: this.$t("baswbin.isOutEnable"),
                 rules: [{ required: true, message: this.$t("baswbin.isOutEnable")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.isValidityPeriod":{
                 label: this.$t("baswbin.isValidityPeriod"),
                 rules: [{ required: true, message: this.$t("baswbin.isValidityPeriod")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.palletDirect":{
                 label: this.$t("baswbin.palletDirect"),
                 rules: [],
                    type: "input"
            },
             "Entity.rackNo":{
                 label: this.$t("baswbin.rackNo"),
                 rules: [{ required: true, message: this.$t("baswbin.rackNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("baswbin.regionNo"),
                 rules: [{ required: true, message: this.$t("baswbin.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.roadwayNo":{
                 label: this.$t("baswbin.roadwayNo"),
                 rules: [{ required: true, message: this.$t("baswbin.roadwayNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: this.$t("baswbin.usedFlag"),
                 rules: [{ required: true, message: this.$t("baswbin.usedFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.virtualFlag":{
                 label: this.$t("baswbin.virtualFlag"),
                 rules: [{ required: true, message: this.$t("baswbin.virtualFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("baswbin.whouseNo"),
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
