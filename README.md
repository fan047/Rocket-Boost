# Rocket Boost

一个使用 Unity 制作的 3D 火箭闯关小游戏。操控火箭从发射台起飞、绕开地形，并抵达终点平台进入下一关。

## 开发环境

- Unity `6000.4.3f1`（Unity 6）
- Universal Render Pipeline（URP）
- Input System
- Cinemachine

## 开始运行

1. 使用 Unity Hub 以 Unity `6000.4.3f1` 或兼容版本打开此项目。
2. 等待 Unity 导入资源。
3. 打开 `Assets/Scenes/Over.unity`。
4. 点击 Unity 顶部的播放按钮。

构建时，Build Settings 已按以下顺序配置三个关卡：

1. `Over`
2. `Under`
3. `Through`

## 操作

- `Space` / 手柄右扳机：火箭主引擎推进。
- `←`、`→` / 手柄左摇杆：旋转火箭。
- `L`：调试时直接进入下一关。
- `P`：调试时开关碰撞结果判定。

> `P` 只会禁用“碰撞后坠毁或过关”的脚本逻辑；火箭的物理 Collider 仍会阻挡墙体和地面。

## 项目结构

```text
Assets/
├── GameDevTV Assets/   # 模型、材质与音效资源
├── Materials/          # 关卡材质和天空盒
├── Particles/          # 引擎、爆炸与成功特效
├── Prefabs/            # 火箭、发射台、终点与地形预制体
├── Scenes/             # Over、Under、Through 三个关卡
├── Scripts/            # 火箭移动与碰撞处理
└── Settings/           # Input System 配置
```

## 碰撞规则

火箭由 `CollisionHandler` 脚本处理碰撞：

- `Friendly`：触发成功效果，并在延迟后进入下一关。
- `Start`：发射台，不会触发坠毁。
- 其他标签或未标记物体：触发爆炸并重载当前关卡。

关卡中的发射台请设为 `Start` 标签，终点平台请设为 `Friendly` 标签。

## 版本控制

仓库已忽略 Unity 自动生成的 `Library`、`Temp`、`Logs`、`obj` 等目录。请提交 `Assets`、`Packages` 和 `ProjectSettings` 中的项目内容及其 `.meta` 文件。
