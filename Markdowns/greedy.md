---
title: "[Algo] 2-2. 貪心演算法 Greedy"
keywords: ["C++", "Leetcode", "Algorithm", "貪心法", "greedy"]
description: "演算法設計，介紹什麼是貪心法，貪心法的解題思維"
date: 2023-01-24T18:31:15+08:00
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
## 一、貪心演算法
+ 是一種**在每一步都採前當下看起來最好的選擇**的一種策略。
+ 由於是當下看起來最好的選擇，故也有可能選到錯的路線，導致最終的答案不是最佳解。
+ 先舉個生活中常見的例子：
    + 今天小明的撲滿裡存滿了大大小小的1塊、5塊跟10塊，今天小明打算要要幫撲滿瘦身，令它的重量降低，那麼小明可以到銀行換鈔，將幣值小、重量重的硬幣集結起來換成幣值大、重量輕的紙鈔。
    + 用貪心演算法的思維，我們一定是從幣值大的 1000 開始換起，再來 500、100、50、10，以此類推，有多少換多少。
    ```C++
    // vector<int>& nums = {1000, 500, 100, 50, 10, 5, 1};
    vector<int> coinChange(vector<int>& nums, int money) {
        vector<int> res(nums.size(), 0);
        for (int i = 0; i < nums.size(); i++) {
            res[i] += (money / nums[i]);
            money %= nums[i];
        }
        return res;
    }
    ```
    + 但若我們新增了一個幣值是 `23`，那麼上面這個思路就有可能會導致錯誤。
+ 貪心演算法的特點
    + 直覺且快速
    + 通常不是最佳的
    + 需要會被要求證明
        1. always stays ahead：跑者每個時間點都在第一名，最後結果會是第一名
            + 用歸納法證明。
        2. exchange argument
            + 用反證法，找到原解的 inversions，並作交換，證明交換後並不影響最佳解。

## 二、貪心演算法的應用
### 0. 核心思維
+ 貪心演算法是從某一個初始狀態出發，每次通過選取區域性最優解向目標前進，並最終期望取得整體最優解的一種演算法。由這個定義可知，貪心選擇標準就是選擇**當前最好**的決策，貪心演算法根據這個標準進行決策，將原問題變成一個相似但規模更小的子問題，而後每一步選出來的一定是原問題整體最優解的一部分。  
如果一個問題貪心後只剩下一個子問題且有最優子結構，那麼該問題就可以使用貪心演算法。當一個問題的整體最優解包含其子問題的最優解時，我們稱次問題具有最優子結構性質。
+ 解題一般步驟
    1. 設計資料結構並找規律
    2. 進心貪心猜想
    3. 正確性證明(歸納法證明或是列舉反例進行反證)
    4. 實現程式碼

