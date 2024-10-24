<template>
    <div class="dashboard-container">
      <el-row :gutter="12">
        <el-col :span="8" :md="8" :xs="24" :sm="24">
          <el-row :gutter="10">
            <el-col :span="12" :sm="24">
              <e-charts-module />
            </el-col>
            <!-- <el-col :span="12" :sm="24">
             
            </el-col> -->
          </el-row>
        </el-col>
        <el-col :span="8" :md="8" :xs="24" :sm="24">
          <e-chartsroadway-module />
        </el-col>
        <el-col :span="8" :md="8" :xs="24" :sm="24">
          <e-chartsalarm-module />
        </el-col>
      </el-row>
      <el-row :gutter="12">
        <el-col :span="8" :xs="24">
          <e-chartss-module />
        </el-col>
        <el-col :span="8" :xs="24">
          <e-chartstaskhis-module />
        </el-col>
        <el-col :span="8" :xs="24">
          <e-chartsout-module />
        </el-col>
      </el-row>
      <el-row :gutter="10">
        <el-col :span="16" :md="16" :xs="24" :sm="24">
          <el-row :gutter="10">
            <el-col :span="12" :xs="24">
            </el-col>
            <el-col :span="12" :xs="24">
            </el-col>
            <el-col :span="24" :sm="24">
              <!-- <e-charts-module /> -->
            </el-col>
          </el-row>
        </el-col>
        <el-col :span="8" :md="8" :xs="24" :sm="24">
          <el-col :span="24">
          </el-col>
          <el-col :span="24">
          </el-col>
        </el-col>
      </el-row>
    </div>
  </template>
  
  <script lang="ts">
  import { Component, Vue } from "vue-property-decorator";
  import indexMixin from "./index-mixin";
  
  @Component({
    mixins: [indexMixin]
  })
  export default class extends Vue {
    created() {
      console.log("默认页面：index");
    }
  }
  </script>
  
  <style lang="less" rel="stylesheet/less" scoped>
  @import "~@/assets/css/mixin.less";
  
  .dashboard-container {
    padding: 10px;
    p {
      text-indent: 2em;
    }
   .title {
      margin-bottom: 5px;
      font-weight: 300;
      font-size: 16px;
    }
   .category {
      text-indent: 0;
      color: #999999;
     .text-success {
        color: red;
      }
    }
   .stats {
      color: #999999;
    }
  
   .el-row {
      margin-bottom: 20px;
    }
   .el-card__header {
      color: #333;
      font-size: 14px;
    }
   .el-card {
      font-size: 14px;
      color: #666;
    }
   .lump-wrap {
      margin-bottom: 0;
      min-height: 160px;
      text-align: center;
      &.cxt-left {
       .el-col {
         .flexalign(flex-start);
          box-sizing: border-box;
          padding-bottom: 10px;
          div {
            background-color: #f8f8f8;
            width: 100%;
            box-sizing: border-box;
            padding: 0 10px;
          }
        }
      }
     .el-col {
       .center(column);
        i {
          width: 100%;
          height: 60px;
          line-height: 60px;
          text-align: center;
          border-radius: 2px;
          font-size: 30px;
          background-color: #f8f8f8;
        }
        h3 {
          padding: 5px 0;
          font-size: 12px;
        }
        p {
          font-style: normal;
          font-size: 30px;
          font-weight: 300;
          color: #009688;
          text-indent: 0;
        }
       .link-a {
          color: #666;
          font-size: 14px;
          text-align: center;
        }
      }
     .link-ctx {
        min-width: 80px;
      }
    }
  }
  </style>