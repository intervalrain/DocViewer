---
title: "[Algo] 3-10. Binary Indexed Tree(Fenwick Tree, BIT)"
keywords: ["C++", "Leetcode", "Algorithm", "BIT", "Binary Indexed Tree", "Fenwick Tree"]
description: "利用 Binary Indexed Tree 做數組的動態更新與查詢，中譯為樹狀樹組"
date: 2023-04-08T17:46:12+08:00
tags: ["CS", "algo"]
draft: false
Categories: CS
author: "Rain Hu"
showToc: true
TocOpen: true
math: true
mermaid: true
hidemeta: false
canonicalURL: "https://intervalrain.github.io/"
disableHLJS: true
disableShare: true
disableHLJS: false
hideSummary: false
searchHidden: false
ShowReadingTime: true
ShowBreadCrumbs: true
ShowPostNavLinks: true
ShowCodeCopyButtons: true
---
> 前言：  
> 若要對一數組做範圍取值，那麼最快的方法是前綴數組(prefix sum)，可以做到\\(O(1)\\)的查詢，但若要做單點更新需要\\(O(n)\\)的時間來維護。  
> 而數組則是做單點更新只需要\\(O(1)\\)的時間，而要範圍取值則需要\\(O(n)\\)的查詢時間。  
> 故若是查詢遠大於更新的情境，則適用前綴數組；若更新遠大於查詢的情境，則適用一般數組。  
> 那假如查詢與更新的次數一樣多呢(動態更新與查詢的情境)，這種情況就可以用到此章節要介紹的資料結構，`Binary Indexed Tree` 了。  
> 此結構可以做到 \\(O(n)\\) 的初始化，\\(\log(n)\\) 的更新與 \\(\log(n)\\) 的查詢。  

\\(
    \begin{array}{|c|c|c|}\hline 
    &\textsf{範圍查詢}&\textsf{單點更新}\\\\\hline
    \textsf{數組}&O(n)&O(1)\\\\\hline
    \textsf{前綴數組}&O(1)&O(n)\\\\\hline
    \textsf{BIT}&O(\log n)&O(\log n)\\\\\hline
    \end{array}
\\)
## 簡介
+ 與線狀樹(Segment Tree)類似，但線狀樹可以看成是 BIT 的擴充版。
+ BIT 的好處是只需要 `n` 的數組空間便可以實作，且其指標移動是透過位元運算，計算相當快速，缺點是無法套用到取極大值、極小值的情境。
![img](https://d3i71xaburhd42.cloudfront.net/1841120d19f4bdc75f225254c52ceabea2774853/3-Figure1-1.png)
+ 參考上圖，BIT 利用「部分presum」的特性，來達到平均 \\(O\log n\\)的查詢與更新的時間，而其實其結構就是 partition 的其中半顆樹。
    + \\(\text{BIT[1]=arr[1]}\\)
    + \\(\text{BIT[2]=arr[1]+arr[2]}\\)
    + \\(\text{BIT[3]=arr[3]}\\)
    + \\(\text{BIT[4]=arr[1]+arr[2]+arr[3]+arr[4]}\\)
    + ...
    + \\(\text{BIT[8]=arr[1]+arr[2]+...+arr[8]}=
    \text{BIT[4]+BIT[6]+BIT[7]+arr[8]}\\)
+ 觀察以上結構，
    + 查詢時，求 [0:n] 的值為把上圖的片段湊起來變成 n 的長度。
        + 如 \\(\text{SUM[0:7]=BIT[7]+BIT[6]+BIT[4]}\\)
            + 位元表示：\\(\text{SUM[0:7]=BIT[1b'111]+BIT[1b'110]+BIT[1b'100]}\\)
        + 如 \\(\text{SUM[0:11]=BIT[11]+BIT[10]+BIT[8]}\\)
            + 位元表示：\\(\text{SUM[0:11]=BIT[1b'1011]+BIT[1b'1010]+BIT[1b'1000]}\\)
        + 可以發現位元的規律是每次把當前的 LSB(least significant bit) 扣掉。
    + 更新時，需要把包含 n 的片段都更新。(設n=18)
        + 如 \\(\text{update(arr[7])=update(BIT[7])+update(BIT[8])+update(BIT[16])}\\)
            + 位元表示：\\(\text{update(arr[7])=update(BIT[1b'111])+update(BIT[1b'1000])+update(BIT[1b'10000])}\\)
        + 如 \\(\text{update(arr[11])=update(BIT[11])+update(BIT[12])+update(BIT[16])}\\)
            + 位元表示：\\(\text{update(arr[7])=update(BIT[1b'1011])+update(BIT[1b'1100])+update(BIT[1b'10000])}\\)
        + 可以發現位元的規律是每次把當前的 LSB 加進來。
    + 統整以上規律我們可以寫成以下的模版
    + 將 `BIT[0]` 設為 dummy，可方便計算。

### 模板
```C++
class BIT {
private: 
    vector<int> bit;
    int lowbit(int a) {
        return a & (-a);
    }
public:
    BIT (int n) {
        bit.assign(n+1, 0);
    }
    void add(int idx, int diff) {
        idx++;
        int n = bit.size();
        while (idx < n) {
            bit[idx] += diff;
            idx += lowbit(idx);
        }
    }
    int query(int idx) {
        int sum = 0;
        idx++;
        while (idx > 0) {
            sum += bit[idx];
            idx -= lowbit(idx);
        }
        return sum;
    }
}
```
[回目錄 Catalog](/leetcode)