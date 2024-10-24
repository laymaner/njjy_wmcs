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
             "Entity.SeqCode":{
                 label: "SeqCode",
                 rules: [],
                    type: "input"
            },
             "Entity.SeqDesc":{
                 label: "SeqDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.SeqType":{
                 label: "SeqType",
                 rules: [],
                    type: "input"
            },
             "Entity.NowSn":{
                 label: "NowSn",
                 rules: [],
                    type: "input"
            },
             "Entity.MinSn":{
                 label: "MinSn",
                 rules: [],
                    type: "input"
            },
             "Entity.MaxSn":{
                 label: "MaxSn",
                 rules: [],
                    type: "input"
            },
             "Entity.SeqSnLen":{
                 label: "SeqSnLen",
                 rules: [],
                    type: "input"
            },
             "Entity.SeqPrefix":{
                 label: "SeqPrefix",
                 rules: [],
                    type: "input"
            },
             "Entity.SeqDate":{
                 label: "SeqDate",
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
