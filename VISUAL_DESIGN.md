# Visual Design: Rocket Boost

**Concept-Derived Visual Tags**: `lighting-emissive-rim`, `background-deep-space-negative-space`, `motionviz-thruster-energy`

## 1. Visual Concept

**冷色深空中的明亮火箭航线。**

画面的主角始终是火箭和它的推进火焰；岩石与地形只构成剪影式危险；终点使用与危险物明显不同的高亮暖色。背景保持暗、静、低对比，避免与关卡轮廓竞争。

## 2. Color Palette

- 背景：深蓝黑 `#07111F`，用于天空盒、远处空间与暗角。
- 玩家/推进：青蓝 `#64E7FF`，用于火箭边缘和主推进火焰。
- 危险：低饱和岩灰 `#46505C`，配少量暗紫阴影 `#23213A`。
- 终点/成功：琥珀金 `#FFD166`，用于终点平台、成功粒子和提示光。
- 坠毁/警告：珊瑚红 `#FF5F57`，仅用于爆炸、碰撞瞬间和危险信号。

## 3. Object Rendering Specifications

- 火箭：保持全场景最高亮度与最清晰轮廓；推进时发出青蓝色 HDR 自发光，使用 Bloom 形成柔和光晕。
- 岩石：保持粗糙、低饱和、偏暗，不添加强烈自发光；关键碰撞边缘可通过侧光与环境光拉开层次。
- 发射台：用青蓝的小范围灯光或发光条提示安全起点。
- 终点平台：使用琥珀金发光边缘和缓慢脉冲粒子，不使用与火箭相同的青蓝色。

## 4. Background & Environment

- 天空盒/远景保持深色且低对比，最亮的星云或星团放在赛道中心线以外。
- 前景与远景岩石使用更暗、更蓝的颜色，制造纵深；不要在火箭前方堆叠高亮纹理。
- 每关保留一个明显的明亮目标点，引导玩家向前方飞行。

## 5. Global Volume Recipe

三个场景共用 `Assets/Settings/SampleSceneProfile.asset`。从当前的 Global Volume 开始，建议按以下顺序逐项尝试：

1. **Tonemapping**：将 `Mode` 从 `Neutral` 改为 `ACES`。这是最明显、也最稳妥的整体提升，会收住过亮区域并增加电影感。
2. **Bloom**：将 `Intensity` 从 `0.25` 提到 `0.4`，`Threshold` 保持 `1`，`Scatter` 保持约 `0.5`。火焰、终点和爆炸需要使用 HDR 自发光颜色，Bloom 才会出现。
3. **Color Adjustments**：添加 Override；建议从 `Contrast +15`、`Saturation -5` 开始。若背景太亮，`Post Exposure` 设为 `-0.2`。
4. **White Balance**：可添加轻微冷色，`Temperature -5` 至 `-10`；不要与鲜艳的星云背景叠加过重。
5. **Vignette**：将当前 `Intensity 0.2` 降到 `0.12` 至 `0.16`，`Smoothness` 约 `0.3`。它应聚焦画面，而不是明显把四角压黑。

## 6. Effects to Avoid by Default

- **Motion Blur**：保持关闭。火箭需要频繁精确避障，持续模糊会削弱岩石边缘和操控感。
- **Depth of Field**：保持关闭。全程清晰比电影镜头感更重要；只在菜单或通关镜头中使用。
- **Chromatic Aberration**：常态保持为 `0`。仅在坠毁瞬间短暂启用，强度不超过 `0.1`。
- **Film Grain**：默认关闭。低多边形火箭与干净的太空风格不需要噪点。

## 7. Feedback Effects

- 推进：主火焰随推力出现青蓝亮度提升与短尾粒子；停止后粒子自然消散。
- 旋转：左右侧推进器短促发光，颜色与主推进一致。
- 接近岩石：保持无 HUD 的可读性；通过岩石轮廓、阴影与火箭距离来提示，不使用常驻屏幕特效。
- 成功：终点平台先脉冲增亮，随后发出琥珀金粒子与一次较强 Bloom。
- 坠毁：红橙爆炸、短暂屏幕震动，以及可选的一帧轻微色差；不要使用长时间红色滤镜。

## 8. AI-Generated Look Suppression Rules

### 8.1 Visual Hierarchy Rules

- Protagonist: 火箭及其青蓝推进光。
- Threat: 暗灰岩石的清晰剪影与碰撞边缘。
- Reward: 琥珀金终点平台与成功粒子。
- 2-second recognition check: 静止画面中，玩家应能在两秒内指出火箭、可撞击岩石和下一关目标。

### 8.2 Limits on Familiar Template Symbols

- Adopted familiar elements (max 2): 星云背景、推进器火焰。
- Replaced unique element: 不用泛用霓虹网格；以低多边形岩石剪影和发射平台灯带建立世界风格。

### 8.3 UI-Independent Feedback

- Score / finish: 终点平台的金色脉冲、粒子上升和高亮扩散；强度为 High。
- Damage: 红橙爆炸、碎片与短促镜头震动；强度为 High。
- Near miss: 火箭尾迹略微拉长、推进光短暂增强；强度为 Low。

### 8.4 Composition and Gaze Guidance

- Initial focal point: 火箭和它前方的安全航道。
- Visual flow: 火箭推进方向 → 地形空隙 → 终点金色亮点。
- Anti-center-clutter implementation: 把高频星点、复杂星云和粒子留在边缘；飞行通道中央维持暗且干净。

## 9. Quality Checklist

- [ ] 火箭、危险岩石和终点在不看文字时可清楚区分。
- [ ] 推进、成功和坠毁的反馈颜色不复用。
- [ ] Bloom 只服务于 HDR 发光物，不让整片岩石发亮。
- [ ] 快速旋转与避障时，画面仍清晰可读。
