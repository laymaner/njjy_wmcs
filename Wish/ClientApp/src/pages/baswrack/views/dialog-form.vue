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
                 label: this.$t("baswrack.areaNo"),
                 rules: [{ required: true, message: this.$t("baswrack.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.isInEnable":{
                 label: this.$t("baswrack.isInEnable"),
                 rules: [{ required: true, message: this.$t("baswrack.isInEnable")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.isOutEnable":{
                 label: this.$t("baswrack.isOutEnable"),
                 rules: [{ required: true, message: this.$t("baswrack.isOutEnable")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.rackIdx":{
                 label: this.$t("baswrack.rackIdx"),
                 rules: [{ required: true, message: this.$t("baswrack.rackIdx")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.rackName":{
                 label: this.$t("baswrack.rackName"),
                 rules: [{ required: true, message: this.$t("baswrack.rackName")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
            //  "Entity.rackNameAlias":{
            //      label: this.$t("baswrack.rackNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.rackNameEn":{
            //      label: this.$t("baswrack.rackNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.rackNo":{
                 label: this.$t("baswrack.rackNo"),
                 rules: [{ required: true, message: this.$t("baswrack.rackNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("baswrack.regionNo"),
                 rules: [{ required: true, message: this.$t("baswrack.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.roadwayNo":{
                 label: this.$t("baswrack.roadwayNo"),
                 rules: [{ required: true, message: this.$t("baswrack.roadwayNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: this.$t("baswrack.usedFlag"),
                 rules: [{ required: true, message: this.$t("baswrack.usedFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.virtualFlag":{
                 label: this.$t("baswrack.virtualFlag"),
                 rules: [{ required: true, message: this.$t("baswrack.virtualFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("baswrack.whouseNo"),
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
