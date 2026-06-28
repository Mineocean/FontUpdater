> **本项目由 AI 辅助生成**，代码结构、实现逻辑均由 AI 工具协助完成。

# 霞鹜文楷字体管理器 (LxgwWenKai Font Manager)

一个用于管理和更新霞鹜文楷系列字体的 Windows 桌面工具。

## 功能特性

- **一键更新**：自动检测并更新霞鹜文楷系列字体
- **字体预览**：实时预览字体效果，支持调整字号
- **自动备份**：更新前自动备份旧版本字体
- **版本管理**：查看已安装字体版本和最新版本
- **批量操作**：支持一键更新所有字体

## 支持的字体

| 字体名称 | 仓库 | 说明 |
|---------|------|------|
| 霞鹜文楷 | lxgw/LxgwWenKai | 主版本 |
| 霞鹜文楷轻便版 | lxgw/LxgwWenKai-Lite | 精简版 |
| 霞鹜文楷GB | lxgw/LxgwWenkaiGB | GB标准版 |
| 霞鹜文楷TC | lxgw/LxgwWenkaiTC | 繁体中文版 |
| 霞鹜文楷屏幕阅读版 | lxgw/LxgwWenKaiScreen | 屏幕阅读优化版 |
| 霞鹜文楷Mono | lxgw/LxgwWenKaiMono | 等宽版 |
| 霞鹜文楷Clear | lxgw/LxgwWenKaiClear | 清晰版 |
| 霞鹜文楷Italic | lxgw/LxgwWenKaiItalic | 斜体版 |

## 系统要求

- Windows 7/8/10/11
- .NET Framework 4.7.2 或更高版本

## 安装使用

1. 从 [Releases](https://github.com/Mineocean/FontUpdater/releases) 下载最新版本
2. 运行 `FontManager.exe`
3. 程序启动时会自动检查更新
4. 点击"检查更新"按钮手动检查
5. 选择需要更新的字体，点击"更新"

## 开发环境

- Visual Studio 2019+
- .NET Framework 4.7.2
- Newtonsoft.Json 13.0.3

## 项目结构

```
FontManager/
├── Models/          # 数据模型
├── Services/        # 业务服务
├── Forms/           # 界面窗口
├── Utils/           # 工具类
└── Program.cs       # 程序入口
```

## 许可证

本项目采用 MIT 许可证 - 详见 [LICENSE](LICENSE) 文件

## 致谢

- [霞鹜文楷](https://github.com/lxgw/LxgwWenKai) - 由 lxgw 开发的开源中文字体
