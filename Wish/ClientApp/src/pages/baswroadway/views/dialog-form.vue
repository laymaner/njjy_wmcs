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
                 label: this.$t("baswroadway.areaNo"),
                 rules: [{ required: true, message: this.$t("baswroadway.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.errFlag":{
                 label: this.$t("baswroadway.errFlag"),
                 rules: [{ required: true, message: this.$t("baswroadway.errFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.errMsg":{
                 label: this.$t("baswroadway.errMsg"),
                 rules: [],
                    type: "input"
            },
             "Entity.regionNo":{
                 label: this.$t("baswroadway.regionNo"),
                 rules: [{ required: true, message: this.$t("baswroadway.regionNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.reservedQty":{
                 label: this.$t("baswroadway.reservedQty"),
                 rules: [{ required: true, message: this.$t("baswroadway.reservedQty")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.roadwayName":{
                 label: this.$t("baswroadway.roadwayName"),
                 rules: [{ required: true, message: this.$t("baswroadway.roadwayName")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
            //  "Entity.roadwayNameAlias":{
            //      label: this.$t("baswroadway.roadwayNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.roadwayNameEn":{
            //      label: this.$t("baswroadway.roadwayNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.roadwayNo":{
                 label: this.$t("baswroadway.roadwayNo"),
                 rules: [{ required: true, message: this.$t("baswroadway.roadwayNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: this.$t("baswroadway.usedFlag"),
                 rules: [{ required: true, message: this.$t("baswroadway.usedFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.virtualFlag":{
                 label: this.$t("baswroadway.virtualFlag"),
                 rules: [{ required: true, message: this.$t("baswroadway.virtualFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("baswroadway.whouseNo"),
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
