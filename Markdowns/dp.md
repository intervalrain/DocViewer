---
title: "[Algo] 2-5. 動態規劃 Dynamic Programming"
keywords: ["C++", "Leetcode", "Algorithm", "動態規劃", "dp", "dynamic programming"]
description: "演算法設計，介紹什麼是動態規劃，並介紹幾種動態規劃常見的題型，與解題框架"
date: 2022-11-15T16:10:53+08:00
tags: ["CS", "Algo"]
draft: false
Categories: CS
author: "Rain Hu"
showToc: true
TocOpen: true
math: true
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
cover:
    image: "/images/faang.webp"
    alt: "Oh! You closed up the window, so you cannot see raining"
    relative: false
    hidden: false
---
## 一、動態規劃的思考藝術
+ 動態規劃其實就是一種暴力枚舉的優化，在暴力枚舉的過程中有著大量的重複，藉由「備忘錄(memoization)」的方式做到剪枝(pruning)來達到優化的一種演算法。
+ 舉例來說：
    + [Leetcode 62. Unique Paths](https://leetcode.com/problems/unique-paths/)  
    機器人由左上走到右下角星星有幾種走法，其中機器人只能選擇往右走或往下走。
    ![robot_maze](https://assets.leetcode.com/uploads/2018/10/22/robot_maze.png)
        + 試想機器人從 `(1,1)` 走到 `(m,n)` 的不同路徑中，可見有大量的重複，比如過程中有一點 `(i,j)`，其 `(1,1)` 走到 `(i,j)` 有 `k` 條不同路徑，麼那對於任何一條固定 `(i,j)` 到 `(m,n)` 的路徑，都需走 `k` 遍來模擬。
        + 但其實我們不必關心具體的走法，我們只關心**狀態**，也就是走法的數目。
        + 同理，我們若知道 `(i,j)` 到 `(m,n)` 共有 `t` 條不同的路徑，那麼 `(1,1) -> (i,j) -> (m,n)` 的不同路徑總數就是 `k*s`。
        + 我們知道最左邊那欄與最上面那列都只有可能有一種路徑可以走，又每一格的路徑來自於上方與左方的和：
        `sum of (i,j) = sum of (i-1,j) + sum of (i,j-1)`
        \\(\begin{array}{|c|c|c|c|c|c|c|}\hline
        \text{1}&\text{1}&\text{1}&\text{1}&\text{1}&\text{1}&\text{1}\\\\\hline
        \text{1}&\text{2}&\text{3}&\text{4}&\text{5}&\text{6}&\text{7}\\\\\hline
        \text{1}&\text{3}&\text{6}&\text{10}&\text{15}&\text{21}&\text{28}\\\\\hline
        \end{array}\\)
        + 寫成程式碼就是
        ```C++
        int uniquePaths(int m, int n) {
            vector<vector<int>> dp(m+1, vector<int>(n+1,0));
            for (int i = 1; i <= m; i++)    // 將第一列填成 1
                dp[i][1] = 1;
            for (int j = 1; j <= n; j++)    // 將第一欄填成 1
                dp[1][j] = 1;
            for (int i = 2; i <= m; i++) {      // 將剩下的格子填完
                for (int j = 2; j <= n; j++) {
                    dp[i][j] = dp[i-1][j] + dp[i][j-1];
                }
            }
            return dp[m][n];
        }
        ```
        + 注意填格子的順序是有一定的限制的，必須要確保相關聯的**子問題**已經處理過。
    + 動態規劃
        + 由上例我們可以發現，原本的問題可以拆解成更小的問題(從 `(1,1)->(m,n)` 變成從 `(1,1)->(i,j)` 和從 `(i,j)->(m,n)`)。
        + 我們令 `f(i,j)` 表示從 `(1,1)->(i,j)` 的不同路徑數，則我們可以得到轉移方程式 `f(i,j)=f(i-1,j)+f(i,j-1)`。
        + 我們發現，想求出 `f(i,j)` 只需要知道幾個更小的 `f(i',j')`。我們將 `f(i',j')` 稱為子問題。
        + 我們捨棄冗餘的訊息(具體的走法)，只記錄對解決問題有幫助的結果。
+ 動態規劃的兩大特點(適用前提)
    + 無後效性
        + 一旦 `f(i,j)` 確定，就不用關心我們如何計算出 `f(i,j)`
        + 想要確定 `f(i,j)`，只需要知道 `f(i-1,j)` 和 `f(i,j-1)` 的值，而至於它們是如何算出來的，對當前或之後的任何子問題都沒有影響。
        + 過去不依賴未來，未來不影響過去。
    + 最優子結構
        + `f(i,j)` 的定義就已經蘊含了**最優**。
        + 大問題的最優解可以由若干個小問題的最優解推出。(max, min, sum...)
    + **DP 能適用於：能將大問題拆成若干小問題，滿足無後效性、最優子結構性質。**
    + 以下介紹幾種刷題會遇到的動態規劃套路：
---
## 二、動態規劃框架
### 1. 定序列型
![houserobber](https://th.bing.com/th/id/R.cdccdab761d5e384392455e3b21e1f90?rik=8eL71YYPOIoqXg&pid=ImgRaw&r=0)
+ 給定一個陣列，其中一個元素可以認為**一天**，並且**今天**的狀態只取決於**昨天**的狀態。
+ 框架：
    + 定義 `dp[i][j]`：表示第 `ith` 輪的第 `j` 種狀態。
    + 將 `dp[i][j]` 與前一輪的狀態 `dp[i-1][j]` 產生關聯。
    + 最終的結果是 `dp[n][j]` 中的某種 aggression (sum, max, min, ...)
+ 範例：[[LeetCode] 198. House Robber](/leetcode/198)
    + 定義 `dp[i][j]：`第 `i` 間房子，`j == 0` 代表不搶，`j == 1` 代表搶。
    + 第 `i` 間房若搶，則前一間房必定不能搶；第 `i` 間房若不搶，前一間房可搶可不搶：
        + `dp[i][0] = max(dp[i-1][1], dp[i-1][0])`
        + `dp[i][1] = dp[i-1][0] + val[i]`
    + 最終的結果是 `max(dp[n-1][0], dp[n-1][1])`。
+ 例題：
    + [[LeetCode] 198. House Robber](/leetcode/198)
    + [[LeetCode] 213. House Robber II](/leetcode/213)
    + [[LeetCode] 337. House Robber III](/leetcode/337)
    + [[LeetCode] 121. Best Time to Buy and Sell Stock](/leetcode/121)
    + [[LeetCode] 122. Best Time to Buy and Sell Stock II](/leetcode/122)
    + [[LeetCode] 123. Best Time to Buy and Sell Stock III](/leetcode/123)
    + [[LeetCode] 188. Best Time to Buy and Sell Stock IV](/leetcode/124)
    + [[LeetCode] 309. Best Time to Buy and Sell Stock with Cooldown](/leetcode/309)
    + [[LeetCode] 714. Best Time to Buy and Sell Stock with Transcation Fee](/leetcode/714)
---
### 2. 不定序列型(LIS)
![russian doll](https://th.bing.com/th/id/R.79a96eff1e4ee74f842e254e01cccc45?rik=GwH83zY4Cs5Qzw&pid=ImgRaw&r=0)
+ 給定一個陣列，其中一個元素可以認為**一天**，並且**今天**的狀態取決於**過去某一天**的狀態。
+ 框架：
    + 定義 `dp[i]`：表示第 `ith` 輪的狀態，一般這個狀態要求和元素 `i` 直接相關。
    + 將 `dp[i]` 與之前的某一狀態 `dp[i]` 產生關聯。
    + 最終的結果為 `dp[i]` 中的某一個。
+ 範例：[[LeetCode] 300. Longest Increasing Subsequence](/leetcode/300)
    + 定義 `dp[i]` 為 `s[1:i]` 中以 `s[i]` 為結尾的最長遞增子序列長度。
    + 尋找最優的前驅狀態 `j`，將 `dp[i]` 與 `dp[j]` 產生關聯。
        + `dp[i] = max(dp[i], dp[j] + 1)`
    + 尋找 `dp[i]` 中的最佳解。
        + `res = max {dp[i]}`
---
### 3. 雙序列型(LCS)
![lcs](https://th.bing.com/th/id/R.e8b955ef2229c8858a11120476dfe1ff?rik=jB2IgQqawrYRiw&riu=http%3a%2f%2fgitlinux.net%2fimg%2fmedia%2f15783018023678.jpg&ehk=ZfWv0g1Tf%2boOlaUSksayNwuU1mMNWs2hELFA2MmoGkI%3d&risl=&pid=ImgRaw&r=0)
+ 給定兩組序列，求兩組序列的某些特性。
+ 框架：
    + 定義 `dp[i][j]`：表示針對 `s[1:i]` 和 `t[1:j]` 的子問題求解。
    + 將 `dp[i][j]` 與之前的某一狀態做關聯，如 `dp[i-1][j], dp[i][j-1], dp[i-1][j-1]`
    + 最終的結果是 `dp[m][n]`。
+ 範例：[[LeetCode] 1143. Longest Common Subsequence](/leetcode/1143)
    + 定義 `dp[i][j]` 為 `s[1:i]` 與 `t[1:j]` 的 LCS 長度。
    + 利用`s[i]`與`t[j]`，使`dp[i][j]`與`dp[i-1][j]`、`dp[i][j-1]`、`dp[i-1][j-1]` 產生關聯。
        + 遍歷兩層迴圈，核心以從 `s[i]` 和 `t[j]` 的關係作破口，對 `dp[i][j]` 作轉移。
        + `s[i] == t[j]` 時，`dp[i][j] = dp[i-1][j-1]`。
        + 相反則，`dp[i][j] = max(dp[i-1], dp[j-1]`。
    + 最後解為 `dp[m][n]`，`m` 為 `s` 的長度，`n` 為 `t` 的長度。
---
### 4. 區間型
![interval](https://th.bing.com/th/id/R.976c07e4c83d50c26fb1c38db476c839?rik=XIoS0Zm%2ba7thtQ&pid=ImgRaw&r=0)
+ 給定一個序列，明確要求分割成 K 個連續區間，要求計算這些區間的某個最優性質。
+ 框架：
    + 定義 `dp[i][k]` 表示針對 `s[1:i]` 分為 `k` 個區間，此時能夠得到最佳解。
    + 搜尋最後一個區間的起始位置 `j`，將 `dp[i][k]` 分割成 `dp[j-1][k-1]` 和 `s[j:i]` 兩部分。
    + 最終的結果是 `dp[n][k]`。
+ 範例：[[LeetCode] 1278. Palindrome Partitioning](/leetcode/1278)
    + 定義 `dp[i][j]`：`s[1:i]` 和 `t[1:j]` 的最長相同子序列(LCS)。
---
### 5. 回文型(LPS)
![LPS](https://www.researchgate.net/publication/224375838/figure/fig2/AS:667803556261906@1536228183920/Palindrome-detection-using-suffix-arrays-demonstrated-on-the-string-mississippi-a-DP.png)
+ 給定一個序列，求一個針對這個序列的最佳解。
+ 框架：
    + 定義 `dp[i][j]`：表示針對 `s[i:j]` 的子問題求解。
    + 將大區間的 `dp[i][j]` 往小區間的 `dp[i'][j']` 轉移。
        + 第一層循環是區間大小，第二層循環是起始點。
    + 最終的結果是 `dp[1][n]`。
+ 範例：[[LeetCode] 516. Longest Palindrome Subsequence](/leetcode/516)
---
### 6. 背包型
![backpack](https://th.bing.com/th/id/R.d85b9e734217ea3248111352673321a6?rik=6xwaTyqvmKedlw&pid=ImgRaw&r=0)
+ 給定 `n` 件物品，每個物品可用可不用(或若干不同用法)，要求以某個有上限 `C` 的代價來實現最大收益。(或下限收益達成最小代價)。
+ 框架：
    + 定義 `dp[i][c]`：表示只從前 `i` 件物品的子集裡選擇、代價為 `c` 的最大收益。
    + 將 `dp[i][c]` 往 `dp[i-1][c']` 轉移，即考慮如何使用物品 `i` 對代價/收益的影響。
        + 第一層循環是物品編號 `i`。
        + 第二層循環是遍歷代價的所有可能值。
    + 最終的結果是 `max{dp[n][c_i]}` 
+ 範例：[[LeetCode] 494. Target Sum](/leetcode/494)
## 三、狀態壓縮
1. 方法1
+ 如果轉移方程式很明顯可以省去使用空間，可利用將不需要的空間重複利用來達到狀態壓縮的效果。
+ 如第 `n` 天的狀態只與前 `1` 天與前 `2` 天的狀態有關。那麼就可以將空間限縮到這三天的關係中。
    + 例 `dp[n] = dp[n-1] + dp[n-2]`。
    + 限縮成 `day3 = day1 + day2` + `day1 = day2, day2 = day3`。
+ 其中在二維動態規劃常用一個手法即是奇偶數交換的手法：
    + 同樣以 [Leetcode 62. Unique Paths](https://leetcode.com/problems/unique-paths/)為例：  
    \\(\begin{array}{|c|c|c|c|c|c|c|}\hline
        \text{1}&\text{1}&\text{1}&\text{1}&\text{1}&\text{1}&\text{1}\\\\\hline
        \text{1}&\text{2}&\text{3}&\text{4}&\text{5}&\text{6}&\text{7}\\\\\hline
        \text{1}&\text{3}&\text{6}&\text{10}&\text{15}&\text{21}&\text{28}\\\\\hline
        \end{array}\\)
    + 原先需要用到 `m x n` 即 `21` 個整數空間來實現動態規劃。
    + 但事實上可以利用由上而下，由左而右的方向來填格子，來實現壓縮，最終到到 \\(O(\text{min}(m,n))\\) 的效果。  
        \\(\begin{array}{|c|c|}\hline
        \text{1}&\text{1}\\\\\hline
        \text{1}&\text{2}\\\\\hline
        \text{1}&\text{3}\\\\\hline
        \end{array}\rightarrow\begin{array}{|c|c|}\hline
        \text{1}&\text{1}\\\\\hline
        \text{3}&\text{2}\\\\\hline
        \text{6}&\text{3}\\\\\hline
        \end{array}\rightarrow\begin{array}{|c|c|}\hline
        \text{1}&\text{1}\\\\\hline
        \text{3}&\text{4}\\\\\hline
        \text{6}&\text{10}\\\\\hline
        \end{array}\rightarrow\begin{array}{|c|c|}\hline
        \text{1}&\text{1}\\\\\hline
        \text{5}&\text{4}\\\\\hline
        \text{15}&\text{10}\\\\\hline
        \end{array}\rightarrow\begin{array}{|c|c|}\hline
        \text{1}&\text{1}\\\\\hline
        \text{5}&\text{6}\\\\\hline
        \text{15}&\text{21}\\\\\hline
        \end{array}\rightarrow\begin{array}{|c|c|}\hline
        \text{1}&\text{1}\\\\\hline
        \text{7}&\text{6}\\\\\hline
        \text{28}&\text{21}\\\\\hline
        \end{array}\\)
    + 原本的狀態轉移方程式為：`dp[m][n] = dp[m-1][n] + dp[m][n-1]`
    + 壓縮後的狀態轉移方程式寫成：`dp[m%2][n] = dp[(m-1)%2][n] + dp[m%2][n-1]`

2. 方法2
+ 如果所需的空間有限制，如在 `30` 個以內的 `bool` 值，可以將之轉換成 `bit`，利用位元運算來達到空間壓縮。
---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
+ 想要複習：[[Algo] 2-4. 回溯法 Backtacking](/cs/algo/backtracking)