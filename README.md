# Unity 程序框架学习 · MyGameFrame

基于《Unity程序基础框架（重置版）》学习可复用的 Unity 程序模块。本项目只学习用户选定的关键部分，具体课时待确认，不以完整学完教程为目标。

- GitHub：[Unity-GameFramework-Learn](https://github.com/CyreneMine/Unity-GameFramework-Learn)
- Unity：`6000.3.10f1`
- 工程结构：仓库根目录就是 Unity 工程。

## 文档导航

| 文档 | 用途 |
| --- | --- |
| [逐 P 教程目录](Docs/教程目录.md) | 主线 P1–P77 与选修课，包含时长、内容概述和选择栏 |
| [学习进度](LearningProgress.md) | 用户确认的学习范围、实际进度、验证记录与下一步 |
| [协作规则](AGENTS.md) | 辅导方式、代码修改边界、模块检查和 Git 约定 |

## 当前状态

2026-10-26 初始化整理时，工程中仅有 `SampleScene` 和默认输入动作资源，`Assets` 下尚无 C# 学习脚本。此次新增文档与 Git 忽略规则，保留已有 Unity 工程文件，未实现框架模块，也未进行编译、Play Mode 或构建验证。用户此前是否观看或练习过课程尚未确认。

教程主线共 77 个课时：74 个视频、3 个文字课时（P7、P14、P39）；另有 8 个选修视频及 1 篇选修文字说明。目录用于选课，条目存在不代表已学或必学。

## 打开工程

1. 在 Unity Hub 中添加本仓库根目录，使用 `6000.3.10f1` 打开。
2. 如需查看初始场景，打开 `Assets/Scenes/SampleScene.unity`。当前 Build Settings 场景列表为空，不声称已配置构建。
3. 学习参考资料不随仓库分发，可按教程目录中的课时编号查阅。

`Packages/manifest.json` 包含 Input System `1.18.0` 和 UGUI `2.0.0`；当前启用新 Input System。教程 P50–P53 的配套源码使用旧输入 API，学习到这一部分时须核对兼容性，再决定采用方式。

## 学习与提交约定

先由用户给出需要学习的 P 号或模块，再确认必要依赖与练习目标。观看、跟做、独立实现和运行验证分别记录。后续提交与 push 均在用户明确要求时执行。

仓库保留 Unity 工程所需的 `Assets`（含 `.meta`）、`Packages` 和 `ProjectSettings`；Unity 缓存、IDE 自动文件、个人设置和构建输出由 `.gitignore` 排除。