### 1. 找零錢問題(Coin Change)
+ 先用剛剛提到的那一題來試做：
+ 以貪心法的思維來做就是，幣值愈大先換，換到不能再換時再往次大的幣值換。
```C++
vector<int> coinChange(vector<int>& nums, int money) {
    vector<int> res(nums.size(), 0);
    for (int i = 0; i < nums.size(); i++) {
        res[i] += (money / nums[i]);
        money %= nums[i];
    }
    return res;
}
```
+ 以範例 `nums = {1000, 500, 100, 50, 23, 10, 5, 1}`，`money = 1069` 來測試看看，以上述得到的結果應該是：(參考例題[Leetcode 322. Coin Change](https://leetcode.com/problems/coin-change/))
    + `{1000, 500, 100, 50, 23, 10, 5, 1} = {1, 0, 0, 1, 0, 1, 1, 4}`，也就是說，得到的硬幣總數是 `8`(假設所有幣值都是硬幣)。
    + 因為夾雜了 `23`，使得問題變得稍微有點不一樣，因為最佳解可以是：  
    `{1000, 500, 100, 50, 23, 10, 5, 1} = {1, 0, 0, 0, 3, 0, 0, 0}`，總數 `4`。
    + 從上面此例來觀察，貪心法是需要有適用時機的，當今天少掉 `23` 的時候，使用貪心法是可以得到最佳解的，因為所有數字互為因數、倍數關係，也就是說，當今天可以用 `1` 張 `1000` 解決的情況，必定可以用其它幣值用更多的代價來替換，如 `2` 張 `500`，或 `10` 張 `100`。但是 `23` 可以替換的是 `2` 個 `10` 塊加上 `3` 個 `1` 塊。用數字為例的話如下
        + \\(\boxed{\begin{array}{ll}
        1069&=1\times1000+1\times50+1\times10+1\times5+4\times1\\\\
        &=1\times(2\times500)+1\times50+1\times10+1\times5+4\times1\\\\
        &=1\times(10\times100)+1\times50+1\times10+1\times5+4\times1\\\\
        &=1\times(20\times50)+1\times50+1\times10+1\times5+4\times1\\\\
        \end{array}}
        \\)
        + 不管怎麼換，總數都是變大。
    + 如果要解出上述的最佳解，需要做一點修正，或是使用暴力破解法，例如 `bfs` 來遍歷所有情形來獲得最小值。
        + 試想要怎麼改寫可以使貪法仍然可以適用，「將23拿掉」那麼貪心法就仍可以適用，那要怎麼有技巧的將 `23` 拿掉呢。
        + `23` 能夠有效的替換表示我們一定會使用到 `23`，也就是說我們可以找到反例使 `23` 不能有效的替換就好了。
            + `23` = `23*1(1)` 換 `10*2 + 1*3(5)`
            + `46` = `23*2(2)` 換 `10*4 + 5*1 + 1*1(6)`
            + `69` = `23*3(3)` 換 `50*1 + 10*1 + 1*4(6)`
            + `92` = `23*4(4)` 換 `50*1 + 10*4 + 1*2(7)`
            + `115` = `23*5(5)` 換 `100*1 + 10*1 + 5*1(3)`
        + 我們可以發現當 `23` 替換到第 `5` 個的時候已經不能有效的替換了，表示我們只有嘗試替換 `0~4` 個 `23` 硬幣，其餘剩下的錢用貪心法去計算，仍然可以得到有效的解。(在此只是為了展示失去「局部最佳性」的範例，不做嚴謹的數學證明)
        + 即求 `min(f(1069)+0, f(1046)+1, f(1023)+2, f(1000)+3, f(976)+4`。
        ```C++
            vector<int> coinChange(vector<int>&nums, int money) { ... } // implement by greedy
            vector<int> coinChangePlus(vector<int>&nums, int money) {
                vector<int> res;
                int coins = INT_MAX;
                for (int i = 0; i <= 4; i++) {
                    vector<int> tmp = coinChange(nums, money-23*i);
                    tmp[4]+=i;
                    int cnt = accumulate(tmp.begin(), tmp.end(), 0);
                    if (cnt < coins) {
                        res = tmp;
                        coins = cnt;
                    }
                }
                return res;
            }
        ```
    + 以上方法當遇到單一奇異數(無因倍數關係)的時候還可以用，但遇到多個奇異數的時候，複雜度就會明顯上升，到時後我們會遇用其它方法來解構。在後面的[動態規劃](/cs/algo/dp)篇，有深入的介紹，如何利用其它技巧達到剪枝得到最佳解。
+ 由此可發現，貪心法不一定會得到最佳解，需要嚴格的驗證「局部最佳性」，才能保證最後的解是最佳解。

### 2. 背包問題(Knapsack Problem)
+ 常見的背包問題分為**分數背包問題**與**0-1背包問題**。
    + 今天在某個場合，你有一個載重5kg的背包，面前有3kg的金沙、3kg的銀沙與2kg的銅沙，已知金的價格比銀高，銀的價格比銅高。你可以任意決定怎麼將它們裝進背包，最後換取對應價值的獎金，試問怎麼裝可以得到最高的獎金？
    + 同樣的場合，金沙、銀沙、銅沙換成了金塊、銀塊、銅塊，分別也是 3kg、3kg、2kg，且不可切割，試問要怎麼裝可以得到最高的獎金？
        + 第1題(分數背包)，顯而易見，用貪心法來做一定是盡可能先裝滿價值高的金沙，再用剩餘的空間以此類推裝填其它的。(3kg金沙+2kg銀沙)
        + 第2題(0-1背包)，由於拿完金塊，無法再拿銀塊，所以最佳解變成了拿金塊與銅塊。(3kg金塊+2kg銅塊)
## 三、例題
### 1. 餅乾分配問題
+ [Leetcode 455. Assign Cookies](https://leetcode.com/problems/assign-cookies/)
+ 有若干個不同份量的餅乾，與若干個需要不同份量才能滿足的小孩，試問餅乾最多可以讓幾個小孩滿意。
    + 把餅乾的份量從小排到大，把小孩從需求小排到需求大。
    + 盡可能的滿足需求小的小孩。(若需求小的都滿足不了，那麼需求大的就不可能滿足了)
    ```C++
    int findContentChildren(vector<int>& children, vector<int>& cookies) {
        sort(children.begin(), children.end());
        sort(cookies.begin(), cookies.end());
        int child = 0, cookie = 0;
        while (child < children.size() && cookie < cookies.size()) {
            if (children[child] <= cookies[cookie]) child++;
            cookie++;
        }
        return child;
    }
    ```
### 2. 股票買賣問題
+ [Leetcode 122. Best Timer to Buy and Sell Stock II](https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/)
+ 有一數列為某上市公司每日的股價，若手上最多只能有一張股票，要怎麼樣買賣可以得到最高獲利。
    + 最高獲利代表所有上升波段的總和，忽略所有下降波段。
    ```C++
    int maxProfit(vector<int>& prices) {
        int sum = 0;
        int last = prices[0];
        for (const auto& price : prices) {
            sum += (price > last) ? price - last : 0;
            last = price
        }
        return sum;
    }
    ```
### 3. 跳躍遊戲
+ [55. Jump Game](https://leetcode.com/problems/jump-game/)  
+ 有一數列表示，在該 `i` 索引位置起，最多可以跳幾個索引長度，試問從索引值為 `0` 開始，可否到達索引值為 `n-1`。
    + 盡可能的往前跳，不斷的更新最遠可以到達的位置。
    ```C++
    bool canJump(vector<int>& nums) {
        int reach = nums[0];
        for (int i = 0; i < nums.size() && i <= reach; i++) {
            if (i == nums.size()-1) return true;
            reach = max(reach, nums[i]+i);
        }
        return false;
    }
    ```
---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
+ 想要複習：[[Algo] 2-1. 暴力演算法](/cs/algo/brute_force)
+ 接著閱讀：[[Algo] 2-3. 分治法](/cs/algo/divide_and_conquer)