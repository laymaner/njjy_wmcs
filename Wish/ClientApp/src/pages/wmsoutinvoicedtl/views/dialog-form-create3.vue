<template>
  <wtm-dialog-box :is-show.sync="isShow" :status="status" :events="formEvent">
    <wtm-create-form :ref="refName" :status="status" :options="formOptions" />
    <el-table :data="tableData" border style="width: 100%">
      <el-table-column label="列名 1" prop="field1" />
      <el-table-column label="列名 2" prop="field2" />
      <el-table-column label="操作">
        <template slot-scope="scope">
          <button style="text-align: center; border-bottom: 1px solid; color: blue; cursor: pointer;" @click="deleteRow(scope.$index)">删除</button>
        </template>
      </el-table-column>
    </el-table>
    <el-button type="primary" style="background-color: #f8f9fa; color: black;" @click="addTableRow">增加行</el-button>
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
        "Entity.supplierCode": {
          label: this.$t("wmsoutinvoicedtl.supplierCode"),
          rules: [],
          type: "input"
        }
      }
    };
  }

  beforeOpen() {}

  // 初始化表格数据，这里使用对象数组来表示多行数据，每一行是一个包含字段值的对象
  data() {
    return {
      tableData: [
        { field1: "", field2: "" }
      ]
    };
  }

  // 添加新行的方法
  addTableRow() {
    this.tableData.push({ field1: "", field2: "" });
  }

  // 删除行的方法
  deleteRow(index) {
    this.tableData.splice(index, 1);
  }
}
</script>