---
title: "[web] html+css"
keywords: ["html", "css", "web"]
description: "html+css"
date: 2024-06-11T00:27:02+08:00
category: "web"
author: "Rain"
---
+ 可搭配程式碼服用：
    + https://github.com/intervalrain/htmls/tree/main/tutorial
+ 使用者文檔：
    + [W3C 官網](https://www.w3.org/)
    + [MDN](https://developer.mozilla.org/)
    + [W3School](https://www.w3schools.com/)

## 一、HTML
1. 全名：超文本標記語言(HyperText Markup Language, HTML)
2. 現最常使用的是 HTML5，由 W3C & WHATWG 所制定。
3. 將檔案設置為 `.html` 即可建立一個 html 檔案。

## 二、HTML 標籤
1. **標籤**又稱**元素**，是 HTML 的基本組成單位。
2. 標籤分為: **雙標籤**和**單標籤**(絕大多數都是雙標籤)。
3. 標籤不區分大小寫，但推薦小寫，因為小寫更規範
4. 雙標籤，`<標籤名>標籤體</標籤名>`
```html
<marquee>Hello World</marquee>
```
5. 單標籤，`<標籤名/>`，`/`可省略
```html
<input/>
```
6. 標籤之間的關係：並列關係、嵌套關係，可以使用 `tab` 鍵進行縮進。
```html
<marquee>
    Hello World
    <input>
</marquee>
```

## 三、HTML 標籤屬性
1. 用於給標籤提供附加訊息。
2. 可以寫在: **起始標籤**或**單標籤**中，`<標籤名 屬性名="屬性值" 屬性名="屬性值">`
```html
<marquee loop="1" bgcolor="orange">Hello World</marquee>
<input type="password">
```
3. 有些特殊的屬性，沒有屬性名，只有屬性值：
```html
<input disabled>
```
4.  
	1. 不同的標籤，有不同的屬性；也有一些通用屬性(在任何標籤內都能寫)。
	2. 屬性名、屬性值不能亂寫，都是 W3C 規範的。
	3. 屬性名、屬性值，都不區分大小寫，但建議小寫。
	4. 單引號與雙引號都可以，甚至不寫也行，但建議雙引號。
	5. 標籤中不要出現同名屬性，否則**後者失效**。

## 四、HTML 基本結構
1. 在網頁中，使用右鍵「檢查」查看源始碼。
2. 「**檢查**」與「**檢視頁面來源**」的差別為：
    + 「**查看頁面來源**」：開發者使用的源始碼。
    + 「**檢查**」：經過瀏覽器處理後的源始碼。
3. 網頁的基本結構
    + 想要呈現在網頁的內容寫在 `body` 標籤中。
    + `head` 標籤中的內容不會出現在網頁中。
    + `head` 標籤中的 `title` 標籤可以指定網頁的標題。
```html
<html>
    <head>
        <title>我是標題</title>
    </head>
    <body>
        <p>Hello World</p>
    </body>
</html>
```

## 五、HTML 注釋
1. 注釋是由 `<!--` 與 `-->` 配對組成。
2. 在 VS Code 中可以使用 `Ctrl+/` 快捷鍵來快速注釋。

## 六、HTML 文檔聲明
1. 作用：告訴瀏覽器當前網頁的版本
2. 寫法：
```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Document</title>
</head>
<body>
    
</body>
</html>
```
3. 文檔聲明，必須寫在網頁的第一行，且在 `html` 標籤的外面。

## 七、HTML 文檔編碼
1. 編寫源始碼時用的文檔黨碼要與瀏覽器渲染時用的解碼一致，否則會產生亂碼。
2. 可以通過 `meta` 標籤配合 `charset` 屬性指定字符編碼。
3. 通常使用 `UTF-8` 萬國編碼。
```html
<meta charset="UTF-8">`
```
4. 其它常用編碼：
	1. **ASCII**: 大寫字母、小寫字母、數字、一些符號，共計 128 個。
	2. **ISO 8859-1**: 在 ASCII 基礎上，擴充了一些希臘字符等，共計 256 個。
	3. **Big5**: 社群最常用的電腦漢字字元集標準，收錄 13060 個漢字。
	4. **UTF-8**: 包含世界業所有語言，所有文字與符號。 

## 八、HTML 設置語言
1. 主要功能：
    - [x] 讓瀏覽器顯示對應的翻譯提示。
    - [x] 有利於檢索引擎優化(SEO, Search Engine Optimization)。
2. 具體寫法：
```html
<html lang="zh-TW">
```
3. 擴展知識： `lang` 屬性的編寫規則。
> 1. 第一種寫法(語言-國家/地區)，如：
>   + `zh-TW`: 中文-台灣(繁體中文)
>   + `zh-CN`: 中文-中國(簡體中文)
>   + `zh`: 中文
>   + `en-US`: 英語-美國
>   + `en-GB`: 英語-英國
> 2. 第二種寫法(語言-具體種類)，不建議使用。
>   + `zh-Hans`: 中文-簡體
>   + `zh-Hant`: 中文-繁體
> 3. W3C 官網說明: [《Language tags in HTML》](https://www.w3.org/International/articles/language-tags/)

## 九 HTML 網頁圖示
1. 在網頁標題旁可以顯示小圖示，通常稱作 `favicon`。
2. 可在 head 中加入 `<link rel="icon" type="image/png" href="檔案位址` 來更變圖示。
```html
<head>
    <title>HTML 教學</title>
    <meta charset="UTF-8">
    <link rel="icon" type="image/png" href="../images/favicon.ico"
</head>
```
