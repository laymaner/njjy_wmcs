const createFloorBtn = (floor) => {
  const floorBtn = `<div class="box">
    <div class="card bg-0${floor}">
      <span class="card-content change-floor-btn" data-floor="${floor}">楼层${floor}</span>
    </div>
  </div>`;
  return floorBtn;
};

const addFloorBtnList = (mapConfig) => {
  console.log("生成楼层列表", mapConfig);
  Object.keys(mapConfig).forEach((f) => {
    const btn = createFloorBtn(f);
    $("#floorBtnList").append(btn);
  });
};
