# 簡介
---
由於公司內部的外包人員常會用 markdown 做筆記，但共享並不容易，且也不方便搜尋，故產生了這個工具。

+ 資料夾：  
![image](https://github.com/intervalrain/DocViewer/assets/68344474/b275c674-8f28-4442-a7ca-56b5f7d1fd92)
+ 頁面清單：    
![docs](https://github.com/intervalrain/DocViewer/assets/68344474/cd145f26-2728-4183-aa68-4a9cc1eba785)
+ 經渲染的 markdown 文章：  
![doc](https://github.com/intervalrain/DocViewer/assets/68344474/d1853a1e-3220-464d-9f88-40589a0f00a5)



## Tech Stack
---
### 後端
+ .NET 8

### 前端
+ ASP.NET Core MVC

### 架構
+ Clean Architecture
  #### Presentation Layer:
    + 使用 ASP.NET Core 來處理用戶界面。
    + 負責 HTTP 請求和回應，包含前端頁面和 API 控制器。
    + Fetch API
  #### Application Layer:
    + 使用 CQRS 模式來區分查詢 (Query) 和命令 (Command)。
    + 利用 MediatR 統一發動事件。
      + 提供 AuthorizationService: 對對應的 Permissions、Roles、Policies 給予權限。
      + 提供 ValidationService: 檢查 command 是否有符合規範。
  #### Domain Layer:
    + 包含實體 (Entities)、值物件 (Value Objects)、聚合根 (Aggregates) 和領域事件 (Domain Events)。
    + 負責核心業務邏輯和規則。
  #### Infrastructure Layer:
    + 處理資料存取，在此為對資料夾作 I/O。
    + 負責外部服務 (例如身份驗證) 的整合。

## 功能需求
---
### 身份驗證:
+ 使用 Windows 認證來辨識公司內部用戶，確保只有授權用戶能夠存取和操作資料。

### 頁面結構
+ Docs: 頁面清單
  + 排序(sort): 可以針對 title, author, category, datetime 等欄位進行排序
  + 篩選(filter): 可以針對 category 進行篩選
  + 搜尋引擎: 採 top-k，以加權方式計算分數，並排出分數最高的前 10 筆文章。
