---
title: "[Algo] 3-2. Binary Search"
keywords: ["C++", "Leetcode", "Algorithm", "binary search"]
description: "介紹二元搜索法，並介紹其適用情境"
date: 2023-05-07T18:46:56+08:00
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
## Binary Search 二元搜索法
+ 通常一般的二分搜是在解決以下這種問題：如果有一個遞增的函數 \\(f\\) 定義在區間
\\([a, a + n)\\) 上，請求出滿足 \\(f(s)\ge c\\)的最小整數\\(s\\)。
+ 若用一般的 linear search 從 a 開始搜直到找到滿足條件的 s，那麼複雜度是 \\(O(n)\\)，而用二元搜索法可以優化時間複雜度變成 \\(O(\log n)\\)。  
想法是對於某個在 \\((a, a + n)\\) 中的整數 \\(k\\)，如果 \\(f(k − 1) \ge c\\)，那麼 \\(s < k\\)，也就是答案會落在區間 \\([a, k)\\) 中。  
反之，如果 \\(f(k − 1) < c\\)，那麼 \\(s \ge k\\)，也就是說你要求的答案會落在 \\([k, a + n)\\)。  
為了讓兩種情況的可能性都盡量低， k 取愈接近 a + n/2 愈好。如此一來，每次候選區間的長度都會縮小一半，因此複雜度為 \\(O(\log n)\\)。
+ 實務上，這種函數 \\(f\\) 常常不能直接得出某一點的值 \\(f(a)\\)（甚至只能確認它和 \\(c\\) 的大小關係），而需要 \\(O(M)\\) 的時間來計算。顯然地，這時複雜度是 \\(O(M \log n)\\)。
### 1. 三元搜
+ 利用二分搜這種「縮短候選人長度」的想法，我們可以找出滿足特定性質的函數的最小值，這種技巧稱為三分搜。
+ 三分搜處理的問題如下：有一個在 \\([a, a + n)\\) 中先嚴格遞減再嚴格遞增的函數 \\(f\\)，請求出 \\(f\\) 在 \\([a, a + n)\\) 的最小值。取在 \\([a, a + n)\\) 中的兩個整數 \\(x < y\\)。如果 \\(f(x) < f(y)\\)，那麼最小值一定落在 \\([a, y)\\)。如果 \\(f(x) > f(y)\\)，那麼最小值一定落在 \\((x, a + n)\\)。如果 \\(f(x) = f(y)\\)，那麼最小值一定落在 \\((x, y)\\)。為了讓候選區間每次都會縮短一定的比例，通常都取 x 跟 y 為區間的三等分點（取中間一點的話常數會變小）。複雜度仍然是 \\(O(\log n)\\)。
### 2. 對答案二分搜
+ 有許多問題都喜歡叫你求「滿足條件的最小值」這種東西。如果這個問題滿足「單調
性」，那或許可以考慮對答案二分搜。
+ 什麼是「單調性」呢？考慮一個函數 P，如果 s 滿足條件，那麼 P(s) = 1，反之則為 0。如果 P 有單調性，我們就說這個問題有單調性。這樣的好處是，我們可以直接用前面的方法二分搜出要求的 s。如果計算 P 的複雜度並不大時，這樣的方法可以有非常的表現效率。在你沒辦法快速求出 s 而只能快速確認一個 s 是否符合條件時，這是一個非常好的方法。
#### 例題：[Leetcode875. Koko Eating Bananas](https://leetcode.com/problems/koko-eating-bananas/)
+ 以此題而言， `canFinish` 就是一個具備單調性的函式，符合我們對答案作二分搜的條件。
```C++
int minEatingSpeed(vector<int>& piles, int h) {
    int max = *max_element(piles.begin(), piles.end());
    if (piles.size() == h)
        return max;
    int left = 1, right = max + 1;
    while (left < right){
        int mid = left + (right-left)/2;
        if (canFinish(piles, mid, h)){
            right = mid;
        } else {
            left = mid + 1;
        }
    }
    return left;
}

bool canFinish(vector<int> piles, int speed, int h){
    int time = 0;
    for (int n : piles){
        time += n/speed + ((n % speed > 0) ? 1 : 0);
    }
    return time <= h;
}
```
---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
+ 想要複習：[[Algo] 3-1. Two Pointer](/cs/algo/two_pointer)
+ 接著閱讀：[[Algo] 3-3. Monotonic Stack](/cs/algo/monotonic_stack)